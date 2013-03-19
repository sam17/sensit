using System;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SENSITServer
{
  public partial class FormOptions : Form
  {
    private Xmlconfig configXML = null;

    private User AdminUser = null;

    /// <summary>
    /// boolean variable representing if the application configuration
    /// has been initialized or not.
    /// Typically it will be false when the application is run for the 
    /// first time after installation and true from then onwards.
    /// It is used to let the application know that before starting it
    /// for the 1st time, the user has to first set the configurations
    /// </summary>
    public bool ConfigInitialized { get; private set; }

    /// <summary>
    /// String representing the path to the directory in which
    /// the rainfall measurement log files will be saved
    /// </summary>
    public string RainfallLogDirPath { get; private set; }
    public string AppLogDirPath { get; private set; }
    public string WinrarDirPath { get; private set; }

    public string GSMCOMPort { get; private set; }
    public int GSMBaudRate { get; private set; }
    public StopBits GSMStopBits { get; private set; }
    public Parity GSMParity { get; private set; }

    public string ArduinoCOMPort { get; private set; }
    public int ArduinoBaudRate { get; private set; }
    public StopBits ArduinoStopBits { get; private set; }
    public Parity ArduinoParity { get; private set; }

    /// <summary>
    /// Number of times the GSM serial port read/write attempts have
    /// to fail before an application restart is attempted
    /// </summary>
    public int NumFailedReads { get; private set; }

    /// <summary>
    /// Number of minutes of inactivity (no message received from 
    /// any sensor) on the server side after which the server 
    /// should restart itself and try communicating again
    /// </summary>
    public int NumInactiveMins { get; private set; }

    public ListBox.ObjectCollection EmailRecipients
    {
      get
      {
        return this.lstEmailRecipients.Items;
      }
    }

    /// <summary>
    /// Constructor of FormOptions class
    /// </summary>
    /// <param name="ConfigXML">Represents the parsed config XML file</param>
    public FormOptions(Xmlconfig configXML, User AdminUser)
    {
      InitializeComponent();

      this.AdminUser = AdminUser;

      ConfigInitialized = false;
      RainfallLogDirPath = string.Empty;
      AppLogDirPath = string.Empty;
      WinrarDirPath = string.Empty;
      GSMCOMPort = string.Empty;
      GSMBaudRate = 0;
      GSMStopBits = StopBits.One;
      GSMParity = Parity.None;
      ArduinoCOMPort = string.Empty;
      ArduinoBaudRate = 0;
      ArduinoStopBits = StopBits.One;
      ArduinoParity = Parity.None;
      NumFailedReads = 0;
      NumInactiveMins = 0;
      this.configXML = configXML;

      #region Initializing the Option parameters

      LoadGUIFromConfig();
      SaveAllTabs(true);

      ConfigInitialized = configXML.Settings[Conf.CONFIG_INITIALIZED_TAG].boolValue;

      if (ConfigInitialized == false)
      {
        MessageBox.Show("It seems like the application has not been configured yet. Please configure it first" + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
        this.ShowDialog();

        if (ConfigInitialized)
        {
          configXML.Settings[Conf.CONFIG_INITIALIZED_TAG].boolValue = true;
          configXML.Save(Conf.CONFIG_FILE_PATH);
        }
        else
        {
          this.Close();
          this.Dispose();
        }
      }

      #endregion
    }

    private void LoadGUIFromConfig()
    {
      #region Processing Logging tab
      ConfigSetting logSettings = configXML.Settings[Conf.LOG_NODE];
      this.txtRainfallLogDir.Text = logSettings[Conf.RAINFALL_LOG_DIR_TAG].Value;
      this.txtAppLogDir.Text = logSettings[Conf.APP_LOG_DIR_TAG].Value;
      this.txtWinrarDir.Text = logSettings[Conf.WINRAR_DIR_TAG].Value;
      #endregion

      #region Processing Communication tab
      ConfigSetting gsmCommSettings = configXML.Settings[Conf.GSM_COMM_NODE];
      this.comboGSMCOMPort.Items.Clear();
      this.comboGSMCOMPort.Items.AddRange(SerialPort.GetPortNames());
      this.comboGSMCOMPort.Text = gsmCommSettings[Conf.GSM_COMPORT_TAG].Value;
      this.comboGSMBaudRate.Text = gsmCommSettings[Conf.GSM_BAUD_TAG].Value;
      this.comboGSMStopBits.Text = gsmCommSettings[Conf.GSM_STOPBITS_TAG].Value;
      this.comboGSMParity.Text = gsmCommSettings[Conf.GSM_PARITY_TAG].Value;

      ConfigSetting arduinoCommSettings = configXML.Settings[Conf.ARDUINO_COMM_NODE];
      this.comboArduinoCOMPort.Items.Clear();
      this.comboArduinoCOMPort.Items.AddRange(SerialPort.GetPortNames());
      this.comboArduinoCOMPort.Text = arduinoCommSettings[Conf.ARDUINO_COMPORT_TAG].Value;
      this.comboArduinoBaudRate.Text = arduinoCommSettings[Conf.ARDUINO_BAUD_TAG].Value;
      this.comboArduinoStopBits.Text = arduinoCommSettings[Conf.ARDUINO_STOPBITS_TAG].Value;
      this.comboArduinoParity.Text = arduinoCommSettings[Conf.ARDUINO_PARITY_TAG].Value;
      #endregion

      #region Processing Notification tab
      ConfigSetting notifSettings = configXML.Settings[Conf.EMAIL_RECIPIENTS_NODE];
      this.lstEmailRecipients.Items.Clear();
      foreach (ConfigSetting cs in notifSettings.GetNamedChildren(Conf.EMAIL_TAG))
      {
        this.lstEmailRecipients.Items.Add(cs.Value);
      }
      this.txtRecipientEmail.Clear();
      #endregion

      #region Processing Parameters tab
      ConfigSetting paramSetting = configXML.Settings[Conf.PARAMS_NODE];
      try
      {
        this.numericFailedReads.Value = paramSetting[Conf.NUM_FAILED_READS_TAG].intValue;
      }
      catch
      {
        this.numericFailedReads.Value = paramSetting[Conf.NUM_FAILED_READS_TAG].intValue = 5;
        configXML.Save(Conf.CONFIG_FILE_PATH);
      }

      try
      {
        this.numericInactiveMins.Value = paramSetting[Conf.NUM_INACTIVE_MINS_TAG].intValue;
      }
      catch
      {
        this.numericInactiveMins.Value = paramSetting[Conf.NUM_INACTIVE_MINS_TAG].intValue = 10;
        configXML.Save(Conf.CONFIG_FILE_PATH);
      }
      #endregion

      #region Processing Account tab
      this.txtCurrentPassword.Text = string.Empty;
      this.txtNewPassword.Text = string.Empty;
      this.txtConfirmPassword.Text = string.Empty;
      this.chkRememberPassword.Checked = configXML.Settings[Conf.ADMIN_NODE][Conf.REMEMBER_PASSWORD_TAG].boolValue;
      #endregion
    }

    private Result SaveLoggingTab(bool saveSilently)
    {
      #region Processing Rainfall log directory path value
      RainfallLogDirPath = this.txtRainfallLogDir.Text.Trim();
      if (System.IO.Directory.Exists(RainfallLogDirPath))
      {
        configXML.Settings[Conf.LOG_NODE][Conf.RAINFALL_LOG_DIR_TAG].Value = RainfallLogDirPath;
        configXML.Save(Conf.CONFIG_FILE_PATH);
      }
      else if (!saveSilently)
      {
        MessageBox.Show("Rainfall log directory specified does not exists." + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        this.tabPaneOptions.SelectedTab = this.tabLogging;
        this.btnBrowseRainfallLogDir.Focus();
        return Result.Failure;
      }
      #endregion

      #region Processing Application log directory path value
      AppLogDirPath = this.txtAppLogDir.Text.Trim();
      if (System.IO.Directory.Exists(AppLogDirPath))
      {
        configXML.Settings[Conf.LOG_NODE][Conf.APP_LOG_DIR_TAG].Value = AppLogDirPath;
        configXML.Save(Conf.CONFIG_FILE_PATH);
      }
      else if (!saveSilently)
      {
        MessageBox.Show("Application log directory specified does not exists." + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        this.tabPaneOptions.SelectedTab = this.tabLogging;
        this.btnBrowseAppLogDir.Focus();
        return Result.Failure;
      }
      #endregion

      #region Processing Winrar directory path value
      WinrarDirPath = this.txtWinrarDir.Text.Trim();
      if (System.IO.Directory.Exists(WinrarDirPath))
      {
        configXML.Settings[Conf.LOG_NODE][Conf.WINRAR_DIR_TAG].Value = WinrarDirPath;
        configXML.Save(Conf.CONFIG_FILE_PATH);
      }
      else if (!saveSilently)
      {
        MessageBox.Show("Winrar directory specified does not exists." + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        this.tabPaneOptions.SelectedTab = this.tabLogging;
        this.btnBrowseWinrarBinDir.Focus();
        return Result.Failure;
      }

      try
      {
        Process rar = new Process();
        rar.StartInfo.FileName = WinrarDirPath + "\\Rar.exe";
        rar.StartInfo.CreateNoWindow = true;
        rar.StartInfo.UseShellExecute = false;
        rar.Start();
        rar.WaitForExit();
      }
      catch (Exception exc)
      {
        if (!saveSilently)
        {
          MessageBox.Show("Rar.exe not found in " + WinrarDirPath + ".\n" + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabLogging;
          this.btnBrowseWinrarBinDir.Focus();
          return Result.Failure;
        }
      }
      #endregion

      return Result.Success;
    }

    private Result SaveCommunicationTab(bool saveSilently)
    {
      #region Reading GSM COM port settings
      try
      {
        GSMCOMPort = this.comboGSMCOMPort.SelectedItem.ToString().Trim().ToUpper(System.Globalization.CultureInfo.InvariantCulture);
      }
      catch
      {
        if (!saveSilently)
        {
          MessageBox.Show("Illegal value for GSM COM Port.\nSelect it from the list instead of typing it in the textbox." + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabCommunication;
          this.comboGSMCOMPort.Focus();
          return Result.Failure;
        }
      }

      try
      {
        GSMBaudRate = Convert.ToInt32(this.comboGSMBaudRate.SelectedItem.ToString().Trim());
      }
      catch
      {
        if (!saveSilently)
        {
          MessageBox.Show("Illegal value for GSM Baud Rate.\nSelect it from the list instead of typing it in the textbox." + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabCommunication;
          this.comboGSMBaudRate.Focus();
          return Result.Failure;
        }
      }

      try
      {
        switch (this.comboGSMStopBits.SelectedItem.ToString().Trim().ToLower(System.Globalization.CultureInfo.InvariantCulture))
        {
          case "none":
            GSMStopBits = StopBits.None;
            break;

          case "one":
            GSMStopBits = StopBits.One;
            break;

          case "two":
            GSMStopBits = StopBits.Two;
            break;

          default:
            throw new Exception();
        }
      }
      catch
      {
        if (!saveSilently)
        {
          MessageBox.Show("Illegal value for GSM Stop Bits.\nSelect it from the list instead of typing it in the textbox." + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabCommunication;
          this.comboGSMStopBits.Focus();
          return Result.Failure;
        }
      }

      try
      {
        switch (this.comboGSMParity.SelectedItem.ToString().Trim().ToLower(System.Globalization.CultureInfo.InvariantCulture))
        {
          case "none":
            GSMParity = Parity.None;
            break;

          case "odd":
            GSMParity = Parity.Odd;
            break;

          case "even":
            GSMParity = Parity.Even;
            break;

          default:
            throw new Exception();
        }
      }
      catch
      {
        if (!saveSilently)
        {
          MessageBox.Show("Illegal value for GSM Parity.\nSelect it from the list instead of typing it in the textbox." + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabCommunication;
          this.comboGSMParity.Focus();
          return Result.Failure;
        }
      }

      ConfigSetting gsmCommElem = configXML.Settings[Conf.GSM_COMM_NODE];
      gsmCommElem[Conf.GSM_COMPORT_TAG].Value = GSMCOMPort;
      gsmCommElem[Conf.GSM_BAUD_TAG].Value = GSMBaudRate.ToString();
      gsmCommElem[Conf.GSM_STOPBITS_TAG].Value = GSMStopBits.ToString();
      gsmCommElem[Conf.GSM_PARITY_TAG].Value = GSMParity.ToString();
      configXML.Save(Conf.CONFIG_FILE_PATH);
      #endregion


      #region Reading Arduino COM port settings
      try
      {
        ArduinoCOMPort = this.comboArduinoCOMPort.SelectedItem.ToString().Trim().ToUpper(System.Globalization.CultureInfo.InvariantCulture);
      }
      catch
      {
        if (!saveSilently)
        {
          MessageBox.Show("Illegal value for Arduino COM Port.\nSelect it from the list instead of typing it in the textbox." + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabCommunication;
          this.comboArduinoCOMPort.Focus();
          return Result.Failure;
        }
      }

      if (ArduinoCOMPort.Equals(GSMCOMPort))
      {
        if (!saveSilently)
        {
          MessageBox.Show("Both GSM and Arduino COM Ports cannot have the same name",
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabCommunication;
          this.comboArduinoCOMPort.Focus();
          return Result.Failure;
        }
      }

      try
      {
        ArduinoBaudRate = Convert.ToInt32(this.comboArduinoBaudRate.SelectedItem.ToString().Trim());
      }
      catch
      {
        if (!saveSilently)
        {
          MessageBox.Show("Illegal value for Arduino Baud Rate.\nSelect it from the list instead of typing it in the textbox." + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabCommunication;
          this.comboArduinoBaudRate.Focus();
          return Result.Failure;
        }
      }

      try
      {
        switch (this.comboArduinoStopBits.SelectedItem.ToString().Trim().ToLower(System.Globalization.CultureInfo.InvariantCulture))
        {
          case "none":
            ArduinoStopBits = StopBits.None;
            break;

          case "one":
            ArduinoStopBits = StopBits.One;
            break;

          case "two":
            ArduinoStopBits = StopBits.Two;
            break;

          default:
            throw new Exception();
        }
      }
      catch
      {
        if (!saveSilently)
        {
          MessageBox.Show("Illegal value for Arduino Stop Bits.\nSelect it from the list instead of typing it in the textbox." + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabCommunication;
          this.comboArduinoStopBits.Focus();
          return Result.Failure;
        }
      }

      try
      {
        switch (this.comboArduinoParity.SelectedItem.ToString().Trim().ToLower(System.Globalization.CultureInfo.InvariantCulture))
        {
          case "none":
            ArduinoParity = Parity.None;
            break;

          case "odd":
            ArduinoParity = Parity.Odd;
            break;

          case "even":
            ArduinoParity = Parity.Even;
            break;

          default:
            throw new Exception();
        }
      }
      catch
      {
        if (!saveSilently)
        {
          MessageBox.Show("Illegal value for Arduino Parity.\nSelect it from the list instead of typing it in the textbox." + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabCommunication;
          this.comboArduinoParity.Focus();
          return Result.Failure;
        }
      }

      ConfigSetting arduinoCommElem = configXML.Settings[Conf.ARDUINO_COMM_NODE];
      arduinoCommElem[Conf.ARDUINO_COMPORT_TAG].Value = ArduinoCOMPort;
      arduinoCommElem[Conf.ARDUINO_BAUD_TAG].Value = ArduinoBaudRate.ToString();
      arduinoCommElem[Conf.ARDUINO_STOPBITS_TAG].Value = ArduinoStopBits.ToString();
      arduinoCommElem[Conf.ARDUINO_PARITY_TAG].Value = ArduinoParity.ToString();
      configXML.Save(Conf.CONFIG_FILE_PATH);
      #endregion

      return Result.Success;
    }

    private Result SaveNotificationTab(bool saveSilently)
    {
      try
      {
        ConfigSetting notifSettings = configXML.Settings[Conf.EMAIL_RECIPIENTS_NODE];
        notifSettings.RemoveChildren();
        foreach (string email in this.lstEmailRecipients.Items)
          notifSettings[Conf.EMAIL_TAG + "##"].Value = email;
        configXML.Save(Conf.CONFIG_FILE_PATH);
      }
      catch (Exception exc)
      {
        if (!saveSilently)
        {
          MessageBox.Show("Could not save recipients' email ids.\n Exception: " + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabNotification;
          this.lstEmailRecipients.Focus();
          return Result.Failure;
        }
      }

      return Result.Success;
    }

    private Result SaveParametersTab(bool saveSilently)
    {
      try
      {
        ConfigSetting paramSetting = configXML.Settings[Conf.PARAMS_NODE];
        paramSetting[Conf.NUM_FAILED_READS_TAG].intValue = this.NumFailedReads = Convert.ToInt32(this.numericFailedReads.Value);
        paramSetting[Conf.NUM_INACTIVE_MINS_TAG].intValue = this.NumInactiveMins = Convert.ToInt32(this.numericInactiveMins.Value);
        configXML.Save(Conf.CONFIG_FILE_PATH);
      }
      catch (Exception exc)
      {
        if (!saveSilently)
        {
          MessageBox.Show("Could not save parameters.\n Exception: " + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabParameters;
          return Result.Failure;
        }
      }
      return Result.Success;
    }

    private Result SaveAccountTab(bool saveSilently)
    {
      try
      {
        configXML.Settings[Conf.ADMIN_NODE][Conf.REMEMBER_PASSWORD_TAG].boolValue = this.chkRememberPassword.Checked;
        configXML.Save(Conf.CONFIG_FILE_PATH);

        // Return back when there has been no attempt to change password
        if (this.txtCurrentPassword.Text.Trim().Length == 0 &&
            this.txtNewPassword.Text.Trim().Length == 0 &&
            this.txtConfirmPassword.Text.Trim().Length == 0)
        {
          return Result.Success;
        }

        if (AdminUser.Equals(this.txtCurrentPassword.Text.Trim()) == false)
        {
          MessageBox.Show("Current password does not match",
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabAccount;
          this.txtCurrentPassword.Focus();
          this.txtCurrentPassword.SelectAll();
          return Result.Failure;
        }

        if (this.txtNewPassword.Text.Trim().Length < 8)
        {
          MessageBox.Show("Password must be atleast 8 characters long",
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabAccount;
          this.txtNewPassword.Focus();
          this.txtNewPassword.SelectAll();
          return Result.Failure;
        }

        if (this.txtNewPassword.Text.Trim().Equals(this.txtConfirmPassword.Text.Trim()) == false)
        {
          MessageBox.Show("New password has not been confirmed",
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabAccount;
          this.txtConfirmPassword.Focus();
          this.txtConfirmPassword.SelectAll();
          return Result.Failure;
        }

        AdminUser.ChangePasword(this.txtNewPassword.Text.Trim());
        configXML.Settings[Conf.ADMIN_NODE][Conf.PASSWORD_HASH_TAG].Value = Util.GetSHA1Hash(this.txtNewPassword.Text.Trim());
        configXML.Save(Conf.CONFIG_FILE_PATH);
      }
      catch (Exception exc)
      {
        if (!saveSilently)
        {
          MessageBox.Show("Could not change password.\n Exception: " + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                          "SENSIT Server",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error);
          this.tabPaneOptions.SelectedTab = this.tabAccount;
          return Result.Failure;
        }
      }
      return Result.Success;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (SaveAllTabs(false) == Result.Success)
        this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private Result SaveAllTabs(bool saveSilently)
    {
      Result res = Result.Success;
      if (SaveLoggingTab(saveSilently) == Result.Failure)
        res = Result.Failure;
      if (SaveCommunicationTab(saveSilently) == Result.Failure)
        res = Result.Failure;
      if (SaveNotificationTab(saveSilently) == Result.Failure)
        res = Result.Failure;
      if (SaveParametersTab(saveSilently) == Result.Failure)
        res = Result.Failure;
      if (SaveAccountTab(saveSilently) == Result.Failure)
        res = Result.Failure;
      ConfigInitialized = true;
      return res;
    }

    private void btnBrowseLogDir_Click(object sender, EventArgs e)
    {
      this.folderBrowserDialog.Description = "Select rainfall log directory";
      this.folderBrowserDialog.ShowDialog();
      this.txtRainfallLogDir.Text = this.folderBrowserDialog.SelectedPath;
    }

    private void FormOptions_Load(object sender, EventArgs e)
    {
      LoadGUIFromConfig();
    }

    private void comboCOMPort_Click(object sender, EventArgs e)
    {
      string[] sp_list = SerialPort.GetPortNames();
      this.comboGSMCOMPort.Items.Clear();
      this.comboGSMCOMPort.Items.AddRange(sp_list);
    }

    private void btnBrowseAppLogDir_Click(object sender, EventArgs e)
    {
      this.folderBrowserDialog.Description = "Select application log directory";
      this.folderBrowserDialog.ShowDialog();
      this.txtAppLogDir.Text = this.folderBrowserDialog.SelectedPath;
    }

    private void comboArduinoCOMPort_Click(object sender, EventArgs e)
    {
      string[] sp_list = SerialPort.GetPortNames();
      this.comboArduinoCOMPort.Items.Clear();
      this.comboArduinoCOMPort.Items.AddRange(sp_list);
    }

    private void btnBrowseWinrarBinDir_Click(object sender, EventArgs e)
    {
      this.folderBrowserDialog.Description = "Select directory containing Rar.exe";
      this.folderBrowserDialog.ShowDialog();
      this.txtWinrarDir.Text = this.folderBrowserDialog.SelectedPath;
    }

    private void btnAddRecipient_Click(object sender, EventArgs e)
    {
      string email = this.txtRecipientEmail.Text.Trim();
      if (email.Length == 0)
      {
        MessageBox.Show("No EmailAddress entered",
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        this.txtRecipientEmail.Focus();
      }
      else if (lstEmailRecipients.Items.Contains(email))
      {
        MessageBox.Show("Email already exists",
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Asterisk);
      }
      else if (Util.IsValidEmail(email))
        lstEmailRecipients.Items.Add(email);
      else
      {
        MessageBox.Show(email + " does not look like a valid email address",
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        this.txtRecipientEmail.Focus();
        this.txtRecipientEmail.SelectAll();
      }
    }

    private void btnRemoveRecipient_Click(object sender, EventArgs e)
    {
      try
      {
        this.lstEmailRecipients.Items.RemoveAt(this.lstEmailRecipients.SelectedIndex);
      }
      catch
      {
        MessageBox.Show("No email address selected!",
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
      }
    }

    private void txtNewPassword_TextChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void numericFailedReads_ValueChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void numericInactiveMins_ValueChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void comboGSMCOMPort_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void comboArduinoCOMPort_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void comboGSMBaudRate_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void comboArduinoBaudRate_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void comboGSMStopBits_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void comboArduinoStopBits_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void comboArduinoParity_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void comboGSMParity_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void FormOptions_Shown(object sender, EventArgs e)
    {
      this.btnApply.Enabled = false;
    }

    private void txtRainfallLogDir_TextChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void txtAppLogDir_TextChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void txtWinrarDir_TextChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void lstEmailRecipients_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      if (this.tabPaneOptions.SelectedTab == this.tabLogging)
        SaveLoggingTab(false);
      else if (this.tabPaneOptions.SelectedTab == this.tabCommunication)
        SaveCommunicationTab(false);
      else if (this.tabPaneOptions.SelectedTab == this.tabNotification)
        SaveNotificationTab(false);
      else if (this.tabPaneOptions.SelectedTab == this.tabParameters)
        SaveParametersTab(false);
      else if (this.tabPaneOptions.SelectedTab == this.tabNotification)
        SaveNotificationTab(false);
      else if (this.tabPaneOptions.SelectedTab == this.tabAccount)
        SaveAccountTab(false);
    }

    private void chkRememberPassword_CheckedChanged(object sender, EventArgs e)
    {
      this.btnApply.Enabled = true;
    }
  }
}