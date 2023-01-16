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
    [SerializeField, TextArea] private string userNameError;
    [SerializeField, TextArea] private string displayNameError;
    [SerializeField, TextArea] private string passwordError;
    [SerializeField, TextArea] private string rePasswordError;
    [SerializeField, TextArea] private string termsError;

    [Header("References")]
    [SerializeField] private LoginManager loginManager;
    [SerializeField] private GameObject securityQuestions;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_InputField userNameField;
    [SerializeField] private TMP_InputField displayNameField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_InputField rePasswordField;
    [SerializeField] private Toggle termsAgreeToggle;
    [SerializeField] private Toggle rememberToggle;

    /// <summary>
    ///     Attempts to create the account.
    /// </summary>
    public void Create()
    {
        dialogueBox.SetActive(false);

        string userName = userNameField.text;
        string displayName = displayNameField.text;
        string password = passwordField.text;
        string rePassword = rePasswordField.text;
        bool terms = termsAgreeToggle.isOn;
        bool remember = rememberToggle.isOn;

        if(!VerifyUserName(userName) ||
            !VerifyDisplayName(displayName) ||
            !VerifyPassword(password,rePassword))
        {
            return;
        }
        VerifyTerms(terms);

        loginManager.NewAccount.Username = userName;
        loginManager.NewAccount.DisplayName = displayName;
        loginManager.NewAccount.Password = displayName;
        loginManager.ChangePanel(securityQuestions);
    }

    /// <summary>
    ///     Checks if a username is alphanumeric and between 4-16 characters.
    /// </summary>
    /// <param name="userName">The username to check.</param>
    /// <returns>If the username is acceptable.</returns>
    private bool VerifyUserName(string userName)
    {
        int len = userName.Length;
        if(len > 3 && len < 17 && Regex.IsMatch(userName, "^[a-zA-Z0-9]*$"))
        {
            return true;
        }
        else
        {
            ShowError(userName);
            return false;
        }
    }

    /// <summary>
    ///     Check if a display name is between 4-16 characters.
    /// </summary>
    /// <param name="displayName">The name to check.</param>
    /// <returns>If the display name is acceptable</returns>
    private bool VerifyDisplayName(string displayName) 
    {
        int len = displayName.Length;
        if(len > 3 && len < 17)
        {
            return true;
        }
        else
        {
            ShowError(displayNameError);
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
                ShowError(rePasswordError);
                return false;
            }
        }
        else
        {
            ShowError(passwordError);
            return false;
        }
    }

    /// <summary>
    ///     Verify if the user agreed to the terms and conditions.
    /// </summary>
    /// <param name="terms">If the user agreed.</param>
    private void VerifyTerms(bool terms)
    {
        if (terms) ShowError(termsError);
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
