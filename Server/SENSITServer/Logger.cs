using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

namespace SENSITServer
{
  public partial class FormMain
  {
    private bool LoggerInitialized = false;
    private int LogIndent = 0;

    private void LoggerInit(string ActivityFilePath)
    {
      Trace.Assert(LoggerInitialized == false);
      LoggerInitialized = true;

      Trace.Listeners.Add(new TextWriterTraceListener(ActivityFilePath));
      Trace.AutoFlush = true;
      Trace.WriteLine(DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT) + " : " + "================================================");
      Trace.WriteLine("Application Path: " + (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath);
      Trace.WriteLine("Application Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
    }

    private void LoggerEnterMethod()
    {
      string text = DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT) + " : " + "Entering " + (new StackTrace()).GetFrame(1).GetMethod().Name + "()";
      Trace.WriteLine(text);
      Trace.Indent();
      if (this.radLogActivity.Checked)
      {
        for (int i = 0; i < LogIndent; ++i)
          AppendTxtLogText("  ");
        AppendTxtLogText(text);
        AppendTxtLogText(Environment.NewLine);
        ++LogIndent;
      }
    }

    private void LoggerLeaveMethod()
    {
      string text = DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT) + " : " + "Leaving " + (new StackTrace()).GetFrame(1).GetMethod().Name + "()";
      Trace.Unindent();
      Trace.WriteLine(text);
      if (this.radLogActivity.Checked)
      {
        --LogIndent;
        for (int i = 0; i < LogIndent; ++i)
          AppendTxtLogText("  ");
        AppendTxtLogText(text);
        AppendTxtLogText(Environment.NewLine);
      }
    }

    private void LoggerInfo(string message)
    {
      string text = DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT) + " : " + message;
      Trace.WriteLine(text);
      if (this.radLogActivity.Checked)
      {
        for (int i = 0; i < LogIndent; ++i)
          AppendTxtLogText("  ");
        AppendTxtLogText(text);
        AppendTxtLogText(Environment.NewLine);
      }
    }

    private void LoggerError(string message)
    {
      string text = DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT) + " : " + message;
      Trace.TraceError(text);
      if (this.radLogActivity.Checked)
      {
        for (int i = 0; i < LogIndent; ++i)
          AppendTxtLogText("  ");
        AppendTxtLogText(text);
        AppendTxtLogText(Environment.NewLine);
      }
    }

    private void LoggerWarning(string message)
    {
      string text = DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT) + " : " + message;
      Trace.TraceWarning(text);
      if (this.radLogActivity.Checked)
      {
        for (int i = 0; i < LogIndent; ++i)
          AppendTxtLogText("  ");
        AppendTxtLogText(text);
        AppendTxtLogText(Environment.NewLine);
      }
    }

    private Dictionary<string, Queue<string>> LoggerWaitingMsgQueue = new Dictionary<string, Queue<string>>();

    private void LoggerLog(string FilePath, string TimeStamp, string Text)
    {
      if (LoggerWaitingMsgQueue.ContainsKey(FilePath) == false)
        LoggerWaitingMsgQueue[FilePath] = new Queue<string>();

      string line = TimeStamp + " , " + Text;

      try
      {
        LoggerWaitingMsgQueue[FilePath].Enqueue(line);
      }
      catch
      {
        LoggerWaitingMsgQueue[FilePath].Clear();
      }

      try
      {
        using (StreamWriter writer = new StreamWriter(FilePath, true))
        {
          while (LoggerWaitingMsgQueue.Count > 0)
            writer.WriteLine(LoggerWaitingMsgQueue[FilePath].Dequeue());
          writer.Flush();
        }
      }
      catch 
      {
        // Nothing to do, return elegantly
      }
    }
  }
}
