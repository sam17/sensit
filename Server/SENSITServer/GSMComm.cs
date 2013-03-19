using System;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Windows.Forms;

namespace SENSITServer
{
  public partial class FormMain
  {
    System.Timers.Timer TimeTicker = new System.Timers.Timer();
    System.Timers.Timer AppRestartTimer = new System.Timers.Timer();

    static readonly object SPortLock = new object();
    static readonly object ProcessSMSLock = new object();

    bool PingGSMInTimer = false;

    /// <summary>
    /// Object of the serial port that will receive from and send to GSM module
    /// </summary>
    private SerialPort SPort = null;

    /// <summary>
    /// Object of the serial port that will receive from and send to Arduino uC
    /// </summary>
    private SerialPort ArduinoSPort = null;

    private int SPortReadFailureCount = 0, SPortWriteFailureCount = 0;

    private Result SerialPortWrite(byte[] buffer, int offset, int count)
    {
      if (SPort != null && SPort.IsOpen)
      {
        try
        {
          SPort.Write(buffer, offset, count);
          SPortWriteFailureCount = 0;
        }
        catch
        {
          ++SPortWriteFailureCount;
          LoggerError("Failed to write to " + SPort.PortName);
          if (SPortWriteFailureCount >= frmOptions.NumFailedReads)
            AttemptApplicationRestart("several serial port write failures");
          return Result.Failure;
        }
      }
      // Logging all the characters transmitted in the serial port log file
      foreach (byte bt in buffer)
        LogTX(Convert.ToChar(bt));
      return Result.Success;
    }

    private Result SerialPortWrite(string value)
    {
      if (SPort != null && SPort.IsOpen)
      {
        try
        {
          SPort.Write(value);
          SPortWriteFailureCount = 0;
        }
        catch
        {
          ++SPortWriteFailureCount;
          LoggerError("Failed to write to " + SPort.PortName);
          if (SPortWriteFailureCount >= frmOptions.NumFailedReads)
            AttemptApplicationRestart("several serial port write failures");
          return Result.Failure;
        }
      }

      // Logging all the characters transmitted in the serial port log file
      foreach (char ch in value)
        LogTX(ch);
      return Result.Success;
    }

    private string SerialPortReadLine()
    {
      string buffer = string.Empty;
      if (SPort != null && SPort.IsOpen)
      {
        try
        {
          buffer = SPort.ReadLine();
          SPortReadFailureCount = 0;
        }
        catch
        {
          ++SPortReadFailureCount;
          LoggerError("Failed to read from " + SPort.PortName);
          if (SPortReadFailureCount >= frmOptions.NumFailedReads)
            AttemptApplicationRestart("several serial port read failures");
          return null;
        }
      }
      // Logging all the characters received in the serial port log file
      foreach (char ch in buffer)
        LogRX(ch);
      LogRX(SPort.NewLine[0]);

      /* Restarting the application restart timer as something has been received
       * successfully by the serial port
       */
      AppRestartTimer.Stop();
      AppRestartTimer.Start();

      return buffer;
    }

    private string SerialPortReadTo(string value)
    {
      string buffer = string.Empty;
      if (SPort != null && SPort.IsOpen)
      {
        try
        {
          buffer = SPort.ReadTo(value);
          SPortReadFailureCount = 0;
        }
        catch
        {
          ++SPortReadFailureCount;
          LoggerError("Failed to read from " + SPort.PortName);
          if (SPortReadFailureCount >= frmOptions.NumFailedReads)
            AttemptApplicationRestart("several serial port read failures");
          return null;
        }
      }

      // Logging all the characters received in the serial port log file
      foreach (char ch in buffer)
        LogRX(ch);
      foreach (char ch in value)
        LogRX(ch);

      /* Restarting the application restart timer as something has been received
       * successfully by the serial port
       */
      AppRestartTimer.Stop();
      AppRestartTimer.Start();

      return buffer;
    }

    private void InitAndRegisterEvents()
    {
      TimeTicker.AutoReset = true;
      TimeTicker.Elapsed += new System.Timers.ElapsedEventHandler(TimeTicker_Elapsed);
      TimeTicker.Interval = 60000; // Setting the interval to 1 min
      TimeTicker.Enabled = true;
      TimeTicker.Start();
      LoggerInfo("Time Timer enabled and started");

      AppRestartTimer.AutoReset = false;
      AppRestartTimer.Elapsed += new System.Timers.ElapsedEventHandler(AppRestartTimer_Elapsed);
      AppRestartTimer.Interval = frmOptions.NumInactiveMins * 60 * 1000;
      AppRestartTimer.Enabled = true;
      AppRestartTimer.Start();
      LoggerInfo("App Restart Timer enabled and started");

      SPort = new SerialPort();
      SPort.DataReceived += new SerialDataReceivedEventHandler(SPort_DataReceived);
    }

    void AppRestartTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      AttemptApplicationRestart("prolonged GSM module inactivity");
    }

    /// <summary>
    /// Updates any changes made to the serial port settings, like
    /// COM Port to connect to, baud rate, etc.
    /// </summary>
    private void GSMUpdateSettings()
    {
      LoggerEnterMethod();

      this.menuStrip.Enabled = false;
      this.grpNetworkStatus.Enabled = false;
      this.grpSensorInfo.Enabled = false;
      this.grpLog.Enabled = false;
      this.Cursor = Cursors.WaitCursor;

      PingGSMInTimer = false;
      LoggerInfo("Disabled pinging GSM module");

      #region Initializing and opening the Arduino COM Port
      try
      {
        SetStripArduinoForeColor(Color.Orange);
        SetStripArduinoCOMPortText("Connecting to " + frmOptions.ArduinoCOMPort + "...");
        SetStripArduinoBaudText(string.Empty);
        SetStripMessageText(string.Empty);

        if (ArduinoSPort == null)
        {
          ArduinoSPort = new SerialPort(frmOptions.ArduinoCOMPort,
                                        frmOptions.ArduinoBaudRate,
                                        frmOptions.ArduinoParity,
                                        8,
                                        frmOptions.ArduinoStopBits);
        }
        else
        {
          if (ArduinoSPort != null && ArduinoSPort.IsOpen)
            ArduinoSPort.Close();
          ArduinoSPort.Dispose();

          ArduinoSPort.PortName = frmOptions.ArduinoCOMPort;
          ArduinoSPort.BaudRate = frmOptions.ArduinoBaudRate;
          ArduinoSPort.DataBits = 8;
          ArduinoSPort.Parity = frmOptions.ArduinoParity;
          ArduinoSPort.StopBits = frmOptions.ArduinoStopBits;
        }
        ArduinoSPort.ReadTimeout = 2000;  // Setting a timeout period of 2 sec for Arduino to respond
        ArduinoSPort.Open();
        
        SetStripArduinoCOMPortText(ArduinoSPort.PortName);
        SetStripArduinoBaudText(ArduinoSPort.BaudRate.ToString() + " bps");
        SetStripArduinoForeColor(Color.Green);
        
        LoggerInfo("Connected to Arduino " + ArduinoSPort.PortName + " @" + ArduinoSPort.BaudRate.ToString() + " bps");
      }
      catch (Exception exc)
      {
        SetStripArduinoForeColor(Color.Red);
        SetStripMessageText(string.Empty);

        if (frmOptions.ArduinoCOMPort.Trim().Equals(string.Empty))
        {
          SetStripArduinoCOMPortText("COM Port name not specified");
          SetStripArduinoBaudText(string.Empty);

          LoggerError("Arduino COM Port name not specified. Update it at Tools->Options->Communications");
#if DEBUG
          MessageBox.Show("Arduino COM Port name not specified. Update it at Tools->Options->Communications\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
#endif
        }
        else
        {
          SetStripArduinoCOMPortText(ArduinoSPort.PortName);
          SetStripArduinoBaudText(ArduinoSPort.BaudRate.ToString() + " bps");

          LoggerError("Connection to Arduino " + frmOptions.ArduinoCOMPort + " failed");
#if DEBUG
          MessageBox.Show("Connection to Arduino " + frmOptions.ArduinoCOMPort + " failed\n" + 
                          "Exception: " + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
#endif
        }
      }
      #endregion


      #region Initializing and opening the GSM COM Port

      bool propertiesChanged = false;  // Flag set if the properties of the GSM COM port have changed
      string COMPort = frmOptions.GSMCOMPort;
      int baudRate = frmOptions.GSMBaudRate;
      StopBits stopBits = frmOptions.GSMStopBits;
      Parity parity = frmOptions.GSMParity;

      try
      {
        SetStripGSMForeColor(Color.Orange);
        SetStripGSMCOMPortText("Connecting to " + COMPort + "...");
        SetStripGSMBaudText(string.Empty);
        SetStripMessageText(string.Empty);

        if (SPort == null)
        {
          SPort = new SerialPort(COMPort, baudRate, parity, 8, stopBits);
          propertiesChanged = true;
        }
        else
        {
          if (SPort != null && SPort.IsOpen)
            SPort.Close();
          SPort.Dispose();

          if (SPort.PortName.Equals(COMPort) == false)
            propertiesChanged = true;
          SPort.PortName = COMPort;

          if (SPort.BaudRate != baudRate)
            propertiesChanged = true;
          SPort.BaudRate = baudRate;

          SPort.DataBits = 8;
          SPort.Parity = parity;
          SPort.StopBits = stopBits;
        }
        SPort.NewLine = "\n";
        SPort.ReadBufferSize = 4096;
        SPort.ReadTimeout = 60 * 1000;  // Setting a timeout period of 1 min for the GSM module to respond in general
        SPort.WriteTimeout = 10 * 1000; // Setting a timeout period of 10s for the GSM module to write commands to it
        SPort.Open();
        SetStripGSMCOMPortText(COMPort);
        SetStripGSMBaudText(baudRate.ToString() + " bps");
        SetStripGSMForeColor(Color.Green);
        LoggerInfo("Connected to GSM " + COMPort + " @" + baudRate.ToString() + " bps");
      }
      catch (Exception exc)
      {
        SetStripGSMForeColor(Color.Red);
        SetStripMessageText(string.Empty);

        if (COMPort.Trim().Equals(string.Empty))
        {
          SetStripGSMCOMPortText("COM Port name not specified");
          SetStripGSMBaudText(string.Empty);

          LoggerError("GSM COM Port name not specified. Update it at Tools->Options->Communications");
#if DEBUG
          MessageBox.Show("GSM COM Port name not specified. Update it at Tools->Options->Communications\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
#endif
        }
        else
        {
          SetStripGSMCOMPortText(COMPort);
          SetStripGSMBaudText(baudRate.ToString() + " bps");

          LoggerError("Connection to GSM module " + COMPort + " failed : " + exc.Message);
#if DEBUG
          MessageBox.Show("Connection to GSM module " + COMPort + " failed\n" +  
                          "Exception: " + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
#endif
        }

        propertiesChanged = false;
      }

      #endregion

      if (propertiesChanged)
      {
        #region Setting SMS system into text mode
        try
        {
          SetStripMessageForeColor(Color.Orange);
          SetStripMessageText("Connecting to GSM module...");

          // No other serial port activity is allowed when the GSM module is restarted
          LoggerInfo("Locking SPort..." + Util.GetMethodNameAndLineNum());
          lock (SPortLock)
          {
            LoggerInfo("Locked SPort..." + Util.GetMethodNameAndLineNum());
            int sPortTimeout = SPort.ReadTimeout;
            SPort.ReadTimeout = 5000;
            if (SerialPortWrite("AT+CMGF=1\r") == Result.Failure)
              throw new Exception("GSM Serial Port write timeout");
            SerialPortReadTo("OK\r");
            SPort.ReadTimeout = sPortTimeout;
          }
          LoggerInfo("Released SPort..." + Util.GetMethodNameAndLineNum());

          SetStripGSMForeColor(Color.Green);
          SetStripGSMCOMPortText(COMPort);
          SetStripGSMBaudText(baudRate.ToString() + " bps");
          SetStripMessageText(string.Empty);
          LoggerInfo("Connected to GSM module");
        }
        catch (Exception exc)
        {
          SetStripGSMForeColor(Color.Red);
          SetStripGSMCOMPortText(COMPort);
          SetStripGSMBaudText(baudRate.ToString() + " bps");
          SetStripMessageText(string.Empty);

          LoggerError("Connection to GSM module failed : " + exc.Message);
#if DEBUG
          MessageBox.Show("Connection to GSM module failed\n" + 
                          "Exception: " + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
#endif
          ResetGSMPower();
        }
        #endregion
      }

      PingGSMInTimer = true;
      LoggerInfo("Enabled pinging GSM module");

      this.menuStrip.Enabled = true;
      this.grpNetworkStatus.Enabled = true;
      this.grpSensorInfo.Enabled = true;
      this.grpLog.Enabled = true;
      this.Cursor = Cursors.Default;
      LoggerLeaveMethod();
    }

    /// <summary>
    /// This timer triggers at regular intervals to check 
    /// if there is any pending message in the serial port
    /// data queue that has not been yet processed. If there
    /// is any, it is immediately sent for processing. 
    /// 
    /// It also updates the time interval for which the server
    /// is running without manual restart of the server
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void TimeTicker_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      LoggerEnterMethod();

      TimeSpan upTime = DateTime.Now.Subtract(StartUpTime);
      this.stripUpTime.Text = "Up Time: " + upTime.Hours.ToString() + " hour(s) " + upTime.Minutes.ToString() + " minute(s)";
      
      if (PingGSMInTimer)
      {
        if (SPort != null)
        {
          bool available = false;
          LoggerInfo("Locking SPort... " + Util.GetMethodNameAndLineNum()); 
          lock (SPortLock)
          {
            LoggerInfo("Locked SPort... " + Util.GetMethodNameAndLineNum()); 
            available = (SPort.IsOpen && SPort.BytesToRead > 0);
          }
          LoggerInfo("Released SPort... " + Util.GetMethodNameAndLineNum()); 
          if (available)
          {
            // If the serial port is working then try to process data received in the serial port
            SPort_DataReceived(null, null);
          }
          else
          {
            LoggerInfo("Starting a new thread for ProcessNewSMSs...");
            Thread th = new Thread(ProcessNewSMSs);
            th.IsBackground = true;
            th.Start();
          }
        }
        else
        {
          // If the serial port is not working then try to open it
          GSMUpdateSettings();
        }
      }
      LoggerLeaveMethod();
    }

    /// <summary>
    /// This function is responsible for reseting the power of the
    /// GSM module with the help of some additional hardware,
    /// like a relay switch
    /// </summary>
    void ResetGSMPower()
    {
      LoggerInfo("Resetitng GSM Power...");
      try
      {
        ArduinoSPort.ReadTimeout = 2000;
        ArduinoSPort.WriteTimeout = 2000;
        ArduinoSPort.Write("r");
        ArduinoSPort.ReadTo("d");
      }
      catch (Exception exc)
      {
        LoggerError("Could not send reset command to arduino @" + ArduinoSPort.PortName);
#if DEBUG
        MessageBox.Show("Could not send reset command to arduino\n" + Util.GetMethodNameAndLineNum() + "\n" + exc.Message,
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
#endif
      }
    }

    /// <summary>
    /// Sends a message to a sensor
    /// Format:
    /// 1 Byte: CommandTag of the message (Payload)
    /// 1 Byte: Length of the message
    /// Length Bytes: Message
    /// </summary>
    /// <param name="msg">It holds the message to be sent</param>
    /// <returns>
    /// Result.Success if writing is successful
    /// otherwise Result.Failure
    /// </returns>
    private Result WritePacket(ServerMsg msg)
    {
      LoggerEnterMethod();

      try
      {
        byte[] byteArr = new byte[1 + 1 + msg.Length];
        int idx = 0;

        byteArr[idx++] = (byte)msg.Tag;                        // CommandTag
        byteArr[idx++] = (byte)(msg.Length * 2);               // Length
        for (int i = 0; i < msg.Length; ++i)
          byteArr[idx++] = msg.Value[i];                       // Payload

        string hexStr = Util.ByteArrToHex(byteArr);
        LoggerInfo("Want to send " + hexStr);

        #region GSM SMS sending
        try
        {
          lock (SPortLock)
          {
            SerialPortWrite("AT+CMGS=\"" + msg.RecipientId + "\"\r");
            SerialPortReadTo("> ");
            SerialPortWrite(hexStr);
            SerialPortWrite(new byte[] { 26 }, 0, 1);
            // Waiting for "OK" from SIM
            SerialPortReadTo("OK\r");
          }
        }
        catch (TimeoutException exc)
        {
          LoggerError("Could not send SMS : " + exc.Message);
#if DEBUG
          MessageBox.Show("GSM module not responding" + "\n" + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
#endif
          LoggerLeaveMethod();
          return Result.Failure;
        }
        #endregion

        prevServerMsg[msg.RecipientId] = msg;

        #region Adding activity message to log files
        
        string activityMsg = string.Empty;
        switch (msg.Tag)
        {
          case ServerMsg.CommandTag.DebugMsg:
            {
              activityMsg = msg.Tag.ToString() + " = " + Util.ByteArrToString(msg.Value);
              break;
            }

          case ServerMsg.CommandTag.Resend:
            {
              activityMsg = msg.Tag.ToString();
              break;
            }

          case ServerMsg.CommandTag.ResetReading:
            {
              activityMsg = msg.Tag.ToString();
              break;
            }

          case ServerMsg.CommandTag.SendDescription:
            {
              activityMsg = msg.Tag.ToString();
              break;
            }

          case ServerMsg.CommandTag.SetPingInterval:
            {
              activityMsg = msg.Tag.ToString() + " = " + Util.ByteArrToFloat(msg.Value).ToString() + " s";
              break;
            }

          case ServerMsg.CommandTag.Sleep:
            {
              activityMsg = msg.Tag.ToString();
              break;
            }

          case ServerMsg.CommandTag.WakeUp:
            {
              activityMsg = msg.Tag.ToString();
              break;
            }

          case ServerMsg.CommandTag.GetPingInterval:
            {
              activityMsg = msg.Tag.ToString();
              break;
            }

          case ServerMsg.CommandTag.GetState:
            {
              activityMsg = msg.Tag.ToString();
              break;
            }

          default:
            {
              LoggerError("Unhandled case " + msg.Tag.ToString());
#if DEBUG
              MessageBox.Show("Unhandled case " + msg.Tag.ToString() + "\n" + Util.GetMethodNameAndLineNum(),
                              "SENSIT Server",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
#endif
              break;
            }
        }
        LoggerInfo("SENT " + activityMsg + " to Sensor " + msg.RecipientId);

        #endregion
      }
      catch (Exception exc)
      {
        LoggerError("Something went wrong : " + exc.Message);
#if DEBUG
        MessageBox.Show("Exception: " + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
#endif
        SetStripMessageText(string.Empty);
        SetStripGSMForeColor(Color.Red);
        LoggerLeaveMethod();
        return Result.Failure;
      }
      LoggerLeaveMethod();
      return Result.Success;
    }

    private void ProcessNewSMSs()
    {
      LoggerEnterMethod();
      LoggerInfo("Locking ProcessSMS... " + Util.GetMethodNameAndLineNum()); 
      lock (ProcessSMSLock) // This function should not be run simultaneously from multiple threads and hence the lock
      {
        LoggerInfo("Locked ProcessSMS... " + Util.GetMethodNameAndLineNum()); 

        PingGSMInTimer = false;
        LoggerInfo("Disabled pinging GSM module");

        // Reading unprocessed SMSs
        try
        {
          // Dictionary mapping SMS index to the SMS message
          Dictionary<string, SensorMsg> smsDict = new Dictionary<string, SensorMsg>();
          List<string> lines = new List<string>();
          string ln = string.Empty;

          LoggerInfo("Locking SPort... " + Util.GetMethodNameAndLineNum()); 
          lock (SPortLock)
          {
            LoggerInfo("Locked SPort... " + Util.GetMethodNameAndLineNum()); 
            int sPortTimeout = SPort.ReadTimeout;
            SPort.ReadTimeout = 5000;
            SerialPortWrite("AT+CMGF=1\r");
            SerialPortReadTo("OK\r");
            SPort.ReadTimeout = sPortTimeout;

            SerialPortWrite("AT+CMGL=\"ALL\"\r");
            while (true)
            {
              ln = SerialPortReadLine();
              if (ln != null && ln.Equals("OK\r") == false)
                lines.Add(ln);
              else
                break;
            }
          }
          LoggerInfo("Released SPort... " + Util.GetMethodNameAndLineNum()); 

          bool msgfound = false;
          string smsIndex = string.Empty;
          SensorMsg sm = null;
          foreach (string line in lines)
          {
            if (msgfound)
            {
              bool msgIsFromSensor = true;

              string msg = line.Trim(new char[] { '\r' });

              byte[] bmsg = null;
              try
              {
                bmsg = Util.HexToByteArr(msg);
                sm.Tag = (SensorMsg.CommandTag)bmsg[0];
              }
              catch (Exception exc)
              {
                LoggerWarning("Either the message is not hex coded or the tag is not defined, " +
                              "meaning that the message is not received from any SENSIT sensor. " +
                              "Exception: " + exc.Message);
                msgIsFromSensor = false;
              }

              if (msgIsFromSensor)
              {
                sm.Length = (byte)((int)bmsg[1] / 2);
                if (sm.Length * 2 + 2 * 2 != msg.Length)
                  msgIsFromSensor = false; // TLV packet inconsistency
                else
                {
                  sm.Value = new byte[sm.Length];
                  for (int i = 0; i < sm.Length; ++i)
                    sm.Value[i] = bmsg[i + 2];
                }
              }


              // If the message is not from a SENSIT sensor, discarding the message
              smsDict.Add(smsIndex, msgIsFromSensor ? sm : null);
              msgfound = false;
            }

            if (line.StartsWith("+CMGL:"))
            {
              string[] tokens = line.Split(new char[] { ',', '"', '\r', ' ' });
              int idx = 0;
              while (tokens[idx].Length == 0) ++idx;
              ++idx;                             // Ignore "+CMGL:"

              while (tokens[idx].Length == 0) ++idx;
              smsIndex = tokens[idx];            // SMS index
              ++idx;

              while (tokens[idx].Length == 0) ++idx;
              ++idx;                             // Ignore "REC"/"STO"

              while (tokens[idx].Length == 0) ++idx;
              ++idx;                             // Ignore "UNREAD"/"READ"/"UNSENT"/"SENT"

              while (tokens[idx].Length == 0) ++idx;
              sm = new SensorMsg();
              sm.SenderId = tokens[idx];         // Sender's number
              ++idx;

              while (tokens[idx].Length == 0) ++idx;
              sm.Date = tokens[idx];             // Date
              ++idx;

              while (tokens[idx].Length == 0) ++idx;
              sm.Time = tokens[idx];             // Time

              msgfound = true;
            }
          }

          #region Processing all the sensor messages received here
          foreach (KeyValuePair<string, SensorMsg> pair in smsDict)
          {
            if (pair.Value != null)
              ProcessMessage(pair.Value);

            // Once the message has been processed, it is deleted from the sim card
            try
            {
              LoggerInfo("Locking SPort... " + Util.GetMethodNameAndLineNum()); 
              lock (SPortLock)
              {
                LoggerInfo("Locked SPort... " + Util.GetMethodNameAndLineNum()); 
                SerialPortWrite("AT+CMGD=" + pair.Key + "\r");
                SerialPortReadTo("OK\r");
              }
              LoggerInfo("Released SPort... " + Util.GetMethodNameAndLineNum()); 
            }
            catch (TimeoutException exc)
            {
              LoggerError("GSM module failed to respond : " + exc.Message);
#if DEBUG
              MessageBox.Show("GSM module not responding" + "\n" + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
#endif
            }
          }
          #endregion
        }
        catch (TimeoutException exc)
        {
          LoggerError("GSM module failed to respond");
#if DEBUG
          MessageBox.Show("GSM module not responding" + "\n" + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
#endif
        }
        PingGSMInTimer = true;
        LoggerInfo("Enabled pinging GSM module");
      }
      LoggerInfo("Released ProcessSMS... " + Util.GetMethodNameAndLineNum()); 
      LoggerLeaveMethod();
    }

    /// <summary>
    /// Event handler for DataReceived event of SPort 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      bool stop = false;
      while (!stop)
      {
        string line = string.Empty;
        LoggerInfo("Locking SPort... " + Util.GetMethodNameAndLineNum()); 
        lock (SPortLock)
        {
          LoggerInfo("Locked SPort... " + Util.GetMethodNameAndLineNum()); 
          if (SPort != null && SPort.IsOpen && SPort.BytesToRead > 0)
            line = SerialPortReadLine();
          else
            stop = true;
        }
        LoggerInfo("Released SPort... " + Util.GetMethodNameAndLineNum()); 

        if (line.Length == 0)
          continue; // Not processing empty lines

        if (line.StartsWith("+CMTI:") || line.StartsWith("Call Ready"))
        {
          /* When a new SMS arrives or when the SIM restarts,
           * process the undeleted SMSs and delete them once processed
           */
          Thread th = new Thread(ProcessNewSMSs);
          th.IsBackground = true;
          th.Start();
        }
        else if (line.StartsWith("ERROR"))
          ResetGSMPower();
      }
    }
  }
}

