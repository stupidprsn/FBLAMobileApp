using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CalendarEvent
{
    public string title;
    public string location;
    public DateTime time;
    public string notes;

    public CalendarEvent(string title, string location, DateTime time, string notes)
    {
        this.title = title;
        this.location = location;
        this.time = time;
        this.notes = notes;
    }

    public CalendarEvent()
    {

    }
}
