using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;

namespace GSMModemEmulator
{
  public partial class FormGSMModemEmulator : Form
  {
    class SMS
    {
      public string Sender;
      public string Message;
      public DateTime RXTimeStamp;

      public SMS()
      {
        Sender = string.Empty;
        Message = string.Empty;
        RXTimeStamp = DateTime.MinValue;
      }

      public SMS(string Sender, string Message, DateTime RXTimeStamp)
      {
        this.Sender = Sender;
        this.Message = Message;
        this.RXTimeStamp = RXTimeStamp;
      }

      public SMS(SMS AnotherSMS)
      {
        this.Sender = AnotherSMS.Sender;
        this.Message = AnotherSMS.Message;
        this.RXTimeStamp = AnotherSMS.RXTimeStamp;
      }

      public bool Equals(SMS AnotherSMS)
      {
        return (this.Sender.Equals(AnotherSMS.Sender) &&
                this.Message.Equals(AnotherSMS.Message) &&
                this.RXTimeStamp.Equals(AnotherSMS.RXTimeStamp));
      }
    }

    SMS EmptySMS = new SMS("(None)", "(None)", DateTime.MinValue);

    private const int INBOX_CAPACITY = 20;
    private Queue<SMS> WaitingSMSQueue = new Queue<SMS>();
    private SMS[] Inbox = new SMS[INBOX_CAPACITY + 1];
    private SerialPort SPort = new SerialPort();
    private System.Timers.Timer DequeueTimer = new System.Timers.Timer(3000);
    private System.Timers.Timer AutomatedRXTimer = new System.Timers.Timer();
    private SerialDataReceivedEventHandler SPortEventHandler = null;
    private Xmlconfig configXML = new Xmlconfig("config.xml", true);

    public FormGSMModemEmulator()
    {
      InitializeComponent();
    }

    private void FormGSMModemEmulator_Load(object sender, EventArgs e)
    {
      Logger.Init("activity.log");

      try
      {
        string[] sp = SerialPort.GetPortNames();
        foreach (string port in sp)
          comboCOMPort.Items.Add(port);
        comboCOMPort.Sorted = true;

        grpAutomatic.Enabled = true;
        grpManual.Enabled = false;
        mnuAutomate_Click(null, null);

        for (int index = 1; index <= INBOX_CAPACITY; ++index)
          Inbox[index] = new SMS(EmptySMS);

        btnReload_Click(null, null);

        DequeueTimer.Enabled = true;
        DequeueTimer.AutoReset = true;
        DequeueTimer.Elapsed += new System.Timers.ElapsedEventHandler(DequeueTimer_Elapsed);
        DequeueTimer.Start();

        AutomatedRXTimer.Enabled = true;
        AutomatedRXTimer.AutoReset = true;
        AutomatedRXTimer.Elapsed += new System.Timers.ElapsedEventHandler(AutomatedRXTimer_Elapsed);
        AutomatedRXTimer.Start();

        SPortEventHandler = new SerialDataReceivedEventHandler(SPort_DataReceived);

        comboCOMPort.SelectedIndex = comboCOMPort.Items.IndexOf(configXML.Settings["COMPort"].Value);
        comboBaud.SelectedIndex = comboBaud.Items.IndexOf(configXML.Settings["Baud"].Value);
        btnConnect_Click(null, null);
      }
      catch
      {
        // Do nothing
      }
    }

    Random rand = new Random(DateTime.Now.Millisecond);
    void AutomatedRXTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      int Rainfall = rand.Next(RangeMin, RangeMax);
      string Sender = comboSender.Items[rand.Next(0, comboSender.Items.Count-1)].ToString();
      ReceiveSMS(Sender, Rainfall);   
    }

    void DequeueTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      for (int index = 1; index <= INBOX_CAPACITY; ++index)
      {
        if (Inbox[index].Equals(EmptySMS))
        {
          Inbox[index] = WaitingSMSQueue.Dequeue();
          SendCMTI(index);
          break;
        }
      }
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
      if (btnConnect.Text.Equals("Connect"))
      {
        try
        {
          SPort.PortName = comboCOMPort.SelectedItem.ToString();
          SPort.BaudRate = Convert.ToInt32(comboBaud.SelectedItem.ToString());
          SPort.Parity = Parity.None;
          SPort.StopBits = StopBits.One;
          SPort.DataBits = 8;
          SPort.NewLine = "\r";
          SPort.Open();
          System.Threading.Thread.Sleep(1000);
          SerialPortWrite("Call Ready\r\n");
          SPort.DataReceived += SPortEventHandler;
          btnConnect.Text = "Disconnect";

          if (mnuAutomate.Checked)
            grpAutomatic.Enabled = true;
          else
            grpManual.Enabled = true;
          Logger.AddInfo("Connected to " + SPort.PortName + " @ " + SPort.BaudRate.ToString());
        }
        catch (Exception exc)
        {
          MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      else
      {
        SPort.Close();
        btnConnect.Text = "Connect";

        if (mnuAutomate.Checked)
          grpAutomatic.Enabled = false;
        else
          grpManual.Enabled = false;
        Logger.AddInfo("Disconnected from " + SPort.PortName);
      }
    }

    void SPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      while (SPort.BytesToRead > 0)
      {
        string line = SPort.ReadLine() + "\r";
        DequeueTimer.Stop();

        Logger.AddInfo("RX : " + line);

        if (line.StartsWith("AT+"))
        {
          if (line.Equals("AT+CMGF=1\r"))
            SendCMGF1();
          else if (line.Equals("AT+CMGL=\"ALL\"\r"))
            SendCMGLALL();
          else if (line.StartsWith("AT+CMGD="))
            DeleteSMS(Convert.ToInt32(line.Substring(8)));
          else if (line.StartsWith("AT+CMGS="))
          {
            SerialPortWrite("> ");
            char esc = (char)26;
            SPort.ReadTo(esc.ToString());
            SerialPortWrite("OK\r\n");
          }
          else
          {
            Logger.AddError("Unhandled AT command received: " + line);
#if DEBUG
            MessageBox.Show("Unhandled AT command received: " + line,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
#endif
          }
        }
        DequeueTimer.Start();
      }
    }

    private void SendCMGF1()
    {
      SerialPortWrite("AT+CMGF=1\r\r\nOK\r");
    }

    private void btnReceive_Click(object sender, EventArgs e)
    {
      string Sender = string.Empty;
      int Rain = 0;

      try
      {
        sender = comboSender.SelectedItem.ToString();
      }
      catch
      {
        MessageBox.Show("Invalid Value for SMS Sender", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        comboSender.Focus();
        return;
      }

      try
      {
        Rain = Convert.ToInt32(txtRainfall.Text);
      }
      catch
      {
        MessageBox.Show("Invalid Value for Rainfall amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtRainfall.SelectAll();
        txtRainfall.Focus();
        return;
      }
      
      ReceiveSMS(Sender, Rain);
    }

    private void mnuAutomate_Click(object sender, EventArgs e)
    {
      if (mnuAutomate.Checked == true)
      {
        mnuAutomate.Checked = false;
        grpAutomatic.Enabled = false;
        grpManual.Enabled = true;
        AutomatedRXTimer.Stop();
      }
      else
      {
        mnuAutomate.Checked = true;
        grpManual.Enabled = false;
        grpAutomatic.Enabled = true;
        AutomatedRXTimer.Start();
      }
    }

    private void ReceiveSMS(string Sender, float Rainfall)
    {
      WaitingSMSQueue.Enqueue(new SMS(Sender, "0308" + Util.ByteArrToHex(Util.FloatToByteArr(Rainfall)), DateTime.Now));
      Logger.AddInfo("Received rainfall = " + Rainfall.ToString() + " from sensor = " + Sender);
    }

    private void SendCMTI(int index)
    {
      SerialPortWrite("\r\n+CMTI: \"SM\"," + index.ToString() + "\r\n");
    }

    private void SendCMGLALL()
    {
      SerialPortWrite("\nAT+CMGL=\"ALL\"\r\r\n");
      for (int index = 1; index <= INBOX_CAPACITY; ++index)
      {
        if (!Inbox[index].Equals(EmptySMS))
        {
          SerialPortWrite(String.Format("\r\n+CMGL: {0},\"REC UNREAD\",\"{1}\",\"\",\"{2:yy\\/MM\\/dd,hh:mm:ss+22}\r\n",
                                        index,
                                        Inbox[index].Sender,
                                        Inbox[index].RXTimeStamp.ToString("")));
          SerialPortWrite(Inbox[index].Message + "\r\n");
          System.Threading.Thread.Sleep(100);
        }
      }
      SerialPortWrite("OK\r\n");
    }

    private void DeleteSMS(int index)
    {
      Inbox[index] = EmptySMS;
      SerialPortWrite("\nAT+CMGD=" + index.ToString() + "\r\r\n");
      SerialPortWrite("OK\r\n");
    }

    private void mnuResponseControl_Click(object sender, EventArgs e)
    {
      if (mnuResponseControl.Text.StartsWith("Stop"))
      {
        mnuResponseControl.Text = "Start Responding to SENSIT Server";
        SPort.DataReceived -= SPortEventHandler;
      }
      else if (mnuResponseControl.Text.StartsWith("Start"))
      {
        mnuResponseControl.Text = "Stop Responding to SENSIT Server";
        SPort.DiscardInBuffer();
        SPort.DataReceived += SPortEventHandler;
      }
      else
        System.Diagnostics.Trace.Assert(false);
    }

    private void mnuAlwaysOnTop_Click(object sender, EventArgs e)
    {
      if (mnuAlwaysOnTop.Checked == false)
      {
        mnuAlwaysOnTop.Checked = true;
        this.TopMost = true;
      }
      else
      {
        mnuAlwaysOnTop.Checked = false;
        this.TopMost = false;
      }
    }

    private void FormGSMModemEmulator_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (SPort.IsOpen)
        SPort.Close();
    }

    private Result SerialPortWrite(string value)
    {
      if (mnuResponseControl.Text.StartsWith("Stop"))
      {
        if (SPort != null && SPort.IsOpen)
        {
          try
          {
            SPort.Write(value);
          }
          catch
          {
            Logger.AddError("Failed to write to " + SPort.PortName);
            return Result.Failure;
          }
        }
        // Logging all the characters transmitted in the serial port log file
        Logger.AddInfo("TX: " + value);
      }
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
        }
        catch
        {
          Logger.AddError("Failed to read from " + SPort.PortName);
          return string.Empty;
        }
      }
      // Logging all the characters received in the serial port log file
      Logger.AddInfo("RX : " + buffer + Environment.NewLine);
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
        }
        catch
        {
          Logger.AddError("Failed to read from " + SPort.PortName);
          return string.Empty;
        }
      }
      // Logging all the characters received in the serial port log file
      Logger.AddInfo("RX : " + buffer + value);
      return buffer;
    }

    int RangeMin = 0;
    int RangeMax = 0;
    int PingInterval = 0;

    private void btnReload_Click(object sender, EventArgs e)
    {
      try
      {
        RangeMin = Convert.ToInt32(txtRangeMin.Text);
        RangeMax = Convert.ToInt32(txtRangeMax.Text);
        PingInterval = Convert.ToInt32(numericPingInterval.Value);
        if (RangeMin > RangeMax)
          throw new Exception("Range Min is greater than Range Max");

        AutomatedRXTimer.Interval = PingInterval * 1000;
      }
      catch (Exception exc)
      {
#if DEBUG
        MessageBox.Show(exc.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
#endif
      }
    }
  }
}
