namespace SENSITServer
{
  struct Conf
  {
    #region Names of tags and nodes used in the application configuration file
    public const string CONFIG_FILE_PATH = "config.xml";
    public const string CONFIG_TEMPLATE_FILE_PATH = "config.template.xml";

    public const string ROOT_NODE = "Config";
    public const string CONFIG_INITIALIZED_TAG = "Initialized";
    public const string YES = "Yes";
    public const string NO = "No";

    public const string ADMIN_NODE = "Admin";
    public const string PASSWORD_HASH_TAG = "PasswordHash";
    public const string REMEMBER_PASSWORD_TAG = "RememberPassword";

    public const string LOG_NODE = "Log";
    public const string RAINFALL_LOG_DIR_TAG = "RainfallLogDir";
    public const string APP_LOG_DIR_TAG = "AppLogDir";
    public const string WINRAR_DIR_TAG = "WinrarDir";

    public const string GSM_COMM_NODE = "GSMComm";
    public const string GSM_COMPORT_TAG = "COMPort";
    public const string GSM_BAUD_TAG = "BaudRate";
    public const string GSM_STOPBITS_TAG = "StopBits";
    public const string GSM_PARITY_TAG = "Parity";

    public const string ARDUINO_COMM_NODE = "ArduinoComm";
    public const string ARDUINO_COMPORT_TAG = "COMPort";
    public const string ARDUINO_BAUD_TAG = "BaudRate";
    public const string ARDUINO_STOPBITS_TAG = "StopBits";
    public const string ARDUINO_PARITY_TAG = "Parity";

    public const string SENSOR_LIST_NODE = "SensorList";
    public const string SENSOR_NODE = "Sensor";
    public const string SENSOR_ID_TAG = "ID";
    public const string SENSOR_DESC_TAG = "Description";
    public const string SENSOR_STATE_TAG = "State";
    public const string SENSOR_STATE_SLEEPING_VALUE = "Sleeping";
    public const string SENSOR_STATE_LOGGING_VALUE = "Logging";
    public const string SENSOR_PING_INTERVAL_TAG = "PingInterval";

    public const string EMAIL_RECIPIENTS_NODE = "EmailRecipients";
    public const string EMAIL_TAG = "Email";

    public const string PARAMS_NODE = "Parameters";
    public const string NUM_FAILED_READS_TAG = "NumFailedReads";
    public const string NUM_INACTIVE_MINS_TAG = "NumInactiveMins";
    #endregion

    /// <summary>
    /// The millimeter equivalent of unit rainfall reading sent by the sensors
    /// In this case, the sensors send the number of tips where each tip is 
    /// equivalent to 0.254 mm of rainfall
    /// </summary>
    public const float SENSOR_READING_TO_RAINFALL_FACTOR = 0.254f;

    /// <summary>
    /// The format in which the timestamp is displayed in log files generated
    /// </summary>
    public const string LOG_TIMESTAMP_FORMAT = "yyyy-MM-dd HH:mm:ss";
  }

  struct Logging
  {
    public const string SENSOR_LOG_READING_FORMAT = "0.0000";
    public const string ACIVITY_FILE_NAME = "activity.sensitlog";
    public const string SERIAL_PORT_LOG_FILE_NAME = "serialport.sensitlog";

    /// <summary>
    /// Name of the archive file that archives alls the logs
    /// and is sent through e-mail when the application attempts
    /// a restart
    /// </summary>
    public const string LOG_ARCHIVE_FILE_NAME = "sensit_log.rar";
  }
}