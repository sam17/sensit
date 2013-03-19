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
using System.Text.RegularExpressions;

namespace SENSITServer
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
    /// <param name="Text">Password for encryption</param>
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
        MessageBox.Show("Encryption failed!\n" + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
#endif
        throw exc;
      }
    }

    /// <summary>
    /// Code copied from http://support.microsoft.com/kb/307010
    /// </summary>
    /// <param name="inputFile">Name of file to be decrypted</param>
    /// <param name="outputFile">Name of the decrypted output file</param>
    /// <param name="Text">Password for decryption</param>
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
        MessageBox.Show("Decryption failed!\n" + exc.Message + "\n" + Util.GetMethodNameAndLineNum(),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
#endif
        throw exc;
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
        return string.Empty;
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

    public static string GetMethodNameAndLineNum()
    {
      StackFrame stackFrame = new StackFrame(1, true);
      string method = stackFrame.GetMethod().ToString();
      int line = stackFrame.GetFileLineNumber();
      return ("Method: " + method + " Line: " + line);
    }

    public static void SendEMail(List<string> Recipients, string AttachmentFileName)
    {
      try
      {
        MailMessage mail = new MailMessage();
        foreach (string emailID in Recipients)
          mail.To.Add(emailID);
        mail.From = new MailAddress("sensitserver@gmail.com");
        mail.Subject = "SENSIT server log @ " + DateTime.Now.ToString(Conf.LOG_TIMESTAMP_FORMAT);

        mail.Body = "Hi all,\n\n" +
                    "This is an auto-generated EmailAddress sent by SENSIT Server application\n" +
                    "because it crashed. Attached is the archived log files that can be\n" +
                    "used to study the cause of the crash\n\n" +
                    "--\nSENSIT Server\n";
        mail.IsBodyHtml = false;
        Attachment atc = new Attachment(AttachmentFileName);
        mail.Attachments.Add(atc);
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.Credentials = new System.Net.NetworkCredential("sensitserver@gmail.com", "sensit123");
        smtp.EnableSsl = true;
        smtp.Send(mail);
        mail.Dispose();
      }
      catch (Exception exc)
      {
#if DEBUG
        MessageBox.Show(String.Format("Could not send e-mail\nException Occurred :{0}", exc.Message),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
#endif
        throw exc;
      }
    }

    public static void ArchiveLogs(string ArchiveFilename, List<string> LogDirList, string RarFilePath)
    {
      try
      {
        Process rar = new Process();
        rar.StartInfo.FileName = RarFilePath;
        rar.StartInfo.Arguments = "a -r -m5 " + ArchiveFilename;
        rar.StartInfo.CreateNoWindow = true;
        rar.StartInfo.UseShellExecute = false;

        #region Deleting any left over temporary log folders/archives
        try
        {
          foreach (string OrigDir in LogDirList)
          {
            string TempLogDir = (new DirectoryInfo(OrigDir)).Name;
            DeleteEntireDirectory(TempLogDir);
          }
          File.SetAttributes(ArchiveFilename, FileAttributes.Normal);
          File.Delete(ArchiveFilename);
        }
        catch
        {
          // Nothing to do
        }
        #endregion

        /* Copying the log folders in the current directory for archiving.
         * Archiving could not be done directly as the activity log file
         * is being used by the application and hence cannot be archived
         * directly.
         */
        foreach (string OrigDir in LogDirList)
        {
          string TempLogDir = (new DirectoryInfo(OrigDir)).Name;
          CopyFolderRecursive(OrigDir, TempLogDir);
          rar.StartInfo.Arguments += " " + TempLogDir;
        }

        rar.Start();
        rar.WaitForExit();

        // Deleting all the temporary log folders created
        foreach (string OrigDir in LogDirList)
        {
          string TempLogDir = (new DirectoryInfo(OrigDir)).Name;
          if (DeleteEntireDirectory(TempLogDir) == Result.Failure)
          {
            throw new Exception("Failed to delete temporary log directory " + TempLogDir);
          }
        }
      }
      catch (Exception exc)
      {
#if DEBUG
        MessageBox.Show(String.Format("Could not create log archive: Exception Occurred :{0}", exc.Message),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
#endif
        throw exc;
      }
    }

    private static readonly object _lock = new object();
    public static Result DeleteEntireDirectory(string TargetDir)
    {
      if (DeleteDirectoryFiles(TargetDir) == Result.Failure)
        return Result.Failure;

      while (Directory.Exists(TargetDir))
      {
        lock (_lock)
        {
          DeleteDirectoryDirs(TargetDir);
        }
      }
      return Result.Success;
    }

    private static void DeleteDirectoryDirs(string TargetDir)
    {
      System.Threading.Thread.Sleep(100);

      if (Directory.Exists(TargetDir))
      {
        string[] dirs = Directory.GetDirectories(TargetDir);

        if (dirs.Length == 0)
          Directory.Delete(TargetDir, false);
        else
        {
          foreach (string dir in dirs)
            DeleteDirectoryDirs(dir);
        }
      }
    }

    private static Result DeleteDirectoryFiles(string TargetDir)
    {
      if (!Directory.Exists(TargetDir))
        return Result.Success;

      string[] files = Directory.GetFiles(TargetDir);
      string[] dirs = Directory.GetDirectories(TargetDir);

      try
      {
        foreach (string file in files)
        {
          File.SetAttributes(file, FileAttributes.Normal);
          File.Delete(file);
        }

        foreach (string dir in dirs)
          DeleteDirectoryFiles(dir);
      }
      catch (Exception exc)
      {
#if DEBUG
        MessageBox.Show(String.Format("Could not delete directory files: Exception Occurred :{0},{1}", exc.Message, exc.StackTrace.ToString()),
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
#endif
        throw exc;
      }
      return Result.Success;
    }

    public static void CopyFolderRecursive(string sourceFolder, string destFolder)
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

    public static bool IsValidEmail(string EmailAddress)
    {
      return (Regex.IsMatch(EmailAddress, "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$"));
    }

    public static string GetSHA1Hash(string strData)
    {
      SHA1 sha1 = new SHA1CryptoServiceProvider();
      byte[] byteData = Encoding.Default.GetBytes(strData);
      byte[] byteHashData = sha1.ComputeHash(byteData);
      string strHashData = "";
      foreach (byte b in byteHashData)
        strHashData += b.ToString() + ".";
      strHashData = strHashData.TrimEnd('.');
      return strHashData;
    }
  }
}
