using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     Manages the user's account.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/17/2023
/// </remarks>
public class AccountManager : MonoBehaviour
{
    [SerializeField] private FileManager fileManager;

    private DataFile<Account> accountFile;

    private List<int> allClasses;

    public void Login()
    {
        accountFile = fileManager.AccountFile;
        RefreshClasses();
    }

    public void RefreshClasses()
    {
        accountFile = fileManager.AccountFile;
        allClasses = accountFile.Data.InClasses.Concat(accountFile.Data.OwnedClasses).ToList();
    }

    public List<int> AllClasses { get { return allClasses; } }

}
