using System.Collections.Generic;

public class Chat
{
    public List<string> messages;
    public byte lastMessenger;

    public Chat()
    {
        messages = new List<string>();
    }
}