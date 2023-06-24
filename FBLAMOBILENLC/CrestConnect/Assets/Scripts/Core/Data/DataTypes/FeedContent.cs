using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     An individual feed post.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/21/2023
/// </remarks>
[System.Serializable]
public class FeedPost
{
    /// <summary>
    ///     The class that the post was made for.
    /// </summary>
    public int ClassID;
    /// <summary>
    ///     The author of the post.
    /// </summary>
    public string TeacherName;
    public string Username;
    /// <summary>
    ///     The post's text.
    /// </summary>
    public string Content;
    /// <summary>
    ///     If the post has an image, it's id
    /// </summary>
    public byte[] ImgID;
    /// <summary>
    ///     The time the post was created.
    /// </summary>
    public string Time;
    /// <summary>
    ///     If the post contains an image.
    /// </summary>
    public bool HasImage;

    public FeedPost(int classID, string teacherName, string username, string content, string time)
    {
        this.ClassID = classID;
        this.Time = time;
        this.TeacherName = teacherName;
        this.Username = username;
        this.Content = content;
        this.HasImage = false;
    }

    public FeedPost(int classID, string teacherName, string username, string content, string time, byte[] imgID)
    {
        this.Time = time;
        this.ClassID = classID;
        this.TeacherName = teacherName;
        this.Username = username;
        this.Content = content;
        this.HasImage = true;
        this.ImgID = imgID;
    }
}
