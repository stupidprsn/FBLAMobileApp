using System;
using System.Collections.Generic;

[Serializable]
public class AClass
{
    public int joinCode;
    public string owner;
    public string name;
    public List<string> members;
    public List<string> text;
    public List<SocialLink> socials;

    public AClass(int joincode, string owner, string name) 
    { 
        this.joinCode = joincode;
        this.owner = owner;
        this.name = name;
        members = new List<string>();
        text = new List<string>()
        {
            "Class has not been set up yet!"
        };
        socials = new List<SocialLink>();
    }
}
