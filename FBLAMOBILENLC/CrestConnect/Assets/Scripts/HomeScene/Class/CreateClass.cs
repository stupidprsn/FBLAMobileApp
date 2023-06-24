using UnityEngine;
using TMPro;
using System;

/// <summary>
///     Manages creating classes.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/17/2023
/// </remarks>
public class CreateClass : MonoBehaviour
{
    [Header("Error Text")]
    [SerializeField, TextArea] private string SuccessText, ClassNameErrorText;

    [Header("References")]
    [SerializeField] private HomeManager homeManager;
    [SerializeField] private GameObject classesPanel;
    [SerializeField] private TMP_InputField createClassName;
    [SerializeField] private DialogueBox dialogueBox;

    private FileManager fileManager;

    /// <summary>
    ///     Creates a class if the class name is valid. 
    /// </summary>
    public void PublishButton()
    {
        // Get input
        string newName = createClassName.text;

        // Validate name
        if (!ValidateName(newName)) return;

        // Get class join code
        DataFile<ClassDictionary> classDictionary = fileManager.ClassDictionaryFile;
        if (!classDictionary.IsLoaded) classDictionary.Load();
        int joinCode = classDictionary.Data.SelectKey();
        Debug.Log(joinCode);
        classDictionary.Data.AddClass(joinCode, newName);
        classDictionary.Save();
        
        // Add class to owned list
        fileManager.AccountFile.Data.OwnedClasses.Add(joinCode);
        fileManager.AccountFile.Save();

        // Create class
        DataFile<AClass> newClass = fileManager.GetClassFile(joinCode);

        newClass.Save(new AClass(joinCode, fileManager.AccountFile.Data.Username, newName));
        dialogueBox.Enable(SuccessText + joinCode.ToString(), () => { homeManager.ChangePanel(classesPanel); });

        homeManager.ResetScreens();
    }

    private bool ValidateName(string name)
    {
        if (name.Length > 3 && name.Length < 17)
        {
            return true;
        }
        else
        {
            dialogueBox.Enable(ClassNameErrorText);
            return false;
        }
    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
    }
}
