/**
 * @file config.h
 *
 * @brief Target: ATmega328P on Arduino Uno Board
 *
 * @date Created: 6/20/2011 9:13:51 PM
 *
 * @author Bittu Sarkar
 */ 

#ifndef CONFIG_H
#define CONFIG_H


/************************************************************************/
/* TYPE AND MACRO DECLARATIONS                                          */
/************************************************************************/

// Description of the sensor
#define SENSOR_DESCRIPTION ("Civil Department, IIT Kharagpur")

// ID of the server
#define SERVER_ID ("+919434760063")

// Time interval between sending of consecutive rainfall measurements to the server
#define PING_INTERVAL (60*15)  // in seconds

// Number of 0s/1s to be present in a stream of sensor inputs for it to be detected as an edge
#define WINDOW_SIZE (3)

#ifdef _DEBUG // Disabling the delays in Debug mode
  #define DELAY_MS(x)
  #define DELAY_US(x)
#else
  #ifndef F_CPU
    #error F_CPU undefined!!
  #endif
  #include <math.h>
  #include <util/delay.h>
  #define DELAY_MS(x) _delay_ms(x)
  #define DELAY_US(x) _delay_us(x)
#endif

#endif // CONFIG_H
