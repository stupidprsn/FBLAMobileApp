using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Chat
{
    public List<string> messages;
    public byte lastMessenger;

    public Chat()
    {
        messages = new List<string>();
    }
}