//using System;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class Calendar : MonoBehaviour
//{
//    [SerializeField] Button nextButton;
//    [SerializeField] Button previousButton;
//    [SerializeField] List<GameObject> empties;
//    [SerializeField] List<GameObject> finalButtons;

//    [SerializeField] TMP_Text dayTitle;
//    [SerializeField] TMP_Text events;

//    [SerializeField] GameObject createClassDialogue;
//    [SerializeField] TMP_InputField createDescription;
//    [SerializeField] TMP_Dropdown createDropdown;

//    [SerializeField] TMP_Text monthTitle;

//    FileManager fileManager;
//    CalendarData calendarData;

//    class Month
//    {
//        public string name;
//        public int startDay;
//        public int dayCount;

//        public Month(string name, int startDay, int dayCount)
//        {
//            this.name = name;
//            this.startDay = startDay;
//            this.dayCount = dayCount;
//        }
//    }

//    private readonly Month[] months = new Month[24]
//    {
//        new Month("January", 0,31),//Jan
//        new Month("February", 3, 28), //Feb
//        new Month("March", 3, 31),//Mar
//        new Month("April", 6, 30),//Apr
//        new Month("May", 1, 31),//May
//        new Month("June", 4, 30),//Jun
//        new Month("July", 6, 31),//Jul
//        new Month("August", 2, 31),//Aug
//        new Month("September", 5, 30),//Sep
//        new Month("October", 0, 31),//Oct
//        new Month("November", 3, 30),//Nov
//        new Month("December", 5, 31),//Dec

//        new Month("January", 1,31),//Jan
//        new Month("February", 4, 29), //Feb
//        new Month("March", 5, 31),//Mar
//        new Month("April", 1, 30),//Apr
//        new Month("May", 3, 31),//May
//        new Month("June", 6, 30),//Jun
//        new Month("July", 1, 31),//Jul
//        new Month("August", 4, 31),//Aug
//        new Month("September", 0, 30),//Sep
//        new Month("October", 2, 31),//Oct
//        new Month("November", 5, 30),//Nov
//        new Month("December", 0, 31)//Dec
//    };

//    private int monthIndex = 0;
//    private int dayIndex;

//    public void IncreaseIndex()
//    {
//        if(monthIndex < months.Length - 1) 
//        {
//            monthIndex++;
//            if(monthIndex == 23)
//            {
//                nextButton.enabled = false;
//            }
//            previousButton.enabled = true;
//            UpdateMonth();
//        }
//    }    

//    public void DecreaseIndex()
//    {
//        if(monthIndex > 0) 
//        {
//            monthIndex--;
//            if(monthIndex == 0)
//            {
//                previousButton.enabled = false;
//            }
//            nextButton.enabled = true;
//            UpdateMonth();
//        }
//    }

//    private void UpdateMonth()
//    {
//        Month thisMonth = months[monthIndex];
//        int daysToShow = thisMonth.dayCount - 28;
//        foreach(GameObject button in finalButtons)
//        {
//            button.SetActive(false);
//        }
//        for (int i = 0; i < daysToShow; i++)
//        {
//            finalButtons[i].SetActive(true);
//        }
//        foreach(GameObject empty in empties)
//        {
//            empty.SetActive(false);
//        }
//        int emptiesToShow = thisMonth.startDay;
//        for (int i = 0; i < emptiesToShow; i++)
//        {
//            empties[i].SetActive(true);
//        }
//        int year;
//        if(monthIndex < 12)
//        {
//            year = 2023;
//        }
//        else
//        {
//            year = 2024;
//        }
//        monthTitle.SetText(thisMonth.name + " " + year.ToString()); ;
//    }

//    public void DateSelect(int day)
//    {
//        dayTitle.SetText(months[monthIndex].name + " " + day.ToString());
//        if (String.IsNullOrEmpty(calendarData.calendar[monthIndex, day - 1])){
//            events.SetText("No events today");
//        }
//        else
//        {
//            events.SetText(calendarData.calendar[monthIndex, day - 1]);
//        }
//        dayIndex = day - 1;
//    }

//    public void AddEvent()
//    {
//        createClassDialogue.SetActive(true);
//    }

//    public void FinalizeEvent()
//    {
//        string text = createDescription.text;
//        createDescription.text = string.Empty;
//        if (String.IsNullOrEmpty(calendarData.calendar[monthIndex, dayIndex]))
//        {
//            calendarData.calendar[monthIndex, dayIndex] = text;
//        }
//        else
//        {
//            calendarData.calendar[monthIndex, dayIndex] += "\n" + text;
//        }
//        fileManager.CalendarFile.Save();
//        createClassDialogue.SetActive(false);
//        DateSelect(dayIndex + 1);
//    }

//    private void Start()
//    {
//        fileManager = SingletonManager.Instance.FileManagerInstance;
//        calendarData = fileManager.CalendarFile.Data;
//        dayTitle.SetText(String.Empty);
//        events.SetText(String.Empty);
//        UpdateMonth();
//    }
//}
