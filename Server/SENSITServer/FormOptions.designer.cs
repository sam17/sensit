namespace SENSITServer
{
  /// <summary>
  /// Form class for the Options dialog box
  /// </summary>
  partial class FormOptions
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.colorDialog = new System.Windows.Forms.ColorDialog();
      this.tabCommunication = new System.Windows.Forms.TabPage();
      this.label16 = new System.Windows.Forms.Label();
      this.label15 = new System.Windows.Forms.Label();
      this.comboArduinoBaudRate = new System.Windows.Forms.ComboBox();
      this.comboArduinoStopBits = new System.Windows.Forms.ComboBox();
      this.comboArduinoParity = new System.Windows.Forms.ComboBox();
      this.label11 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.label13 = new System.Windows.Forms.Label();
      this.comboArduinoCOMPort = new System.Windows.Forms.ComboBox();
      this.label14 = new System.Windows.Forms.Label();
      this.comboGSMBaudRate = new System.Windows.Forms.ComboBox();
      this.comboGSMStopBits = new System.Windows.Forms.ComboBox();
      this.comboGSMParity = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.comboGSMCOMPort = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tabLogging = new System.Windows.Forms.TabPage();
      this.btnBrowseWinrarBinDir = new System.Windows.Forms.Button();
      this.txtWinrarDir = new System.Windows.Forms.TextBox();
      this.label17 = new System.Windows.Forms.Label();
      this.btnBrowseAppLogDir = new System.Windows.Forms.Button();
      this.txtAppLogDir = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.btnBrowseRainfallLogDir = new System.Windows.Forms.Button();
      this.txtRainfallLogDir = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tabPaneOptions = new System.Windows.Forms.TabControl();
      this.tabNotification = new System.Windows.Forms.TabPage();
      this.btnRemoveRecipient = new System.Windows.Forms.Button();
      this.btnAddRecipient = new System.Windows.Forms.Button();
      this.lstEmailRecipients = new System.Windows.Forms.ListBox();
      this.txtRecipientEmail = new System.Windows.Forms.TextBox();
      this.label18 = new System.Windows.Forms.Label();
      this.tabParameters = new System.Windows.Forms.TabPage();
      this.label20 = new System.Windows.Forms.Label();
      this.numericInactiveMins = new System.Windows.Forms.NumericUpDown();
      this.label19 = new System.Windows.Forms.Label();
      this.numericFailedReads = new System.Windows.Forms.NumericUpDown();
      this.tabAccount = new System.Windows.Forms.TabPage();
      this.chkRememberPassword = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtConfirmPassword = new System.Windows.Forms.TextBox();
      this.txtNewPassword = new System.Windows.Forms.TextBox();
      this.txtCurrentPassword = new System.Windows.Forms.TextBox();
      this.label23 = new System.Windows.Forms.Label();
      this.label22 = new System.Windows.Forms.Label();
      this.label21 = new System.Windows.Forms.Label();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.comboBox2 = new System.Windows.Forms.ComboBox();
      this.comboBox3 = new System.Windows.Forms.ComboBox();
      this.comboBox4 = new System.Windows.Forms.ComboBox();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.btnApply = new System.Windows.Forms.Button();
      this.tabCommunication.SuspendLayout();
      this.tabLogging.SuspendLayout();
      this.tabPaneOptions.SuspendLayout();
      this.tabNotification.SuspendLayout();
      this.tabParameters.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericInactiveMins)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericFailedReads)).BeginInit();
      this.tabAccount.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(316, 202);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 5;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(397, 202);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 6;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // folderBrowserDialog
      // 
      this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
      // 
      // tabCommunication
      // 
      this.tabCommunication.Controls.Add(this.label16);
      this.tabCommunication.Controls.Add(this.label15);
      this.tabCommunication.Controls.Add(this.comboArduinoBaudRate);
      this.tabCommunication.Controls.Add(this.comboArduinoStopBits);
      this.tabCommunication.Controls.Add(this.comboArduinoParity);
      this.tabCommunication.Controls.Add(this.label11);
      this.tabCommunication.Controls.Add(this.label12);
      this.tabCommunication.Controls.Add(this.label13);
      this.tabCommunication.Controls.Add(this.comboArduinoCOMPort);
      this.tabCommunication.Controls.Add(this.label14);
      this.tabCommunication.Controls.Add(this.comboGSMBaudRate);
      this.tabCommunication.Controls.Add(this.comboGSMStopBits);
      this.tabCommunication.Controls.Add(this.comboGSMParity);
      this.tabCommunication.Controls.Add(this.label5);
      this.tabCommunication.Controls.Add(this.label4);
      this.tabCommunication.Controls.Add(this.label3);
      this.tabCommunication.Controls.Add(this.comboGSMCOMPort);
      this.tabCommunication.Controls.Add(this.label2);
      this.tabCommunication.Location = new System.Drawing.Point(4, 22);
      this.tabCommunication.Name = "tabCommunication";
      this.tabCommunication.Padding = new System.Windows.Forms.Padding(3);
      this.tabCommunication.Size = new System.Drawing.Size(533, 158);
      this.tabCommunication.TabIndex = 1;
      this.tabCommunication.Text = "Communication";
      this.tabCommunication.UseVisualStyleBackColor = true;
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Location = new System.Drawing.Point(6, 81);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(97, 13);
      this.label16.TabIndex = 17;
      this.label16.Text = "Arduino Serial Port:";
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(6, 23);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(123, 13);
      this.label15.TabIndex = 16;
      this.label15.Text = "GSM Module Serial Port:";
      // 
      // comboArduinoBaudRate
      // 
      this.comboArduinoBaudRate.FormattingEnabled = true;
      this.comboArduinoBaudRate.Items.AddRange(new object[] {
            "9600",
            "115200"});
      this.comboArduinoBaudRate.Location = new System.Drawing.Point(209, 97);
      this.comboArduinoBaudRate.Name = "comboArduinoBaudRate";
      this.comboArduinoBaudRate.Size = new System.Drawing.Size(70, 21);
      this.comboArduinoBaudRate.TabIndex = 6;
      this.comboArduinoBaudRate.Text = "9600";
      this.toolTip.SetToolTip(this.comboArduinoBaudRate, "Baud Rate is currently fixed at 2400bps");
      this.comboArduinoBaudRate.SelectedIndexChanged += new System.EventHandler(this.comboArduinoBaudRate_SelectedIndexChanged);
      // 
      // comboArduinoStopBits
      // 
      this.comboArduinoStopBits.Enabled = false;
      this.comboArduinoStopBits.FormattingEnabled = true;
      this.comboArduinoStopBits.Items.AddRange(new object[] {
            "None",
            "One",
            "Two"});
      this.comboArduinoStopBits.Location = new System.Drawing.Point(343, 97);
      this.comboArduinoStopBits.Name = "comboArduinoStopBits";
      this.comboArduinoStopBits.Size = new System.Drawing.Size(71, 21);
      this.comboArduinoStopBits.TabIndex = 7;
      this.comboArduinoStopBits.Text = "One";
      this.toolTip.SetToolTip(this.comboArduinoStopBits, "Not configurable");
      this.comboArduinoStopBits.SelectedIndexChanged += new System.EventHandler(this.comboArduinoStopBits_SelectedIndexChanged);
      // 
      // comboArduinoParity
      // 
      this.comboArduinoParity.Enabled = false;
      this.comboArduinoParity.FormattingEnabled = true;
      this.comboArduinoParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
      this.comboArduinoParity.Location = new System.Drawing.Point(462, 97);
      this.comboArduinoParity.Name = "comboArduinoParity";
      this.comboArduinoParity.Size = new System.Drawing.Size(51, 21);
      this.comboArduinoParity.TabIndex = 8;
      this.comboArduinoParity.Text = "None";
      this.toolTip.SetToolTip(this.comboArduinoParity, "Not configurable");
      this.comboArduinoParity.SelectedIndexChanged += new System.EventHandler(this.comboArduinoParity_SelectedIndexChanged);
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(285, 100);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(52, 13);
      this.label11.TabIndex = 12;
      this.label11.Text = "Stop Bits:";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(420, 100);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(36, 13);
      this.label12.TabIndex = 11;
      this.label12.Text = "Parity:";
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(142, 100);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(61, 13);
      this.label13.TabIndex = 10;
      this.label13.Text = "Baud Rate:";
      // 
      // comboArduinoCOMPort
      // 
      this.comboArduinoCOMPort.FormattingEnabled = true;
      this.comboArduinoCOMPort.Location = new System.Drawing.Point(68, 97);
      this.comboArduinoCOMPort.Name = "comboArduinoCOMPort";
      this.comboArduinoCOMPort.Size = new System.Drawing.Size(68, 21);
      this.comboArduinoCOMPort.Sorted = true;
      this.comboArduinoCOMPort.TabIndex = 5;
      this.toolTip.SetToolTip(this.comboArduinoCOMPort, "Name of the Serial Port that connects to the communication module on this server");
      this.comboArduinoCOMPort.SelectedIndexChanged += new System.EventHandler(this.comboArduinoCOMPort_SelectedIndexChanged);
      this.comboArduinoCOMPort.Click += new System.EventHandler(this.comboArduinoCOMPort_Click);
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(6, 100);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(56, 13);
      this.label14.TabIndex = 8;
      this.label14.Text = "COM Port:";
      // 
      // comboGSMBaudRate
      // 
      this.comboGSMBaudRate.FormattingEnabled = true;
      this.comboGSMBaudRate.Items.AddRange(new object[] {
            "9600",
            "115200"});
      this.comboGSMBaudRate.Location = new System.Drawing.Point(209, 39);
      this.comboGSMBaudRate.Name = "comboGSMBaudRate";
      this.comboGSMBaudRate.Size = new System.Drawing.Size(70, 21);
      this.comboGSMBaudRate.TabIndex = 2;
      this.comboGSMBaudRate.Text = "115200";
      this.toolTip.SetToolTip(this.comboGSMBaudRate, "Baud Rate is currently fixed at 2400bps");
      this.comboGSMBaudRate.SelectedIndexChanged += new System.EventHandler(this.comboGSMBaudRate_SelectedIndexChanged);
      // 
      // comboGSMStopBits
      // 
      this.comboGSMStopBits.Enabled = false;
      this.comboGSMStopBits.FormattingEnabled = true;
      this.comboGSMStopBits.Items.AddRange(new object[] {
            "None",
            "One",
            "Two"});
      this.comboGSMStopBits.Location = new System.Drawing.Point(343, 39);
      this.comboGSMStopBits.Name = "comboGSMStopBits";
      this.comboGSMStopBits.Size = new System.Drawing.Size(71, 21);
      this.comboGSMStopBits.TabIndex = 3;
      this.comboGSMStopBits.Text = "One";
      this.toolTip.SetToolTip(this.comboGSMStopBits, "Not configurable");
      this.comboGSMStopBits.SelectedIndexChanged += new System.EventHandler(this.comboGSMStopBits_SelectedIndexChanged);
      // 
      // comboGSMParity
      // 
      this.comboGSMParity.Enabled = false;
      this.comboGSMParity.FormattingEnabled = true;
      this.comboGSMParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
      this.comboGSMParity.Location = new System.Drawing.Point(462, 39);
      this.comboGSMParity.Name = "comboGSMParity";
      this.comboGSMParity.Size = new System.Drawing.Size(51, 21);
      this.comboGSMParity.TabIndex = 4;
      this.comboGSMParity.Text = "None";
      this.toolTip.SetToolTip(this.comboGSMParity, "Not configurable");
      this.comboGSMParity.SelectedIndexChanged += new System.EventHandler(this.comboGSMParity_SelectedIndexChanged);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(285, 42);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(52, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Stop Bits:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(420, 42);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(36, 13);
      this.label4.TabIndex = 3;
      this.label4.Text = "Parity:";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(142, 42);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(61, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Baud Rate:";
      // 
      // comboGSMCOMPort
      // 
      this.comboGSMCOMPort.FormattingEnabled = true;
      this.comboGSMCOMPort.Location = new System.Drawing.Point(68, 39);
      this.comboGSMCOMPort.Name = "comboGSMCOMPort";
      this.comboGSMCOMPort.Size = new System.Drawing.Size(68, 21);
      this.comboGSMCOMPort.Sorted = true;
      this.comboGSMCOMPort.TabIndex = 1;
      this.toolTip.SetToolTip(this.comboGSMCOMPort, "Name of the Serial Port that connects to the communication module on this server");
      this.comboGSMCOMPort.SelectedIndexChanged += new System.EventHandler(this.comboGSMCOMPort_SelectedIndexChanged);
      this.comboGSMCOMPort.Click += new System.EventHandler(this.comboCOMPort_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 42);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(56, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "COM Port:";
      // 
      // tabLogging
      // 
      this.tabLogging.Controls.Add(this.btnBrowseWinrarBinDir);
      this.tabLogging.Controls.Add(this.txtWinrarDir);
      this.tabLogging.Controls.Add(this.label17);
      this.tabLogging.Controls.Add(this.btnBrowseAppLogDir);
      this.tabLogging.Controls.Add(this.txtAppLogDir);
      this.tabLogging.Controls.Add(this.label6);
      this.tabLogging.Controls.Add(this.btnBrowseRainfallLogDir);
      this.tabLogging.Controls.Add(this.txtRainfallLogDir);
      this.tabLogging.Controls.Add(this.label1);
      this.tabLogging.Location = new System.Drawing.Point(4, 22);
      this.tabLogging.Name = "tabLogging";
      this.tabLogging.Padding = new System.Windows.Forms.Padding(3);
      this.tabLogging.Size = new System.Drawing.Size(533, 158);
      this.tabLogging.TabIndex = 0;
      this.tabLogging.Text = "Logging";
      this.tabLogging.UseVisualStyleBackColor = true;
      // 
      // btnBrowseWinrarBinDir
      // 
      this.btnBrowseWinrarBinDir.Location = new System.Drawing.Point(499, 61);
      this.btnBrowseWinrarBinDir.Name = "btnBrowseWinrarBinDir";
      this.btnBrowseWinrarBinDir.Size = new System.Drawing.Size(24, 23);
      this.btnBrowseWinrarBinDir.TabIndex = 6;
      this.btnBrowseWinrarBinDir.Text = "...";
      this.toolTip.SetToolTip(this.btnBrowseWinrarBinDir, "Browse");
      this.btnBrowseWinrarBinDir.UseVisualStyleBackColor = true;
      this.btnBrowseWinrarBinDir.Click += new System.EventHandler(this.btnBrowseWinrarBinDir_Click);
      // 
      // txtWinrarDir
      // 
      this.txtWinrarDir.Location = new System.Drawing.Point(140, 63);
      this.txtWinrarDir.Name = "txtWinrarDir";
      this.txtWinrarDir.Size = new System.Drawing.Size(353, 20);
      this.txtWinrarDir.TabIndex = 5;
      this.toolTip.SetToolTip(this.txtWinrarDir, "Path to Winrar installation folder containing Rar.exe file");
      this.txtWinrarDir.TextChanged += new System.EventHandler(this.txtWinrarDir_TextChanged);
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Location = new System.Drawing.Point(6, 66);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(86, 13);
      this.label17.TabIndex = 6;
      this.label17.Text = "Winrar Directory:";
      // 
      // btnBrowseAppLogDir
      // 
      this.btnBrowseAppLogDir.Location = new System.Drawing.Point(499, 35);
      this.btnBrowseAppLogDir.Name = "btnBrowseAppLogDir";
      this.btnBrowseAppLogDir.Size = new System.Drawing.Size(24, 23);
      this.btnBrowseAppLogDir.TabIndex = 4;
      this.btnBrowseAppLogDir.Text = "...";
      this.toolTip.SetToolTip(this.btnBrowseAppLogDir, "Browse");
      this.btnBrowseAppLogDir.UseVisualStyleBackColor = true;
      this.btnBrowseAppLogDir.Click += new System.EventHandler(this.btnBrowseAppLogDir_Click);
      // 
      // txtAppLogDir
      // 
      this.txtAppLogDir.Location = new System.Drawing.Point(140, 37);
      this.txtAppLogDir.Name = "txtAppLogDir";
      this.txtAppLogDir.Size = new System.Drawing.Size(353, 20);
      this.txtAppLogDir.TabIndex = 3;
      this.toolTip.SetToolTip(this.txtAppLogDir, "Path to the folder where all the application log files will be saved");
      this.txtAppLogDir.TextChanged += new System.EventHandler(this.txtAppLogDir_TextChanged);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 40);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(128, 13);
      this.label6.TabIndex = 3;
      this.label6.Text = "Application Log Directory:";
      // 
      // btnBrowseRainfallLogDir
      // 
      this.btnBrowseRainfallLogDir.Location = new System.Drawing.Point(499, 9);
      this.btnBrowseRainfallLogDir.Name = "btnBrowseRainfallLogDir";
      this.btnBrowseRainfallLogDir.Size = new System.Drawing.Size(24, 23);
      this.btnBrowseRainfallLogDir.TabIndex = 2;
      this.btnBrowseRainfallLogDir.Text = "...";
      this.toolTip.SetToolTip(this.btnBrowseRainfallLogDir, "Browse");
      this.btnBrowseRainfallLogDir.UseVisualStyleBackColor = true;
      this.btnBrowseRainfallLogDir.Click += new System.EventHandler(this.btnBrowseLogDir_Click);
      // 
      // txtRainfallLogDir
      // 
      this.txtRainfallLogDir.Location = new System.Drawing.Point(140, 11);
      this.txtRainfallLogDir.Name = "txtRainfallLogDir";
      this.txtRainfallLogDir.Size = new System.Drawing.Size(353, 20);
      this.txtRainfallLogDir.TabIndex = 1;
      this.toolTip.SetToolTip(this.txtRainfallLogDir, "Path to the folder where all the sensor log files will be saved");
      this.txtRainfallLogDir.TextChanged += new System.EventHandler(this.txtRainfallLogDir_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 14);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(111, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Rainfall Log Directory:";
      // 
      // tabPaneOptions
      // 
      this.tabPaneOptions.Controls.Add(this.tabLogging);
      this.tabPaneOptions.Controls.Add(this.tabCommunication);
      this.tabPaneOptions.Controls.Add(this.tabNotification);
      this.tabPaneOptions.Controls.Add(this.tabParameters);
      this.tabPaneOptions.Controls.Add(this.tabAccount);
      this.tabPaneOptions.Location = new System.Drawing.Point(12, 12);
      this.tabPaneOptions.Name = "tabPaneOptions";
      this.tabPaneOptions.SelectedIndex = 0;
      this.tabPaneOptions.Size = new System.Drawing.Size(541, 184);
      this.tabPaneOptions.TabIndex = 0;
      // 
      // tabNotification
      // 
      this.tabNotification.Controls.Add(this.btnRemoveRecipient);
      this.tabNotification.Controls.Add(this.btnAddRecipient);
      this.tabNotification.Controls.Add(this.lstEmailRecipients);
      this.tabNotification.Controls.Add(this.txtRecipientEmail);
      this.tabNotification.Controls.Add(this.label18);
      this.tabNotification.Location = new System.Drawing.Point(4, 22);
      this.tabNotification.Name = "tabNotification";
      this.tabNotification.Padding = new System.Windows.Forms.Padding(3);
      this.tabNotification.Size = new System.Drawing.Size(533, 158);
      this.tabNotification.TabIndex = 2;
      this.tabNotification.Text = "Notification";
      this.tabNotification.UseVisualStyleBackColor = true;
      // 
      // btnRemoveRecipient
      // 
      this.btnRemoveRecipient.Location = new System.Drawing.Point(170, 57);
      this.btnRemoveRecipient.Name = "btnRemoveRecipient";
      this.btnRemoveRecipient.Size = new System.Drawing.Size(108, 23);
      this.btnRemoveRecipient.TabIndex = 3;
      this.btnRemoveRecipient.Text = "Remove Recipient";
      this.btnRemoveRecipient.UseVisualStyleBackColor = true;
      this.btnRemoveRecipient.Click += new System.EventHandler(this.btnRemoveRecipient_Click);
      // 
      // btnAddRecipient
      // 
      this.btnAddRecipient.Location = new System.Drawing.Point(170, 28);
      this.btnAddRecipient.Name = "btnAddRecipient";
      this.btnAddRecipient.Size = new System.Drawing.Size(108, 23);
      this.btnAddRecipient.TabIndex = 2;
      this.btnAddRecipient.Text = "Add Recipient";
      this.btnAddRecipient.UseVisualStyleBackColor = true;
      this.btnAddRecipient.Click += new System.EventHandler(this.btnAddRecipient_Click);
      // 
      // lstEmailRecipients
      // 
      this.lstEmailRecipients.FormattingEnabled = true;
      this.lstEmailRecipients.Location = new System.Drawing.Point(284, 14);
      this.lstEmailRecipients.Name = "lstEmailRecipients";
      this.lstEmailRecipients.Size = new System.Drawing.Size(243, 134);
      this.lstEmailRecipients.TabIndex = 4;
      this.lstEmailRecipients.SelectedIndexChanged += new System.EventHandler(this.lstEmailRecipients_SelectedIndexChanged);
      // 
      // txtRecipientEmail
      // 
      this.txtRecipientEmail.Location = new System.Drawing.Point(9, 30);
      this.txtRecipientEmail.Name = "txtRecipientEmail";
      this.txtRecipientEmail.Size = new System.Drawing.Size(155, 20);
      this.txtRecipientEmail.TabIndex = 1;
      // 
      // label18
      // 
      this.label18.AutoSize = true;
      this.label18.Location = new System.Drawing.Point(6, 14);
      this.label18.Name = "label18";
      this.label18.Size = new System.Drawing.Size(197, 13);
      this.label18.TabIndex = 0;
      this.label18.Text = "Enter EmailAddress of error log recipient:";
      // 
      // tabParameters
      // 
      this.tabParameters.Controls.Add(this.label20);
      this.tabParameters.Controls.Add(this.numericInactiveMins);
      this.tabParameters.Controls.Add(this.label19);
      this.tabParameters.Controls.Add(this.numericFailedReads);
      this.tabParameters.Location = new System.Drawing.Point(4, 22);
      this.tabParameters.Name = "tabParameters";
      this.tabParameters.Padding = new System.Windows.Forms.Padding(3);
      this.tabParameters.Size = new System.Drawing.Size(533, 158);
      this.tabParameters.TabIndex = 3;
      this.tabParameters.Text = "Parameters";
      this.tabParameters.UseVisualStyleBackColor = true;
      // 
      // label20
      // 
      this.label20.AutoSize = true;
      this.label20.Location = new System.Drawing.Point(6, 34);
      this.label20.Name = "label20";
      this.label20.Size = new System.Drawing.Size(188, 13);
      this.label20.TabIndex = 3;
      this.label20.Text = "Number of inactive minutes to tolerate:";
      // 
      // numericInactiveMins
      // 
      this.numericInactiveMins.Location = new System.Drawing.Point(226, 32);
      this.numericInactiveMins.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
      this.numericInactiveMins.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericInactiveMins.Name = "numericInactiveMins";
      this.numericInactiveMins.Size = new System.Drawing.Size(77, 20);
      this.numericInactiveMins.TabIndex = 2;
      this.numericInactiveMins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericInactiveMins.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.numericInactiveMins.ValueChanged += new System.EventHandler(this.numericInactiveMins_ValueChanged);
      // 
      // label19
      // 
      this.label19.AutoSize = true;
      this.label19.Location = new System.Drawing.Point(6, 8);
      this.label19.Name = "label19";
      this.label19.Size = new System.Drawing.Size(214, 13);
      this.label19.TabIndex = 1;
      this.label19.Text = "Number of failed serial port reads to tolerate:";
      // 
      // numericFailedReads
      // 
      this.numericFailedReads.Location = new System.Drawing.Point(226, 6);
      this.numericFailedReads.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
      this.numericFailedReads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numericFailedReads.Name = "numericFailedReads";
      this.numericFailedReads.Size = new System.Drawing.Size(77, 20);
      this.numericFailedReads.TabIndex = 1;
      this.numericFailedReads.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.numericFailedReads.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      this.numericFailedReads.ValueChanged += new System.EventHandler(this.numericFailedReads_ValueChanged);
      // 
      // tabAccount
      // 
      this.tabAccount.Controls.Add(this.chkRememberPassword);
      this.tabAccount.Controls.Add(this.groupBox1);
      this.tabAccount.Location = new System.Drawing.Point(4, 22);
      this.tabAccount.Name = "tabAccount";
      this.tabAccount.Padding = new System.Windows.Forms.Padding(3);
      this.tabAccount.Size = new System.Drawing.Size(533, 158);
      this.tabAccount.TabIndex = 4;
      this.tabAccount.Text = "Account";
      this.tabAccount.UseVisualStyleBackColor = true;
      // 
      // chkRememberPassword
      // 
      this.chkRememberPassword.AutoSize = true;
      this.chkRememberPassword.Location = new System.Drawing.Point(6, 123);
      this.chkRememberPassword.Name = "chkRememberPassword";
      this.chkRememberPassword.Size = new System.Drawing.Size(126, 17);
      this.chkRememberPassword.TabIndex = 4;
      this.chkRememberPassword.Text = "Remember Password";
      this.chkRememberPassword.UseVisualStyleBackColor = true;
      this.chkRememberPassword.CheckedChanged += new System.EventHandler(this.chkRememberPassword_CheckedChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtConfirmPassword);
      this.groupBox1.Controls.Add(this.txtNewPassword);
      this.groupBox1.Controls.Add(this.txtCurrentPassword);
      this.groupBox1.Controls.Add(this.label23);
      this.groupBox1.Controls.Add(this.label22);
      this.groupBox1.Controls.Add(this.label21);
      this.groupBox1.Location = new System.Drawing.Point(6, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(296, 102);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Change Password";
      // 
      // txtConfirmPassword
      // 
      this.txtConfirmPassword.Location = new System.Drawing.Point(115, 73);
      this.txtConfirmPassword.Name = "txtConfirmPassword";
      this.txtConfirmPassword.PasswordChar = '*';
      this.txtConfirmPassword.Size = new System.Drawing.Size(175, 20);
      this.txtConfirmPassword.TabIndex = 3;
      this.txtConfirmPassword.TextChanged += new System.EventHandler(this.txtConfirmPassword_TextChanged);
      // 
      // txtNewPassword
      // 
      this.txtNewPassword.Location = new System.Drawing.Point(115, 47);
      this.txtNewPassword.Name = "txtNewPassword";
      this.txtNewPassword.PasswordChar = '*';
      this.txtNewPassword.Size = new System.Drawing.Size(175, 20);
      this.txtNewPassword.TabIndex = 2;
      this.txtNewPassword.TextChanged += new System.EventHandler(this.txtNewPassword_TextChanged);
      // 
      // txtCurrentPassword
      // 
      this.txtCurrentPassword.Location = new System.Drawing.Point(115, 13);
      this.txtCurrentPassword.Name = "txtCurrentPassword";
      this.txtCurrentPassword.PasswordChar = '*';
      this.txtCurrentPassword.Size = new System.Drawing.Size(175, 20);
      this.txtCurrentPassword.TabIndex = 1;
      // 
      // label23
      // 
      this.label23.AutoSize = true;
      this.label23.Location = new System.Drawing.Point(6, 76);
      this.label23.Name = "label23";
      this.label23.Size = new System.Drawing.Size(94, 13);
      this.label23.TabIndex = 5;
      this.label23.Text = "Confirm Password:";
      // 
      // label22
      // 
      this.label22.AutoSize = true;
      this.label22.Location = new System.Drawing.Point(6, 50);
      this.label22.Name = "label22";
      this.label22.Size = new System.Drawing.Size(81, 13);
      this.label22.TabIndex = 4;
      this.label22.Text = "New Password:";
      // 
      // label21
      // 
      this.label21.AutoSize = true;
      this.label21.Location = new System.Drawing.Point(6, 16);
      this.label21.Name = "label21";
      this.label21.Size = new System.Drawing.Size(93, 13);
      this.label21.TabIndex = 3;
      this.label21.Text = "Current Password:";
      // 
      // comboBox1
      // 
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[] {
            "9600",
            "115200"});
      this.comboBox1.Location = new System.Drawing.Point(209, 11);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(70, 21);
      this.comboBox1.TabIndex = 7;
      this.comboBox1.Text = "115200";
      this.toolTip.SetToolTip(this.comboBox1, "Baud Rate is currently fixed at 2400bps");
      // 
      // comboBox2
      // 
      this.comboBox2.Enabled = false;
      this.comboBox2.FormattingEnabled = true;
      this.comboBox2.Items.AddRange(new object[] {
            "None",
            "One",
            "Two"});
      this.comboBox2.Location = new System.Drawing.Point(343, 11);
      this.comboBox2.Name = "comboBox2";
      this.comboBox2.Size = new System.Drawing.Size(71, 21);
      this.comboBox2.TabIndex = 6;
      this.comboBox2.Text = "One";
      this.toolTip.SetToolTip(this.comboBox2, "Not configurable");
      // 
      // comboBox3
      // 
      this.comboBox3.Enabled = false;
      this.comboBox3.FormattingEnabled = true;
      this.comboBox3.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
      this.comboBox3.Location = new System.Drawing.Point(462, 11);
      this.comboBox3.Name = "comboBox3";
      this.comboBox3.Size = new System.Drawing.Size(51, 21);
      this.comboBox3.TabIndex = 5;
      this.comboBox3.Text = "None";
      this.toolTip.SetToolTip(this.comboBox3, "Not configurable");
      // 
      // comboBox4
      // 
      this.comboBox4.FormattingEnabled = true;
      this.comboBox4.Location = new System.Drawing.Point(68, 11);
      this.comboBox4.Name = "comboBox4";
      this.comboBox4.Size = new System.Drawing.Size(68, 21);
      this.comboBox4.Sorted = true;
      this.comboBox4.TabIndex = 1;
      this.toolTip.SetToolTip(this.comboBox4, "Name of the Serial Port that connects to the communication module on this server");
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(285, 14);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(52, 13);
      this.label7.TabIndex = 4;
      this.label7.Text = "Stop Bits:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(420, 14);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(36, 13);
      this.label8.TabIndex = 3;
      this.label8.Text = "GSMParity:";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(142, 14);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(61, 13);
      this.label9.TabIndex = 2;
      this.label9.Text = "Baud Rate:";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(6, 14);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(56, 13);
      this.label10.TabIndex = 0;
      this.label10.Text = "COM Port:";
      // 
      // btnApply
      // 
      this.btnApply.Enabled = false;
      this.btnApply.Location = new System.Drawing.Point(478, 202);
      this.btnApply.Name = "btnApply";
      this.btnApply.Size = new System.Drawing.Size(75, 23);
      this.btnApply.TabIndex = 7;
      this.btnApply.Text = "Apply";
      this.btnApply.UseVisualStyleBackColor = true;
      this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
      // 
      // FormOptions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(565, 233);
      this.Controls.Add(this.btnApply);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.tabPaneOptions);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormOptions";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Options";
      this.Load += new System.EventHandler(this.FormOptions_Load);
      this.Shown += new System.EventHandler(this.FormOptions_Shown);
      this.tabCommunication.ResumeLayout(false);
      this.tabCommunication.PerformLayout();
      this.tabLogging.ResumeLayout(false);
      this.tabLogging.PerformLayout();
      this.tabPaneOptions.ResumeLayout(false);
      this.tabNotification.ResumeLayout(false);
      this.tabNotification.PerformLayout();
      this.tabParameters.ResumeLayout(false);
      this.tabParameters.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericInactiveMins)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericFailedReads)).EndInit();
      this.tabAccount.ResumeLayout(false);
      this.tabAccount.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    private System.Windows.Forms.ColorDialog colorDialog;
    private System.Windows.Forms.TabPage tabCommunication;
    private System.Windows.Forms.ComboBox comboGSMBaudRate;
    private System.Windows.Forms.ComboBox comboGSMStopBits;
    private System.Windows.Forms.ComboBox comboGSMParity;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox comboGSMCOMPort;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TabPage tabLogging;
    private System.Windows.Forms.Button btnBrowseRainfallLogDir;
    private System.Windows.Forms.TextBox txtRainfallLogDir;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TabControl tabPaneOptions;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.Button btnBrowseAppLogDir;
    private System.Windows.Forms.TextBox txtAppLogDir;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.ComboBox comboArduinoBaudRate;
    private System.Windows.Forms.ComboBox comboArduinoStopBits;
    private System.Windows.Forms.ComboBox comboArduinoParity;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.ComboBox comboArduinoCOMPort;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.ComboBox comboBox2;
    private System.Windows.Forms.ComboBox comboBox3;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.ComboBox comboBox4;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Button btnBrowseWinrarBinDir;
    private System.Windows.Forms.TextBox txtWinrarDir;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.TabPage tabNotification;
    private System.Windows.Forms.Button btnRemoveRecipient;
    private System.Windows.Forms.Button btnAddRecipient;
    private System.Windows.Forms.ListBox lstEmailRecipients;
    private System.Windows.Forms.TextBox txtRecipientEmail;
    private System.Windows.Forms.Label label18;
    private System.Windows.Forms.TabPage tabParameters;
    private System.Windows.Forms.Label label19;
    private System.Windows.Forms.NumericUpDown numericFailedReads;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.NumericUpDown numericInactiveMins;
    private System.Windows.Forms.TabPage tabAccount;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtConfirmPassword;
    private System.Windows.Forms.TextBox txtNewPassword;
    private System.Windows.Forms.TextBox txtCurrentPassword;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.Button btnApply;
    private System.Windows.Forms.CheckBox chkRememberPassword;
  }
}