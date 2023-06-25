using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CalendarEvent
{
    public string title;
    public int classID;
    public string location;
    public DateTime time;
    public string notes;

    public CalendarEvent(string title, int classID, string location, DateTime time, string notes)
    {
        this.title = title;
        this.classID = classID;
        this.location = location;
        this.time = time;
        this.notes = notes;
    }

    public CalendarEvent()
    {

    }
}
