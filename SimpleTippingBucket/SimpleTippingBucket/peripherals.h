/**
 * @file peripheral.h
 *
 * @date Created: 11/21/2011 11:18:39 PM
 * @author Bittu Sarkar
 */ 


#ifndef PERIPHERAL_H
#define PERIPHERAL_H

#include <avr/io.h>
#include "config.h"
#include "comdef.h"

/************************************************************************/
/* TYPE AND MACRO DECLARATIONS                                          */
/************************************************************************/

#define enable_comm_timer_interrupt() (TIMSK1 |= (1<<OCIE1A))     
#define disable_comm_timer_interrupt() (TIMSK1 &= ~(1<<OCIE1A))    
#define comm_timer_interrupt_enabled() (!!(TIMSK1 & (1<<OCIE1A)))  

#define enable_counter_interrupt() (TIMSK1 |= (1<<OCIE1A))         
#define disable_counter_interrupt() (TIMSK1 &= ~(1<<OCIE1A))
#define counter_interrupt_enabled() (!!(TIMSK1 & (1<<OCIE1A)))


/************************************************************************/
/* FUNCTION DECLARATIONS                                                */
/************************************************************************/
void    uart_init(uint32_t baud_rate);

uint8_t uart_data_available(void);

uint8_t uart_read_byte(void);

void    uart_read_line(char* line);

void    uart_write_byte(uint8_t data);

void    uart_write_string(const char* str);

// Writes a byte as 2 hex characters to the UART module
void    uart_write_2_hex(uint8_t byte);

/** 
 * @brief This function waits for a string to be received by the USART module
 *        It has been implemented as a Finite State Machine. It does not wait
 *        for the trailing '\0' of the string though.
 *        This algorithm is not fool proof in general sense and in very rare 
 *        cases may fail but it is sufficient for the SENSIT application.
 *
 * @param str
 *    This is the string which is waited for
 *
 * @return None
 */       
void uart_read_upto(const char* str);

#define uart_write_debug_string(x) //uart_write_string(x)

#endif // PERIPHERAL_H