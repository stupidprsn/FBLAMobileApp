using TMPro;
using UnityEngine;

/// <summary>
///     Creates functionality for checking the security questions.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/15/2023
/// </remarks>
public class ResetPassword : MonoBehaviour
{
    [SerializeField, TextArea] private string error;
    [Header("References")]
    [SerializeField] private LoginManager loginManager;
    [SerializeField] private GameObject changePasswordPanel;
    [SerializeField] private TMP_InputField input1;
    [SerializeField] private TMP_InputField input2;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text errorText;

    public void CheckQuestions()
    {
        string answer1 = input1.text;
        string answer2 = input2.text;

        FileManager fileManager = FileManager.Instance;
        Account account = fileManager.AccountFile.Data;

        if(answer1.Equals(account.Security1) && answer2.Equals(account.Security2))
        {
            loginManager.ChangePanel(changePasswordPanel);
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
