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
      DebugMsg, 
      Resend,   
      ResetReading,
      SetServerAddress,
      SetPingInterval,
      GetPingInterval,
      SendDescription,
      WakeUp,
      Sleep,
      GetState
    }

    public string RecipientId;
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
      Sleeping = 0,   // Sensor does not send any measurement data to the server
      Logging,        // Sensor sending measurement data to the server
      BeingCalibrated // Sensor is being calibrated and hence not logging to the server
    }

    public string Id;
    public string Description;
    public StateEnum State;
    public SensorMsg LastMsgSent;
    public float LastReading;
    public string LastReadingTime;
    public float PingInterval;
   
    public Sensor(string Id)
    {
      // Initial State is Logging
      SensorInit(Id, StateEnum.Logging.ToString(), null, -1);
    }

    public Sensor(string Id, string state)
    {
      SensorInit(Id, state, null, -1);
    }

    public Sensor(string Id, string State, string Description)
    {
      SensorInit(Id, State, Description, -1);
    }

    public Sensor(string Id, string State, string Description, float PingInterval)
    {
      SensorInit(Id, State, Description, PingInterval);
    }

    private void SensorInit(string Id, string State, string Description, float PingInterval)
    {
      this.Id = Id;
      this.State = (StateEnum)Enum.Parse(typeof(StateEnum), State);
      this.Description = (Description == null) ? "(None)" : Description;
      this.PingInterval = PingInterval;

      this.LastMsgSent = null;
      this.LastReading = -1;
      this.LastReadingTime = string.Empty; // Default Date value, meaning uninitialized
    }
  }

  class SensorMsg
  {
    public enum CommandTag
    {
      None = 0,
      DebugMsg,
      Resend,
      Log,
      Description,
      PingInterval,
      State
    }

    public string SenderId;

    // Date
    public string Date;

    // Time
    public string Time;
    
    // Message
    public CommandTag Tag;
    public byte Length;
    public byte[] Value;

    public SensorMsg()
    {
      SenderId = "";
      Tag = CommandTag.None;
      Length = 0;
      Value = null;
    }

    // copy constructor
    public SensorMsg(SensorMsg sensorMsg)
    {
      this.SenderId = sensorMsg.SenderId;
      this.Date = sensorMsg.Date;
      this.Time = sensorMsg.Time;
      this.Tag = sensorMsg.Tag;
      this.Length = sensorMsg.Length;
      this.Value = sensorMsg.Value;
    }
  }
}