using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
///     Manages file storage.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/15/2023
/// </remarks>
public class FileManager : Singleton<FileManager>
{
    /// <summary>
    ///     If the file manager has finished loading all of its data files.
    /// </summary>
    public bool FinishedLoading { get; private set; } = false;

    public DataFile<AccountDictionary> AccountDictionaryFile { get; private set; }
    public DataFile<ClassDictionary> ClassDictionaryFile { get; private set; }
    public DataFile<InitialData> InitialDataFile { get; private set; }
    public DataFile<Account> AccountFile { get; private set; }
    public DataFile<CalendarData> CalendarFile { get; private set; }

    public List<DataFile<AClass>> OwnClassList { get; private set; }
    public List<DataFile<AClass>> InClassList { get; private set; }

    public DataFile<FeedList> feedList;

    public void LoadClasses()
    {

        OwnClassList= new List<DataFile<AClass>>();
        InClassList= new List<DataFile<AClass>>();

        Account acc = AccountFile.Data;
        for (int i = 0; i < acc.InClasses.Count; i++)
        {
            InClassList.Add(new DataFile<AClass>(acc.InClasses[i].ToString() + ".fbla"));
            InClassList[i].Load();
        }
        for (int i = 0; i < acc.OwnedClasses.Count; i++)
        {
            OwnClassList.Add(new DataFile<AClass>(acc.OwnedClasses[i].ToString() + ".fbla"));
            OwnClassList[i].Load();
        }

        CalendarFile.Load();
    }

    public void CreateClass(short joincode, byte owner, string name)
    {
        AClass newClass = new AClass(joincode, owner, name);
        DataFile<AClass> newClassFile = new DataFile<AClass>(joincode.ToString() + ".fbla");
        newClassFile.Save(newClass);
        OwnClassList.Add(newClassFile);
    }

    /// <summary>
    ///     Loads an account by ID.
    /// </summary>
    /// <param name="id">The account's ID.</param>
    public void LoadAccount(byte id)
    {
        AccountFile = new DataFile<Account>(id.ToString() + ".fbla");
        AccountFile.Load();
    }

    /// <summary>
    ///     Creates a new account.
    /// </summary>
    /// <param name="acc">Account Info</param>
    public void CreateAccount(Account acc)
    {
        AccountFile = new DataFile<Account>(acc.ID.ToString() + ".fbla");
        AccountFile.Save(acc);
    }

    private void Awake()
    {
        SingletonCheck(this);
    }

    private void Start()
    {
        AccountDictionaryFile = new DataFile<AccountDictionary>("AccountDictionary.fbla");
        InitialDataFile = new DataFile<InitialData>("InitialData.fbla");
        ClassDictionaryFile = new DataFile<ClassDictionary>("ClassDictionary.fbla");
        CalendarFile = new DataFile<CalendarData>("Calendar.fbla");
        feedList = new DataFile<FeedList>("feedlist.fbla");

        // Create an account dictionary if one does not already exist.
        if (!AccountDictionaryFile.FileExists) AccountDictionaryFile.Save(new AccountDictionary());

        if (!ClassDictionaryFile.FileExists) ClassDictionaryFile.Save(new ClassDictionary());
        if (!CalendarFile.FileExists) CalendarFile.Save(new CalendarData());
        if (!feedList.FileExists) feedList.Save(new FeedList());
        if (!InitialDataFile.FileExists) 
        {
            InitialDataFile.Save(new InitialData());
        }
        else
        {
            InitialDataFile.Load();
        }

        FinishedLoading = true;
    }
}
