using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SENSITServer
{
  /// <summary>
  /// Class representing a registered user for SENSIT Server application
  /// </summary>
  public class User
  {
    /// <summary>
    /// Password Hash of the user
    /// </summary>
    public string PasswordHash{get; private set;}

    /// <summary>
    /// Constructor of User class
    /// </summary>
    /// <param name="Text">Password of the user</param>
    /// <param name="IsPasswordHash">True if Text is the password hash, False if Text is the password</param>
    public User(string Text, bool IsPasswordHash)
    {
      if (!IsPasswordHash)
        this.PasswordHash = Util.GetSHA1Hash(Text);
      else
        this.PasswordHash = Text;
    }

    /// <summary>
    /// Used to change the Text of the user
    /// </summary>
    /// <param name="newPassword">New Text for the user</param>
    public void ChangePasword(string newPassword)
    {
      this.PasswordHash = Util.GetSHA1Hash(newPassword);
    }

    /// <summary>
    /// Compares the login credentials of the object user with that provided
    /// </summary>
    /// <param name="Text">Password to compare</param>
    /// <returns>true if both the login credentials match, false otherwise</returns>
    public bool Equals(string password)
    {
      return (this.PasswordHash.Equals(Util.GetSHA1Hash(password)));
    }

    /// <summary>
    /// Compares the login credentials of the object user with anotherUser
    /// </summary>
    /// <param name="anotherUser">User object to compare with</param>
    /// <returns>true if both the login credentials match, false otherwise</returns>
    public bool Equals(User anotherUser)
    {
      return (this.PasswordHash.Equals(anotherUser.PasswordHash));
    }
  }
}
