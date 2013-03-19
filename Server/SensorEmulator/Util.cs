using System;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SensorEmulator
{
  enum Result
  {
    Success,
    Failure
  }

  class Util
  {
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
        MessageBox.Show("Encryption failed!\n" + exc.Message,
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
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
        MessageBox.Show("Decryption failed!\n" + exc.Message,
                        "SENSIT Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
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

    [Obsolete]
    public static string StrToHex(string str)
    {
      try
      {
        string hex = "";
        foreach (char c in str)
        {
          int tmp = c;
          hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
        }
        return hex;
      }
      catch
      {
        return null;
      }
    }

    [Obsolete]
    public static string HexToStr(string hex)
    {
      try
      {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i <= hex.Length - 2; i += 2)
        {
          sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(i, 2), System.Globalization.NumberStyles.HexNumber))));
        }
        return sb.ToString();
      }
      catch
      {
        return null;
      }
    }

    [Obsolete]
    public static float HexToFloat(string hex)
    {
      try
      {
        byte[] bb = Util.HexToByteArr(hex);
        return BitConverter.ToSingle(bb, 0);
      }
      catch
      {
        return 0;
      }
    }

    [Obsolete]
    public static string FloatToHex(float real)
    {
      try
      {
        byte[] bb = BitConverter.GetBytes(real);
        return Util.ByteArrToHex(bb);
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

    public static DialogResult InputBox(string title, string promptText, ref string value)
    {
      Form form = new Form();
      Label label = new Label();
      TextBox textBox = new TextBox();
      Button buttonOk = new Button();
      Button buttonCancel = new Button();

      form.Text = title;
      label.Text = promptText;
      textBox.Text = value;

      buttonOk.Text = "OK";
      buttonCancel.Text = "Cancel";
      buttonOk.DialogResult = DialogResult.OK;
      buttonCancel.DialogResult = DialogResult.Cancel;

      label.SetBounds(9, 20, 372, 13);
      textBox.SetBounds(12, 36, 372, 20);
      buttonOk.SetBounds(228, 72, 75, 23);
      buttonCancel.SetBounds(309, 72, 75, 23);

      label.AutoSize = true;
      textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
      buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

      form.ClientSize = new Size(396, 107);
      form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
      form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
      form.FormBorderStyle = FormBorderStyle.FixedDialog;
      form.StartPosition = FormStartPosition.CenterScreen;
      form.MinimizeBox = false;
      form.MaximizeBox = false;
      form.AcceptButton = buttonOk;
      form.CancelButton = buttonCancel;

      DialogResult dialogResult = form.ShowDialog();
      value = textBox.Text;
      return dialogResult;
    }
  }
}
