using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
///     Manages file storage.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 06/12/2023
/// </remarks>
public class FileManager : MonoBehaviour
{
    [SerializeField] private AccountManager accountManager;
    [SerializeField] private Sprite debugProfilePicture;

    /// <summary>
    ///     If the file manager has finished loading all of its data files.
    /// </summary>
    public bool FinishedLoading { get; private set; } = false;

    #region DataFiles
    public DataFile<AccountDictionary> AccountDictionaryFile { get; private set; }
    public DataFile<ClassDictionary> ClassDictionaryFile { get; private set; }
    public DataFile<InitialData> InitialDataFile { get; private set; }
    public DataFile<Account> AccountFile { get; private set; }
    //public DataFile<CalendarData> CalendarFile { get; private set; }
    public DataFile<FeedList> FeedListFile { get; private set; }
    public DataFile<CalendarList> CalendarListFile { get; private set; }
    #endregion

    #region Account
    /// <summary>
    ///     Loads an account by username.
    /// </summary>
    /// <param name="username">The account's username.</param>
    public void LoadAccount(string username)
    {
        AccountFile = new DataFile<Account>("Accounts", username + ".fbla");
        AccountFile.Load();
        accountManager.Login();
    }

    /// <summary>
    ///     Creates a new account.
    /// </summary>
    /// <param name="acc">Account Info</param>
    public void CreateAccount(Account acc)
    {
        AccountDictionaryFile.Data.AddName(acc.Username, acc.DisplayName);
        AccountDictionaryFile.Save();
        AccountFile = new DataFile<Account>("Accounts", acc.Username + ".fbla");
        AccountFile.Save(acc);
    }
    #endregion

    public Sprite GetProfilePicture(string username)
    {
        DataFile<Account> account = new("Accounts", username + ".fbla");

        Texture2D image;
        image = new Texture2D(1, 1);
        image.LoadImage(account.Data.ProfilePicture);

        return Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
    }

    #region Class
    /// <summary>
    ///     Returns the datafile for a class.
    /// </summary>
    /// <param name="classCode">Join Code</param>
    /// <returns>Class's <see cref="DataFile{T}"/></returns>
    public DataFile<AClass> GetClassFile(int classCode)
    {
        return new DataFile<AClass>("Classes", classCode.ToString() + ".fbla");
    }

    /// <summary>
    ///     Returns the datafile for a class.
    /// </summary>
    /// <param name="classCode">Join Code</param>
    /// <returns>Class's <see cref="DataFile{T}"/></returns>
    public DataFile<AClass> GetClassFile(string classCode)
    {
        return new DataFile<AClass>("Classes", classCode + ".fbla");
    }
    #endregion

    #region Posts
    /// <summary>
    ///     Saves a feed post to nonvolitile memory.
    /// </summary>
    /// <param name="post">The post to save.</param>
    public void CreatePost(FeedPost post)
    {
        int currentIndex = FeedListFile.Data.CurrentIndex;
        FeedListFile.Data.CurrentIndex++;
        FeedListFile.Data.List.Add(post.ClassID);
        FeedListFile.Save();

        DataFile<FeedPost> postFile = new("Feed", currentIndex.ToString() + ".fbla");
        postFile.Save(post);
    }

    /// <summary>
    ///     Retrieves a post by its ID.
    /// </summary>
    /// <param name="i">Post ID</param>
    public FeedPost GetPost(int i)
    {
        return new DataFile<FeedPost>("Feed", i.ToString() + ".fbla").Data;
    }
    #endregion

    public void CreateCalendarEvent(CalendarEvent cEvent)
    {
        int currentIndex = CalendarListFile.Data.currentIndex;
        CalendarListFile.Data.currentIndex++;
        if(CalendarListFile.Data.calendarDictionary.ContainsKey(cEvent.time))
        {
            CalendarListFile.Data.calendarDictionary[cEvent.time].Add(currentIndex.ToString());
        }
        else
        {
            CalendarListFile.Data.calendarDictionary.Add(cEvent.time, new List<string>() { currentIndex.ToString() });
        }
        CalendarListFile.Save();

        DataFile<CalendarEvent> calendarEvent = new("Calendar", currentIndex.ToString() + ".fbla");
        calendarEvent.Save(cEvent);
    } 

    public List<CalendarEvent> GetCalendarEvents(DateTime time)
    {
        if (!CalendarListFile.Data.calendarDictionary.ContainsKey(time)) return null;

        List<CalendarEvent> calendarEvents = new();

        foreach(string s in CalendarListFile.Data.calendarDictionary[time])
        {
            calendarEvents.Add(new DataFile<CalendarEvent>("Calendar", s + ".fbla").Data);
        }
        return calendarEvents;
    }

    private void Awake()
    {
        InitialDataFile = new DataFile<InitialData>("InitialData.fbla");

        AccountDictionaryFile = new DataFile<AccountDictionary>("AccountDictionary.fbla");
        ClassDictionaryFile = new DataFile<ClassDictionary>("ClassDictionary.fbla");
        //CalendarFile = new DataFile<CalendarData>("Calendar.fbla");
        FeedListFile = new DataFile<FeedList>("feedlist.fbla");
        CalendarListFile = new DataFile<CalendarList>("CalendarList.fbla");

        if (!InitialDataFile.FileExists) 
        {
            Initiate();
        }
        else
        {
            InitialDataFile.Load();
        }

        FinishedLoading = true;
    }

    private void Initiate()
    {
        if (!AccountDictionaryFile.FileExists) AccountDictionaryFile.Save(new AccountDictionary());
        if (!ClassDictionaryFile.FileExists) ClassDictionaryFile.Save(new ClassDictionary());
        //if (!CalendarFile.FileExists) CalendarFile.Save(new CalendarData());
        if (!FeedListFile.FileExists) FeedListFile.Save(new FeedList());
        if (!CalendarListFile.FileExists) CalendarListFile.Save(new CalendarList());

        string directoryPath = Path.Combine(Application.persistentDataPath, "Accounts");
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        directoryPath = Path.Combine(Application.persistentDataPath, "Assets");
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        directoryPath = Path.Combine(Application.persistentDataPath, "Classes");
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        directoryPath = Path.Combine(Application.persistentDataPath, "Feed");
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        directoryPath = Path.Combine(Application.persistentDataPath, "Calendar");
        if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

        DataFile<Account> debugAccount = new("Accounts", "debug.fbla");
        if(!debugAccount.FileExists)
        {
            debugAccount.Save(new Account(
                AccountType.Student,
                "debug",
                "debug",
                "debug",
                new byte[0],
                "debug",
                "debug"
            ));
        }

        InitialDataFile.Save(new InitialData());
    }
}
