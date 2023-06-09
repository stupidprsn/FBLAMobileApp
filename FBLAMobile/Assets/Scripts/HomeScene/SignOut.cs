using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignOut : MonoBehaviour
{
    public void SignOutButton()
    {
        FileManager fileManager = FileManager.Instance;
        fileManager.InitialDataFile.Save(new InitialData());
        TransitionManager.Instance.BasicTransition(Scenes.Login);
    }
}
