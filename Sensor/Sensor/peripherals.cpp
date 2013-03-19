/**
 * @file peripheral.c
 *
 * @date Created: 11/22/2011 12:23:43 PM
 * @author Bittu Sarkar
 */

#include <string.h>
#include <avr/io.h>
#include <avr/interrupt.h>
#include <avr/eeprom.h>
#include "comdef.h"
#include "config.h"
#include "utility.h"
#include "peripherals.h"

/************************************************************************/
/* TYPE AND MACRO DECLARATIONS                                          */
/************************************************************************/
#define USART_RX_BUFF_SIZE (256)


/************************************************************************/
/* VARIABLE DEFINITIONS                                                 */
/************************************************************************/
static volatile uint8_t URBuff[USART_RX_BUFF_SIZE];	// USART Receive Buffer
static volatile int16_t UQFront;
static volatile int16_t UQEnd;

/************************************************************************/
/* FUNCTION DEFINITIONS                                                 */
/************************************************************************/

// Initialize I/O pins
void io_port_init(void)
{
  // Setting Sensor pin as input with internal pull-up enabled
  DDRD &= ~(1<<2);
  PORTD |= (1<<2);

  // Error Indicator LED - Output pin
  DDRB |= (1<<5);
  PORTB &= ~(1<<5);
} // io_port_init


/**
 * @brief Initialize TIMER0 where the bulk of the code runs
 */
void timer_init(void)
{
  // Timer1 is used for timing the communication with the server to send it the rainfall logs periodically
  // CTC mode, Prescalar = 1024, OC1x pins disconnected, output compare match 1 A interrupt enabled
  TCCR1A = 0;
  TCCR1B = (1<<WGM12) | (1<<CS12) | (1<<CS10);
  TCCR1C = 0;
  OCR1A = (uint16_t)(F_CPU/1024.0 - 0.5); // OC1A interrupt occurs every 1 second
  TIMSK1 |= (1<<OCIE1A);
} // timer_init


/**
 * @brief Setting up external interrupt EXT0 for capturing the signals from the sensor
 */
void ext_interrupt_init(void)
{
	EICRA = (1<<ISC00) | (1<<ISC01); // The rising edge of INT0 generates an interrupt request
  EIMSK = (1<<INT0); // External Interrupt Request 0 Enable
} // ext_interrupt_init


/**
 * @brief Initializes the USART module in asynchronous mode.
 *        Enables RX and TX and also the RX complete interrupt
 *
 * @param baud_rate
 *    Baud rate of the USART module
 *
 * @return Nothing
 */
void uart_init(uint32_t baud_rate)
{
  // Setup q
  UQFront = UQEnd = -1;

  //Set Baud rate
  UBRR0 = ((F_CPU - baud_rate * 8L) / (baud_rate * 16L));

  /* Set Frame Format

  Asynchronous mode
  No Parity
  1 StopBit
  char size 8

  Enable Interrupts
  RXCIE0 - Receive complete
  Enable The receiver and transmitter
  */

  UCSR0A = 0;
  UCSR0B = (1<<RXCIE0) | (1<<RXEN0) | (1<<TXEN0);
  UCSR0C = (1<<USBS0) | (1<<UCSZ01) | (1<<UCSZ00);
} // uart_init


void spi_init(void)
{
	SPCR = (1<<SPE) | (1<<MSTR);
} // spi_init


void spi_write_string(const char* str)
{
	while (*str)
	{
		SPDR = *str;
		++str;
	}
	SPDR = '\n';
} // spi_write_string

	
// The USART ISR
ISR(USART_RX_vect)
{
  //Read the data
  char data = UDR0;

  //Now add it to q
  if ((UQEnd == USART_RX_BUFF_SIZE - 1 && UQFront == 0) || UQEnd + 1 == UQFront)
  {
    //Q Full
    ++UQFront;
    if (UQFront == USART_RX_BUFF_SIZE)
      UQFront = 0;
  }


  if (UQEnd == USART_RX_BUFF_SIZE - 1)
    UQEnd = 0;
  else
    ++UQEnd;

  URBuff[UQEnd] = data;

  if (UQFront == -1)
    UQFront = 0;
} // USART_RX_vect


uint8_t uart_data_available(void)
{
  if (UQFront == -1)
    return 0;
  if (UQFront <= UQEnd)
    return (UQEnd - UQFront + 1);
  else
    return (USART_RX_BUFF_SIZE - UQFront + UQEnd + 1);
} // uart_data_available


uint8_t uart_read_byte(void)
{
	uint8_t data;

  while (!uart_data_available());

  data = URBuff[UQFront];

  if (UQFront == UQEnd)
  {
    // If single data is left, so empty q
    UQFront = UQEnd = -1;
  }
  else
  {
    ++UQFront;

    if (UQFront == USART_RX_BUFF_SIZE)
      UQFront = 0;
  }
  return data;
}


/**
 * @brief This is a blocking function that tries to read a line from the USART peripheral
 *        and stops when it receives a LF ('\n') character which it overwrites with a '\0' 
 *        in the line variable.
 *
 * @param line
 *    It is a char pointer to the line read from the USART peripheral. It starts with the 
 *    1st character read from the USART till it receives a '\n' which it eventually replaces
 *    with '\0'.
 *    NOTE: Memory space for line must be allocated before calling this function
 *
 * @return None
 */
void uart_read_line(char* line)
{
  uint8_t byte;
  while ((byte = uart_read_byte()) != '\n')
  {
	 // uart_write_byte(byte);
    *line = byte;
    line++;
  }
  *line = '\0';
} // uart_read_line


void uart_write_byte(uint8_t data)
{
  // Wait For Transmitter to become ready
  while (!(UCSR0A & (1<<UDRE0)));

  // Now write
  UDR0 = data;
  DELAY_MS(100);
}


void uart_write_string(const char* str)
{
  while(*str)
  {
    uart_write_byte(*str);
    str++;
  }
}


void uart_write_2_hex(uint8_t byte)
{
  uart_write_byte(nibble_to_hex[byte>>4]);       // Writing the upper nibble first
  uart_write_byte(nibble_to_hex[byte & 0x0F]);   // Then writing the lower nibble
} // uart_write_2_hex


void uart_read_upto(const char* str)
{
	uint8_t state        = 0;              // Initial state
	uint8_t accept_state = strlen(str);    // Final state
  while (true)
  {
    uint8_t ch = uart_read_byte();
	  if (ch == str[state])
	    ++state;
		else if (ch == str[0])
		  state = 1;
		else
		  state = 0;
		if (state == accept_state)
		  break;
  }
}
