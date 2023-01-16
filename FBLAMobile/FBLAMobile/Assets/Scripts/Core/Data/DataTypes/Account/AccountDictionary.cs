using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
///     Keeps tracks of all accounts.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/14/2023
/// </remarks>
[Serializable]
public class AccountDictionary
{
    // Connects every username with its account ID.
    private readonly Dictionary<string, byte> namesDictionary;
    // List of available account IDs.
    private readonly List<byte> availableKeys;

    /// <summary>
    ///     Adds a name to the user dictionary.
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="id">ID</param>
    public void AddName(string username, byte id)
    {
        namesDictionary.Add(username, id);
        FileManager.Instance.AccountDictionaryFile.Save();
    }

    /// <summary>
    ///     Returns the ID associated with a username.
    /// </summary>
    /// <param name="name">The username.</param>
    /// <returns>Returns 0 if the username does not exist, returns the id if it does.</returns>
    public byte GetID(string name)
    {
        if(namesDictionary.TryGetValue(name, out byte id))
        {
            return id;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    ///     Returns a key and removes it from the list of available keys.
    /// </summary>
    /// <returns> The key. Returns 0 if no keys are available. </returns>
    public byte SelectKey()
    {
        int count = namesDictionary.Count;
        if(count == 0)
        {
            return 0;
        }

        int random = UnityEngine.Random.Range(0, count - 1);
        byte key = availableKeys[random];
        availableKeys.RemoveAt(random);
        FileManager.Instance.AccountDictionaryFile.Save();
        return key;
    }

    /// <summary>
    ///     Basic constructor.
    /// </summary>
    public AccountDictionary() 
    {
        namesDictionary = new Dictionary<string, byte>();
        availableKeys = Enumerable.Range(1, 255).Select(x => (byte) x).ToList();
    }
}