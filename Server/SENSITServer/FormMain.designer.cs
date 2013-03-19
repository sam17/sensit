namespace SENSITServer
{
  /// <summary>
  /// Main class representing the main form of the application
  /// </summary>
  partial class FormMain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
      this.grpNetworkStatus = new System.Windows.Forms.GroupBox();
      this.btnDeleteSensors = new System.Windows.Forms.Button();
      this.grpSetPingInterval = new System.Windows.Forms.GroupBox();
      this.label9 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.numericSS = new System.Windows.Forms.NumericUpDown();
      this.numericMM = new System.Windows.Forms.NumericUpDown();
      this.numericHH = new System.Windows.Forms.NumericUpDown();
      this.btnSetPingInterval = new System.Windows.Forms.Button();
      this.listKnownSensors = new System.Windows.Forms.ListBox();
      this.btnResetReading = new System.Windows.Forms.Button();
      this.btnSleep = new System.Windows.Forms.Button();
      this.btnSelectAll = new System.Windows.Forms.Button();
      this.btnWakeUp = new System.Windows.Forms.Button();
      this.grpSensorInfo = new System.Windows.Forms.GroupBox();
      this.btnRefreshPingInterval = new System.Windows.Forms.Button();
      this.btnRefreshSensorState = new System.Windows.Forms.Button();
      this.txtSensorId = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtSensorLastReadingTime = new System.Windows.Forms.TextBox();
      this.txtSensorLastReading = new System.Windows.Forms.TextBox();
      this.txtSensorPingInterval = new System.Windows.Forms.TextBox();
      this.txtSensorState = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txtDesc = new System.Windows.Forms.TextBox();
      this.menuStrip = new System.Windows.Forms.MenuStrip();
      this.mnuServer = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuRunOnSysStartup = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuEnableAllButtons = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.stripGSM = new System.Windows.Forms.ToolStripStatusLabel();
      this.stripGSMCOMPort = new System.Windows.Forms.ToolStripStatusLabel();
      this.stripGSMBaud = new System.Windows.Forms.ToolStripStatusLabel();
      this.stripArduino = new System.Windows.Forms.ToolStripStatusLabel();
      this.stripArduinoCOMPort = new System.Windows.Forms.ToolStripStatusLabel();
      this.stripArduinoBaud = new System.Windows.Forms.ToolStripStatusLabel();
      this.stripUpTime = new System.Windows.Forms.ToolStripStatusLabel();
      this.stripMessage = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.txtLog = new System.Windows.Forms.TextBox();
      this.grpLog = new System.Windows.Forms.GroupBox();
      this.radLogSerialPort = new System.Windows.Forms.RadioButton();
      this.radLogActivity = new System.Windows.Forms.RadioButton();
      this.chkEnableLogging = new System.Windows.Forms.CheckBox();
      this.btnClearLog = new System.Windows.Forms.Button();
      this.grpNetworkStatus.SuspendLayout();
      this.grpSetPingInterval.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericSS)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericMM)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericHH)).BeginInit();
      this.grpSensorInfo.SuspendLayout();
      this.menuStrip.SuspendLayout();
      this.statusStrip.SuspendLayout();
      this.grpLog.SuspendLayout();
      this.SuspendLayout();
      // 
      // grpNetworkStatus
      // 
      this.grpNetworkStatus.Controls.Add(this.btnDeleteSensors);
      this.grpNetworkStatus.Controls.Add(this.grpSetPingInterval);
      this.grpNetworkStatus.Controls.Add(this.listKnownSensors);
      this.grpNetworkStatus.Controls.Add(this.btnResetReading);
      this.grpNetworkStatus.Controls.Add(this.btnSleep);
      this.grpNetworkStatus.Controls.Add(this.btnSelectAll);
      this.grpNetworkStatus.Controls.Add(this.btnWakeUp);
      this.grpNetworkStatus.Location = new System.Drawing.Point(12, 27);
      this.grpNetworkStatus.Name = "grpNetworkStatus";
      this.grpNetworkStatus.Size = new System.Drawing.Size(315, 190);
      this.grpNetworkStatus.TabIndex = 1;
      this.grpNetworkStatus.TabStop = false;
      this.grpNetworkStatus.Text = "Sensor Network";
      // 
      // btnDeleteSensors
      // 
      this.btnDeleteSensors.Location = new System.Drawing.Point(141, 156);
      this.btnDeleteSensors.Name = "btnDeleteSensors";
      this.btnDeleteSensors.Size = new System.Drawing.Size(166, 23);
      this.btnDeleteSensors.TabIndex = 50;
      this.btnDeleteSensors.Text = "Delete Sensors";
      this.toolTip.SetToolTip(this.btnDeleteSensors, "Deletes all records of the selected sensors");
      this.btnDeleteSensors.UseVisualStyleBackColor = true;
      this.btnDeleteSensors.Click += new System.EventHandler(this.btnDeleteSensors_Click);
      // 
      // grpSetPingInterval
      // 
      this.grpSetPingInterval.Controls.Add(this.label9);
      this.grpSetPingInterval.Controls.Add(this.label8);
      this.grpSetPingInterval.Controls.Add(this.label7);
      this.grpSetPingInterval.Controls.Add(this.numericSS);
      this.grpSetPingInterval.Controls.Add(this.numericMM);
      this.grpSetPingInterval.Controls.Add(this.numericHH);
      this.grpSetPingInterval.Controls.Add(this.btnSetPingInterval);
      this.grpSetPingInterval.Location = new System.Drawing.Point(141, 77);
      this.grpSetPingInterval.Name = "grpSetPingInterval";
      this.grpSetPingInterval.Size = new System.Drawing.Size(166, 71);
      this.grpSetPingInterval.TabIndex = 49;
      this.grpSetPingInterval.TabStop = false;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(150, 16);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(12, 13);
      this.label9.TabIndex = 55;
      this.label9.Text = "s";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(95, 16);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(15, 13);
      this.label8.TabIndex = 54;
      this.label8.Text = "m";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(42, 16);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(13, 13);
      this.label7.TabIndex = 53;
      this.label7.Text = "h";
      // 
      // numericSS
      // 
      this.numericSS.Location = new System.Drawing.Point(111, 14);
      this.numericSS.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
      this.numericSS.Name = "numericSS";
      this.numericSS.ReadOnly = true;
      this.numericSS.Size = new System.Drawing.Size(33, 20);
      this.numericSS.TabIndex = 52;
      this.toolTip.SetToolTip(this.numericSS, "Set the number of seconds here");
      // 
      // numericMM
      // 
      this.numericMM.Location = new System.Drawing.Point(56, 14);
      this.numericMM.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
      this.numericMM.Name = "numericMM";
      this.numericMM.ReadOnly = true;
      this.numericMM.Size = new System.Drawing.Size(33, 20);
      this.numericMM.TabIndex = 51;
      this.toolTip.SetToolTip(this.numericMM, "Set the number of minutes here");
      // 
      // numericHH
      // 
      this.numericHH.Location = new System.Drawing.Point(6, 14);
      this.numericHH.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.numericHH.Name = "numericHH";
      this.numericHH.ReadOnly = true;
      this.numericHH.Size = new System.Drawing.Size(33, 20);
      this.numericHH.TabIndex = 50;
      this.toolTip.SetToolTip(this.numericHH, "Set the number of hours here");
      // 
      // btnSetPingInterval
      // 
      this.btnSetPingInterval.Location = new System.Drawing.Point(6, 40);
      this.btnSetPingInterval.Name = "btnSetPingInterval";
      this.btnSetPingInterval.Size = new System.Drawing.Size(154, 23);
      this.btnSetPingInterval.TabIndex = 49;
      this.btnSetPingInterval.Text = "Set Ping Interval";
      this.toolTip.SetToolTip(this.btnSetPingInterval, "Sends signals to all the selected sensors above to set their ping intervals to th" +
              "exc Time set above");
      this.btnSetPingInterval.UseVisualStyleBackColor = true;
      this.btnSetPingInterval.Click += new System.EventHandler(this.btnSetPingInterval_Click);
      // 
      // listKnownSensors
      // 
      this.listKnownSensors.FormattingEnabled = true;
      this.listKnownSensors.Location = new System.Drawing.Point(6, 19);
      this.listKnownSensors.Name = "listKnownSensors";
      this.listKnownSensors.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.listKnownSensors.Size = new System.Drawing.Size(129, 134);
      this.listKnownSensors.TabIndex = 42;
      this.toolTip.SetToolTip(this.listKnownSensors, "Lists all the sensors known to the server");
      this.listKnownSensors.SelectedIndexChanged += new System.EventHandler(this.listKnownSensors_SelectedIndexChanged);
      // 
      // btnResetReading
      // 
      this.btnResetReading.Location = new System.Drawing.Point(141, 19);
      this.btnResetReading.Name = "btnResetReading";
      this.btnResetReading.Size = new System.Drawing.Size(166, 23);
      this.btnResetReading.TabIndex = 21;
      this.btnResetReading.Text = "Reset Readings";
      this.toolTip.SetToolTip(this.btnResetReading, "Resets the rainfall reading of all the selected sensors above to zero");
      this.btnResetReading.UseVisualStyleBackColor = true;
      this.btnResetReading.Click += new System.EventHandler(this.btnResetReading_Click);
      // 
      // btnSleep
      // 
      this.btnSleep.Location = new System.Drawing.Point(230, 48);
      this.btnSleep.Name = "btnSleep";
      this.btnSleep.Size = new System.Drawing.Size(77, 23);
      this.btnSleep.TabIndex = 6;
      this.btnSleep.Text = "Sleep";
      this.toolTip.SetToolTip(this.btnSleep, "Sends signals to all the selected sensors above to sleep and stop logging rainfal" +
              "ln data");
      this.btnSleep.UseVisualStyleBackColor = true;
      this.btnSleep.Click += new System.EventHandler(this.btnSleep_Click);
      // 
      // btnSelectAll
      // 
      this.btnSelectAll.Location = new System.Drawing.Point(6, 156);
      this.btnSelectAll.Name = "btnSelectAll";
      this.btnSelectAll.Size = new System.Drawing.Size(129, 23);
      this.btnSelectAll.TabIndex = 2;
      this.btnSelectAll.Text = "Select All";
      this.toolTip.SetToolTip(this.btnSelectAll, "Selects/Deselects all the sensors in the list above");
      this.btnSelectAll.UseVisualStyleBackColor = true;
      this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
      // 
      // btnWakeUp
      // 
      this.btnWakeUp.Location = new System.Drawing.Point(141, 48);
      this.btnWakeUp.Name = "btnWakeUp";
      this.btnWakeUp.Size = new System.Drawing.Size(77, 23);
      this.btnWakeUp.TabIndex = 5;
      this.btnWakeUp.Text = "Wake Up";
      this.toolTip.SetToolTip(this.btnWakeUp, "Sends signals to all the selected sensors above to wake up");
      this.btnWakeUp.UseVisualStyleBackColor = true;
      this.btnWakeUp.Click += new System.EventHandler(this.btnWakeUp_Click);
      // 
      // grpSensorInfo
      // 
      this.grpSensorInfo.Controls.Add(this.btnRefreshPingInterval);
      this.grpSensorInfo.Controls.Add(this.btnRefreshSensorState);
      this.grpSensorInfo.Controls.Add(this.txtSensorId);
      this.grpSensorInfo.Controls.Add(this.label3);
      this.grpSensorInfo.Controls.Add(this.txtSensorLastReadingTime);
      this.grpSensorInfo.Controls.Add(this.txtSensorLastReading);
      this.grpSensorInfo.Controls.Add(this.txtSensorPingInterval);
      this.grpSensorInfo.Controls.Add(this.txtSensorState);
      this.grpSensorInfo.Controls.Add(this.label6);
      this.grpSensorInfo.Controls.Add(this.label5);
      this.grpSensorInfo.Controls.Add(this.label4);
      this.grpSensorInfo.Controls.Add(this.label1);
      this.grpSensorInfo.Controls.Add(this.label2);
      this.grpSensorInfo.Controls.Add(this.txtDesc);
      this.grpSensorInfo.Location = new System.Drawing.Point(12, 223);
      this.grpSensorInfo.Name = "grpSensorInfo";
      this.grpSensorInfo.Size = new System.Drawing.Size(315, 243);
      this.grpSensorInfo.TabIndex = 2;
      this.grpSensorInfo.TabStop = false;
      this.grpSensorInfo.Text = "Selected Sensor Info";
      // 
      // btnRefreshPingInterval
      // 
      this.btnRefreshPingInterval.Location = new System.Drawing.Point(282, 159);
      this.btnRefreshPingInterval.Name = "btnRefreshPingInterval";
      this.btnRefreshPingInterval.Size = new System.Drawing.Size(25, 23);
      this.btnRefreshPingInterval.TabIndex = 43;
      this.btnRefreshPingInterval.Text = "R";
      this.toolTip.SetToolTip(this.btnRefreshPingInterval, "Reload the current ping interval of the selected sensor");
      this.btnRefreshPingInterval.UseVisualStyleBackColor = true;
      this.btnRefreshPingInterval.Click += new System.EventHandler(this.btnRefreshPingInterval_Click);
      // 
      // btnRefreshSensorState
      // 
      this.btnRefreshSensorState.Location = new System.Drawing.Point(282, 133);
      this.btnRefreshSensorState.Name = "btnRefreshSensorState";
      this.btnRefreshSensorState.Size = new System.Drawing.Size(25, 23);
      this.btnRefreshSensorState.TabIndex = 42;
      this.btnRefreshSensorState.Text = "R";
      this.toolTip.SetToolTip(this.btnRefreshSensorState, "Reload the current State of the selected sensor");
      this.btnRefreshSensorState.UseVisualStyleBackColor = true;
      this.btnRefreshSensorState.Click += new System.EventHandler(this.btnRefreshSensorState_Click);
      // 
      // txtSensorId
      // 
      this.txtSensorId.BackColor = System.Drawing.Color.White;
      this.txtSensorId.Location = new System.Drawing.Point(113, 19);
      this.txtSensorId.Name = "txtSensorId";
      this.txtSensorId.ReadOnly = true;
      this.txtSensorId.Size = new System.Drawing.Size(194, 20);
      this.txtSensorId.TabIndex = 40;
      this.toolTip.SetToolTip(this.txtSensorId, "ID of the selected sensor");
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 22);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(21, 13);
      this.label3.TabIndex = 39;
      this.label3.Text = "ID:";
      // 
      // txtSensorLastReadingTime
      // 
      this.txtSensorLastReadingTime.BackColor = System.Drawing.Color.White;
      this.txtSensorLastReadingTime.Location = new System.Drawing.Point(113, 187);
      this.txtSensorLastReadingTime.Name = "txtSensorLastReadingTime";
      this.txtSensorLastReadingTime.ReadOnly = true;
      this.txtSensorLastReadingTime.Size = new System.Drawing.Size(194, 20);
      this.txtSensorLastReadingTime.TabIndex = 38;
      this.toolTip.SetToolTip(this.txtSensorLastReadingTime, "Time of the last reading sent by the selected sensor");
      // 
      // txtSensorLastReading
      // 
      this.txtSensorLastReading.BackColor = System.Drawing.Color.White;
      this.txtSensorLastReading.Location = new System.Drawing.Point(113, 213);
      this.txtSensorLastReading.Name = "txtSensorLastReading";
      this.txtSensorLastReading.ReadOnly = true;
      this.txtSensorLastReading.Size = new System.Drawing.Size(194, 20);
      this.txtSensorLastReading.TabIndex = 37;
      this.toolTip.SetToolTip(this.txtSensorLastReading, "Last reading sent by the selected sensor");
      // 
      // txtSensorPingInterval
      // 
      this.txtSensorPingInterval.BackColor = System.Drawing.Color.White;
      this.txtSensorPingInterval.Location = new System.Drawing.Point(113, 161);
      this.txtSensorPingInterval.Name = "txtSensorPingInterval";
      this.txtSensorPingInterval.ReadOnly = true;
      this.txtSensorPingInterval.Size = new System.Drawing.Size(161, 20);
      this.txtSensorPingInterval.TabIndex = 36;
      this.toolTip.SetToolTip(this.txtSensorPingInterval, "Time interval of sending rainfall logs by the selected sensor");
      // 
      // txtSensorState
      // 
      this.txtSensorState.BackColor = System.Drawing.Color.White;
      this.txtSensorState.Location = new System.Drawing.Point(113, 135);
      this.txtSensorState.Name = "txtSensorState";
      this.txtSensorState.ReadOnly = true;
      this.txtSensorState.Size = new System.Drawing.Size(161, 20);
      this.txtSensorState.TabIndex = 35;
      this.toolTip.SetToolTip(this.txtSensorState, "Current State of the selected sensor");
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 164);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(69, 13);
      this.label6.TabIndex = 32;
      this.label6.Text = "Ping Interval:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 216);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(73, 13);
      this.label5.TabIndex = 30;
      this.label5.Text = "Last Reading:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 190);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(99, 13);
      this.label4.TabIndex = 28;
      this.label4.Text = "Last Reading Time:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 138);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 26;
      this.label1.Text = "State:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 45);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(63, 13);
      this.label2.TabIndex = 25;
      this.label2.Text = "Description:";
      // 
      // txtDesc
      // 
      this.txtDesc.BackColor = System.Drawing.SystemColors.Window;
      this.txtDesc.Location = new System.Drawing.Point(6, 61);
      this.txtDesc.Multiline = true;
      this.txtDesc.Name = "txtDesc";
      this.txtDesc.ReadOnly = true;
      this.txtDesc.Size = new System.Drawing.Size(301, 68);
      this.txtDesc.TabIndex = 24;
      this.toolTip.SetToolTip(this.txtDesc, "Description of the selected sensor");
      // 
      // menuStrip
      // 
      this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuServer,
            this.mnuTools,
            this.mnuHelp});
      this.menuStrip.Location = new System.Drawing.Point(0, 0);
      this.menuStrip.Name = "menuStrip";
      this.menuStrip.Size = new System.Drawing.Size(803, 24);
      this.menuStrip.TabIndex = 3;
      // 
      // mnuServer
      // 
      this.mnuServer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRunOnSysStartup,
            this.mnuAlwaysOnTop,
            this.mnuExit});
      this.mnuServer.Name = "mnuServer";
      this.mnuServer.Size = new System.Drawing.Size(91, 20);
      this.mnuServer.Text = "SENSIT Server";
      // 
      // mnuRunOnSysStartup
      // 
      this.mnuRunOnSysStartup.Name = "mnuRunOnSysStartup";
      this.mnuRunOnSysStartup.Size = new System.Drawing.Size(190, 22);
      this.mnuRunOnSysStartup.Text = "Run at System Startup";
      this.mnuRunOnSysStartup.Click += new System.EventHandler(this.mnuRunOnSysStartup_Click);
      // 
      // mnuAlwaysOnTop
      // 
      this.mnuAlwaysOnTop.Name = "mnuAlwaysOnTop";
      this.mnuAlwaysOnTop.Size = new System.Drawing.Size(190, 22);
      this.mnuAlwaysOnTop.Text = "Always on Top";
      this.mnuAlwaysOnTop.ToolTipText = "Forces this window to appear on top of all other open windows";
      this.mnuAlwaysOnTop.Click += new System.EventHandler(this.mnuAlwaysOnTop_Click);
      // 
      // mnuExit
      // 
      this.mnuExit.Name = "mnuExit";
      this.mnuExit.Size = new System.Drawing.Size(190, 22);
      this.mnuExit.Text = "Exit";
      this.mnuExit.ToolTipText = "Exit SENSIT Server";
      this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
      // 
      // mnuTools
      // 
      this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEnableAllButtons,
            this.mnuOptions});
      this.mnuTools.Name = "mnuTools";
      this.mnuTools.Size = new System.Drawing.Size(48, 20);
      this.mnuTools.Text = "Tools";
      // 
      // mnuEnableAllButtons
      // 
      this.mnuEnableAllButtons.Name = "mnuEnableAllButtons";
      this.mnuEnableAllButtons.Size = new System.Drawing.Size(170, 22);
      this.mnuEnableAllButtons.Text = "Enable All Buttons";
      this.mnuEnableAllButtons.ToolTipText = "Enables all the buttons in this window which might get disabled permanently due t" +
          "o failure in communication with any sensor";
      this.mnuEnableAllButtons.Click += new System.EventHandler(this.mnuEnableAllButtons_Click);
      // 
      // mnuOptions
      // 
      this.mnuOptions.Name = "mnuOptions";
      this.mnuOptions.Size = new System.Drawing.Size(170, 22);
      this.mnuOptions.Text = "Options...";
      this.mnuOptions.ToolTipText = "Click to configure SENSIT Server options";
      this.mnuOptions.Click += new System.EventHandler(this.mnuOptions_Click);
      // 
      // mnuHelp
      // 
      this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
      this.mnuHelp.Name = "mnuHelp";
      this.mnuHelp.Size = new System.Drawing.Size(44, 20);
      this.mnuHelp.Text = "Help";
      // 
      // mnuAbout
      // 
      this.mnuAbout.Name = "mnuAbout";
      this.mnuAbout.Size = new System.Drawing.Size(191, 22);
      this.mnuAbout.Text = "About SENSIT Server...";
      this.mnuAbout.ToolTipText = "About SENSIT Server";
      this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
      // 
      // statusStrip
      // 
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripGSM,
            this.stripGSMCOMPort,
            this.stripGSMBaud,
            this.stripArduino,
            this.stripArduinoCOMPort,
            this.stripArduinoBaud,
            this.stripUpTime,
            this.stripMessage});
      this.statusStrip.Location = new System.Drawing.Point(0, 472);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Size = new System.Drawing.Size(803, 22);
      this.statusStrip.TabIndex = 4;
      this.statusStrip.Text = "SENSIT Server";
      // 
      // stripGSM
      // 
      this.stripGSM.Name = "stripGSM";
      this.stripGSM.Size = new System.Drawing.Size(32, 17);
      this.stripGSM.Text = "GSM";
      // 
      // stripGSMCOMPort
      // 
      this.stripGSMCOMPort.Name = "stripGSMCOMPort";
      this.stripGSMCOMPort.Size = new System.Drawing.Size(60, 17);
      this.stripGSMCOMPort.Text = "COM Port";
      // 
      // stripGSMBaud
      // 
      this.stripGSMBaud.Name = "stripGSMBaud";
      this.stripGSMBaud.Size = new System.Drawing.Size(34, 17);
      this.stripGSMBaud.Text = "Baud";
      // 
      // stripArduino
      // 
      this.stripArduino.Name = "stripArduino";
      this.stripArduino.Size = new System.Drawing.Size(50, 17);
      this.stripArduino.Text = "Arduino";
      // 
      // stripArduinoCOMPort
      // 
      this.stripArduinoCOMPort.Name = "stripArduinoCOMPort";
      this.stripArduinoCOMPort.Size = new System.Drawing.Size(60, 17);
      this.stripArduinoCOMPort.Text = "COM Port";
      // 
      // stripArduinoBaud
      // 
      this.stripArduinoBaud.Name = "stripArduinoBaud";
      this.stripArduinoBaud.Size = new System.Drawing.Size(34, 17);
      this.stripArduinoBaud.Text = "Baud";
      // 
      // stripUpTime
      // 
      this.stripUpTime.ForeColor = System.Drawing.Color.Blue;
      this.stripUpTime.Name = "stripUpTime";
      this.stripUpTime.Size = new System.Drawing.Size(168, 17);
      this.stripUpTime.Text = "Up Time: 0 hour(s) 0 minute(s)";
      // 
      // stripMessage
      // 
      this.stripMessage.Name = "stripMessage";
      this.stripMessage.Size = new System.Drawing.Size(53, 17);
      this.stripMessage.Text = "Message";
      // 
      // toolTip
      // 
      this.toolTip.AutoPopDelay = 7000;
      this.toolTip.InitialDelay = 500;
      this.toolTip.ReshowDelay = 100;
      // 
      // txtLog
      // 
      this.txtLog.BackColor = System.Drawing.Color.White;
      this.txtLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtLog.Location = new System.Drawing.Point(6, 19);
      this.txtLog.Multiline = true;
      this.txtLog.Name = "txtLog";
      this.txtLog.ReadOnly = true;
      this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtLog.Size = new System.Drawing.Size(446, 384);
      this.txtLog.TabIndex = 2;
      this.toolTip.SetToolTip(this.txtLog, "Displays the entire serial port data traffic or the activity log according to the" +
              " user\'s choice");
      this.txtLog.WordWrap = false;
      this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
      // 
      // grpLog
      // 
      this.grpLog.Controls.Add(this.radLogSerialPort);
      this.grpLog.Controls.Add(this.radLogActivity);
      this.grpLog.Controls.Add(this.chkEnableLogging);
      this.grpLog.Controls.Add(this.txtLog);
      this.grpLog.Controls.Add(this.btnClearLog);
      this.grpLog.Location = new System.Drawing.Point(333, 27);
      this.grpLog.Name = "grpLog";
      this.grpLog.Size = new System.Drawing.Size(458, 439);
      this.grpLog.TabIndex = 5;
      this.grpLog.TabStop = false;
      this.grpLog.Text = "Log";
      // 
      // radLogSerialPort
      // 
      this.radLogSerialPort.AutoSize = true;
      this.radLogSerialPort.Checked = true;
      this.radLogSerialPort.Location = new System.Drawing.Point(198, 413);
      this.radLogSerialPort.Name = "radLogSerialPort";
      this.radLogSerialPort.Size = new System.Drawing.Size(94, 17);
      this.radLogSerialPort.TabIndex = 5;
      this.radLogSerialPort.TabStop = true;
      this.radLogSerialPort.Text = "Serial Port Log";
      this.radLogSerialPort.UseVisualStyleBackColor = true;
      this.radLogSerialPort.CheckedChanged += new System.EventHandler(this.radLogSerialPort_CheckedChanged);
      // 
      // radLogActivity
      // 
      this.radLogActivity.AutoSize = true;
      this.radLogActivity.Location = new System.Drawing.Point(112, 413);
      this.radLogActivity.Name = "radLogActivity";
      this.radLogActivity.Size = new System.Drawing.Size(80, 17);
      this.radLogActivity.TabIndex = 4;
      this.radLogActivity.Text = "Activity Log";
      this.radLogActivity.UseVisualStyleBackColor = true;
      this.radLogActivity.CheckedChanged += new System.EventHandler(this.radLogActivity_CheckedChanged);
      // 
      // chkEnableLogging
      // 
      this.chkEnableLogging.AutoSize = true;
      this.chkEnableLogging.Checked = true;
      this.chkEnableLogging.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkEnableLogging.Location = new System.Drawing.Point(6, 414);
      this.chkEnableLogging.Name = "chkEnableLogging";
      this.chkEnableLogging.Size = new System.Drawing.Size(100, 17);
      this.chkEnableLogging.TabIndex = 3;
      this.chkEnableLogging.Text = "Enable Logging";
      this.chkEnableLogging.UseVisualStyleBackColor = true;
      this.chkEnableLogging.CheckedChanged += new System.EventHandler(this.chkEnableLogging_CheckedChanged);
      // 
      // btnClearLog
      // 
      this.btnClearLog.Location = new System.Drawing.Point(360, 410);
      this.btnClearLog.Name = "btnClearLog";
      this.btnClearLog.Size = new System.Drawing.Size(92, 23);
      this.btnClearLog.TabIndex = 1;
      this.btnClearLog.Text = "Clear Log";
      this.btnClearLog.UseVisualStyleBackColor = true;
      this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(803, 494);
      this.Controls.Add(this.grpLog);
      this.Controls.Add(this.statusStrip);
      this.Controls.Add(this.grpSensorInfo);
      this.Controls.Add(this.grpNetworkStatus);
      this.Controls.Add(this.menuStrip);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip;
      this.MaximizeBox = false;
      this.Name = "FormMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SENSIT Server";
      this.Load += new System.EventHandler(this.FormMain_Load);
      this.Shown += new System.EventHandler(this.FormMain_Shown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
      this.ResizeEnd += new System.EventHandler(this.FormMain_ResizeEnd);
      this.grpNetworkStatus.ResumeLayout(false);
      this.grpSetPingInterval.ResumeLayout(false);
      this.grpSetPingInterval.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericSS)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericMM)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numericHH)).EndInit();
      this.grpSensorInfo.ResumeLayout(false);
      this.grpSensorInfo.PerformLayout();
      this.menuStrip.ResumeLayout(false);
      this.menuStrip.PerformLayout();
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.grpLog.ResumeLayout(false);
      this.grpLog.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox grpNetworkStatus;
    private System.Windows.Forms.GroupBox grpSensorInfo;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.Button btnSelectAll;
    private System.Windows.Forms.ToolStripMenuItem mnuServer;
    private System.Windows.Forms.ToolStripMenuItem mnuExit;
    private System.Windows.Forms.ToolStripMenuItem mnuHelp;
    private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    private System.Windows.Forms.ToolStripMenuItem mnuTools;
    private System.Windows.Forms.ToolStripMenuItem mnuOptions;
    private System.Windows.Forms.Button btnWakeUp;
    private System.Windows.Forms.Button btnSleep;
    private System.Windows.Forms.TextBox txtDesc;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel stripGSMCOMPort;
    private System.Windows.Forms.ToolStripStatusLabel stripGSMBaud;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button btnResetReading;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtSensorLastReadingTime;
    private System.Windows.Forms.TextBox txtSensorLastReading;
    private System.Windows.Forms.TextBox txtSensorPingInterval;
    private System.Windows.Forms.TextBox txtSensorState;
    private System.Windows.Forms.TextBox txtSensorId;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ListBox listKnownSensors;
    private System.Windows.Forms.ToolStripMenuItem mnuAlwaysOnTop;
    private System.Windows.Forms.ToolStripMenuItem mnuEnableAllButtons;
    private System.Windows.Forms.Button btnRefreshSensorState;
    private System.Windows.Forms.Button btnRefreshPingInterval;
    private System.Windows.Forms.GroupBox grpSetPingInterval;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.NumericUpDown numericSS;
    private System.Windows.Forms.NumericUpDown numericMM;
    private System.Windows.Forms.NumericUpDown numericHH;
    private System.Windows.Forms.Button btnSetPingInterval;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.Button btnDeleteSensors;
    private System.Windows.Forms.ToolStripStatusLabel stripMessage;
    private System.Windows.Forms.ToolStripMenuItem mnuRunOnSysStartup;
    private System.Windows.Forms.GroupBox grpLog;
    private System.Windows.Forms.Button btnClearLog;
    private System.Windows.Forms.TextBox txtLog;
    private System.Windows.Forms.CheckBox chkEnableLogging;
    private System.Windows.Forms.RadioButton radLogSerialPort;
    private System.Windows.Forms.RadioButton radLogActivity;
    private System.Windows.Forms.ToolStripStatusLabel stripUpTime;
    private System.Windows.Forms.ToolStripStatusLabel stripGSM;
    private System.Windows.Forms.ToolStripStatusLabel stripArduino;
    private System.Windows.Forms.ToolStripStatusLabel stripArduinoCOMPort;
    private System.Windows.Forms.ToolStripStatusLabel stripArduinoBaud;
  }
}

