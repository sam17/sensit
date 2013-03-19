/**
 * @file main.c
 *
 * @date Created: 6/20/2011 9:05:56 PM
 * @author Bittu Sarkar
 */ 

#define DEFINE_GLOBALS

#include <avr/io.h>
#include <avr/interrupt.h>
#include "config.h"
#include "peripherals.h"
#include "comm.h"
	
// Main entry of code
int main(void) 
{
	cli();
	io_port_init();
	ext_interrupt_init();
  timer_init();
  uart_init(9600);    // Baud rate for GSM communication
  comm_init();
  sei();
  
  uart_write_string("AT+CMGF=1\r");
  uart_read_upto("OK\r");
  
  // Main application loop
  while (true)
  {
    listen_for_server_msg();
  }	
  return 0;
} // main

