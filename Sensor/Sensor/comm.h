/**
 * @file comm.h
 *
 * @brief This file has all the communication related code
 *
 * @date Created: 6/21/2011 8:30:20 AM
 * @author Bittu Sarkar
 */

#ifndef COMM_H
#define COMM_H

#include <stdint.h>
#include <avr/eeprom.h>
#include "comdef.h"


/************************************************************************/
/* FUNCTION DECLARATIONS                                                */
/************************************************************************/

// Initializes communication related variables
void comm_init(void);

// Receive data packet from the server
void listen_for_server_msg(void);

// Processes the message received from the server
void process_server_msg(void);

#endif // COMM_H
