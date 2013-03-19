using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

using SENSITServer;

namespace SensorEmulator
{
  public partial class FormMain : Form
  {
    private Sensor sensor = null;
    private Random rand = null;
    
    #region Main
    public FormMain()
    {
      InitializeComponent();
    }

    private void Main_Load(object sender, EventArgs e)
    {
      string[] sp_list = SerialPort.GetPortNames();
      this.comboCOM.Items.Clear();
      this.comboCOM.Items.AddRange(sp_list);

      rand = new Random((int)DateTime.Now.Ticks);
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
      if (this.btnConnect.Text == "Connect")
      {
        try
        {
          string comPort = this.comboCOM.SelectedItem.ToString().Trim().ToUpper();
          int baud = Convert.ToInt32(this.txtBaud.Text);
          // Initializing and opening the COM Port
          SPort.PortName = comPort;
          SPort.BaudRate = baud;
          SPort.StopBits = StopBits.One;
          SPort.Parity = Parity.None;
          SPort.ReadBufferSize = 4096;
          SPort.Open();
          this.btnConnect.Text = "Disconnect";
          this.comboCOM.Enabled = false;
          this.txtBaud.Enabled = false;
          this.txtID.Enabled = false;
          this.txtDesc.Enabled = false;
        }
        catch (Exception exc)
        {
          MessageBox.Show(exc.Message,
                          "SensorEmulator",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
        }
      }
      else if (this.btnConnect.Text == "Disconnect")
      {
        SPort.Close();
        this.btnConnect.Text = "Connect";
        this.comboCOM.Enabled = true;
        this.txtBaud.Enabled = true;
        this.txtID.Enabled = true;
        this.txtDesc.Enabled = true;
      }
      else
      {
        MessageBox.Show("Unexpected value of Text property of btnConnect",
                        "Sensor Emulator",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
      }

      bool retry;
      do
      {
        retry = false;
        try
        {
          string id = this.txtID.Text;
          string desc = this.txtDesc.Text;
          sensor = new Sensor(id, Sensor.StateEnum.Logging.ToString(), desc);
          this.lblState.Text = "State: Logging";
        }
        catch
        {
          MessageBox.Show("Wrong ID and/or description",
                          "Sensor Emulator",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          retry = true;
        }
      } while (retry);
    }

    private void btnLog_Click(object sender, EventArgs e)
    {
      SensorMsg msg = new SensorMsg();

      if (sensor != null && SPort.IsOpen && sensor.State == Sensor.StateEnum.Logging)
      {
        msg.SenderId = sensor.Id;
        msg.Tag = SensorMsg.CommandTag.Log;
        msg.Length = 4;
        msg.Value = BitConverter.GetBytes(Convert.ToSingle(this.txtCurrM.Text));
        WritePacket(msg);
        float rm = (float)rand.NextDouble() * 2;
        this.txtCurrM.Text = rm.ToString();
      }
    }

    private void btnDebug_Click(object sender, EventArgs e)
    {
      SensorMsg msg = new SensorMsg();
      msg.SenderId = sensor.Id;
      msg.Tag = SensorMsg.CommandTag.DebugMsg;
      string debugmsg = this.txtDebug.Text;
      msg.Length = (byte)debugmsg.Length;
      msg.Value = (new UTF8Encoding()).GetBytes(debugmsg);
      WritePacket(msg);
    }

    private void btnStart_Click(object sender, EventArgs e)
    {
      SensorMsg msg = new SensorMsg();
      msg.SenderId = sensor.Id;
      msg.Tag = SensorMsg.CommandTag.StartCalibration;
      msg.Length = 0;
      msg.Value = null;
      WritePacket(msg);
    }

    private void btnH2F_Click(object sender, EventArgs e)
    {
      try
      {
        string input = this.txtInput.Text;
        byte[] bb = Util.HexToByteArr(input);
        float ff = BitConverter.ToSingle(bb, 0);
        this.txtOutput.Text = ff.ToString();
      }
      catch
      {
        this.txtOutput.Text = "ERROR";
      }
    }

    private void btnF2H_Click(object sender, EventArgs e)
    {
      try
      {
        float input = Convert.ToSingle(this.txtInput.Text);
        byte[] bb = BitConverter.GetBytes(input);
        string output = Util.ByteArrToHex(bb);
        this.txtOutput.Text = output;
      }
      catch
      {
        this.txtOutput.Text = "ERROR";
      }
    }

    private void btnS2H_Click(object sender, EventArgs e)
    {
      try
      {
        string hex = "";
        foreach (char c in this.txtInput.Text)
        {
          int tmp = c;
          hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
        }
        this.txtOutput.Text = hex;
      }
      catch
      {
        this.txtOutput.Text = "ERROR";
      }
    }

    private void btnH2S_Click(object sender, EventArgs e)
    {
      try
      {
        string input = this.txtInput.Text;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i <= input.Length - 2; i += 2)
        {
          sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(input.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
        }
        this.txtOutput.Text = sb.ToString();
      }
      catch
      {
        this.txtOutput.Text = "ERROR";
      }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      this.txtRX.Text = "";
    }
    #endregion

    #region Comm

    private const byte Preamble1 = 0xAA;
    private const byte Preamble2 = 0x55;
    private const byte Epilog = 0xFF;

    class CalibParam
    {
      public bool CalibDone;
      public float MinHeight;
      public float MaxHeight;
      public int NumSamples;
      public float[] MeasuredHeight;
      public float[] CalculatedADC;

      public CalibParam()
      {
        MeasuredHeight = new float[16];
        CalculatedADC = new float[16];
      }
    }

    CalibParam calibParam = new CalibParam();

    private Result WritePacket(SensorMsg msg)
    {
      try
      {
        byte[] byteArr = new byte[1 + 1 + Sensor.IdLength + 1 + 1 + msg.Length + 1];
        int idx = 0;
        byteArr[idx++] = Preamble1;                           // Preamble 1
        byteArr[idx++] = Preamble2;                           // Preamble 2
        for (int i = 0; i < msg.SenderId.Length; i++)
        {
          byteArr[idx++] = Convert.ToByte(msg.SenderId[i]); // ID of sensor
        }
        byteArr[idx++] = (byte)msg.Tag;                       // Tag
        byteArr[idx++] = (byte)(msg.Length * 2);              // Length - Actual length is twice the length of the byte array
                                                          // coz its encoded as a hex string
        for (int i = 0; i < msg.Length; i++)
        {
          byteArr[idx++] = msg.Value[i];     // Value
        }
        byteArr[idx++] = Epilog;    // Epilog

        string hexStr = Util.ByteArrToHex(byteArr);
        SPort.Write(hexStr);
        prevSensorMsg = msg;
        return Result.Success;
      }
      catch (Exception exc)
      {
        MessageBox.Show(exc.Message,
                        "SensorEmulator",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        return Result.Failure;
      }
    }

    /// <summary>
    /// Reads one byte from the Serial Port input buffer
    /// Before calling this function, make sure there are
    /// at least 2 bytes in the serial port input stream 
    /// otherwise an error message will be displayed
    /// </summary>
    /// <returns>Byte read</returns>
    private byte Read2Hex()
    {
      char[] charReceived = new char[2];

      try
      {
        SPort.Read(charReceived, 0, 2);
      }
      catch (Exception exc)
      {
        MessageBox.Show(exc.Message,
                        "Sensor Emulator",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
      }
      return Convert.ToByte(new string(charReceived), 16);
    }

    private enum SPortState
    {
      ExpectingP1,  // Expecting 1 byte of Preamble1
      ExpectingP2,  // Expecting 1 byte of Preamble2
      ExpectingID,  // Expecting 1 byte of Sensor ID
      ExpectingT,   // Expecting 1 byte of Tag
      ExpectingL,   // Expecting 1 byte of Length
      ExpectingV,   // Expecting Length bytes of Value
      ExpectingEp   // Expecting 1 byte of Epilog
    }

    // Initial StateEnum when it is expecting Preamble1
    SPortState state = SPortState.ExpectingP1;

    // Message received from the sensor
    ServerMsg serverMsg = new ServerMsg();

    private SensorMsg prevSensorMsg;

    private delegate void AddToTxtRXDelegate(string str);

    private void AddToTxtRX(string str)
    {
      if (this.txtRX.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.txtRX.Invoke(new AddToTxtRXDelegate(this.AddToTxtRX), str);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.txtRX.Text += str;
      }
    }

    float adc = 0;
    void ProcessCalibSample(float height)
    {
      // Saving the height measured
      calibParam.MeasuredHeight[calibParam.NumSamples] = height;

      // Saving the corresponding ADC reading
      calibParam.CalculatedADC[calibParam.NumSamples] = ++adc;
      ++calibParam.NumSamples;
    } // ProcessCalibSample

    void CalibrateSensor()
    {
      // Estimate values of ParamA and ParamB by straight line fitting over the sample points
      float sum_x = 0.0f;
      float sum_y = 0.0f;
      float sum_x2 = 0.0f;
      float sum_xy = 0.0f;

      int N = calibParam.NumSamples;
      for (int i = 0; i < N; ++i)
      {
        sum_x += (float)calibParam.CalculatedADC[i];
        sum_y += (float)calibParam.MeasuredHeight[i];
        sum_xy += (float)calibParam.CalculatedADC[i] * calibParam.MeasuredHeight[i];
        sum_x2 += (float)calibParam.CalculatedADC[i] * calibParam.CalculatedADC[i];
      }

      float ParamB = (N * sum_xy - sum_x * sum_y) / (N * sum_x2 - sum_x * sum_x);
      float ParamA = (sum_y - ParamB * sum_x) / N;

      // Saving the calibration data to the EEPROM so that later on, the MCU can startup without further requirement of calibration
      MessageBox.Show("Min H = " + calibParam.MinHeight + " Max H = " + calibParam.MaxHeight +
                      "A = " + ParamA + " B = " + ParamB, "Calibration Results", MessageBoxButtons.OK);
    } // CalibrateSensor


    /// <summary>
    /// This function processes any message received from any
    /// sensor. This function must be called by the derived classes
    /// of this class upon receiving a complete message packet from
    /// a sensor
    /// </summary>
    /// <param name="serverMsg">Message packet from sensor</param>
    private void ProcessMessage(ServerMsg serverMsg)
    {
      SensorMsg nextSensorMsg = new SensorMsg();
      nextSensorMsg.SenderId = sensor.Id;
   
      AddToTxtRX("--------------------\r\n");
      switch (serverMsg.Tag)
      {
        case ServerMsg.CommandTag.SendDescription:
          AddToTxtRX("SendDescription\r\n");
          nextSensorMsg.Tag = SensorMsg.CommandTag.Description;
          nextSensorMsg.Length = (byte)sensor.Desc.Length;
          nextSensorMsg.Value = (new UTF8Encoding()).GetBytes(sensor.Desc);
          WritePacket(nextSensorMsg);
          break;

        case ServerMsg.CommandTag.DebugMsg:
          AddToTxtRX("DebugMsg\r\n");
          AddToTxtRX(Util.ByteArrToString(serverMsg.Value) + "\r\n");
          break;

        case ServerMsg.CommandTag.Resend:
          AddToTxtRX("Resend\r\n");
          WritePacket(prevSensorMsg);
          break;

        case ServerMsg.CommandTag.ResetReading:
          AddToTxtRX("ResetReading\r\n");
          nextSensorMsg.Tag = SensorMsg.CommandTag.DoneResetReading;
          nextSensorMsg.Length = 0;
          nextSensorMsg.Value = null;
          WritePacket(nextSensorMsg);
          break;

        case ServerMsg.CommandTag.SetPingInterval:
          AddToTxtRX("SetPingTimePeriod\r\n");
          float new_t = Util.ByteArrToFloat(serverMsg.Value);
          AddToTxtRX(new_t.ToString()+"\r\n");
          nextSensorMsg.Tag = SensorMsg.CommandTag.DoneSetPingInterval;
          nextSensorMsg.Length = 4;
          nextSensorMsg.Value = Util.FloatToByteArr(new_t);
          WritePacket(nextSensorMsg);
          break;

        case ServerMsg.CommandTag.SetServerAddress:
          AddToTxtRX("SetServerAddress\r\n");
          string new_add = Util.ByteArrToString(serverMsg.Value);
          AddToTxtRX(new_add + "\r\n");
          break;

        case ServerMsg.CommandTag.Sleep:
          AddToTxtRX("Sleep\r\n");
          nextSensorMsg.Tag = SensorMsg.CommandTag.DoneSleep;
          nextSensorMsg.Length = 0;
          nextSensorMsg.Value = null;
          WritePacket(nextSensorMsg);
          break;

        case ServerMsg.CommandTag.WakeUp:
          AddToTxtRX("WakeUp\r\n");
          nextSensorMsg.Tag = SensorMsg.CommandTag.DoneWakeUp;
          nextSensorMsg.Length = 0;
          nextSensorMsg.Value = null;
          WritePacket(nextSensorMsg);
          break;

        case ServerMsg.CommandTag.Calibrate:
          AddToTxtRX("Calibrate\r\n");
          calibParam.CalibDone = false;
          calibParam.NumSamples = 0;
          nextSensorMsg.Tag = SensorMsg.CommandTag.StartCalibration;
          nextSensorMsg.Length = 0;
          nextSensorMsg.Value = null;
          WritePacket(nextSensorMsg);

          break;

        case ServerMsg.CommandTag.MinHeight:
          AddToTxtRX("Min Height = " + Util.ByteArrToFloat(serverMsg.Value).ToString() + "\r\n");

          calibParam.MinHeight = Util.ByteArrToFloat(serverMsg.Value);
          
          nextSensorMsg.Tag = SensorMsg.CommandTag.DoneMinHeight;
          nextSensorMsg.Length = 4;
          nextSensorMsg.Value = serverMsg.Value;
          WritePacket(nextSensorMsg);          
          break;

        case ServerMsg.CommandTag.MaxHeight:
          AddToTxtRX("Max Height = " + Util.ByteArrToFloat(serverMsg.Value).ToString() + "\r\n");
          calibParam.MaxHeight = Util.ByteArrToFloat(serverMsg.Value);

          nextSensorMsg.Tag = SensorMsg.CommandTag.DoneMaxHeight;
          nextSensorMsg.Length = 4;
          nextSensorMsg.Value = serverMsg.Value;
          WritePacket(nextSensorMsg);          
          break;

        case ServerMsg.CommandTag.Height:
          AddToTxtRX("Height = " + Util.ByteArrToFloat(serverMsg.Value).ToString() + "\r\n");
          ProcessCalibSample(Util.ByteArrToFloat(serverMsg.Value));

          nextSensorMsg.Tag = SensorMsg.CommandTag.DoneHeight;
          nextSensorMsg.Length = 4;
          nextSensorMsg.Value = serverMsg.Value;
          WritePacket(nextSensorMsg);
          break;

        case ServerMsg.CommandTag.CalibrationDone:
          AddToTxtRX("Calibration Done\r\n");

          CalibrateSensor();    // Calibrating the sensor with the collected data

          nextSensorMsg.Tag = SensorMsg.CommandTag.DoneCalibration;
          nextSensorMsg.Length = 0;
          nextSensorMsg.Value = null;
          WritePacket(nextSensorMsg);
          break;

        default:
          MessageBox.Show("Unhandled case " + serverMsg.Tag.ToString(),
                          "Sensor Emulator",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          break;
      }
    }
    #endregion

    private void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      while (SPort.BytesToRead >= 2)
      {
        switch (state)
        {
          case SPortState.ExpectingP1:
            if (SPort.BytesToRead >= 2)
            {
              byte preamble1 = Read2Hex();
              if (preamble1 == Preamble1)
              {
                state = SPortState.ExpectingP2;
              }
              else
              {
                state = SPortState.ExpectingP1;
              }
            }
            break;

          case SPortState.ExpectingP2:
            if (SPort.BytesToRead >= 2)
            {
              byte preamble2 = Read2Hex();
              if (preamble2 == Preamble2)
              {
                state = SPortState.ExpectingID;
              }
              else
              {
                state = SPortState.ExpectingP1;
              }
            }
            break;

          case SPortState.ExpectingID:
            if (SPort.BytesToRead >= Sensor.IdLength * 2)
            {
              serverMsg.RecipientId = "";
              byte[] byteID = new byte[Sensor.IdLength];
              for (int i = 0; i < Sensor.IdLength; i++)
              {
                byteID[i] = Read2Hex();
              }
              serverMsg.RecipientId = Util.ByteArrToString(byteID);
              if (serverMsg.RecipientId == sensor.Id)
              {
                state = SPortState.ExpectingT;
              }
              else
              {
                state = SPortState.ExpectingP1;
              }
            }
            break;

          case SPortState.ExpectingT:
            if (SPort.BytesToRead >= 2)
            {
              serverMsg.Tag = (ServerMsg.CommandTag)Read2Hex();
              state = SPortState.ExpectingL;
            }
            break;

          case SPortState.ExpectingL:
            if (SPort.BytesToRead >= 2)
            {
              serverMsg.Length = (byte)(Read2Hex()/2);
              state = SPortState.ExpectingV;
            }
            break;

          case SPortState.ExpectingV:
            if (serverMsg.Length > 0 && SPort.BytesToRead >= serverMsg.Length * 2)
            {
              serverMsg.Value = new byte[serverMsg.Length];
              for (int i = 0; i < serverMsg.Length; i++)
              {
                serverMsg.Value[i] = Read2Hex();
              }
            }
            state = SPortState.ExpectingEp;
            break;

          case SPortState.ExpectingEp:
            if (SPort.BytesToRead >= 2)
            {
              int epilog = Read2Hex();
              if (epilog == Epilog)
              {
                // A complete sensor message packet is received. Process it
                ProcessMessage(serverMsg);
              }
              state = SPortState.ExpectingP1;
            }
            break;

          default:
            MessageBox.Show("Unexpected Serial Port state",
                            "Sensor Emulator",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
            break;
        }
      }
    }

    private void btnB2H_Click(object sender, EventArgs e)
    {
      try
      {
        char[] delim = {' ', '-'};
        string input = this.txtInput.Text;
        string[] strBytes = input.Split(delim);
        byte[] bb = new byte[strBytes.Length];

        for (int i = 0; i < bb.Length; i++)
        {
          bb[i] = Convert.ToByte(strBytes[i]);
        }
        this.txtOutput.Text = Util.ByteArrToHex(bb);
      }
      catch
      {
        this.txtOutput.Text = "ERROR";
      }
    }

    private void btnH2B_Click(object sender, EventArgs e)
    {
      try
      {
        string input = this.txtInput.Text;
        byte[] bb = Util.HexToByteArr(input);

        this.txtOutput.Text = "";
        for (int i = 0; i < bb.Length; i++)
        {
          this.txtOutput.Text += bb[i].ToString() + " ";  
        }
      }
      catch
      {
        this.txtOutput.Text = "ERROR";
      }
    }

    private void btnResend_Click(object sender, EventArgs e)
    {
      WritePacket(prevSensorMsg);
    }

    private void btnSendDesc_Click(object sender, EventArgs e)
    {
      SensorMsg msg = new SensorMsg();
      msg.SenderId = sensor.Id;
      msg.Tag = SensorMsg.CommandTag.Description;
      string desc = this.txtDesc.Text;
      msg.Length = (byte)desc.Length;
      msg.Value = (new UTF8Encoding()).GetBytes(desc);
      WritePacket(msg);
    }
  }
}