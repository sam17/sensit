using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SENSITServer
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      bool NewWindow;
      Mutex MutexRunOnceInstance = new Mutex(true, Assembly.GetExecutingAssembly().GetType().GUID.ToString(), out NewWindow);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      if (!NewWindow)
      {
        MessageBox.Show("An instance of SENSIT Server is already running",
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Stop);
        Environment.Exit(0);
      }
      Application.Run(new FormMain(ref MutexRunOnceInstance));
      GC.KeepAlive(MutexRunOnceInstance);
    }
  }
}
