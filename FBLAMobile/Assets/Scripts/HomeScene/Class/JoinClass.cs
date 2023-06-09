using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class JoinClass : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputCode;
    [SerializeField] private GameObject successDialogue;
    [SerializeField] private GameObject failDialogue;
    [SerializeField] private HomeManager homeManager;
    [SerializeField] private ClassesPage classesPage;

    private FileManager fileManager;
    private DataFile<Account> account;
    private void Start()
    {
        fileManager = FileManager.Instance;
        account = fileManager.AccountFile;
    }

    public void FinalizeJoin()
    {
        string newName = inputCode.text;
        DataFile<AClass> temp = new DataFile<AClass>(newName + ".fbla");
        if (temp.FileExists)
        {
            temp.Load();
            account.Data.InClasses.Add(short.Parse(newName));
            account.Save();
            temp.Data.members.Add(account.Data.ID);
            temp.Save();
            successDialogue.SetActive(true);
            homeManager.ResetScreens();
            return;
        }
        else
        {
            failDialogue.SetActive(true);

        }

    }
}
