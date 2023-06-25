using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalendarAddEvent : MonoBehaviour
{
    [SerializeField] private HomeManager homeManager;
    [SerializeField] private GameObject calendarHome;
    [SerializeField] private NewCalendar newCalendar;
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

        DateTime time;
        if (timeText.Equals(string.Empty))
        {
            time = newCalendar.daySelected;
        }
        else
        {
            int year = newCalendar.daySelected.Year;
            int month = newCalendar.daySelected.Month;
            int day = newCalendar.daySelected.Day;
            string[] timeSelected = timeText.Split(":");

            int hour = int.Parse(timeSelected[0]);
            int minute = int.Parse(timeSelected[1]);

            if (AMPM == 0)
            {
                if (hour == 12) hour = 0;
            }
            else
            {
                hour += 12;
            }
            time = new DateTime(year, month, day, hour, minute, 0);
        }

        CalendarEvent calendarEvent = new (
            titleText,
            classID,
            locationText,
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
        title.SetText("Add Event " + newCalendar.daySelected.ToString("d"));
    }
}
