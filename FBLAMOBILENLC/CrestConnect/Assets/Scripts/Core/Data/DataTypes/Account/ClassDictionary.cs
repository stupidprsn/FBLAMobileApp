using System;
using System.Collections.Generic;

/// <summary>
///     Keeps tracks of all classes.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 06/16/2023
/// </remarks>
[Serializable]
public class ClassDictionary
{
    private readonly Dictionary<int, string> classes;
    private int currentValue;

    /// <summary>
    ///     Checks if there are still keys avaliable.
    /// </summary>
    /// <returns>If there are keys still avaliable</returns>
    public bool KeyAvaliable()
    {
        if(currentValue < 9999)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///     Returns a key.
    /// </summary>
    /// <returns> The key. </returns>
    public int SelectKey()
    {
        currentValue += UnityEngine.Random.Range(1, 100);
        return currentValue;
    }

    /// <summary>
    ///     Adds a class to the class dictionary.
    /// </summary>
    /// <param name="code">Join code</param>
    /// <param name="displayName">Display Name</param>
    public void AddClass(int code, string displayName)
    {
        classes.Add(code, displayName);
    }

    /// <summary>
    ///     Checks if a join code is valid.
    /// </summary>
    /// <param name="code">Join code</param>
    /// <returns>If the join code is valid./returns>
    public bool CodeExists(int code)
    {
        return classes.ContainsKey(code);
    }

    /// <summary>
    ///     Returns the name of a class via its code.
    /// </summary>
    /// <param name="code">Join Code</param>
    /// <returns>Class Name</returns>
    public string GetClassName(int code)
    {
        return classes[code];
    }

    /// <summary>
    ///     Basic constructor.
    /// </summary>
    public ClassDictionary() 
    {
        classes = new();
        currentValue = 1000;
    }
}