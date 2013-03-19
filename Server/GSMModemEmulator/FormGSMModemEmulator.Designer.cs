namespace GSMModemEmulator
{
  partial class FormGSMModemEmulator
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
      this.grpManual = new System.Windows.Forms.GroupBox();
      this.btnReceive = new System.Windows.Forms.Button();
      this.txtRainfall = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.comboSender = new System.Windows.Forms.ComboBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.comboBaud = new System.Windows.Forms.ComboBox();
      this.btnConnect = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.comboCOMPort = new System.Windows.Forms.ComboBox();
      this.grpAutomatic = new System.Windows.Forms.GroupBox();
      this.btnReload = new System.Windows.Forms.Button();
      this.numericPingInterval = new System.Windows.Forms.NumericUpDown();
      this.label7 = new System.Windows.Forms.Label();
      this.txtRangeMax = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtRangeMin = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.emulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuAutomate = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuResponseControl = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuAlwaysOnTop = new System.Windows.Forms.ToolStripMenuItem();
      this.grpManual.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.grpAutomatic.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericPingInterval)).BeginInit();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // grpManual
      // 
      this.grpManual.Controls.Add(this.btnReceive);
      this.grpManual.Controls.Add(this.txtRainfall);
      this.grpManual.Controls.Add(this.label2);
      this.grpManual.Controls.Add(this.label1);
      this.grpManual.Controls.Add(this.comboSender);
      this.grpManual.Enabled = false;
      this.grpManual.Location = new System.Drawing.Point(185, 27);
      this.grpManual.Name = "grpManual";
      this.grpManual.Size = new System.Drawing.Size(196, 109);
      this.grpManual.TabIndex = 0;
      this.grpManual.TabStop = false;
      this.grpManual.Text = "Manual SMS Receipt";
      // 
      // btnReceive
      // 
      this.btnReceive.Location = new System.Drawing.Point(111, 77);
      this.btnReceive.Name = "btnReceive";
      this.btnReceive.Size = new System.Drawing.Size(75, 23);
      this.btnReceive.TabIndex = 4;
      this.btnReceive.Text = "Receive";
      this.btnReceive.UseVisualStyleBackColor = true;
      this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
      // 
      // txtRainfall
      // 
      this.txtRainfall.Location = new System.Drawing.Point(56, 51);
      this.txtRainfall.Name = "txtRainfall";
      this.txtRainfall.Size = new System.Drawing.Size(130, 20);
      this.txtRainfall.TabIndex = 3;
      this.txtRainfall.Text = "0";
      this.txtRainfall.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 54);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(45, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Rainfall:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(44, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Sender:";
      // 
      // comboSender
      // 
      this.comboSender.FormattingEnabled = true;
      this.comboSender.Items.AddRange(new object[] {
            "+918348468052",
            "+918348468053",
            "+918348468054",
            "+918348468055"});
      this.comboSender.Location = new System.Drawing.Point(56, 24);
      this.comboSender.Name = "comboSender";
      this.comboSender.Size = new System.Drawing.Size(130, 21);
      this.comboSender.TabIndex = 0;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.comboBaud);
      this.groupBox2.Controls.Add(this.btnConnect);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.label4);
      this.groupBox2.Controls.Add(this.comboCOMPort);
      this.groupBox2.Location = new System.Drawing.Point(12, 27);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(167, 109);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Communication";
      // 
      // comboBaud
      // 
      this.comboBaud.FormattingEnabled = true;
      this.comboBaud.Items.AddRange(new object[] {
            "9600",
            "115200"});
      this.comboBaud.Location = new System.Drawing.Point(68, 51);
      this.comboBaud.Name = "comboBaud";
      this.comboBaud.Size = new System.Drawing.Size(87, 21);
      this.comboBaud.TabIndex = 5;
      // 
      // btnConnect
      // 
      this.btnConnect.Location = new System.Drawing.Point(80, 77);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(75, 23);
      this.btnConnect.TabIndex = 4;
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 54);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(53, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Baudrate:";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 27);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(56, 13);
      this.label4.TabIndex = 1;
      this.label4.Text = "COM Port:";
      // 
      // comboCOMPort
      // 
      this.comboCOMPort.FormattingEnabled = true;
      this.comboCOMPort.Location = new System.Drawing.Point(68, 21);
      this.comboCOMPort.Name = "comboCOMPort";
      this.comboCOMPort.Size = new System.Drawing.Size(87, 21);
      this.comboCOMPort.TabIndex = 0;
      // 
      // grpAutomatic
      // 
      this.grpAutomatic.Controls.Add(this.btnReload);
      this.grpAutomatic.Controls.Add(this.numericPingInterval);
      this.grpAutomatic.Controls.Add(this.label7);
      this.grpAutomatic.Controls.Add(this.txtRangeMax);
      this.grpAutomatic.Controls.Add(this.label6);
      this.grpAutomatic.Controls.Add(this.txtRangeMin);
      this.grpAutomatic.Controls.Add(this.label5);
      this.grpAutomatic.Location = new System.Drawing.Point(12, 142);
      this.grpAutomatic.Name = "grpAutomatic";
      this.grpAutomatic.Size = new System.Drawing.Size(369, 116);
      this.grpAutomatic.TabIndex = 2;
      this.grpAutomatic.TabStop = false;
      this.grpAutomatic.Text = "Automatic SMS Receipt";
      // 
      // btnReload
      // 
      this.btnReload.Location = new System.Drawing.Point(284, 45);
      this.btnReload.Name = "btnReload";
      this.btnReload.Size = new System.Drawing.Size(75, 23);
      this.btnReload.TabIndex = 5;
      this.btnReload.Text = "Reload";
      this.btnReload.UseVisualStyleBackColor = true;
      this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
      // 
      // numericPingInterval
      // 
      this.numericPingInterval.Location = new System.Drawing.Point(92, 48);
      this.numericPingInterval.Name = "numericPingInterval";
      this.numericPingInterval.Size = new System.Drawing.Size(59, 20);
      this.numericPingInterval.TabIndex = 7;
      this.numericPingInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 50);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(69, 13);
      this.label7.TabIndex = 6;
      this.label7.Text = "Ping Interval:";
      // 
      // txtRangeMax
      // 
      this.txtRangeMax.Location = new System.Drawing.Point(168, 22);
      this.txtRangeMax.Name = "txtRangeMax";
      this.txtRangeMax.Size = new System.Drawing.Size(48, 20);
      this.txtRangeMax.TabIndex = 3;
      this.txtRangeMax.Text = "100";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(146, 25);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(16, 13);
      this.label6.TabIndex = 2;
      this.label6.Text = "to";
      // 
      // txtRangeMin
      // 
      this.txtRangeMin.Location = new System.Drawing.Point(92, 22);
      this.txtRangeMin.Name = "txtRangeMin";
      this.txtRangeMin.Size = new System.Drawing.Size(48, 20);
      this.txtRangeMin.TabIndex = 1;
      this.txtRangeMin.Text = "1";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 25);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(80, 13);
      this.label5.TabIndex = 0;
      this.label5.Text = "Rainfall Range:";
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emulatorToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(396, 24);
      this.menuStrip1.TabIndex = 3;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // emulatorToolStripMenuItem
      // 
      this.emulatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAutomate,
            this.mnuResponseControl,
            this.mnuAlwaysOnTop});
      this.emulatorToolStripMenuItem.Name = "emulatorToolStripMenuItem";
      this.emulatorToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
      this.emulatorToolStripMenuItem.Text = "Emulator";
      // 
      // mnuAutomate
      // 
      this.mnuAutomate.Enabled = false;
      this.mnuAutomate.Name = "mnuAutomate";
      this.mnuAutomate.Size = new System.Drawing.Size(195, 22);
      this.mnuAutomate.Text = "Automate SMS Receipt";
      this.mnuAutomate.Click += new System.EventHandler(this.mnuAutomate_Click);
      // 
      // mnuResponseControl
      // 
      this.mnuResponseControl.Name = "mnuResponseControl";
      this.mnuResponseControl.Size = new System.Drawing.Size(195, 22);
      this.mnuResponseControl.Text = "Stop Responding";
      this.mnuResponseControl.Click += new System.EventHandler(this.mnuResponseControl_Click);
      // 
      // mnuAlwaysOnTop
      // 
      this.mnuAlwaysOnTop.Name = "mnuAlwaysOnTop";
      this.mnuAlwaysOnTop.Size = new System.Drawing.Size(195, 22);
      this.mnuAlwaysOnTop.Text = "Always on top";
      this.mnuAlwaysOnTop.Click += new System.EventHandler(this.mnuAlwaysOnTop_Click);
      // 
      // FormGSMModemEmulator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(396, 270);
      this.Controls.Add(this.grpAutomatic);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.grpManual);
      this.Controls.Add(this.menuStrip1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "FormGSMModemEmulator";
      this.Text = "GSM Modem Emulator";
      this.Load += new System.EventHandler(this.FormGSMModemEmulator_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGSMModemEmulator_FormClosing);
      this.grpManual.ResumeLayout(false);
      this.grpManual.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.grpAutomatic.ResumeLayout(false);
      this.grpAutomatic.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericPingInterval)).EndInit();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.GroupBox grpManual;
    private System.Windows.Forms.Button btnReceive;
    private System.Windows.Forms.TextBox txtRainfall;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox comboSender;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.ComboBox comboBaud;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox comboCOMPort;
    private System.Windows.Forms.GroupBox grpAutomatic;
    private System.Windows.Forms.Button btnReload;
    private System.Windows.Forms.NumericUpDown numericPingInterval;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtRangeMax;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtRangeMin;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem emulatorToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnuAutomate;
    private System.Windows.Forms.ToolStripMenuItem mnuResponseControl;
    private System.Windows.Forms.ToolStripMenuItem mnuAlwaysOnTop;
  }
}

