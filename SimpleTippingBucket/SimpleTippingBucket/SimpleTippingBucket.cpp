#include <avr/io.h>
#include <avr/interrupt.h>
#include "config.h"
#include "comdef.h"
#include "peripherals.h"
#include "utility.h"

ISR(BADISR_vect)
{
	DDRB |= (1<<5);
	while (true)
	{
		PORTB |= (1<<5);
		DELAY_MS(200);
		PORTB &= ~(1<<5);
		DELAY_MS(200);
	}
}


void setup()
{
  // Set the pin connected to the sensor as input pin with internal pull-up enabled
  DDRD &= ~(1<<2);
  PORTD |= (1<<2);
  
  cli();
  uart_init(9600);
  uart_write_byte('\r');
  DELAY_MS(1000);  // Wait for a second while the modem sends an "OK"
  
  // Setting up timer for capturing the signals from the sensor
  // CTC mode, Prescalar = 1024
  TCCR0A = (1<<WGM01);
  TCCR0B = (1<<CS02) | (1<<CS00);
  OCR0A = (uint8_t)(F_CPU*0.001024f/1024.0f - 0.5f);  // triggers every 1024 microseconds
  TIMSK0 = (1<<OCIE0A);
  
  // Setting up timer responsible for sending SMSs at fixed time intervals
  // CTC mode, Prescalar = 1024
  TCCR1A = 0;
  TCCR1B = (1<<WGM12) | (1<<CS12) | (1<<CS10);
  TCCR1C = 0;
  OCR1A = (uint16_t)(F_CPU/1024.0f - 0.5f);  // triggers every 1 second
  TIMSK1 = (1<<OCIE1A);
  sei();
} // setup

int main(void)
{
	setup();
  while (true);
  return 0;
}

