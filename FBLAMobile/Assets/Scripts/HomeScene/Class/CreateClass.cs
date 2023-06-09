using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateClass : MonoBehaviour
{
    private FileManager fileManager;
    [SerializeField] HomeManager homeManager;

    [SerializeField] GameObject successDialogue;
    [SerializeField] TMP_Text successText;
    [SerializeField] GameObject failDialogue;
    [SerializeField] TMP_InputField createClassName;
    [SerializeField] TMP_InputField socialInput;

    public void FinalizeClass()
    {
        string newName = createClassName.text;
        string newSocial = socialInput.text;

        if (newName.Length > 3 && newName.Length < 17)
        {
            if (!fileManager.ClassDictionaryFile.IsLoaded) fileManager.ClassDictionaryFile.Load();
            short joinCode = fileManager.ClassDictionaryFile.Data.SelectKey();
            fileManager.AccountFile.Data.OwnedClasses.Add(joinCode);
            fileManager.AccountFile.Save();
            DataFile<AClass> newClass = new DataFile<AClass>(joinCode + ".fbla");
            if(!string.IsNullOrEmpty(newSocial))
            {
                newClass.Data.social = newSocial;
                newClass.Save();
            }
            newClass.Save(new AClass(joinCode, fileManager.AccountFile.Data.ID, newName));
            fileManager.OwnClassList.Add(newClass);
            successText.SetText("The join code for your new class is: " + joinCode);
            successDialogue.SetActive(true);
            homeManager.ResetScreens();
        }
        else
        {
            failDialogue.SetActive(true);
        }
    }

    private void Start()
    {
        fileManager = FileManager.Instance;
    }
}
