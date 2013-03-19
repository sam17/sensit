using System;
using System.Windows.Forms;

namespace SENSITServer
{
  public partial class FormLogin : Form
  {
    private User adminUser;

    /// <summary>
    /// Tells whether a user has successfully loged in
    /// </summary>
    public bool LoginSuccessful{get; private set;}
    
    /// <summary>
    /// Constructor for FormLogin
    /// </summary>
    /// <param name="AdminUser">Has AdminUser username and pasword hash</param>
    public FormLogin(User adminUser)
    {
      InitializeComponent();

      this.adminUser = adminUser;
    }

    private void HandleLogin()
    {
      User user = new User(this.txtPassword.Text, false);

      if ((LoginSuccessful = user.Equals(adminUser)) == false)
      {
        MessageBox.Show("Password mismatch.\n\n" + "Please send an e-mail to bittusrk@gmail.com requesting for the login Text in case you do not have it.\n" + Util.GetMethodNameAndLineNum(),
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
        this.txtPassword.Focus();
        this.txtPassword.SelectAll();
      }
      else
      {
        this.Close();
      }
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
      HandleLogin();  
    }

    private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == 13) // Enter key pressed
      {
        HandleLogin();
      }
    }

    private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == 13) // Enter key pressed
      {
        HandleLogin();
      }
    }
  }
}
