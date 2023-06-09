using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    public string text;

    public Message(string text)
    {
        this.text = text;
    }
}

[System.Serializable]
public class MessageHistory
{
    public List<Message> messageList;

    public MessageHistory()
    {
        messageList = new List<Message>();
    }
}