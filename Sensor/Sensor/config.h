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

#define TIMER_INTERRUPT_PERIOD (1000) // (in ms)

// Rainfall amount (in mm) equivalent of one tip of the sensor
#define NUM_TIPS_2_RAINFALL_FACTOR (1.0f)

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

#define uart_write_debug_string(x) //uart_write_string(x)

#endif // CONFIG_H
