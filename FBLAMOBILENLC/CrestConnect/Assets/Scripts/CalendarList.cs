using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class CalendarList
{
    public Dictionary<DateTime, List<string>> calendarDictionary;
    public int currentIndex;

    public CalendarList()
    {
        calendarDictionary = new Dictionary<DateTime, List<string>>();
        currentIndex = 0;
    }
}
