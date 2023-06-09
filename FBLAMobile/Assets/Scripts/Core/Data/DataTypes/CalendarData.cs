using System;

[Serializable]
public class CalendarData
{
    public string[,] calendar;

    public CalendarData() 
    {
        calendar = new string[24, 31];
    }
}
