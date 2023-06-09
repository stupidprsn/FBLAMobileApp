using System;
using System.IO;
using TMPro;
using UnityEngine;

/// <summary>
///     Retrieve username by ID.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/15/2023
/// </remarks>
public class ForgotUsername : MonoBehaviour
{
    [SerializeField, TextArea] private string showUsername;
    [SerializeField, TextArea] private string error;

    [Header("References")]
    [SerializeField] private TMP_InputField idInput;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text errorText;

    /// <summary>
    ///     Called by button.
    /// </summary>
    public void FindUsername()
    {
        DataFile<Account> temp = new DataFile<Account>(idInput.text + ".fbla");
        if(temp.FileExists)
        {
            temp.Load();
            ShowError(showUsername + temp.Data.Username);
        }
        else
        {
            ShowError(error);
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
