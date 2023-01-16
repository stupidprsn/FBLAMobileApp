using TMPro;
using UnityEngine;

/// <summary>
///     Creates functionality for the panel to create security questions.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/15/2023
/// </remarks>
public class CreateSecurityQuestions : MonoBehaviour
{
    [SerializeField, TextArea] private string error; 

    [Header("References")]
    [SerializeField] private TMP_InputField input1;
    [SerializeField] private TMP_InputField input2;
    [SerializeField] private LoginManager loginManager;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private GameObject AddClassPanel;

    /// <summary>
    ///     Creates the user account.
    /// </summary>
    public void CreateAccount()
    {
        string answer1 = input1.text;
        string answer2 = input2.text;
        if (!ValidateText(answer1, answer2)) return;

        FileManager fileManager = FileManager.Instance;

        if(!fileManager.AccountDictionaryFile.IsLoaded)
        {
            fileManager.AccountDictionaryFile.Load();
        }
        byte id = fileManager.AccountDictionaryFile.Data.SelectKey();

        Account accountInfo = loginManager.NewAccount;
        fileManager.CreateAccount(new Account(
            id,
            accountInfo.AccountType,
            accountInfo.Username,
            accountInfo.DisplayName,
            accountInfo.Password,
            answer1,
            answer2
        ));
        fileManager.AccountDictionaryFile.Data.AddName(accountInfo.Username, id);
        if(loginManager.Remember)
        {
            fileManager.InitialDataFile.Save(new InitialData(id));
        }

        loginManager.ChangePanel(AddClassPanel);
    }

    /// <summary>
    ///     Validates that the user answered both questions.
    /// </summary>
    /// <param name="text1">First Answer</param>
    /// <param name="text2">Second Answer</param>
    /// <returns>If the user answered both questions.</returns>
    private bool ValidateText(string text1, string text2)
    {
        if(text1.Length > 0 && text2.Length > 0)
        {
            return true;
        }
        else
        {
            ShowError(error);
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
