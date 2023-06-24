using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private HomeManager homeManager;
    [SerializeField] private GameObject navBar;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject chatPreset;
    [SerializeField] private GameObject individualChat;

    FileManager fileManager;
    List<DataFile<Chat>> allChats;

    private void Start()
    {
        //LoadChats();
        fileManager = SingletonManager.Instance.FileManagerInstance;
    }

    //private void LoadChats()
    //{
    //    List<byte> chats = new();
    //    allChats = new List<DataFile<Chat>>();


    //    AClass aClass;

    //    foreach (DataFile<AClass> i in fileManager.OwnClassList)
    //    {
    //        aClass = i.Data;
    //        foreach (byte j in aClass.members)
    //        {
    //            chats.Add(j);
    //        }
    //    }

    //    foreach (DataFile<AClass> i in fileManager.InClassList)
    //    {
    //        chats.Add(i.Data.owner);
    //    }

    //    string id = fileManager.AccountFile.Data.ID.ToString();
    //    DataFile<Chat> temp;
    //    foreach (Transform child in content)
    //    {
    //        Destroy(child.gameObject);
    //    }
    //    foreach (byte person in chats)
    //    {
    //        temp = new DataFile<Chat>(Path.Combine("Chat", id + "-" + person.ToString() + ".fbla"));
    //        allChats.Add(temp);
    //        if (!temp.FileExists)
    //        {
    //            temp.Save(new Chat());
    //        }

    //        DataFile<Account> tempAcc = new DataFile<Account>(Path.Combine(person.ToString() + ".fbla"));
    //        tempAcc.Load();

    //        GameObject newChatOption = Instantiate(chatPreset, content);
    //        newChatOption.GetComponentInChildren<TMP_Text>().SetText(tempAcc.Data.DisplayName);
    //        newChatOption.GetComponent<Button>().onClick.AddListener(() => {
    //            homeManager.ChangePanel(individualChat);
    //            navBar.SetActive(false);
    //        });

    //    }
    //}

}
