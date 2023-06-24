using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarAddEvent : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_InputField titleInput, locationInput, timeInput, notesInput;
    [SerializeField] private TMP_Dropdown selectClassDropdown, AMPMDropdown;

    private FileManager fileManager;
    private List<int> ownedClasses;

    /// <summary>
    ///     Loads all of the user's owned classes into the select class dropdown.
    /// </summary>
    public void LoadClassDropdown()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        selectClassDropdown.ClearOptions();
        ownedClasses = fileManager.AccountFile.Data.OwnedClasses;

        List<string> ownedClassesString = ownedClasses.ConvertAll<string>(x => fileManager.ClassDictionaryFile.Data.GetClassName(x));
        selectClassDropdown.AddOptions(ownedClassesString);
    }

    public void FinalizeButton()
    {
        Account account = fileManager.AccountFile.Data;
        string titleText = titleInput.text;
        string locationText = locationInput.text;
        string timeText = timeInput.text;
        string notesText = notesInput.text;
        int AMPM = AMPMDropdown.value;

        int classID = account.OwnedClasses[selectClassDropdown.value];

        CalendarEvent calendarEvent = new CalendarEvent(
            titleText,
            locationText,
            new DateTime(),
            notesText);

        //if (imageSelected)
        //{
        //    feedPost = new FeedPost(
        //        classID,
        //        account.DisplayName,
        //        account.Username,
        //        text,
        //        DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"),
        //        rawImage
        //        );
        //}
        //else
        //{
        //    feedPost = new FeedPost(
        //        classID,
        //        account.DisplayName,
        //        account.Username,
        //        text,
        //        DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt")
        //    );
        //}
        //imageSelected = false;
        //fileManager.CreatePost(feedPost);

        //homeManger.ResetScreens();
        //homeManger.ChangePanel(feedPanel);
    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        LoadClassDropdown();
    }
}
