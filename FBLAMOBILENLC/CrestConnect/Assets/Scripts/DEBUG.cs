using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     Used for debugging, makes app load without starting
///     in "Login" scene.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/17/2023
/// </remarks>
public class DEBUG : MonoBehaviour
{
    [SerializeField] private FileManager fileManager;

    static bool hasRan = false;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Login" || hasRan) return;
        DelayedStart();

    }

    private void DelayedStart()
    {
        InitialData initialData = fileManager.InitialDataFile.Data;
        if(initialData.SignedIn)
        {
            fileManager.LoadAccount(initialData.Username);

        }
        else
        {
            fileManager.LoadAccount("debug");
        }
        hasRan = true;
    }
}
