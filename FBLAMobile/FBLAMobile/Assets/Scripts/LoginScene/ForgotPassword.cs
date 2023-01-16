using System;
using System.IO;
using TMPro;
using UnityEngine;

/// <summary>
///     Reset password.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/15/2023
/// </remarks>
public class ForgotPassword : MonoBehaviour
{
    [SerializeField, TextArea] private string error;

    [Header("References")]
    [SerializeField] private LoginManager loginManager;
    [SerializeField] private GameObject securityPanel;
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text errorText;

    /// <summary>
    ///     Called by button.
    /// </summary>
    public void FindAccount()
    {
        string username = usernameInput.text;
        FileManager fileManager = FileManager.Instance;
        if(!fileManager.AccountDictionaryFile.IsLoaded)
        {
            fileManager.AccountDictionaryFile.Load();
        }
        AccountDictionary accountDictionary = fileManager.AccountDictionaryFile.Data;
        byte id = accountDictionary.GetID(username);
        if(id == 0)
        {
            ShowError(error);
        }
        else
        {
            fileManager.LoadAccount(id);
            loginManager.ChangePanel(securityPanel);   
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
