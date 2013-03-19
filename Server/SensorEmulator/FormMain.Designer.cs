namespace SensorEmulator
{
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
      this.btnDone = new System.Windows.Forms.Button();
      this.btnLog = new System.Windows.Forms.Button();
      this.txtCurrM = new System.Windows.Forms.TextBox();
      this.btnDebug = new System.Windows.Forms.Button();
      this.txtDebug = new System.Windows.Forms.TextBox();
      this.btnStart = new System.Windows.Forms.Button();
      this.comboCOM = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.grpLog = new System.Windows.Forms.GroupBox();
      this.btnSendDesc = new System.Windows.Forms.Button();
      this.btnResend = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.txtBaud = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.btnConnect = new System.Windows.Forms.Button();
      this.grpCalib = new System.Windows.Forms.GroupBox();
      this.txtInput = new System.Windows.Forms.TextBox();
      this.txtOutput = new System.Windows.Forms.TextBox();
      this.btnH2F = new System.Windows.Forms.Button();
      this.btnF2H = new System.Windows.Forms.Button();
      this.btnS2H = new System.Windows.Forms.Button();
      this.btnH2S = new System.Windows.Forms.Button();
      this.grpDebug = new System.Windows.Forms.GroupBox();
      this.btnH2B = new System.Windows.Forms.Button();
      this.btnB2H = new System.Windows.Forms.Button();
      this.grpRX = new System.Windows.Forms.GroupBox();
      this.btnClear = new System.Windows.Forms.Button();
      this.txtRX = new System.Windows.Forms.TextBox();
      this.grpInit = new System.Windows.Forms.GroupBox();
      this.lblState = new System.Windows.Forms.Label();
      this.txtDesc = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtID = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.SPort = new System.IO.Ports.SerialPort(this.components);
      this.grpLog.SuspendLayout();
      this.grpCalib.SuspendLayout();
      this.grpDebug.SuspendLayout();
      this.grpRX.SuspendLayout();
      this.grpInit.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnDone
      // 
      this.btnDone.Location = new System.Drawing.Point(255, 48);
      this.btnDone.Name = "btnDone";
      this.btnDone.Size = new System.Drawing.Size(75, 23);
      this.btnDone.TabIndex = 1;
      this.btnDone.Text = "Send Done";
      this.btnDone.UseVisualStyleBackColor = true;
      // 
      // btnLog
      // 
      this.btnLog.Location = new System.Drawing.Point(255, 76);
      this.btnLog.Name = "btnLog";
      this.btnLog.Size = new System.Drawing.Size(75, 23);
      this.btnLog.TabIndex = 2;
      this.btnLog.Text = "Send Log";
      this.btnLog.UseVisualStyleBackColor = true;
      this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
      // 
      // txtCurrM
      // 
      this.txtCurrM.Location = new System.Drawing.Point(167, 78);
      this.txtCurrM.Name = "txtCurrM";
      this.txtCurrM.Size = new System.Drawing.Size(82, 20);
      this.txtCurrM.TabIndex = 3;
      this.txtCurrM.Text = "0.0";
      this.txtCurrM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // btnDebug
      // 
      this.btnDebug.Location = new System.Drawing.Point(255, 105);
      this.btnDebug.Name = "btnDebug";
      this.btnDebug.Size = new System.Drawing.Size(75, 23);
      this.btnDebug.TabIndex = 4;
      this.btnDebug.Text = "Send Debug";
      this.btnDebug.UseVisualStyleBackColor = true;
      this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
      // 
      // txtDebug
      // 
      this.txtDebug.Location = new System.Drawing.Point(65, 107);
      this.txtDebug.Name = "txtDebug";
      this.txtDebug.Size = new System.Drawing.Size(184, 20);
      this.txtDebug.TabIndex = 5;
      // 
      // btnStart
      // 
      this.btnStart.Location = new System.Drawing.Point(6, 19);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(75, 23);
      this.btnStart.TabIndex = 6;
      this.btnStart.Text = "Send Start";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
      // 
      // comboCOM
      // 
      this.comboCOM.FormattingEnabled = true;
      this.comboCOM.Location = new System.Drawing.Point(189, 12);
      this.comboCOM.Name = "comboCOM";
      this.comboCOM.Size = new System.Drawing.Size(64, 21);
      this.comboCOM.TabIndex = 7;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(127, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(56, 13);
      this.label1.TabIndex = 8;
      this.label1.Text = "COM Port:";
      // 
      // grpLog
      // 
      this.grpLog.Controls.Add(this.btnSendDesc);
      this.grpLog.Controls.Add(this.btnResend);
      this.grpLog.Controls.Add(this.label6);
      this.grpLog.Controls.Add(this.label5);
      this.grpLog.Controls.Add(this.txtCurrM);
      this.grpLog.Controls.Add(this.btnDone);
      this.grpLog.Controls.Add(this.btnLog);
      this.grpLog.Controls.Add(this.txtDebug);
      this.grpLog.Controls.Add(this.btnDebug);
      this.grpLog.Location = new System.Drawing.Point(12, 111);
      this.grpLog.Name = "grpLog";
      this.grpLog.Size = new System.Drawing.Size(336, 139);
      this.grpLog.TabIndex = 9;
      this.grpLog.TabStop = false;
      this.grpLog.Text = "Log";
      // 
      // btnSendDesc
      // 
      this.btnSendDesc.Location = new System.Drawing.Point(174, 19);
      this.btnSendDesc.Name = "btnSendDesc";
      this.btnSendDesc.Size = new System.Drawing.Size(75, 23);
      this.btnSendDesc.TabIndex = 18;
      this.btnSendDesc.Text = "Send Desc";
      this.btnSendDesc.UseVisualStyleBackColor = true;
      this.btnSendDesc.Click += new System.EventHandler(this.btnSendDesc_Click);
      // 
      // btnResend
      // 
      this.btnResend.Location = new System.Drawing.Point(255, 19);
      this.btnResend.Name = "btnResend";
      this.btnResend.Size = new System.Drawing.Size(75, 23);
      this.btnResend.TabIndex = 17;
      this.btnResend.Text = "Resend";
      this.btnResend.UseVisualStyleBackColor = true;
      this.btnResend.Click += new System.EventHandler(this.btnResend_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 110);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(53, 13);
      this.label6.TabIndex = 16;
      this.label6.Text = "Message:";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 81);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(155, 13);
      this.label5.TabIndex = 15;
      this.label5.Text = "Enter the current measurement:";
      // 
      // txtBaud
      // 
      this.txtBaud.Location = new System.Drawing.Point(200, 39);
      this.txtBaud.Name = "txtBaud";
      this.txtBaud.Size = new System.Drawing.Size(53, 20);
      this.txtBaud.TabIndex = 11;
      this.txtBaud.Text = "2400";
      this.txtBaud.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(159, 42);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(35, 13);
      this.label2.TabIndex = 10;
      this.label2.Text = "Baud:";
      // 
      // btnConnect
      // 
      this.btnConnect.Location = new System.Drawing.Point(259, 12);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(71, 47);
      this.btnConnect.TabIndex = 9;
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // grpCalib
      // 
      this.grpCalib.Controls.Add(this.btnStart);
      this.grpCalib.Location = new System.Drawing.Point(12, 256);
      this.grpCalib.Name = "grpCalib";
      this.grpCalib.Size = new System.Drawing.Size(336, 98);
      this.grpCalib.TabIndex = 10;
      this.grpCalib.TabStop = false;
      this.grpCalib.Text = "Calibration";
      // 
      // txtInput
      // 
      this.txtInput.Location = new System.Drawing.Point(7, 17);
      this.txtInput.Name = "txtInput";
      this.txtInput.Size = new System.Drawing.Size(318, 20);
      this.txtInput.TabIndex = 7;
      // 
      // txtOutput
      // 
      this.txtOutput.Location = new System.Drawing.Point(7, 68);
      this.txtOutput.Name = "txtOutput";
      this.txtOutput.Size = new System.Drawing.Size(318, 20);
      this.txtOutput.TabIndex = 8;
      // 
      // btnH2F
      // 
      this.btnH2F.Location = new System.Drawing.Point(7, 41);
      this.btnH2F.Name = "btnH2F";
      this.btnH2F.Size = new System.Drawing.Size(40, 23);
      this.btnH2F.TabIndex = 10;
      this.btnH2F.Text = "H2F";
      this.btnH2F.UseVisualStyleBackColor = true;
      this.btnH2F.Click += new System.EventHandler(this.btnH2F_Click);
      // 
      // btnF2H
      // 
      this.btnF2H.Location = new System.Drawing.Point(53, 41);
      this.btnF2H.Name = "btnF2H";
      this.btnF2H.Size = new System.Drawing.Size(40, 23);
      this.btnF2H.TabIndex = 11;
      this.btnF2H.Text = "F2H";
      this.btnF2H.UseVisualStyleBackColor = true;
      this.btnF2H.Click += new System.EventHandler(this.btnF2H_Click);
      // 
      // btnS2H
      // 
      this.btnS2H.Location = new System.Drawing.Point(99, 41);
      this.btnS2H.Name = "btnS2H";
      this.btnS2H.Size = new System.Drawing.Size(40, 23);
      this.btnS2H.TabIndex = 12;
      this.btnS2H.Text = "S2H";
      this.btnS2H.UseVisualStyleBackColor = true;
      this.btnS2H.Click += new System.EventHandler(this.btnS2H_Click);
      // 
      // btnH2S
      // 
      this.btnH2S.Location = new System.Drawing.Point(145, 41);
      this.btnH2S.Name = "btnH2S";
      this.btnH2S.Size = new System.Drawing.Size(40, 23);
      this.btnH2S.TabIndex = 13;
      this.btnH2S.Text = "H2S";
      this.btnH2S.UseVisualStyleBackColor = true;
      this.btnH2S.Click += new System.EventHandler(this.btnH2S_Click);
      // 
      // grpDebug
      // 
      this.grpDebug.Controls.Add(this.btnH2B);
      this.grpDebug.Controls.Add(this.btnB2H);
      this.grpDebug.Controls.Add(this.btnH2S);
      this.grpDebug.Controls.Add(this.btnS2H);
      this.grpDebug.Controls.Add(this.txtInput);
      this.grpDebug.Controls.Add(this.btnF2H);
      this.grpDebug.Controls.Add(this.txtOutput);
      this.grpDebug.Controls.Add(this.btnH2F);
      this.grpDebug.Location = new System.Drawing.Point(12, 360);
      this.grpDebug.Name = "grpDebug";
      this.grpDebug.Size = new System.Drawing.Size(336, 100);
      this.grpDebug.TabIndex = 11;
      this.grpDebug.TabStop = false;
      this.grpDebug.Text = "Debugging";
      // 
      // btnH2B
      // 
      this.btnH2B.Location = new System.Drawing.Point(237, 41);
      this.btnH2B.Name = "btnH2B";
      this.btnH2B.Size = new System.Drawing.Size(40, 23);
      this.btnH2B.TabIndex = 15;
      this.btnH2B.Text = "H2B";
      this.btnH2B.UseVisualStyleBackColor = true;
      this.btnH2B.Click += new System.EventHandler(this.btnH2B_Click);
      // 
      // btnB2H
      // 
      this.btnB2H.Location = new System.Drawing.Point(191, 41);
      this.btnB2H.Name = "btnB2H";
      this.btnB2H.Size = new System.Drawing.Size(40, 23);
      this.btnB2H.TabIndex = 14;
      this.btnB2H.Text = "B2H";
      this.btnB2H.UseVisualStyleBackColor = true;
      this.btnB2H.Click += new System.EventHandler(this.btnB2H_Click);
      // 
      // grpRX
      // 
      this.grpRX.Controls.Add(this.btnClear);
      this.grpRX.Controls.Add(this.txtRX);
      this.grpRX.Location = new System.Drawing.Point(354, 12);
      this.grpRX.Name = "grpRX";
      this.grpRX.Size = new System.Drawing.Size(285, 448);
      this.grpRX.TabIndex = 12;
      this.grpRX.TabStop = false;
      this.grpRX.Text = "Received Messages";
      // 
      // btnClear
      // 
      this.btnClear.Location = new System.Drawing.Point(107, 414);
      this.btnClear.Name = "btnClear";
      this.btnClear.Size = new System.Drawing.Size(75, 23);
      this.btnClear.TabIndex = 14;
      this.btnClear.Text = "Clear";
      this.btnClear.UseVisualStyleBackColor = true;
      this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
      // 
      // txtRX
      // 
      this.txtRX.AcceptsReturn = true;
      this.txtRX.AcceptsTab = true;
      this.txtRX.BackColor = System.Drawing.SystemColors.Window;
      this.txtRX.Location = new System.Drawing.Point(6, 21);
      this.txtRX.Multiline = true;
      this.txtRX.Name = "txtRX";
      this.txtRX.ReadOnly = true;
      this.txtRX.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtRX.Size = new System.Drawing.Size(273, 387);
      this.txtRX.TabIndex = 0;
      // 
      // grpInit
      // 
      this.grpInit.Controls.Add(this.lblState);
      this.grpInit.Controls.Add(this.txtDesc);
      this.grpInit.Controls.Add(this.label4);
      this.grpInit.Controls.Add(this.txtID);
      this.grpInit.Controls.Add(this.label3);
      this.grpInit.Controls.Add(this.txtBaud);
      this.grpInit.Controls.Add(this.btnConnect);
      this.grpInit.Controls.Add(this.label2);
      this.grpInit.Controls.Add(this.comboCOM);
      this.grpInit.Controls.Add(this.label1);
      this.grpInit.Location = new System.Drawing.Point(12, 12);
      this.grpInit.Name = "grpInit";
      this.grpInit.Size = new System.Drawing.Size(336, 98);
      this.grpInit.TabIndex = 11;
      this.grpInit.TabStop = false;
      this.grpInit.Text = "Initialization";
      // 
      // lblState
      // 
      this.lblState.AutoSize = true;
      this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblState.Location = new System.Drawing.Point(164, 79);
      this.lblState.Name = "lblState";
      this.lblState.Size = new System.Drawing.Size(41, 13);
      this.lblState.TabIndex = 16;
      this.lblState.Text = "State:";
      // 
      // txtDesc
      // 
      this.txtDesc.Location = new System.Drawing.Point(47, 38);
      this.txtDesc.Multiline = true;
      this.txtDesc.Name = "txtDesc";
      this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtDesc.Size = new System.Drawing.Size(106, 54);
      this.txtDesc.TabIndex = 15;
      this.txtDesc.Text = "Civil Department, IIT Kharagpur";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 42);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(35, 13);
      this.label4.TabIndex = 14;
      this.label4.Text = "Desc:";
      // 
      // txtID
      // 
      this.txtID.Location = new System.Drawing.Point(47, 12);
      this.txtID.Name = "txtID";
      this.txtID.Size = new System.Drawing.Size(74, 20);
      this.txtID.TabIndex = 13;
      this.txtID.Text = "8348468052";
      this.txtID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 16);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(21, 13);
      this.label3.TabIndex = 12;
      this.label3.Text = "ID:";
      // 
      // SPort
      // 
      this.SPort.BaudRate = 2400;
      this.SPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SPort_DataReceived);
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(651, 472);
      this.Controls.Add(this.grpInit);
      this.Controls.Add(this.grpRX);
      this.Controls.Add(this.grpDebug);
      this.Controls.Add(this.grpCalib);
      this.Controls.Add(this.grpLog);
      this.Name = "FormMain";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Sensor Emulator";
      this.Load += new System.EventHandler(this.Main_Load);
      this.grpLog.ResumeLayout(false);
      this.grpLog.PerformLayout();
      this.grpCalib.ResumeLayout(false);
      this.grpDebug.ResumeLayout(false);
      this.grpDebug.PerformLayout();
      this.grpRX.ResumeLayout(false);
      this.grpRX.PerformLayout();
      this.grpInit.ResumeLayout(false);
      this.grpInit.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnDone;
    private System.Windows.Forms.Button btnLog;
    private System.Windows.Forms.TextBox txtCurrM;
    private System.Windows.Forms.Button btnDebug;
    private System.Windows.Forms.TextBox txtDebug;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.ComboBox comboCOM;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox grpLog;
    private System.Windows.Forms.GroupBox grpCalib;
    private System.Windows.Forms.TextBox txtBaud;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnConnect;
    private System.Windows.Forms.TextBox txtInput;
    private System.Windows.Forms.Button btnF2H;
    private System.Windows.Forms.Button btnH2F;
    private System.Windows.Forms.TextBox txtOutput;
    private System.Windows.Forms.Button btnS2H;
    private System.Windows.Forms.Button btnH2S;
    private System.Windows.Forms.GroupBox grpDebug;
    private System.Windows.Forms.GroupBox grpRX;
    private System.Windows.Forms.TextBox txtRX;
    private System.Windows.Forms.Button btnClear;
    private System.Windows.Forms.GroupBox grpInit;
    private System.Windows.Forms.TextBox txtDesc;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtID;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.IO.Ports.SerialPort SPort;
    private System.Windows.Forms.Button btnH2B;
    private System.Windows.Forms.Button btnB2H;
    private System.Windows.Forms.Label lblState;
    private System.Windows.Forms.Button btnResend;
    private System.Windows.Forms.Button btnSendDesc;
  }
}

