using System;
using System.IO;
using System.Security;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Net.Mail;

namespace GSMModemEmulator
{
  enum Result
  {
    Success,
    Failure
  }

  class Util
  {
    bool IsFileLocked(FileInfo file)
    {
      FileStream stream = null;

      try
      {
        stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
      }
      catch (IOException)
      {
        //the file is unavailable because it is:
        //still being written to
        //or being processed by another thread
        //or does not exist (has already been processed)
        return true;
      }
      finally
      {
        if (stream != null)
          stream.Close();
      }

      //file is not locked
      return false;
    }

    /// <summary>
    /// Code copied from http://support.microsoft.com/kb/307010
    /// </summary>
    /// <param name="inputFile">Name of file to be encrypted</param>
    /// <param name="outputFile">Name of the encrypted output file</param>
    /// <param name="password">Password for encryption</param>
    public static void EncryptFile(string inputFile,
                                   string outputFile,
                                   string password)
    {
      try
      {
        FileStream fsInput = new FileStream(inputFile,
                                            FileMode.Open,
                                            FileAccess.Read);

        FileStream fsEncrypted = new FileStream(outputFile,
                                                FileMode.Create,
                                                FileAccess.Write);

        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        DES.Key = ASCIIEncoding.ASCII.GetBytes(password);
        DES.IV = ASCIIEncoding.ASCII.GetBytes(password);

        ICryptoTransform desencrypt = DES.CreateEncryptor();
        CryptoStream cryptostream = new CryptoStream(fsEncrypted,
                                                     desencrypt,
                                                     CryptoStreamMode.Write);

        byte[] bytearrayinput = new byte[fsInput.Length];
        fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
        fsInput.Close();

        cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
        cryptostream.Flush();
        cryptostream.Close();
      }
      catch (Exception exc)
      {
#if DEBUG
        MessageBox.Show("Encryption failed!\n" + exc.Message + "\n" + Util.GetDebugInfo(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
#endif
      }
    }

    /// <summary>
    /// Code copied from http://support.microsoft.com/kb/307010
    /// </summary>
    /// <param name="inputFile">Name of file to be decrypted</param>
    /// <param name="outputFile">Name of the decrypted output file</param>
    /// <param name="password">Password for decryption</param>
    public static void DecryptFile(string inputFile,
                                   string outputFile,
                                   string password)
    {
      try
      {
        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        // A 64 bit key and IV is required for this provider.
        // Set secret key For DES algorithm.
        DES.Key = ASCIIEncoding.ASCII.GetBytes(password);
        // Set initialization vector.
        DES.IV = ASCIIEncoding.ASCII.GetBytes(password);

        // Create a file stream to read the encrypted file back.
        FileStream fsread = new FileStream(inputFile,
                                           FileMode.Open,
                                           FileAccess.Read);
        // Create a DES decryptor from the DES instance.
        ICryptoTransform desdecrypt = DES.CreateDecryptor();
        // Create crypto stream set to read and do a 
        // DES decryption transform on incoming bytes.
        CryptoStream cryptostreamDecr = new CryptoStream(fsread,
                                                         desdecrypt,
                                                         CryptoStreamMode.Read);
        // Print the contents of the decrypted file.
        StreamWriter fsDecrypted = new StreamWriter(outputFile);
        fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
        fsDecrypted.Flush();
        fsDecrypted.Close();
      }
      catch (Exception exc)
      {
#if DEBUG
        MessageBox.Show("Decryption failed!\n" + exc.Message + "\n" + Util.GetDebugInfo(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
#endif
      }
    }

    public static string ByteArrToHex(byte[] byteArr)
    {
      try
      {
        string hex = BitConverter.ToString(byteArr);
        return hex.Replace("-", "");
      }
      catch
      {
        return null;
      }
    }

    public static byte[] HexToByteArr(string hex)
    {
      try
      {
        byte[] byteArr = new byte[hex.Length / 2];
        for (int i = 0; i < hex.Length; i += 2)
        {
          byteArr[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return byteArr;
      }
      catch
      {
        return null;
      }
    }

    public static float ByteArrToFloat(byte[] byteArr)
    {
      return BitConverter.ToSingle(byteArr, 0);
    }

    public static string ByteArrToString(byte[] byteArr)
    {
      return ASCIIEncoding.ASCII.GetString(byteArr);
    }

    public static byte[] FloatToByteArr(float real)
    {
      byte[] byteArr = BitConverter.GetBytes(real);
      if (BitConverter.IsLittleEndian == false)  // We want little endian format
      {
        Array.Reverse(byteArr);
      }
      return byteArr;
    }

    public static string GetDebugInfo()
    {
#if DEBUG
      StackFrame stackFrame = new StackFrame(1, true);
      string method = stackFrame.GetMethod().ToString();
      int line = stackFrame.GetFileLineNumber();
      return ("Method: " + method + "\nLine: " + line);
#else
      return string.Empty;
#endif
    }

    public static void SendMail(List<string> Recipients, string AttachmentFile)
    {
      try
      {
        MailMessage mail = new MailMessage();
        foreach (string emailID in Recipients)
          mail.To.Add(emailID);
        mail.From = new MailAddress("sensitserver@gmail.com");
        mail.Subject = "SENSIT server log @ " + DateTime.Now.ToString();

        mail.Body = "Hi all,\n\n" +
                    "This is an auto-generated email sent by SENSIT Server application\n" +
                    "because it crashed. Attached is the archived log files that can be\n" +
                    "used to study the cause of the crash\n\n" +
                    "--\nSENSIT Server\n";
        mail.IsBodyHtml = false;
        mail.Attachments.Add(new Attachment(AttachmentFile));
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Credentials = new System.Net.NetworkCredential("sensitserver@gmail.com", "sensit123");
        smtp.EnableSsl = true;
        smtp.Send(mail);
      }
      catch (Exception exc)
      {
#if DEBUG
        MessageBox.Show(String.Format("Could not send e-mail\nException Occurred :{0}", exc.Message),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
#endif
      }
    }

    public static Result ArchiveLogs(string ArchiveFilename, List<string> DirList, string RarFilePath)
    {
      try
      {
        Process rar = new Process();
        rar.StartInfo.FileName = RarFilePath;
        rar.StartInfo.Arguments = "a -r -m5 " + ArchiveFilename + ".rar";
        rar.StartInfo.CreateNoWindow = true;
        rar.StartInfo.UseShellExecute = false;

        /* Copying the log folders in the current directory for archiving.
         * Archiving could not be done directly as the activity log file
         * is being used by the application and hence cannot be archived
         * directly.
         */
        foreach (string Dir in DirList)
        {
          string TargetDir = (new DirectoryInfo(Dir)).Name;
          if (Directory.Exists(TargetDir))
            Directory.Delete(TargetDir, true);
          CopyFolderRecursive(Dir, TargetDir);
          rar.StartInfo.Arguments += " " + TargetDir;
        }

        rar.Start();
        rar.WaitForExit();

        // Deleting the temporary log folders created
        foreach (string Dir in DirList)
        {
          string TargetDir = (new DirectoryInfo(Dir)).Name;
          if (Directory.Exists(TargetDir))
            Directory.Delete(TargetDir, true);
        }
      }
      catch (Exception exc)
      {
#if DEBUG
        MessageBox.Show(String.Format("Exception Occurred :{0},{1}", exc.Message, exc.StackTrace.ToString()),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
#endif
        return Result.Failure;
      }
      return Result.Success;
    }

    static public void CopyFolderRecursive(string sourceFolder, string destFolder)
    {
      if (!Directory.Exists(destFolder))
        Directory.CreateDirectory(destFolder);
      string[] files = Directory.GetFiles(sourceFolder);
      foreach (string file in files)
      {
        string name = Path.GetFileName(file);
        string dest = Path.Combine(destFolder, name);
        File.Copy(file, dest);
      }
      string[] folders = Directory.GetDirectories(sourceFolder);
      foreach (string folder in folders)
      {
        string name = Path.GetFileName(folder);
        string dest = Path.Combine(destFolder, name);
        CopyFolderRecursive(folder, dest);
      }
    }
  }

  class Logger
  {
    private static bool Initialized = false;

    public static void Init(string ActivityFilePath)
    {
      Trace.Assert(Initialized == false);
      Initialized = true;

      Trace.Listeners.Add(new TextWriterTraceListener(ActivityFilePath));
      Trace.AutoFlush = true;
      Trace.WriteLine(DateTime.Now.ToString() + " : " + "---------------------------NEW SESSION---------------------------");
    }

    public static void LogEnterMethod()
    {
      Trace.WriteLine(DateTime.Now.ToString() + " : " + "Entering " + (new StackTrace()).GetFrame(1).GetMethod().Name + "()");
      Trace.Indent();
    }

    public static void LogLeaveMethod()
    {
      Trace.Unindent();
      Trace.WriteLine(DateTime.Now.ToString() + " : " + "Leaving " + (new StackTrace()).GetFrame(1).GetMethod().Name + "()");
    }

    public static void AddInfo(string message)
    {
      Trace.WriteLine(DateTime.Now.ToString() + " : " + message);
    }

    public static void AddError(string message)
    {
      Trace.TraceError(DateTime.Now.ToString() + " : " + message);
    }
  }
}
