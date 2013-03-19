using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace SENSITServer
{
  public partial class FormMain
  {
    // Stores the time when the serial port last received and transmitted data respectively
    DateTime lastRXTime = DateTime.Now, lastTXTime = DateTime.Now;

    // Stores whether the serial port is receiving or transmitting data
    SerialPortLogger.LogState logState = SerialPortLogger.LogState.Init;

    private void LogRX(char ch)
    {
      SerialPortLogger.LogRX(ch);
      if (logState == SerialPortLogger.LogState.Init || logState == SerialPortLogger.LogState.TX || DateTime.Now.Subtract(lastRXTime).TotalSeconds > 5)
      {
        lastRXTime = DateTime.Now;
        string text = String.Format(Environment.NewLine + "(RX) {0} : {1}", DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT), ch);
        if (this.radLogSerialPort.Checked)
          AppendTxtLogText(text);
        logState = SerialPortLogger.LogState.RX;
      }
      else if (logState == SerialPortLogger.LogState.RX)
      {
        if (this.radLogSerialPort.Checked)
          AppendTxtLogText(ch.ToString());
      }
    }

    private void LogTX(char ch)
    {
      SerialPortLogger.LogTX(ch);
      if (logState == SerialPortLogger.LogState.Init || logState == SerialPortLogger.LogState.RX || DateTime.Now.Subtract(lastTXTime).TotalSeconds > 5)
      {
        lastTXTime = DateTime.Now;
        string text = String.Format(Environment.NewLine + "(TX) {0} : {1}", DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT), ch);
        if (this.radLogSerialPort.Checked) 
          AppendTxtLogText(text);
        logState = SerialPortLogger.LogState.TX;
      }
      else if (logState == SerialPortLogger.LogState.TX)
      {
        if (this.radLogSerialPort.Checked)
          AppendTxtLogText(ch.ToString());
      }
    }

    private Dictionary<string, ServerMsg> prevServerMsg = new Dictionary<string, ServerMsg>();

    /// <summary>
    /// It processes any message received from any
    /// sensor. This function must be called by the derived classes
    /// of this class upon receiving a complete message packet from
    /// a sensor
    /// </summary>
    /// <param name="sensorMsg">Message packet from sensor</param>
    private void ProcessMessage(SensorMsg sensorMsg)
    {
      LoggerEnterMethod();

      ServerMsg nextServerMsg;

      string Id = sensorMsg.SenderId;
      switch (sensorMsg.Tag)
      {
        case SensorMsg.CommandTag.Log:
          {
            float reading = Util.ByteArrToFloat(sensorMsg.Value) * Conf.SENSOR_READING_TO_RAINFALL_FACTOR;
            LoggerInfo("RECEIVED Rainfall measurement = " + reading + " from Sensor " + Id + " at " + sensorMsg.Date + ", " + sensorMsg.Time);

            /* Check if the sensor is already known or not.
             * If not, add it to the list of known sensors and
             * ask for its description
             */
            if (KnownSensors.ContainsKey(Id) == false)
            {
              KnownSensors.Add(Id, new Sensor(Id));
              AddToListKnownSensors(Id);
              LoggerInfo("ADDED Sensor " + Id);
              nextServerMsg = new ServerMsg();
              nextServerMsg.RecipientId = Id;
              nextServerMsg.Tag = ServerMsg.CommandTag.SendDescription;
              nextServerMsg.Length = 0;
              nextServerMsg.Value = null;
              WritePacket(nextServerMsg);
            }

            // Save the last reading of the sensor
            KnownSensors[Id].LastReading = reading;
            KnownSensors[Id].LastReadingTime = sensorMsg.Date + " " + sensorMsg.Time;
            SaveSensorInfo(Id);

            // Logging the reading in the appropriate log file
            string path = frmOptions.RainfallLogDirPath + "/" + Id + "/" + Id + "_" + KnownSensors[Id].LastReadingTime.Split(new char[] { ' ' })[0].Replace('/', '-') + ".csv";
            string dir = Path.GetDirectoryName(path);
            if (Directory.Exists(dir) == false)
              Directory.CreateDirectory(dir);

            LoggerLog(path,
                       KnownSensors[Id].LastReadingTime.Split(new char[] { ' ' })[1],
                       String.Format("{0:" + Logging.SENSOR_LOG_READING_FORMAT + "}", KnownSensors[Id].LastReading));
            break;
          }

        case SensorMsg.CommandTag.Description:
          {
            string desc = Util.ByteArrToString(sensorMsg.Value);
            LoggerInfo("RECEIVED Description = " + desc + " from Sensor " + Id);
            KnownSensors[Id].Description = desc;
            SaveSensorInfo(Id);
            break;
          }

        case SensorMsg.CommandTag.Resend:
          {
            LoggerInfo("RECEIVED Resend from Sensor " + Id);
            WritePacket(prevServerMsg[Id]);
            break;
          }

        case SensorMsg.CommandTag.DebugMsg:
          {
            string dm = Util.ByteArrToString(sensorMsg.Value);
            LoggerInfo("RECEIVED Debug Message = " + dm + " from Sensor " + Id);
            break;
          }

        case SensorMsg.CommandTag.PingInterval:
          {
            float pingInterval = Util.ByteArrToFloat(sensorMsg.Value);
            LoggerInfo("RECEIVED PingInterval = " + pingInterval.ToString() + " s from Sensor " + Id);
            KnownSensors[Id].PingInterval = pingInterval;
            SaveSensorInfo(Id);
            break;
          }

        case SensorMsg.CommandTag.State:
          {
            string state = Util.ByteArrToString(sensorMsg.Value);
            LoggerInfo("RECEIVED State = " + state + " from Sensor " + Id);
            KnownSensors[Id].State = (Sensor.StateEnum)Enum.Parse(typeof(Sensor.StateEnum), state);
            SaveSensorInfo(Id);
            break;
          }

        default:
          {
            LoggerError("Unhandled case " + sensorMsg.Tag.ToString());
#if DEBUG
            MessageBox.Show("Unhandled case " + sensorMsg.Tag.ToString() + "\n" + Util.GetMethodNameAndLineNum(),
                            "SENSIT Server",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
#endif
            break;
          }
      }

      RefreshGuiSensorInfo();

      // Remembering the previous message received from each sensor
      KnownSensors[Id].LastMsgSent = sensorMsg;

      LoggerLeaveMethod();
    }
  }
}
