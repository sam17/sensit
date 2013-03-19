using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;

namespace SENSITServer
{
  public partial class FormMain
  {
    private void SetMenuItemEnabled(ToolStripMenuItem item, bool enabled)
    {
      try
      {
        this.BeginInvoke(new MethodInvoker(delegate() { item.Enabled = enabled; }));
      }
      catch
      {
        // Do nothing
      }
    }

    #region code copied from http://stackoverflow.com/questions/626988/prevent-autoscrolling-in-c-sharp-richtextbox with minor changes
    
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);
    const int WM_USER = 0x400;
    const int EM_HIDESELECTION = WM_USER + 63;

    void txtLogAppend(string text)
    {
      if (this.chkEnableLogging.Checked)
      {
        bool focused = this.txtLog.Focused;

        // backup initial selection
        int selection = this.txtLog.SelectionStart;
        int length = this.txtLog.SelectionLength;
        // allow autoscroll if selection is at end of text
        bool autoscroll = (selection == this.txtLog.Text.Length);

        if (!autoscroll)
        {
          // shift focus from RichTextBox to some other control
          if (focused)
            this.grpLog.Focus();

          // hide selection
          SendMessage(this.txtLog.Handle, EM_HIDESELECTION, 1, 0);
        }

        this.txtLog.AppendText(text);

        if (!autoscroll)
        {
          // restore initial selection
          this.txtLog.SelectionStart = selection;
          this.txtLog.SelectionLength = length;
          // unhide selection
          SendMessage(this.txtLog.Handle, EM_HIDESELECTION, 0, 0);
          // restore focus to RichTextBox
          if (focused)
            this.txtLog.Focus();
        }
      }
    }
    #endregion

    private delegate void AppendTxtLogTextDelegate(string text);
    private void AppendTxtLogText(string text)
    {
      if (this.txtLog.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.txtLog.Invoke(new AppendTxtLogTextDelegate(AppendTxtLogText), text);
      }
      else
      {
        // This is the UI thread so perform the task.
        txtLogAppend(text);
      }
    }

    private delegate void SetStripMessageForeColorDelegate(Color color);
    private void SetStripMessageForeColor(Color color)
    {
      if (this.statusStrip.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.statusStrip.Invoke(new SetStripMessageForeColorDelegate(this.SetStripMessageForeColor), color);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.stripMessage.ForeColor = color;
      }
    }

    private delegate void SetStripGSMColorDelegate(Color color);
    private void SetStripGSMForeColor(Color color)
    {
      if (this.statusStrip.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.statusStrip.Invoke(new SetStripGSMColorDelegate(this.SetStripGSMForeColor), color);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.stripGSM.ForeColor = color;
      }
    }

    private delegate void SetStripArduinoForeColorDelegate(Color color);
    private void SetStripArduinoForeColor(Color color)
    {
      if (this.statusStrip.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.statusStrip.Invoke(new SetStripArduinoForeColorDelegate(this.SetStripArduinoForeColor), color);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.stripArduino.ForeColor = color;
      }
    }

    private delegate void SetStripMessageTextDelegate(string text);
    private void SetStripMessageText(string text)
    {
      if (this.statusStrip.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.statusStrip.Invoke(new SetStripMessageTextDelegate(this.SetStripMessageText), text);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.stripMessage.Text = text;
      }
    }

    private delegate void SetStripGSMCOMPortTextDelegate(string text);
    private void SetStripGSMCOMPortText(string text)
    {
      if (this.statusStrip.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.statusStrip.Invoke(new SetStripGSMCOMPortTextDelegate(this.SetStripGSMCOMPortText), text);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.stripGSMCOMPort.Text = text;
      }
    }

    private delegate void SetStripGSMBaudTextDelegate(string text);
    private void SetStripGSMBaudText(string text)
    {
      if (this.statusStrip.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.statusStrip.Invoke(new SetStripGSMBaudTextDelegate(this.SetStripGSMBaudText), text);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.stripGSMBaud.Text = text;
      }
    }

    private delegate void SetStripArduinoCOMPortTextDelegate(string text);
    private void SetStripArduinoCOMPortText(string text)
    {
      if (this.statusStrip.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.statusStrip.Invoke(new SetStripArduinoCOMPortTextDelegate(this.SetStripArduinoCOMPortText), text);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.stripArduinoCOMPort.Text = text;
      }
    }

    private delegate void SetStripArduinoBaudTextDelegate(string text);
    private void SetStripArduinoBaudText(string text)
    {
      if (this.statusStrip.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.statusStrip.Invoke(new SetStripArduinoBaudTextDelegate(this.SetStripArduinoBaudText), text);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.stripArduinoBaud.Text = text;
      }
    }

    private delegate void AddToListKnownSensorsDelegate(string str);
    private void AddToListKnownSensors(string str)
    {
      if (this.listKnownSensors.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.listKnownSensors.Invoke(new AddToListKnownSensorsDelegate(this.AddToListKnownSensors), str);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.listKnownSensors.Items.Add(str);
      }
    }

    private delegate void SetTxtSensorStateTextDelegate(string text);
    private void SetTxtSensorStateText(string text)
    {
      if (this.txtSensorState.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.txtSensorState.Invoke(new SetTxtSensorStateTextDelegate(this.SetTxtSensorStateText), text);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.txtSensorState.Text = text;
      }
    }

    private delegate void SetTxtSensorPingIntervalTextDelegate(float pingInterval);
    private void SetTxtSensorPingIntervalText(float pingInterval)
    {
      if (this.txtSensorPingInterval.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.txtSensorPingInterval.Invoke(new SetTxtSensorPingIntervalTextDelegate(this.SetTxtSensorPingIntervalText), pingInterval);
      }
      else
      {
        // This is the UI thread so perform the task.
        int pingIntervalI = (int)pingInterval;
        if (pingIntervalI >= 0)
        {
          int ss = pingIntervalI % 60;
          int mm = (pingIntervalI / 60) % 60;
          int hh = pingIntervalI / 3600;
          this.txtSensorPingInterval.Text = String.Format("{0:00}:{1:00}:{2:00}", hh, mm, ss);
        }
        else
          this.txtSensorPingInterval.Text = "(Unknown)";
      }
    }

    private delegate void SetBtnRefreshPingIntervalEnabledDelegate(bool enabled);
    private void SetBtnRefreshPingIntervalEnabled(bool enabled)
    {
      if (this.btnRefreshPingInterval.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.btnRefreshPingInterval.Invoke(new SetBtnRefreshPingIntervalEnabledDelegate(this.SetBtnRefreshPingIntervalEnabled), enabled);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.btnRefreshPingInterval.Enabled = enabled;
      }
    }

    private delegate void SetBtnRefreshSensorStateEnabledDelegate(bool enabled);
    private void SetBtnRefreshSensorStateEnabled(bool enabled)
    {
      if (this.btnRefreshSensorState.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.btnRefreshSensorState.Invoke(new SetBtnRefreshSensorStateEnabledDelegate(this.SetBtnRefreshSensorStateEnabled), enabled);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.btnRefreshSensorState.Enabled = enabled;
      }
    }

    private delegate void SetBtnWakeUpEnabledDelegate(bool enabled);
    private void SetBtnWakeUpEnabled(bool enabled)
    {
      if (this.btnWakeUp.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.btnWakeUp.Invoke(new SetBtnWakeUpEnabledDelegate(this.SetBtnWakeUpEnabled), enabled);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.btnWakeUp.Enabled = enabled;
      }
    }

    private delegate void SetBtnSleepEnabledDelegate(bool enabled);
    private void SetBtnSleepEnabled(bool enabled)
    {
      if (this.btnSleep.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.btnSleep.Invoke(new SetBtnSleepEnabledDelegate(this.SetBtnSleepEnabled), enabled);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.btnSleep.Enabled = enabled;
      }
    }

    private delegate void SetBtnSetPingIntervalEnabledDelegate(bool enabled);
    private void SetBtnSetPingIntervalEnabled(bool enabled)
    {
      if (this.btnSetPingInterval.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.btnSetPingInterval.Invoke(new SetBtnSetPingIntervalEnabledDelegate(this.SetBtnSetPingIntervalEnabled), enabled);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.btnSetPingInterval.Enabled = enabled;
      }
    }

    private delegate void SetBtnResetReadingEnabledDelegate(bool enabled);
    private void SetBtnResetReadingEnabled(bool enabled)
    {
      if (this.btnResetReading.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.btnResetReading.Invoke(new SetBtnResetReadingEnabledDelegate(this.SetBtnResetReadingEnabled), enabled);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.btnResetReading.Enabled = enabled;
      }
    }

    private delegate void SetGrpNetworkStatusEnabledDelegate(bool enabled);
    private void SetGrpNetworkStatusEnabled(bool enabled)
    {
      if (this.grpNetworkStatus.InvokeRequired)
      {
        // This is a worker thread so delegate the task.
        this.grpNetworkStatus.Invoke(new SetGrpNetworkStatusEnabledDelegate(this.SetGrpNetworkStatusEnabled), enabled);
      }
      else
      {
        // This is the UI thread so perform the task.
        this.grpNetworkStatus.Enabled = enabled;
      }
    }
  }
}
    