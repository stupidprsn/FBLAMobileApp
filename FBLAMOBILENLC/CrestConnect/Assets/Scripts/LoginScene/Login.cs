using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField] private LoginManager loginManager;
    [SerializeField] private TMP_InputField usernameInput, passwordInput;
    [SerializeField] private Toggle rememberToggle;
    [SerializeField] private DialogueBox dialogueBox;

    private FileManager fileManager;

    /// <summary>
    ///     Creates functionality for login button.
    /// </summary>
    public void LoginButton()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        bool remember = rememberToggle.isOn;

        if(!fileManager.AccountDictionaryFile.IsLoaded) fileManager.AccountDictionaryFile.Load();
        AccountDictionary accountDictionary = fileManager.AccountDictionaryFile.Data;

        if(!accountDictionary.UsernameExists(username))
        {
            dialogueBox.Enable(error);
            return;
        }
        else
        {
            fileManager.LoadAccount(username);
            Account account = fileManager.AccountFile.Data;
            if (account.Password.Equals(password))
            {
                if (remember)
                {
                    fileManager.InitialDataFile.Save(new InitialData(username, account.AccountType));
                }
                loginManager.Login();
            }
            else
            {
                dialogueBox.Enable(error);
            }
        }

    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
    }
}
