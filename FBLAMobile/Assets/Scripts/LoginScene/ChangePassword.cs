using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Creates functionality for reseting password.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/15/2023
/// </remarks>
public class ChangePassword : MonoBehaviour
{
    [SerializeField, TextArea] private string passwordError;
    [SerializeField, TextArea] private string rePasswordError;

    [Header("References")]
    [SerializeField] private TMP_InputField input1;
    [SerializeField] private TMP_InputField input2;
    [SerializeField] private Toggle rememberToggle;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text errorText;

    public void ResetPassword()
    {
        string answer1 = input1.text;
        string answer2 = input2.text;
        bool remember = rememberToggle.isOn;

        if(!VerifyPassword(answer1, answer2))
        {
            return;
        }

        FileManager fileManager = FileManager.Instance;
        Account account = fileManager.AccountFile.Data;
        account.Password = answer1;
        fileManager.AccountFile.Save();

        if(remember)
        {
            fileManager.InitialDataFile.Save(new InitialData(account.ID));
        }
        TransitionManager.Instance.BasicTransition(Scenes.StudentView);
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
        if (len > 7 && len < 33 && password.Any(char.IsUpper) &&
            password.Any(char.IsLower) && password.Any(char.IsNumber))
        {
            if (password.Equals(rePassword))
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
    ///     Shows an error.
    /// </summary>
    /// <param name="msg">The error message to display.</param>
    private void ShowError(string msg)
    {
        errorText.SetText(msg);
        dialogueBox.SetActive(true);
    }
}
