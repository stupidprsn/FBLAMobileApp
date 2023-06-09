using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FeedPost
{
    public byte classID;
    public string className;
    public string teacherName;
    public string content;
    public string imagePath;
    public string time;
    public bool hasImage;

    public FeedPost(byte classID, string className, string teacherName, string content, string time)
    {
        this.classID = classID;
        this.className = className;
        this.time = time;
        this.teacherName = teacherName;
        this.content = content;
        this.hasImage = false;
    }

    public FeedPost(byte classID, string className, string teacherName, string content, string time, string imagePath)
    {
        this.time = time;
        this.classID = classID;
        this.className = className;
        this.teacherName = teacherName;
        this.content = content;
        this.hasImage = true;
        this.imagePath = imagePath;
    }
}

[System.Serializable]
public class FeedList
{
    public List<FeedPost> list;

    public FeedList()
    {
        list = new List<FeedPost>();
    }
}