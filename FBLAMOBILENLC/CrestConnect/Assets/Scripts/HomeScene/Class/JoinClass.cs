using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
///     Allows the user to join a new class.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/16/2023
/// </remarks>
public class JoinClass : MonoBehaviour
{
    [Header("Text")]
    [SerializeField, TextArea] private string SuccessText, InputError, InvalidError;

    [Header("References")]
    [SerializeField] private TMP_InputField inputCode;
    [SerializeField] private HomeManager homeManager;
    [SerializeField] private GameObject classesPanel;
    [SerializeField] private DialogueBox dialogueBox;

    private FileManager fileManager;

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
    }

    public void FinalizeJoin()
    {
        // Get input
        string code = inputCode.text;

        // Validate name
        if (!ValidateCode(code)) return;

        // Add Class to member and member to class
        DataFile<Account> accountFile = fileManager.AccountFile;
        accountFile.Data.InClasses.Add(int.Parse(code));
        accountFile.Save();

        DataFile<AClass> classFile = fileManager.GetClassFile(code);
        classFile.Data.members.Add(accountFile.Data.Username);
        classFile.Save();

        dialogueBox.Enable(SuccessText, () => { homeManager.ChangePanel(classesPanel); });

        homeManager.ResetScreens();
    }

    private bool ValidateCode(string code)
    {
        // Check 4 digit and all number
        if (code.Length == 4 && code.All(char.IsDigit))
        {
            // Check code refers to a class.
            DataFile<ClassDictionary> classDictionary = fileManager.ClassDictionaryFile;
            if (!classDictionary.IsLoaded) classDictionary.Load();
            if(classDictionary.Data.CodeExists(int.Parse(code)))
            {
                return true;
            }
            else
            {
                dialogueBox.Enable(InvalidError);
                return false;
            }
        }
        else
        {
            dialogueBox.Enable(InputError);
            return false;
        }
    }
}
