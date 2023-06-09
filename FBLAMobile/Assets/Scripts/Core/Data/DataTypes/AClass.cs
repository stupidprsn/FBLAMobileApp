using System;
using System.Collections.Generic;

[Serializable]
public class AClass
{
    public short joinCode;
    public byte owner;
    public string name;
    public List<byte> members;
    public string social;

    public AClass(short joincode, byte owner, string name) 
    { 
        this.joinCode = joincode;
        this.owner = owner;
        this.name = name;
        members = new List<byte>();
    }
}
