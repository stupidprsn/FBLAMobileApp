using System;

[Serializable]
public class CalendarAbsence : CalendarEvent
{
    public CalendarAbsence(string title, int classID, string location, DateTime time, string notes) : base(title, classID, location, time, notes) { }
}
