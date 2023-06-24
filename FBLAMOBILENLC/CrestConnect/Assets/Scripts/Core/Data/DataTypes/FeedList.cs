using System.Collections.Generic;

/// <summary>
///     Keeps track of all posts in the feed.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/21/2023
/// </remarks>
[System.Serializable]
public class FeedList
{
    private readonly List<int> list;

    /// <summary>
    ///     A list of the class ID's of the posts.
    /// </summary>
    public List<int> List { get { return list; } }

    public int CurrentIndex { get; set; }

    /// <summary>
    ///     Main constructor.
    /// </summary>
    public FeedList()
    {
        list = new List<int>();
        CurrentIndex = 0;
    }
}