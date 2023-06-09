using System.IO;
using UnityEngine;

/// <summary>
///     Manages path and creates save/load methods.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/15/2023
/// </remarks>
/// <typeparam name="T">
///     The data class stored in the file.
/// </typeparam>
public class DataFile<T> where T : class
{
    /// <summary>
    ///     The absolute file path.
    /// </summary>
    private protected readonly string path;

    /// <summary>
    ///     The data.
    /// </summary>
    private T data;

    /// <summary>
    ///     The data stored.
    /// </summary>
    public T Data
    {
        get
        {
            // If it is loaded, simply return the data, otherwise retrieve it.
            if (IsLoaded) return data;
            Load();
            return data;
        }
    }

    /// <summary>
    ///     Checks if the file exists yet.
    /// </summary>
    public bool FileExists => File.Exists(path);

    /// <summary>
    ///     If the data has been loaded.
    /// </summary>
    public bool IsLoaded { get; private set; }

    /// <summary>
    ///     Saves data to nonvolatile memory.
    /// </summary>
    public void Save()
    {
        DataManager.SaveData(path, data);
    }

    /// <summary>
    ///     Overload method that saves
    ///     data provided in arguement.
    /// </summary>
    /// <param name="data">
    ///     The data to save.
    /// </param>
    public void Save(T saveData)
    {
        data = saveData;
        IsLoaded = true;
        DataManager.SaveData(path, data);
    }

    /// <summary>
    ///     Loads the data from the file.
    /// </summary>
    public void Load()
    {
        data = DataManager.LoadData<T>(path);
        IsLoaded = true;
    }

    /// <summary>
    ///     Constructor for DataFile class. 
    /// </summary>
    /// <param name="fileName">
    ///     Name of the file.
    /// </param>
    public DataFile(string fileName)
    {
        // Generates path name in the persistent data path folder (LocalLow in windows)
        path = Path.Combine(Application.persistentDataPath, fileName);
        IsLoaded = false;
    }
}