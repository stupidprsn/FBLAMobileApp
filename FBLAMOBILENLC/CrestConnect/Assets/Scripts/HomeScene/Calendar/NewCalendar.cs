using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewCalendar : MonoBehaviour
{
    [SerializeField] List<GameObject> empties;
    [SerializeField] List<GameObject> finalButtons;

    [SerializeField] TMP_Text dayTitle;
    [SerializeField] TMP_Text monthTitle;

    [SerializeField] GameObject noEventsPrefab, eventPrefab;

    private FileManager fileManager;

    public void NextButton()
    {
        daySelected = daySelected.AddMonths(1);
        UpdateDisplay();
    }

    public void PrevButton()
    {
        daySelected = daySelected.AddMonths(-1);
        UpdateDisplay();
    }

    public DateTime daySelected;

    private void UpdateDisplay()
    {
        Debug.Log(daySelected.ToString("D"));
        // Show correct number of days in the month.
        int daysToShow = DateTime.DaysInMonth(daySelected.Year, daySelected.Month) - 28;
        foreach (GameObject button in finalButtons)
        {
            button.SetActive(false);
        }
        for (int i = 0; i < daysToShow; i++)
        {
            finalButtons[i].SetActive(true);
        }

        // Make the day start on the right day of week.
        foreach (GameObject empty in empties)
        {
            empty.SetActive(false);
        }
        int emptiesToShow = (int) new DateTime(daySelected.Year, daySelected.Month, 1).DayOfWeek;
        for (int i = 0; i < emptiesToShow; i++)
        {
            empties[i].SetActive(true);
        }

        // Update title
        monthTitle.SetText(daySelected.ToString("Y"));

    }

    // Start is called before the first frame update
    void Start()
    {
        daySelected = DateTime.Now;
        UpdateDisplay();
        DateSelect(daySelected.Day);
    }

    public void DateSelect(int day)
    {
        daySelected = new DateTime(daySelected.Year, daySelected.Month, day);
        dayTitle.SetText(daySelected.ToString("D"));
        // Access data
        List<CalendarEvent> events = fileManager.GetCalendarEvents(daySelected);
        if(events.Count == 0)
        {

        }
        foreach (CalendarEvent cEvent in fileManager.GetCalendarEvents(daySelected))
        {

        }
    }
}
