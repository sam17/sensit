using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SENSITServer
{
  class ServerMsg
  {
    public enum CommandTag
    {
      None = 0,
      Resend,
      DebugMsg,
      SetBaud = 10,
      ResetReading,
      SetServerAddress,
      SetPingInterval,
      GetPingInterval,
      SendDescription,
      WakeUp,
      Sleep,
      GetState,
      Calibrate,
      MinHeight,
      MaxHeight,
      Height,
      CalibrationDone
    }

    public string     RecipientId;
    public CommandTag Tag;

    public byte Length;

    public byte[] Value;

    public ServerMsg()
    {
      RecipientId = "";
      Tag = CommandTag.None;
      Length = 0;
      Value = null;
    }
  }

  class Sensor
  {
    public enum StateEnum
    {
      Sleeping,       // Sensor does not send any measurement data to the server
      Logging,        // Sensor sending measurement data to the server
      BeingCalibrated // Sensor is being calibrated and hence not logging to the server
    }

    public class CalibData
    {
      public float minHeight;
      public float maxHeight;
      public List<float> height;

      public CalibData()
      {
        minHeight = maxHeight = 0;
        height = new List<float>();
      }
    }

    public static int IdLength = 10;

    public string Id;
    public string Desc;
    public StateEnum State;
    public SensorMsg.CommandTag NextExpectedMsg;
    public SensorMsg LastMsgSent;
    public float LastReading;
    public DateTime LastReadingTime;
    public float PingInterval;
    public CalibData CalibParameters;


    public Sensor(string Id)
    {
      // Initial State is Logging
      SensorInit(Id, StateEnum.Logging.ToString(), null, -1);
    }

    public Sensor(string Id, string state)
    {
      SensorInit(Id, state, null, -1);
    }

    public Sensor(string Id, string state, string Desc)
    {
      SensorInit(Id, state, Desc, -1);
    }

    public Sensor(string Id, string state, string Desc, float PingInterval)
    {
      SensorInit(Id, state, Desc, PingInterval);
    }

    private void SensorInit(string Id, string state, string Desc, float PingInterval)
    {
      if (Id.Length > Sensor.IdLength)
      {
        MessageBox.Show("Sensor ID length cannot be greater than " + Sensor.IdLength,
                        "SensorEmulator",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
      }
      this.Id = Id.PadLeft(Sensor.IdLength, ' ');
      this.State = (StateEnum)Enum.Parse(typeof(StateEnum), state);
      this.Desc = (Desc == null) ? "(None)" : Desc;
      this.PingInterval = (PingInterval < 0) ? 5 : PingInterval;  // Default ping period of 5 min

      this.NextExpectedMsg = SensorMsg.CommandTag.None;
      this.LastMsgSent = null;
      this.LastReading = -1;
      this.LastReadingTime = DateTime.MinValue; // Default date value, meaning uninitialized

      this.CalibParameters = new CalibData();
    }
  }

  class SensorMsg
  {
    public enum CommandTag
    {
      None = 0,
      Resend,
      DebugMsg,
      Log = 10,
      StartCalibration,
      DoneMinHeight,
      DoneMaxHeight,
      DoneHeight,
      DoneCalibration,
      Description,
      DoneResetReading,
      DoneSetPingInterval,
      PingInterval,
      DoneWakeUp,
      DoneSleep,
      State
    }

    public string     SenderId;
    public CommandTag Tag;
    public byte       Length;
    public byte[]     Value;

    public SensorMsg()
    {
      SenderId = "";
      Tag = CommandTag.None;
      Length = 0;
      Value = null;
    }
  }
}