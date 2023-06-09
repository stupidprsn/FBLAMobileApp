using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/// <summary>
///     Creates functionality for the log in panel.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/15/2023
/// </remarks>
public class Login : MonoBehaviour
{
    [SerializeField, TextArea] private string error;

    [Header("References")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Toggle rememberToggle;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text errorText;

    /// <summary>
    ///     Creates functionality for login button.
    /// </summary>
    public void LoginButton()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        bool remember = rememberToggle.isOn;
        FileManager fileManager = FileManager.Instance;

        if(!fileManager.AccountDictionaryFile.IsLoaded)
        {
            fileManager.AccountDictionaryFile.Load();
        }

        AccountDictionary accountDictionary = fileManager.AccountDictionaryFile.Data;
        byte id = accountDictionary.GetID(username);
        if (id == 0) 
        {
            ShowError(error);
        } 
        else
        {
            fileManager.LoadAccount(id);
            if(fileManager.AccountFile.Data.Password.Equals(password))
            {
                if (remember)
                {
                    fileManager.InitialDataFile.Save(new InitialData(id));
                }
                
                TransitionManager.Instance.BasicTransition((int)fileManager.AccountFile.Data.AccountType + 1);
            }
            else
            {
                ShowError(error);
            }
        }

    }

    /// <summary>
    ///     Shows an error.
    /// </summary>
    /// <param name="msg">The error message to display.</param>
    private void ShowError(string msg)
    {
        errorText.SetText(msg);
        dialogueBox.SetActive(true);
    }
}
