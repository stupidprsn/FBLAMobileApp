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
        LoadChats();
    }

    public void LoadChats()
    {
        List<byte> chats = new List<byte>();
        allChats = new List<DataFile<Chat>>();

        fileManager = FileManager.Instance;

        AClass aClass;

        if(fileManager.AccountFile.Data.AccountType == AccountType.Teacher)
        {
            foreach (DataFile<AClass> i in fileManager.OwnClassList)
            {
                aClass = i.Data;
                foreach (byte j in aClass.members)
                {
                    chats.Add(j);
                }
            }
        }
        else
        {
            foreach (DataFile<AClass> i in fileManager.InClassList)
            {
                Debug.Log(i.Data.owner);
                chats.Add(i.Data.owner);
            }
        }

        string id = fileManager.AccountFile.Data.ID.ToString();
        DataFile<Chat> chatFiles;
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        foreach (byte person in chats)
        {
            Debug.Log(person);
            chatFiles = new DataFile<Chat>(id.ToString() + "-" + person.ToString() + ".fbla");
            allChats.Add(chatFiles);
            if (!chatFiles.FileExists)
            {
                chatFiles.Save(new Chat());
            }

            DataFile<Account> tempAcc = new DataFile<Account>(Path.Combine(person.ToString() + ".fbla"));
            tempAcc.Load();

            GameObject newChatOption = Instantiate(chatPreset, content);
            newChatOption.GetComponentInChildren<TMP_Text>().SetText(tempAcc.Data.DisplayName);
            newChatOption.GetComponent<Button>().onClick.AddListener(() => {
                homeManager.ChangePanel(individualChat);
                navBar.SetActive(false);
            });

        }
    }

}
