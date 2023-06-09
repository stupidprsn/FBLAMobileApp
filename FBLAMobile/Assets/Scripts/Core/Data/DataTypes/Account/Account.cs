using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
///     Stores data regarding a user's account.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/14/2023
/// </remarks>
[Serializable]
public class Account
{
    /// <summary>
    ///     The user's internal ID.
    /// </summary>
    public byte ID { get; private set; }
    /// <summary>
    ///     The account's type.
    /// </summary>
    public AccountType AccountType { get; set; }
    /// <summary>
    ///     The user's username (used for sign in).
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    ///     THe user's display name.
    /// </summary>
    public string DisplayName { get; set; }
    /// <summary>
    ///     The user's password.
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    ///     The user's answer to their first security question.
    /// </summary>
    public string Security1 { get; set; }
    /// <summary>
    ///     The user's answer to their second security question.
    /// </summary>
    public string Security2 { get; set; }

    public List<short> OwnedClasses { get; set; }
    public List<short> InClasses { get; set; }

    public void JoinClass(short joincode)
    {
        InClasses.Add(joincode);
        DataFile<AClass> file = new DataFile<AClass>(joincode.ToString() + ".fbla");
        file.Load();
        file.Data.members.Add(ID);
        file.Save();
        FileManager.Instance.AccountFile.Save();
    }

    public void LeaveClass(bool isOwn, short joincode)
    {
        if(isOwn)
        {
            OwnedClasses.Remove(joincode);
        }
        else
        {
            InClasses.Remove(joincode);
        }
        FileManager.Instance.AccountFile.Save();
    }

    /// <summary>
    ///     Default constructor.
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="type">The account type</param>
    /// <param name="username">Username</param>
    /// <param name="displayName">Display name</param>
    /// <param name="password">Password</param>
    /// <param name="security1">Answer to security question 1.</param>
    /// <param name="security2">Answer to security question 2.</param>
    public Account(byte id, AccountType type, string username, string displayName, string password, string security1, string security2)
    {
        this.ID = id;
        this.AccountType = type;
        this.Username = username;
        this.DisplayName = displayName;
        this.Password = password;
        this.Security1 = security1;
        this.Security2 = security2;
        OwnedClasses = new List<short>();
        InClasses= new List<short>();

        FileManager.Instance.AccountDictionaryFile.Data.AddName(username, id);
    }

    public Account()
    {

    }
}