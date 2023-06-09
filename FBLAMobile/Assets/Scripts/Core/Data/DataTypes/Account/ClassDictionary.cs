using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
///     Keeps tracks of all accounts.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/15/2023
/// </remarks>
[Serializable]
public class ClassDictionary
{
    // List of available account IDs.
    private readonly List<short> availableKeys;

    /// <summary>
    ///     Returns a key and removes it from the list of available keys.
    /// </summary>
    /// <returns> The key. Returns 0 if no keys are available. </returns>
    public short SelectKey()
    {
        int count = availableKeys.Count;
        if(count == 0)
        {
            return 0;
        }

        int random = UnityEngine.Random.Range(0, count - 1);
        short key = availableKeys[random];
        availableKeys.RemoveAt(random);
        FileManager.Instance.ClassDictionaryFile.Save();
        return key;
    }

    /// <summary>
    ///     Basic constructor.
    /// </summary>
    public ClassDictionary() 
    {
        availableKeys = Enumerable.Range(1000, 9999).Select(x => (short)x).ToList();
    }
}