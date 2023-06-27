using System;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NewCalendar : MonoBehaviour
{
    [SerializeField] List<GameObject> empties;
    [SerializeField] List<GameObject> finalButtons;

    [SerializeField] TMP_Text dayTitle;
    [SerializeField] TMP_Text monthTitle;

    [SerializeField] Transform content;
    [SerializeField] GameObject noEventsPrefab, eventPrefab, timelessPrefab;
    [SerializeField] DialogueBox dialogueBox;
    [SerializeField] Sprite zoomImg, teamsImg;

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
        int emptiesToShow = (int)new DateTime(daySelected.Year, daySelected.Month, 1).DayOfWeek;
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
        fileManager = SingletonManager.Instance.FileManagerInstance;
        daySelected = DateTime.Now;
        UpdateDisplay();
        DateSelect(daySelected.Day);
    }

    public void DateSelect(int day)
    {
        daySelected = new DateTime(daySelected.Year, daySelected.Month, day);
        dayTitle.SetText(daySelected.ToString("D"));
        // Access data
        List<CalendarEvent> events = SingletonManager.Instance.FileManagerInstance.GetCalendarEvents(daySelected);
        foreach (Transform t in content)
        {
            Destroy(t.gameObject);
        }

        if (events is null)
        {
            Instantiate(noEventsPrefab, content);
        }
        else
        {
            events.Sort((x, y) => DateTime.Compare(x.time, y.time));

            bool loadEvent = false;
            List<int> classes = SingletonManager.Instance.AccountManagerInstance.AllClasses;

            List<CalendarAbsence> absences = new ();

            foreach (CalendarEvent cEvent in fileManager.GetCalendarEvents(daySelected))
            {
                foreach (int j in classes)
                {
                    if (j == cEvent.classID)
                    {
                        loadEvent = true;
                        break;
                    }
                }
                if (!loadEvent) continue;

                if(cEvent is CalendarAbsence absence)
                {
                    absences.Add(absence);
                    continue;
                }

                GameObject newEvent = Instantiate(eventPrefab, content);
                CalendarReference cReference = newEvent.GetComponent<CalendarReference>();

                if (cEvent.time.TimeOfDay == TimeSpan.Zero)
                {

                }
                else
                {
                    cReference.time.SetText(cEvent.time.ToString("t"));
                }
                cReference.title.SetText(cEvent.title);
                cReference.button.onClick.AddListener(() =>
                {
                    string displayText = cEvent.title;
                    if (cEvent.time.TimeOfDay == TimeSpan.Zero)
                    {
                        displayText += string.Format("\n{0}", cEvent.time.ToString("D"));
                    }
                    else
                    {
                        displayText += string.Format("\n{0}", cEvent.time.ToString("f"));
                    }
                    if (!cEvent.location.Equals(string.Empty))
                    {
                        displayText += string.Format("\n{0}", cEvent.location);
                    }
                    displayText += string.Format("\n{0}", fileManager.ClassDictionaryFile.Data.GetClassName(cEvent.classID));
                    displayText += string.Format("\n{0}", cEvent.notes);

                    if(cEvent.location.StartsWith("https://zoom.us"))
                    {
                        dialogueBox.EnableMisc(zoomImg, () => Application.OpenURL(cEvent.location));
                    }
                    else if (cEvent.location.StartsWith("https://teams.live"))
                    {
                        dialogueBox.EnableMisc(teamsImg, () => Application.OpenURL(cEvent.location));
                    }
                    dialogueBox.Enable(displayText);
                });
            }

            Dictionary<int, List<CalendarAbsence>> absenceDictionary = new ();
            if(absences.Count != 0)
            {
                foreach(CalendarAbsence absence in absences)
                {
                    if(absenceDictionary.ContainsKey(absence.classID))
                    {
                        absenceDictionary[absence.classID].Add(absence);
                    }
                    else
                    {
                        absenceDictionary.Add(absence.classID, new List<CalendarAbsence>() { absence });
                    }
                }

                foreach(var absense in absenceDictionary)
                {
                    GameObject newEvent = Instantiate(timelessPrefab, content);
                    CalendarReference cReference = newEvent.GetComponent<CalendarReference>();
                    cReference.title.SetText(absense.Value.Count.ToString() + " students absent in " + fileManager.ClassDictionaryFile.Data.GetClassName(absense.Key));
                    cReference.button.onClick.AddListener(() =>
                    {
                        string displayText = cReference.title.text;
                        foreach(CalendarAbsence studentAbsence in absense.Value)
                        {
                            displayText += string.Format("\n• {0} will be absent for {1}.", studentAbsence.title, studentAbsence.location);
                            if(!studentAbsence.notes.Equals(string.Empty))
                            {
                                displayText += "; " + studentAbsence.notes;
                            }
                        }
                        dialogueBox.Enable(displayText);
                    });
                    newEvent.transform.SetAsFirstSibling();
                }
            }
        }

    }


}
