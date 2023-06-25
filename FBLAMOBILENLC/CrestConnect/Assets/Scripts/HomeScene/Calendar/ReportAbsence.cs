using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReportAbsence : MonoBehaviour
{
    [SerializeField] private HomeManager homeManager;
    [SerializeField] private GameObject calendarHome;
    [SerializeField] private NewCalendar newCalendar;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_InputField titleInput, notesInput;
    [SerializeField] private TMP_Dropdown selectClassDropdown, reasonDropdown;

    private FileManager fileManager;
    private List<int> inClasses;

    /// <summary>
    ///     Loads all of the user's owned classes into the select class dropdown.
    /// </summary>
    public void LoadClassDropdown()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        selectClassDropdown.ClearOptions();
        inClasses = fileManager.AccountFile.Data.InClasses;

        List<string> inClassesString = inClasses.ConvertAll<string>(x => fileManager.ClassDictionaryFile.Data.GetClassName(x));
        selectClassDropdown.AddOptions(inClassesString);
    }

    public void FinalizeButton()
    {
        Account account = fileManager.AccountFile.Data;
        string titleText = titleInput.text;
        string notesText = notesInput.text;
        string reasonText = reasonDropdown.options[reasonDropdown.value].text;
        int classID = account.InClasses[selectClassDropdown.value];

        DateTime time = newCalendar.daySelected;

        CalendarAbsence calendarEvent = new(
            titleText,
            classID,
            reasonText,
            time,
            notesText
        );

        fileManager.CreateCalendarEvent(calendarEvent);
        newCalendar.DateSelect(time.Day);

        homeManager.ResetScreens();
        homeManager.ChangePanel(calendarHome);
    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        LoadClassDropdown();
    }

    private void OnEnable()
    {
        title.SetText("Report absence " + newCalendar.daySelected.ToString("d"));
    }
}
