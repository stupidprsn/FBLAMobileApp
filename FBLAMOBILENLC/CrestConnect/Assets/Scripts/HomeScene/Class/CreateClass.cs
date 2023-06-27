using UnityEngine;
using TMPro;
using System.Collections.Generic;

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

        AClass aClass = new AClass(joinCode, fileManager.AccountFile.Data.Username, newName);
        if(newName.Equals("FBLA"))
        {
            aClass.text = new List<string>()
            {
                "Description",
                "Future Business Leaders of America (FBLA) is a dynamic and prestigious organization that empowers young individuals to become future leaders in the business world. FBLA provides a platform for students to develop essential skills such as leadership, communication, and entrepreneurship. With a strong emphasis on practical experience, FBLA offers a range of activities including conferences, workshops, and competitions where members can showcase their talents and network with industry professionals. By fostering a supportive and innovative community, FBLA cultivates a passion for business and prepares its members for successful careers in various fields. Through its dedication to education and leadership development, FBLA shapes the next generation of business leaders with confidence, integrity, and vision.",
                "How to Join",
                "Applications for new and existing members open in October of every school year. Contact Jasmine Lane for more details.\r\n"
            };
        }

        newClass.Save(aClass);
        dialogueBox.Enable(SuccessText + joinCode.ToString(), () => { homeManager.ChangePanel(classesPanel); });

        homeManager.ResetScreens();
    }

    private bool ValidateName(string name)
    {
        if (name.Length > 3 && name.Length < 33)
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
