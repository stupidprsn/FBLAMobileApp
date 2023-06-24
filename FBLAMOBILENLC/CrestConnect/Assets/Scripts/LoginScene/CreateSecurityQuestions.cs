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
    [SerializeField] private TMP_InputField input1, input2;
    [SerializeField] private LoginManager loginManager;
    [SerializeField] private DialogueBox dialogueBox;

    private FileManager fileManager;
    private TransitionManager transitionManager;

    /// <summary>
    ///     Creates the user account.
    /// </summary>
    public void CreateAccount()
    {
        // Gather user input
        string answer1 = input1.text;
        string answer2 = input2.text;
        if (!ValidateText(answer1, answer2)) return;

        // Create account
        Account accountInfo = loginManager.NewAccount;
        fileManager.CreateAccount(new Account(
            accountInfo.AccountType,
            accountInfo.Username,
            accountInfo.DisplayName,
            accountInfo.Password,
            accountInfo.ProfilePicture,
            answer1,
            answer2
        ));


        // Remember the account
        if(loginManager.Remember)
        {
            fileManager.InitialDataFile.Save(new InitialData(accountInfo.Username, accountInfo.AccountType));
        }

        transitionManager.BasicTransition(Scenes.StudentView);
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
            dialogueBox.Enable(error);
            return false;
        }
    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        transitionManager = SingletonManager.Instance.TransitionManagerInstance;
    }
}
