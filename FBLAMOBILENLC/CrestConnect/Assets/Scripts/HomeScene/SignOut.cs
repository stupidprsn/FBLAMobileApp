using UnityEngine;

/// <summary>
///     Allows the user to sign out of their account.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/16/2023
/// </remarks>
public class SignOut : MonoBehaviour
{
    private FileManager fileManager;
    private TransitionManager transitionManager;

    /// <summary>
    ///     Signs the user out.
    /// </summary>
    public void SignOutButton()
    {
        fileManager.InitialDataFile.Save(new InitialData());
        transitionManager.BasicTransition(Scenes.Login);
    }

    private void Start()
    {
        SingletonManager singletonManager = SingletonManager.Instance;
        fileManager = singletonManager.FileManagerInstance;
        transitionManager = singletonManager.TransitionManagerInstance;
    }
}
