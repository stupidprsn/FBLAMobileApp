using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using System.Linq;

/// <summary>
///     Manages creating an account.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/13/2023
/// </remarks>
public class CreateAccount : MonoBehaviour
{
    [Header("Error Messages")]
    [SerializeField, TextArea] private string usernameError, usernameTakenError, displayNameError, passwordError, rePasswordError, termsError;

    [Header("References")]
    [SerializeField] private LoginManager loginManager;
    [SerializeField] private GameObject nextPanel;
    [SerializeField] private TMP_InputField userNameField, passwordField, rePasswordField;
    [SerializeField] private Toggle termsAgreeToggle, rememberToggle;
    [SerializeField] private DialogueBox dialogueBox;

    private FileManager fileManager; 

    /// <summary>
    ///     Attempts to create the account.
    /// </summary>
    public void Create()
    {
        // Gather user input
        string userName = userNameField.text;
        //string displayName = displayNameField.text;
        string password = passwordField.text;
        string rePassword = rePasswordField.text;
        bool terms = termsAgreeToggle.isOn;
        bool remember = rememberToggle.isOn;

        // Verify user input
        if(!VerifyUserName(userName) ||
            //!VerifyDisplayName(displayName) ||
            !VerifyPassword(password,rePassword) ||
            !VerifyTerms(terms))
        {
            return;
        }

        // Record user input
        loginManager.NewAccount.Username = userName;
        //loginManager.NewAccount.DisplayName = displayName;
        loginManager.NewAccount.Password = password;
        loginManager.Remember = remember;

        // Move to next step
        loginManager.ChangePanel(nextPanel);
    }

    /// <summary>
    ///     Checks if a username is alphanumeric and between 4-16 characters and is not already taken.
    /// </summary>
    /// <param name="userName">The username to check.</param>
    /// <returns>If the username is acceptable.</returns>
    private bool VerifyUserName(string username)
    {
        int len = username.Length;
        if(len > 3 && len < 17 && Regex.IsMatch(username, "^[a-zA-Z0-9]*$"))
        {
            if(fileManager.AccountDictionaryFile.Data.UsernameExists(username))
            {
                dialogueBox.Enable(usernameTakenError);
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            dialogueBox.Enable(usernameError);
            return false;
        }
    }

    /// <summary>
    ///     Checks if the password has at least one uppercase letter, lowercase letter, number
    ///     is between 8-32 characters and matches.
    /// </summary>
    /// <param name="password">The first password</param>
    /// <param name="rePassword">The second password</param>
    /// <returns>If the password is acceptable.</returns>
    private bool VerifyPassword(string password, string rePassword)
    {
        int len = password.Length;
        if(len > 7 && len < 33 && password.Any(char.IsUpper) && 
            password.Any(char.IsLower) && password.Any(char.IsNumber))
        {
            if(password.Equals(rePassword))
            {
                return true;
            }
            else
            {
                dialogueBox.Enable(rePasswordError);
                return false;
            }
        }
        else
        {
            dialogueBox.Enable(passwordError);
            return false;
        }
    }

    /// <summary>
    ///     Verify if the user agreed to the terms and conditions.
    /// </summary>
    /// <param name="terms">If the user agreed.</param>
    private bool VerifyTerms(bool terms)
    {
        if (terms) 
        {
            return true;
        }
        else
        {
            dialogueBox.Enable(termsError);
            return false;
        }

    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
    }

}
