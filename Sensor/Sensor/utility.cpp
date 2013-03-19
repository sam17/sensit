/**
 * @file utility.c
 *
 * @date Created: 11/22/2011 12:24:16 PM
 * @author Bittu Sarkar
 */ 

#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include "comdef.h"
#include "config.h"
#include "utility.h"
#include "peripherals.h"

static inline uint8_t hex_to_nibble(char hex)
{
	if (hex >= '0' && hex <= '9')
		return (hex - '0');
	else if (hex >= 'A' && hex <= 'F')
		return (hex - 'A' + 0xA);
  else
    return 0xFF;
}


void byte_arr_to_hex(const uint8_t * byte_arr, uint8_t length, uint8_t* hex)
{
	uint8_t i, j;
	for (i=0, j=0; i<length; ++i)
	{
		*(hex + j++) = nibble_to_hex[byte_arr[i] >> 4];
	  *(hex + j++) = nibble_to_hex[byte_arr[i] & 0x0F];
	}
}


Result hex_to_byte_arr(const char* hex, uint8_t length, uint8_t* byte_arr)
{
  if ((length & 1) != 0)  // If the length of the hex string is odd, it is invalid
    return Failure;
	
  uint8_t i, j;
  for (i=0, j=0; j<length; ++i)
  {
    uint8_t nibb = hex_to_nibble(hex[j++]);
    if (nibb != 0xFF)
    {
		  byte_arr[i] = nibb << 4;
      nibb = hex_to_nibble(hex[j++]);
      if (nibb != 0xFF)
        byte_arr[i] |= nibb;
      else
        return Failure;
    }
    else
      return Failure;
  }      
  return Success;
}


void float_to_byte_arr(float num, uint8_t * const byte_arr)
{
	uint8_t* fPtr = (uint8_t *)(&num);
	uint8_t i;
	for (i=0; i<4; ++i)
		byte_arr[i] = fPtr[i];
}

float byte_arr_to_float(const uint8_t* const byte_arr)
{
	float num;
	memcpy(&num, byte_arr, 4);
	return num;
}

bool starts_with(const char* source, const char* prefix)
{
	while (*source && *source == *prefix)
  {
	  source++;
	  prefix++;      
  }
  return (*prefix == 0);
}