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

static volatile int tips_count = 0;

/************************************************************************/
/* FUNCTION DEFINITIONS                                                 */
/************************************************************************/

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
}

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


ISR(TIMER0_COMPA_vect)
{
  static uint8_t state = 0;
  uint8_t input = (PIND & (1<<2));
  
  if (state < WINDOW_SIZE)
  {
	  if (input == 0)
	    ++state;
		else
		  state = 0;
  }	
  else if (state == WINDOW_SIZE)
  {
	  if (input != 0)
	    ++state;
  }
  else if (state < 2*WINDOW_SIZE)
  {
	  if (input == 0)
	    state = 1;
		else
		  ++state;
  } 
  else if (state == 2*WINDOW_SIZE)
  {
	  ++tips_count;
	  
	  if (input == 0)
	    state = 1;
		else
		  state = 0;
  }
}

ISR(TIMER1_COMPA_vect, ISR_NOBLOCK)
{
	static uint16_t timer_count = 0;

  if (timer_count < PING_INTERVAL - 1)
    ++timer_count;
  else
  {
    timer_count = 0;
	
    uart_write_string("AT+CMGF=1\r");    //Because we want to send the SMS in text mode
    DELAY_MS(500);

    uart_write_string("AT+CMGS=\"");
	  uart_write_string(SERVER_ID);
	  uart_write_string("\"\r");    //Start accepting the text for the message to be sent to the number specified
    DELAY_MS(500);

    uint8_t m_byte[4];
    char m_hex[20];
	  
	  cli();
	  int tc = tips_count;
	  tips_count = 0; // resetting the tips_count
	  sei();
	  
    float_to_byte_arr(tc, m_byte);
    byte_arr_to_hex(m_byte, 4, m_hex);
    m_hex[8] = '\0';
    uart_write_string("0308");
    uart_write_string(m_hex);
    DELAY_MS(500);
    uart_write_byte(26);  //Equivalent to sending Ctrl+Z
  }
}
