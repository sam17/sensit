using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace SENSITServer
{
  class SerialPortLogger
  {
    public enum LogState
    {
      Init,
      RX,
      TX
    }

    public static string FilePath { get; set; }

    private static LogState state = LogState.Init;

    private static DateTime lastRXTime = DateTime.MinValue, lastTXTime = DateTime.MinValue;

    public static void LogRX(char ch)
    {
      using (StreamWriter writer = new StreamWriter(FilePath, true))
      {
        if (state == LogState.Init || state == LogState.TX || DateTime.Now.Subtract(lastRXTime).TotalSeconds > 5)
        {
          lastRXTime = DateTime.Now;
          string text = String.Format(Environment.NewLine + "(RX) {0} : {1}", DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT), ch);
          writer.Write(text);
          writer.Flush();
          state = LogState.RX;
        }
        else if (state == LogState.RX)
        {
          writer.Write(ch);
          writer.Flush();
        }
      }
    }

    public static void LogTX(char ch)
    {
      using (StreamWriter writer = new StreamWriter(FilePath, true))
      {
        if (state == LogState.Init || state == LogState.RX || DateTime.Now.Subtract(lastTXTime).TotalSeconds > 5)
        {
          lastTXTime = DateTime.Now;
          string text = String.Format(Environment.NewLine + "(TX) {0} : {1}", DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT), ch);
          writer.Write(text);
          writer.Flush();
          state = LogState.TX;
        }
        else if (state == LogState.TX)
        {
          writer.Write(ch);
          writer.Flush();
        }
      }
    }
  }
}
