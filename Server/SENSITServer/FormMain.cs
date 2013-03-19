using System;
using System.Diagnostics;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Win32;
using System.Reflection;

namespace SENSITServer
{
  public partial class FormMain : Form
  {
    User AdminUser = null;

    private Xmlconfig ConfigXML = null;

    private FormOptions frmOptions;
    
    /// <summary>
    /// Stores the time at which the application started
    /// </summary>
    private DateTime StartUpTime = DateTime.Now;

    /// <summary>
    /// List of sensors that the server knows exist
    /// </summary>
    private Dictionary<string, Sensor> KnownSensors;

    /// <summary>
    /// The path to the key where Windows looks for startup applications
    /// </summary>
    RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);


    Mutex MutexRunOneInstance;

    public FormMain(ref Mutex MutexRunOneInstance)
    {
      this.MutexRunOneInstance = MutexRunOneInstance;

      InitializeComponent();
      // Check to see the current state (running at startup or not)
      if (rkApp.GetValue(Application.ProductName) == null)
      {
        // The value doesn't exist, the application is not set to run at startup
        this.mnuRunOnSysStartup.Checked = false;
      }
      else
      {
        // The value exists, the application is set to run at startup
        this.mnuRunOnSysStartup.Checked = true;
      }
    }

    private void mnuExit_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void mnuOptions_Click(object sender, EventArgs e)
    {
      bool topMost = this.TopMost;
      this.TopMost = false;
      frmOptions.ShowDialog();
      this.TopMost = topMost;
      GSMUpdateSettings();
    }

    private void mnuAbout_Click(object sender, EventArgs e)
    {
      bool topMost = this.TopMost;
      this.TopMost = false;
      FormAboutSENSIT abt = new FormAboutSENSIT();
      abt.ShowDialog();
      this.TopMost = topMost;
    }

    private void btnSelectAll_Click(object sender, EventArgs e)
    {
      int count = listKnownSensors.Items.Count;
      if (count == 0)
      {
        MessageBox.Show("No sensor in the list" + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
      }
      else
      {
        if (btnSelectAll.Text == "Select All")
        {
          for (int i = 0; i < count; i++)
          {
            listKnownSensors.SetSelected(i, true);
          }
          btnSelectAll.Text = "Deselect All";
        }
        else
        {
          listKnownSensors.ClearSelected();
          ResetGuiSensorInfo();
          btnSelectAll.Text = "Select All";
        }
      }
    }

    /// <summary>
    /// Sends the Wake Up message to all the selected sensors
    /// and expects to receive the DoneWakeUp message in return
    /// as acknowledgement
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnWakeUp_Click(object sender, EventArgs e)
    {
      int count = listKnownSensors.SelectedItems.Count;
      if (count == 0)
      {
        MessageBox.Show("No sensor selected" + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
      }
      else
      {
        ServerMsg msg = new ServerMsg();
        for (int i = 0; i < count; i++)
        {
          msg.RecipientId = (string)this.listKnownSensors.SelectedItems[i];
          msg.Tag = ServerMsg.CommandTag.WakeUp;
          msg.Length = 0;
          msg.Value = null;
          WritePacket(msg);
        }
      }
    }

    /// <summary>
    /// Sends the Sleep message to all the selected sensors
    /// and expects to receive the DoneSleep message in return
    /// as acknowledgement
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnSleep_Click(object sender, EventArgs e)
    {
      int count = listKnownSensors.SelectedItems.Count;
      if (count == 0)
      {
        MessageBox.Show("No sensor selected" + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
      }
      else
      {
        ServerMsg msg = new ServerMsg();
        for (int i = 0; i < count; i++)
        {
          msg.RecipientId = (string)this.listKnownSensors.SelectedItems[i];
          msg.Tag = ServerMsg.CommandTag.Sleep;
          msg.Length = 0;
          msg.Value = null;
          WritePacket(msg);
        }
      }
    }

    /// <summary>
    /// Sends the Set Ping Interval message to all the selected sensors
    /// and expects to receive the DoneSetPingInterval message in return
    /// as acknowledgement
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnSetPingInterval_Click(object sender, EventArgs e)
    {
      int count = listKnownSensors.SelectedItems.Count;
      if (count == 0)
      {
        MessageBox.Show("No sensor selected\n" + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
      }
      else
      {
        ServerMsg msg = new ServerMsg();

        decimal hh = numericHH.Value;
        decimal mm = numericMM.Value;
        decimal ss = numericSS.Value;

        float pingInterval = (float)((hh * 60 * 60) + (mm * 60) + ss);

        for (int i = 0; i < count; ++i)
        {
          msg.RecipientId = (string)this.listKnownSensors.SelectedItems[i];
          msg.Tag = ServerMsg.CommandTag.SetPingInterval;
          msg.Length = 4;
          msg.Value = Util.FloatToByteArr(pingInterval);
          WritePacket(msg);
        }
      }
    }

    /// <summary>
    /// Sends the Reset Reading message to all the selected sensors
    /// and expects to receive the DoneResetReaading message in return
    /// as acknowledgement
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnResetReading_Click(object sender, EventArgs e)
    {
      int count = listKnownSensors.SelectedItems.Count;
      if (count == 0)
      {
        MessageBox.Show("No sensor selected" + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
      }
      else
      {
        ServerMsg msg = new ServerMsg();
        for (int i = 0; i < count; ++i)
        {
          msg.RecipientId = (string)this.listKnownSensors.SelectedItems[i];
          msg.Tag = ServerMsg.CommandTag.ResetReading;
          msg.Length = 0;
          msg.Value = null;
          WritePacket(msg);
        }
      }
    }

    /// <summary>
    /// Saves the sensor information in the config file
    /// </summary>
    /// <param name="Id">Id of the sensor</param>
    private void SaveSensorInfo(string Id)
    {
      ConfigSetting sensorListNode = ConfigXML.Settings[Conf.SENSOR_LIST_NODE];
      string xmlSensorID = Id.Replace("+", "ID");

      if (KnownSensors.ContainsKey(Id))  // Sensor with given Id is to be remembered
      {
        // Updating the sensor with the given Id in the config file
        sensorListNode[xmlSensorID][Conf.SENSOR_ID_TAG].Value = Id;
        Sensor sensor = KnownSensors[Id];
        sensorListNode[xmlSensorID][Conf.SENSOR_DESC_TAG].Value = sensor.Description;
        sensorListNode[xmlSensorID][Conf.SENSOR_STATE_TAG].Value = sensor.State.ToString();
        sensorListNode[xmlSensorID][Conf.SENSOR_PING_INTERVAL_TAG].floatValue = sensor.PingInterval;
      }
      else // Sensor with given Id is to be removed
        sensorListNode[xmlSensorID].Remove();
      ConfigXML.Save(Conf.CONFIG_FILE_PATH);
    }

    private void RefreshGuiSensorInfo()
    {
      if (InvokeRequired)
        this.Invoke(new MethodInvoker(RefreshGuiSensorInfo));
      else
      {
        if (this.listKnownSensors.SelectedItems.Count == 1)
        {
          string id = this.listKnownSensors.Text;
          Sensor selectedSensor = KnownSensors[id];
          this.txtSensorId.Text = id;
          this.txtDesc.Text = selectedSensor.Description;
          this.txtSensorState.Text = selectedSensor.State.ToString();
          if (selectedSensor.LastReadingTime.Equals(string.Empty) == false)
          {
            string lastReading = String.Format("{0:0.0000}", selectedSensor.LastReading);
            this.txtSensorLastReading.Text = lastReading + " mm";
            this.txtSensorLastReadingTime.Text = selectedSensor.LastReadingTime;
          }
          else
          {
            this.txtSensorLastReading.Text = "N.A.";
            this.txtSensorLastReadingTime.Text = "N.A.";
          }
          SetTxtSensorPingIntervalText(selectedSensor.PingInterval);

          this.btnRefreshPingInterval.Enabled = true;
          this.btnRefreshSensorState.Enabled = true;
        }
      }
    }

    private void ResetGuiSensorInfo()
    {
      if (InvokeRequired)
        this.Invoke(new MethodInvoker(ResetGuiSensorInfo));
      else
      {
        this.txtSensorId.Text = "(Select a sensor)";
        this.txtDesc.Text = "(Select a sensor)";
        this.txtSensorState.Text = "(Select a sensor)";
        this.txtSensorLastReading.Text = "(Select a sensor)";
        this.txtSensorLastReadingTime.Text = "(Select a sensor)";
        this.txtSensorPingInterval.Text = "(Select a sensor)";
        this.btnRefreshPingInterval.Enabled = false;
        this.btnRefreshSensorState.Enabled = false;
      }
    }

    private void listKnownSensors_SelectedIndexChanged(object sender, EventArgs e)
    {
      RefreshGuiSensorInfo();
    }

    private void mnuAlwaysOnTop_Click(object sender, EventArgs e)
    {
      if (this.mnuAlwaysOnTop.Checked == false)
      {
        this.TopMost = true;
        this.mnuAlwaysOnTop.Checked = true;
      }
      else
      {
        this.TopMost = false;
        this.mnuAlwaysOnTop.Checked = false;
      }
    }

    private void mnuEnableAllButtons_Click(object sender, EventArgs e)
    {
      this.grpNetworkStatus.Enabled = true;
      this.grpSensorInfo.Enabled = true;
      SetEnableAllInGroupBox(this.grpNetworkStatus, true);
      SetEnableAllInGroupBox(this.grpSensorInfo, true);
    }

    private void SetEnableAllInGroupBox(GroupBox grpBox, bool enable)
    {
      grpBox.Enabled = enable;
      foreach (Control ctrl in grpBox.Controls)
      {
        if (ctrl is TextBox)
        {
          ((TextBox)ctrl).Enabled = enable;
        }
        else if (ctrl is Button)
        {
          ((Button)ctrl).Enabled = enable;
        }
        else if (ctrl is RadioButton)
        {
          ((RadioButton)ctrl).Enabled = enable;
        }
        else if (ctrl is CheckBox)
        {
          ((CheckBox)ctrl).Enabled = enable;
        }
        else if (ctrl is GroupBox)
        {
          SetEnableAllInGroupBox((GroupBox)ctrl, enable);
        }
      }
    }

    private void btnRefreshSensorState_Click(object sender, EventArgs e)
    {
      ServerMsg msg = new ServerMsg();
      msg.RecipientId = (string)this.listKnownSensors.SelectedItem;
      msg.Tag = ServerMsg.CommandTag.GetState;
      msg.Length = 0;
      msg.Value = null;
      WritePacket(msg);
    }

    private void btnRefreshPingInterval_Click(object sender, EventArgs e)
    {
      ServerMsg msg = new ServerMsg();
      msg.RecipientId = (string)this.listKnownSensors.SelectedItem;
      msg.Tag = ServerMsg.CommandTag.GetPingInterval;
      msg.Length = 0;
      msg.Value = null;
      WritePacket(msg);
    }

    // Is true when the application tries to restart itself, otherwise false
    bool AutomaticRestart = false;

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (AutomaticRestart == false)
      {
        DialogResult dr = MessageBox.Show("Really close SENSIT Server?",
                                          "SENSIT Server",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question,
                                          MessageBoxDefaultButton.Button2);
        if (dr == DialogResult.No)
          e.Cancel = true;
      }

      if (SPort != null)
      {
        if (SPort.IsOpen)
          SPort.Close();
        SPort.Dispose();
        Thread.Sleep(500);
      }
      

      if (ArduinoSPort != null)
      {
        if (ArduinoSPort.IsOpen)
          ArduinoSPort.Close();
        ArduinoSPort.Dispose();
        Thread.Sleep(500);
      }
    }

    private void btnDeleteSensors_Click(object sender, EventArgs e)
    {
      int count = listKnownSensors.SelectedItems.Count;
      if (count == 0)
      {
        MessageBox.Show("No sensor selected" + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
      }
      else
      {
        DialogResult dialogResult = MessageBox.Show("Really delete the sensor(s)?",
                                          "SENSIT Server",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question,
                                          MessageBoxDefaultButton.Button2);
        if (dialogResult == DialogResult.Yes)
        {
          while (this.listKnownSensors.SelectedItems.Count > 0)
          {
            string id = (string)this.listKnownSensors.SelectedItems[0];
            this.listKnownSensors.Items.Remove(id);
            KnownSensors.Remove(id);
            SaveSensorInfo(id);
            LoggerInfo("Deleted sensor " + id);
          }
          this.txtSensorId.Text = "(Select a sensor)";
          this.txtDesc.Text = "(Select a sensor)";
          this.txtSensorState.Text = "(Select a sensor)";
          this.txtSensorLastReading.Text = "(Select a sensor)";
          this.txtSensorLastReadingTime.Text = "(Select a sensor)";
          this.txtSensorPingInterval.Text = "(Select a sensor)";
          this.btnRefreshPingInterval.Enabled = false;
          this.btnRefreshSensorState.Enabled = false;
        }
      }
    }

    private void mnuRunOnSysStartup_Click(object sender, EventArgs e)
    {
      if (this.mnuRunOnSysStartup.Checked) // app should not start at system startup
      {
        this.mnuRunOnSysStartup.Checked = false;
        // Remove the value from the registry so that the application doesn't start
        rkApp.DeleteValue(Application.ProductName, false);
      }
      else // app should start at system startup
      {
        this.mnuRunOnSysStartup.Checked = true;
        // Add the value in the registry so that the application runs at startup
        rkApp.SetValue(Application.ProductName, Application.ExecutablePath);
      }
    }

    private void btnClearLog_Click(object sender, EventArgs e)
    {
      this.txtLog.Clear();
    }

    private void txtLog_TextChanged(object sender, EventArgs e)
    {
      /* When the size of the text exceeds 3/4th of its maximum size
       * then the initial 1/2 of the text is trimmed out
       */
      if (this.txtLog.Text.Length > this.txtLog.MaxLength * 3 / 4)
        this.txtLog.Text = this.txtLog.Text.Substring(this.txtLog.Text.Length / 2);
    }

    private void AttemptApplicationRestart(string reason)
    {
      AutomaticRestart = true;

      TimeTicker.Enabled = false;
      AppRestartTimer.Enabled = false;
      LoggerInfo("Locking SPort... " + Util.GetMethodNameAndLineNum()); 
      lock (SPortLock)
      {
        LoggerInfo("Locked SPort... " + Util.GetMethodNameAndLineNum());
        LoggerInfo("Locking ProcessSMS... " + Util.GetMethodNameAndLineNum()); 
        lock (ProcessSMSLock)
        {
          LoggerInfo("Locked ProcessSMS... " + Util.GetMethodNameAndLineNum()); 
          LoggerInfo("Emailing the application crash log files due to " + reason + "...");
          List<string> DirList = new List<string>()
          {
            frmOptions.RainfallLogDirPath,
            frmOptions.AppLogDirPath
          };

          try
          {
            Util.ArchiveLogs(Logging.LOG_ARCHIVE_FILE_NAME, DirList, frmOptions.WinrarDirPath + "\\Rar.exe");

            List<string> recipients = new List<string>();
            foreach (object obj in frmOptions.EmailRecipients)
              recipients.Add(obj.ToString());

            Util.SendEMail(recipients, Logging.LOG_ARCHIVE_FILE_NAME);
            recipients.Clear();

            File.SetAttributes(Logging.LOG_ARCHIVE_FILE_NAME, FileAttributes.Normal);
            File.Delete(Logging.LOG_ARCHIVE_FILE_NAME);
          }
          catch (Exception exc)
          {
            LoggerError("Error in archiving/emailing. Exception: " + exc.Message);
          }

          MutexRunOneInstance.Close();
          LoggerInfo("Restarting application due to " + reason);

          StackFrame[] stackFrames = (new StackTrace()).GetFrames();  // get method calls (frames)

          // write call stack method names
          LoggerInfo("Stack Trace just before the application restart:");
          foreach (StackFrame stackFrame in stackFrames)
          {
            LoggerInfo(stackFrame.GetMethod().Name + " Line: " + stackFrame.GetFileLineNumber().ToString());   // write method name
          }

          if (SPort != null)
          {
            if (SPort.IsOpen)
              SPort.Close();
            SPort.Dispose();
            Thread.Sleep(500);
          }

          if (ArduinoSPort != null)
          {
            if (ArduinoSPort.IsOpen)
              ArduinoSPort.Close();
            ArduinoSPort.Dispose();
            Thread.Sleep(500);
          }

          Application.Restart();
        }
        LoggerInfo("Released ProcessSMS... " + Util.GetMethodNameAndLineNum()); 
      }
      LoggerInfo("Released SPort... " + Util.GetMethodNameAndLineNum()); 
    }

    private void chkEnableLogging_CheckedChanged(object sender, EventArgs e)
    {
      this.txtLog.Enabled = this.radLogActivity.Enabled = this.radLogSerialPort.Enabled = this.chkEnableLogging.Checked;
    }

    private void FormMain_ResizeEnd(object sender, EventArgs e)
    {
      int displacement = this.Width - Convert.ToInt32(this.Tag);
      this.grpLog.Width += displacement;
      this.txtLog.Width += displacement;
      this.btnClearLog.Left += displacement;
      this.Tag = this.Width;
    }

    private void radLogActivity_CheckedChanged(object sender, EventArgs e)
    {
      this.txtLog.Clear();
    }

    private void radLogSerialPort_CheckedChanged(object sender, EventArgs e)
    {
      this.txtLog.Clear();
    }

    private void FormMain_Shown(object sender, EventArgs e)
    {
      ResetGuiSensorInfo();

      this.MinimumSize = this.Size;
      Screen[] s = Screen.AllScreens;

      int maxwidth = 0;
      foreach (Screen scr in s)
      {
        if (scr.Bounds.Width > maxwidth)
          maxwidth = scr.Bounds.Width;
      }
      this.MaximumSize = new Size(maxwidth, this.Size.Height);

      // The Tag of FormMain stores its instanteneous width to be used during resizing of form
      this.Tag = this.Width;
      // The Tag of grpLog stores its initial width to be used during resizing of form
      this.grpLog.Tag = this.grpLog.Width;
      // The Tag of txtLog stores its initial width to be used during resizing of form
      this.txtLog.Tag = this.txtLog.Width;
      // The Tag of btnClearLog stores its initial left position to be used during resizing of form
      this.btnClearLog.Tag = this.btnClearLog.Left;

      frmOptions = new FormOptions(ConfigXML, AdminUser);
      if (frmOptions.ConfigInitialized == false)
      {
        this.Close();
        return;
      }

      LoggerInit(frmOptions.AppLogDirPath + "/" + Logging.ACIVITY_FILE_NAME);
      SerialPortLogger.FilePath = frmOptions.AppLogDirPath + "/" + Logging.SERIAL_PORT_LOG_FILE_NAME;

      InitAndRegisterEvents();
      GSMUpdateSettings();
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
      #region Loading the configuration XML file

      try
      {
        ConfigXML = new Xmlconfig(Conf.CONFIG_FILE_PATH, false);
      }
      catch (Exception exc)
      {
        LoggerError("The " + Conf.CONFIG_FILE_PATH + " file is either corrupted or does not exist. It will be reset to original form.\n\n" + exc.Message);
#if DEBUG
        MessageBox.Show("The " + Conf.CONFIG_FILE_PATH + " file is either corrupted or does not exist. It will be reset to original form.\n\n" + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
#endif

        /* Copy contents from config.template.xml file.
         * This will be the case when the application is
         * started for the very first time after installation.
         */
        try
        {
          File.Copy(Conf.CONFIG_TEMPLATE_FILE_PATH, Conf.CONFIG_FILE_PATH, true);
        }
        catch (Exception exc1)
        {
          LoggerError("Could not open " + Conf.CONFIG_TEMPLATE_FILE_PATH + "\n" + exc1.Message);
#if DEBUG
          MessageBox.Show("Could not open " + Conf.CONFIG_TEMPLATE_FILE_PATH + "\n" + exc1.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
#endif
          this.Close();
        }
      }

      ConfigXML = new Xmlconfig(Conf.CONFIG_FILE_PATH, false);

      #region Loading the information about the known sensors
      {
        KnownSensors = new Dictionary<string, Sensor>();
        IList<ConfigSetting> sensorList = ConfigXML.Settings[Conf.SENSOR_LIST_NODE].Children();

        foreach (ConfigSetting sensorNode in sensorList)
        {
          string Id = sensorNode[Conf.SENSOR_ID_TAG].Value;
          string description = sensorNode[Conf.SENSOR_DESC_TAG].Value;
          string state = sensorNode[Conf.SENSOR_STATE_TAG].Value;
          float pingInterval = sensorNode[Conf.SENSOR_PING_INTERVAL_TAG].floatValue;

          KnownSensors.Add(Id, new Sensor(Id, state, description, pingInterval));
          // Updating the sensor info in the checked list box
          AddToListKnownSensors(Id);
        }
      }
      #endregion

      ConfigXML.Save(Conf.CONFIG_FILE_PATH);
      #endregion


      #region Ask user to login. Only when correctly logged in, do proceed to other tasks
      string passwordHash = ConfigXML.Settings[Conf.ADMIN_NODE][Conf.PASSWORD_HASH_TAG].Value;
      bool rememberPassword = ConfigXML.Settings[Conf.ADMIN_NODE][Conf.REMEMBER_PASSWORD_TAG].boolValue;

      if (passwordHash.Length == 0)
      {
        /* Case when the server is started for the 1st time after fresh installation
         * hence give default password
         */
        AdminUser = new User("sensit123", false);
      }
      else
      {
        // Server has already been started so load the saved password's hash 
        AdminUser = new User(passwordHash, true);
      }

      ConfigXML.Settings[Conf.ADMIN_NODE][Conf.PASSWORD_HASH_TAG].Value = AdminUser.PasswordHash;
      ConfigXML.Settings[Conf.ADMIN_NODE][Conf.REMEMBER_PASSWORD_TAG].boolValue = rememberPassword;

      if (rememberPassword == false || passwordHash.Length == 0)
      {
        FormLogin frmLogin = new FormLogin(AdminUser);
        frmLogin.ShowDialog();
        if (frmLogin.LoginSuccessful == false)
        {
          this.Close();
          return;
        }
      }
      #endregion
    }
  }
}