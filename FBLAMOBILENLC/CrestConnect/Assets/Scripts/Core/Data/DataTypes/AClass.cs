using System;
using System.Collections.Generic;

[Serializable]
public class AClass
{
    public int joinCode;
    public string owner;
    public string name;
    public List<string> members;

    public string description;
    public string howToJoin;
    public List<SocialLink> socials;

    public AClass(int joincode, string owner, string name) 
    { 
        this.joinCode = joincode;
        this.owner = owner;
        this.name = name;
        members = new List<string>();

        description = string.Empty;
        howToJoin = string.Empty;
        socials = new List<SocialLink>();
    }
}
