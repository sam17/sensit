/**
 * @file utility.h
 *
 * @date Created: 6/20/2011 9:16:03 PM
 * @author Bittu Sarkar
 */

#ifndef UTILITY_H
#define UTILITY_H

#include <stdint.h>
#include "comdef.h"

/************************************************************************/
/* TYPE AND MACRO DECLARATIONS                                          */
/************************************************************************/
#define STRING_OF(x)    (#x)
#define CONCAT(a,b)     a##b
#define DDR(x)          CONCAT(DDR, x)
#define PIN(x)          CONCAT(PIN, x)
#define PORT(x)         CONCAT(PORT, x)

const char nibble_to_hex[] = "0123456789ABCDEF";

/************************************************************************/
/* FUNCTION DECLARATIONS                                                */
/************************************************************************/

Result hex_to_byte_arr(const char* hex, uint8_t length, uint8_t* byte_arr);

void float_to_byte_arr(float num, uint8_t* byte_arr);

float byte_arr_to_float(const uint8_t* const byte_arr);

bool starts_with(const char* source, const char* prefix);

void show_error(const char* msg);

#endif // UTILITY_H
