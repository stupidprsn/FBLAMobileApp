using System;
using System.Collections.Generic;

/// <summary>
///     Keeps tracks of all accounts.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 06/15/2023
/// </remarks>
[Serializable]
public class AccountDictionary
{
    // Connects every username with its account ID.
    private readonly Dictionary<string, string> namesDictionary;

    /// <summary>
    ///     Adds a name to the user dictionary.
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="displayName">Display Name</param>
    public void AddName(string username, string displayName)
    {
        namesDictionary.Add(username, displayName);
    }

    public bool UsernameExists(string username)
    {
        return namesDictionary.ContainsKey(username);
    }

    public string GetDisplayName(string username)
    {
        return namesDictionary[username];
    }

    /// <summary>
    ///     Basic constructor.
    /// </summary>
    public AccountDictionary() 
    {
        namesDictionary = new Dictionary<string, string>();
    }
}