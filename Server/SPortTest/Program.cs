using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;
using System.IO;

namespace SENSITServer
{
  class Program
  {
    private static Dictionary<string, Queue<string>> WaitingMsgQueue = new Dictionary<string, Queue<string>>();

    public static void Log(string FilePath, string TimeStamp, string Text)
    {
      if (WaitingMsgQueue.ContainsKey(FilePath) == false)
        WaitingMsgQueue[FilePath] = new Queue<string>();

      string line = TimeStamp + " , " + Text;

      WaitingMsgQueue[FilePath].Enqueue(line);

      try
      {
        using (StreamWriter writer = new StreamWriter(FilePath, true))
        {
          while (WaitingMsgQueue.Count > 0)
            writer.WriteLine(WaitingMsgQueue[FilePath].Dequeue());
          writer.Flush();
        }
      }
      catch
      {
        // Nothing to do, return elegantly
      }
    }

    static void T1()
    {
      int count = 0;
      while (true)
      {
        Log("abc.txt", DateTime.Now.ToString(), count.ToString());
        count++;
      }
    }

    static void T2()
    {
      int count = 0;
      while (true)
      {
        using (StreamWriter writer = new StreamWriter("abcd.txt", true))
        {
          writer.WriteLine(count.ToString());
          writer.Flush();
        }
        count++;
      }
    }

    static void Main(string[] args)
    {
      Thread t1 = new Thread(T1);
      t1.IsBackground = true;
      t1.Start();

      Thread t2 = new Thread(T1);
      t2.IsBackground = true;
      t2.Start();

      Thread t3 = new Thread(T1);
      t3.IsBackground = true;
      t3.Start();

      while (true)
      {
        Thread.Sleep(10);
      }
    }
  }
}
