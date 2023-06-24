using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
///     Manages serializing and deserializing data 
///     to and from a non volatile state.
/// </summary>
/// <remarks>
///     <para>
///         This class was inspired by "SAVE & LOAD SYSTEM in Unity" 
///         by "Asbjørn Thirslund (Brackeys)" 2018.
///         
///         The members: <see cref="SaveData{T}(string, T)"/> and part of 
///         <see cref="LoadData{T}(string)"/> are creddited to Brackeys.
///         
///         <seealso href="https://www.youtube.com/watch?v=XOjd_qU2Ido"/>
///     </para>
///     <para>
///         Hanlin Zhang
///         Last Modified: 06/12/2023
///     </para>
/// </remarks>
public class DataTools
{
    /// <summary>
    ///     Serializes and saves data.
    /// </summary>
    /// <typeparam name="T"> Type of data </typeparam>
    /// <param name="path"> Path to save it to </param>
    /// <param name="data"> The data to save </param>
    public static void SaveData<T>(string path, T data)
    {
        BinaryFormatter formatter = new();
        FileStream stream = new(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    ///     Deserializes and loads data.
    /// </summary>
    /// <typeparam name="T"> Type of data </typeparam>
    /// <param name="path"> Path where data is saved at </param>
    /// <returns> The data </returns>
    public static T LoadData<T>(string path) where T : class
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            T data;

            // Make sure the leaderboard is not empty
            if (stream.Length != 0)
            {
                data = formatter.Deserialize(stream) as T;
            }
            else
            {
                Debug.LogError($"Save file: \"{path}\" is empty");
                return null;
            }

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError($"Save file: \"{path}\" not found");
            return null;
        }
    }
}