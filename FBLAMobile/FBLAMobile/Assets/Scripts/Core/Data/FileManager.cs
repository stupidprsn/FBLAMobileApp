using System.IO;

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
    public DataFile<InitialData> InitialDataFile { get; private set; }
    public DataFile<Account> AccountFile { get; private set; }

    /// <summary>
    ///     Loads an account by ID.
    /// </summary>
    /// <param name="id">The account's ID.</param>
    public void LoadAccount(byte id)
    {
        AccountFile = new DataFile<Account>(Path.Combine("Accounts", id.ToString() + ".fbla"));
    }

    /// <summary>
    ///     Creates a new account.
    /// </summary>
    /// <param name="acc">Account Info</param>
    public void CreateAccount(Account acc)
    {
        AccountFile = new DataFile<Account>(Path.Combine("Accounts", acc.ID.ToString() + ".fbla"));
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

        // Create an account dictionary if one does not already exist.
        if (!AccountDictionaryFile.FileExists) AccountDictionaryFile.Save(new AccountDictionary());

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
