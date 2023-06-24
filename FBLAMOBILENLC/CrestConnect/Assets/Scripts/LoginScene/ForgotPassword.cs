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
    [SerializeField] private DialogueBox dialogueBox;

    private FileManager fileManager;

    /// <summary>
    ///     Called by button.
    /// </summary>
    public void FindAccount()
    {
        string username = usernameInput.text;

        if(!fileManager.AccountDictionaryFile.IsLoaded)
        {
            fileManager.AccountDictionaryFile.Load();
        }
        AccountDictionary accountDictionary = fileManager.AccountDictionaryFile.Data;
        if(accountDictionary.UsernameExists(username))
        {
            dialogueBox.Enable(error);
        }
        else
        {
            fileManager.LoadAccount(username);
            loginManager.ChangePanel(securityPanel);   
        }
    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
    }
}
