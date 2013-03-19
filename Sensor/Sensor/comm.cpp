/**
 * @file comm.c
 *
 * @date Created: 11/22/2011 12:17:22 PM
 * @author Bittu Sarkar
 */

#include <avr/interrupt.h>
#include <avr/eeprom.h>
#include <avr/pgmspace.h>
#include <string.h>
#include "comm.h"
#include "peripherals.h"
#include "utility.h"
#include "config.h"

/************************************************************************/
/* TYPE AND MACRO DECLARATIONS                                          */
/************************************************************************/
// Sensor States
#define Sleeping  (0x12)
#define	Logging   (0x13)

// Communication packet content info
#define SENSOR_MSG_VALUE_MAX_LENGTH (100)
#define SERVER_MSG_VALUE_MAX_LENGTH (100)

typedef enum server_msg_tag
{
  //None = 0,     ... Already defined above so cannot define again
  //DebugMsg,     ... Already defined above so cannot define again
  //Resend,       ... Already defined above so cannot define again
  ResetReading = 3,
  SetServerAddress,
  SetPingInterval,
  GetPingInterval,
  SendDescription,
  WakeUp,
  Sleep,
  GetState,
  MAX_SERVER_MSG_TAGS
} server_msg_tag;

typedef struct server_msg_t
{
  server_msg_tag tag;
  uint8_t        length;
  uint8_t        value[SERVER_MSG_VALUE_MAX_LENGTH];
} server_msg_t;

typedef enum sensor_msg_tag
{
  None = 0,
  DebugMsg,
  Resend,
  Log,
  Description,
  PingInterval,
  State,
  MAX_SENSOR_MSG_TAGS
} sensor_msg_tag;

typedef struct sensor_msg_t
{
  sensor_msg_tag tag;
  uint8_t        length;
  uint8_t        value[SENSOR_MSG_VALUE_MAX_LENGTH];
} sensor_msg_t;


/************************************************************************/
/* VARIABLE DEFINITIONS                                                 */
/************************************************************************/

// Stores the time period of pinging the server with rainfall reading logs (in seconds)
static float EEMEM ee_ping_interval;
static float       ping_interval;

/**
 * This pair of variables keep track of the number of tips that have been 
 * detected. It is reset every time an SMS is sent to the server carrying 
 * the current value of this variable
 */
static uint16_t EEMEM ee_num_tips;
static uint16_t       num_tips;

// Stores the state in which the sensor is, Sleeping or Logging
static uint8_t EEMEM ee_sensor_state;
static uint8_t       sensor_state;

// Stores the next message to be sent by the sensor from the main thread
static sensor_msg_t next_sensor_msg_main;

// Stores the next message to be sent by the sensor from the ISR thread
volatile static sensor_msg_t next_sensor_msg_isr;

// Saves the previous message sent by the server from any thread
static sensor_msg_t prev_sensor_msg;

// Stores the incoming server messages
static server_msg_t server_msg;

/**
 * This variable sets the number of times a counter counts before it
 * sends a reading to the server. The counter in question is
 * periodically updated in a timer interrupt for accurate timings.
 *
 * IT IS COMPUTED BY THE CODE. THE USER NEED NOT WORRY ABOUT ITS VALUE
 */
volatile static uint16_t comm_counter_top;

/**
 * This flag is set when the code is inside the process_server_msg
 * function. It prevents the Timer from sending any Log message to the
 * server when the sensor is processing the server's message
 */
volatile static bool signal_processing;


/************************************************************************/
/* FUNCTION DEFINITIONS                                                 */
/************************************************************************/

void comm_init(void)
{
  ping_interval     = eeprom_read_float(&ee_ping_interval);
  comm_counter_top  = ((uint16_t)((float)ping_interval*1000/TIMER_INTERRUPT_PERIOD + 0.5f));
  signal_processing = false;

  // Reading Sensor State from the EEPROM
  sensor_state      = eeprom_read_byte(&ee_sensor_state);

  if (sensor_state != Sleeping && sensor_state != Logging)
  {
    // First time reading from the EEPROM. Default action is to log
    sensor_state = Logging;
    eeprom_write_byte(&ee_sensor_state, sensor_state);
  }

  if (sensor_state == Sleeping)
  {
    disable_comm_timer_interrupt();
    disable_counter_interrupt();
  }
  else
  {
    enable_comm_timer_interrupt();
    enable_counter_interrupt();
  }
}


inline void set_num_tips(uint16_t num)
{
  num_tips = num;
  eeprom_write_word(&ee_num_tips, num);
}


/**
 * @brief Sends a message to the server
 *        Format:
 *        1 Byte: CommandTag of the message
 *        1 Byte: Length of the message
 *        Length Bytes: Message
 *
 * @param sensor_msg
 *    It holds the message to be sent
 *
 * @return
 *    Returns Result.Success if writing is successful otherwise Result.Failure
 */
Result write_packet(const sensor_msg_t* sensor_msg)
{
  bool timerEnabled = comm_timer_interrupt_enabled();
  disable_comm_timer_interrupt(); // Preventing the timer from entering its ISR

  // Writing AT+CMGS="<Server Number>"\r
  uart_write_string("AT+CMGS=\"");
  uart_write_string(SERVER_ID);
  uart_write_string("\"\r");

  uart_write_debug_string("__Waiting for > \r\n");
  uart_read_upto("> ");

  // Writing the actual SMS message now
  uart_write_2_hex(sensor_msg->tag);              // Writing the tag
  uart_write_2_hex(sensor_msg->length << 1);      // Writing the length

  uint8_t i;
  for (i=0; i<sensor_msg->length; ++i)
    uart_write_2_hex(sensor_msg->value[i]);       // Writing the value

  uart_write_byte(26);                       // Writing Ctrl+Z

  // Wait for "OK\r"
  uart_write_debug_string("__Waiting for OK\\r: response to send sms\r\n");
  uart_read_upto("OK\r");

  // Saving the current message in prevSensorMsgMainThread in case the server asks to re-transmit the message
  memcpy(&prev_sensor_msg, sensor_msg, sizeof(sensor_msg_t));

  if (timerEnabled)                           // Resetting the status of the timer's interrupt
    enable_comm_timer_interrupt();

  return Success;
} // write_packet


/**
 * @brief Tries to read a SENSIT server message. Once it reads a message successfully,
 *        it saves it in server_msg, processes it and when all unread SMSs have been
 *        processed, it deletes them
 *
 * @return None
 */
void listen_for_server_msg(void)
{
	static char line[200];
  uart_write_debug_string("__Waiting for a line: start of listen\r\n");
  uart_read_line(line);
  uart_write_debug_string("__Line = ");
  uart_write_debug_string(line);
  uart_write_debug_string("\r\n");

  if (starts_with(line, "+CMTI:") || starts_with(line, "Call Ready"))
  {
    uart_write_string("AT+CMGL=\"ALL\"\r");

    bool msg_found = false;
    static char sms_index_to_delete[50][3];
    uint8_t num_sms_to_delete = 0;
    while (true)
    {
      uart_write_debug_string("__Waiting for a line: response to +CMGL\r\n");
      uart_read_line(line);
      uart_write_debug_string("__Line = ");
      uart_write_debug_string(line);
      uart_write_debug_string("\r\n");

      if (strcmp(line, "OK\r") == 0)
      {
        uart_write_debug_string("__Read OK: end of +CMGL\\r\r\n");
        break;
      }

      if (msg_found)
      {
        int len = strlen(line);
        line[--len] = '\0'; // discarding trailing '\r'

        uart_write_debug_string("__SMS=");
        uart_write_debug_string(line);
        uart_write_debug_string("\r\n");

        uint8_t bmsg[100];
        if (hex_to_byte_arr(line, len, bmsg) == Success)
        {
          if (bmsg[0] < MAX_SERVER_MSG_TAGS)  // Valid tag
          {
            server_msg.tag = (server_msg_tag)bmsg[0];
            server_msg.length = bmsg[1]>>1;
            if (bmsg[1] + 4U == strlen(line))
            {
              uint8_t i;
              for (i=0; i<server_msg.length; ++i)
                server_msg.value[i] = bmsg[i+2];
              process_server_msg();
            }
          }
        }
        msg_found = false;
      }

      if (starts_with(line, "+CMGL:"))
      {
        // Example line (@@ marks the token boundaries): @@+CMGL:@@2@@"REC@@READ"@@"+918348468052"@@""@@"12/03/28@@12:23:49+22"@@
        char* tok = strtok(line, ",\r ");  // Ignoring "+CMGL:"

        tok = strtok(NULL, ",\r ");
        strcpy(sms_index_to_delete[num_sms_to_delete++], tok);  // Adding the SMS index to a list for deletion later on

        tok = strtok(NULL, ",\r ");   // Ignoring "REC"/"STO"
        tok = strtok(NULL, ",\r ");   // Ignoring "UNREAD"/"READ"/"UNSENT"/"SENT"

        tok = strtok(NULL, ",\r \""); // Sender's number. Separator " added to get the sender's number without quotes
        uart_write_debug_string("__Sender's number = ");
        uart_write_debug_string(tok);
        uart_write_debug_string("\r\n");
        if (strcmp(tok, SERVER_ID) == 0) // If the SMS was by the SENSIT server, indicate to process it
          msg_found = true;
      }
    }
    uart_write_debug_string("__num_to_delete = ");
	  uart_write_byte('0' + num_sms_to_delete);
	  uart_write_debug_string("\r\n");
    // Here all the messages have been processed, so delete them now
    for (uint8_t i=0; i<num_sms_to_delete; ++i)
    {
      uart_write_string("AT+CMGD=");
      uart_write_string(sms_index_to_delete[i]);
      uart_write_byte('\r');
      uart_write_debug_string("__Waiting for OK\\r: response to delete\r\n");
      uart_read_upto("OK\r");
	    uart_write_debug_string("__Deleted\r\n");
    }
  }
  uart_write_debug_string("__End of listening\r\n");
} // listen_for_server_msg

void process_server_msg(void)
{
  signal_processing = true;

  switch (server_msg.tag)
  {
  case SendDescription:
    uart_write_debug_string("__In SendDescription\r\n");
    next_sensor_msg_main.tag = Description;
    next_sensor_msg_main.length = strlen(SENSOR_DESCRIPTION);
    memcpy(next_sensor_msg_main.value, SENSOR_DESCRIPTION, next_sensor_msg_main.length);
    write_packet(&next_sensor_msg_main);
    break;

  case SetPingInterval:
    uart_write_debug_string("__In SetPingInterval\r\n");
    ping_interval = byte_arr_to_float(server_msg.value);
    comm_counter_top = ((uint16_t)((float)ping_interval/TIMER_INTERRUPT_PERIOD + 0.5f));
    break;

  case Sleep:
    uart_write_debug_string("__In Sleep\r\n");
    sensor_state = Sleeping;
    eeprom_write_byte(&ee_sensor_state, sensor_state);
    disable_comm_timer_interrupt();   // Stopping sensor data processing
    disable_counter_interrupt();
    break;

  case WakeUp:
    uart_write_debug_string("__In WakeUp\r\n");
    sensor_state = Logging;
    eeprom_write_byte(&ee_sensor_state, sensor_state);
    enable_comm_timer_interrupt();
    disable_counter_interrupt();
    break;

  case DebugMsg:  // Ignore compiler warning: case value not in enumerated type 'server_msg_tag'
    uart_write_debug_string("__In DebugMsg\r\n");
    break;

  case ResetReading:
    uart_write_debug_string("__ResetReading\r\n");
    set_num_tips(0);
    break;

  case GetPingInterval:
    uart_write_debug_string("__In GetPingInterval\r\n");
    next_sensor_msg_main.tag = PingInterval;
    next_sensor_msg_main.length = 4;
    float_to_byte_arr(ping_interval, next_sensor_msg_main.value);
    write_packet(&next_sensor_msg_main);
    break;

  case GetState:
    uart_write_debug_string("__In GetState\r\n");
    next_sensor_msg_main.tag = State;
    switch (sensor_state)
    {
    case Logging:
      next_sensor_msg_main.length = strlen(STRING_OF(Logging));
      memcpy(next_sensor_msg_main.value, STRING_OF(Logging), next_sensor_msg_main.length);
      break;

    case Sleeping:
      next_sensor_msg_main.length = strlen(STRING_OF(Sleeping));
      memcpy(next_sensor_msg_main.value, STRING_OF(Sleeping), next_sensor_msg_main.length);
      break;
    }
    write_packet(&next_sensor_msg_main);
    break;

  case Resend:  // Ignore compiler warning: case value not in enumerated type 'server_msg_tag'
    uart_write_debug_string("__In Resend\r\n");
    write_packet(&prev_sensor_msg);
    break;

  default:
    PORTB |= (1<<5);
  }
  signal_processing = false;
} // process_server_msg


ISR(TIMER1_COMPA_vect, ISR_NOBLOCK)
{
  // Static variable definition and initialization
  static uint16_t comm_counter = 0;

  if (!signal_processing)
  {
    /* When the timer runs out and it is time to send data to the server
     * or when the server has reduced the log interval of the sensor,
     * the following condition holds
     */
    if (comm_counter >= comm_counter_top)
    {
      // Sending rainfall amount to server
      next_sensor_msg_isr.tag = Log;
      next_sensor_msg_isr.length = 4;
      float_to_byte_arr(num_tips*NUM_TIPS_2_RAINFALL_FACTOR, (uint8_t *)next_sensor_msg_isr.value);
      write_packet((sensor_msg_t *)&next_sensor_msg_isr);

      // Resetting the communication counter to zero
      comm_counter = 0;

      // Resetting the number of tips of the sensor to zero
      num_tips = 0;
    }
  }
  ++comm_counter;
} // TIMER0_COMP_vect


ISR(INT0_vect)
{
	// Since the interrupt is rising edge triggered, we are expecting a string of ones after the edge
	// This is a simple filtering measure taken to avoid signal noise
	for (uint8_t i=0; i<10; ++i, DELAY_US(100))
  {
    if ((PIND & (1<<2)) == 0)
      return;
  }
  ++num_tips;
} // INT0_vect

