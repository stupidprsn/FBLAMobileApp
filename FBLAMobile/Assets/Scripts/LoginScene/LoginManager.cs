using System.Collections;
using UnityEngine;

/// <summary>
///     Manages the scene where users log in.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/13/2023
/// </remarks>
public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private Transform safeArea;
    [SerializeField]
    private GameObject loadingPanel;
    [SerializeField]
    private GameObject choosePanel;

    /// <summary>
    ///     Stores data about an account being registered.
    /// </summary>
    public Account NewAccount { get; set; }
    
    /// <summary>
    ///     If the user's sign in should be remembered.
    /// </summary>
    public bool Remember { get; set; }

    /// <summary>
    ///     The current panel that is active in the scene. 
    /// </summary>
    private GameObject currentPanel;

    /// <summary>
    ///     Are the panels changing.
    /// </summary>
    private bool isChanging;

    /// <summary>
    ///     Changes the active panel.
    /// </summary>
    /// <param name="toPanel">The panel to shift to.</param>
    public void ChangePanel(GameObject toPanel)
    {
        if (isChanging) return;
        isChanging = true;

        currentPanel.SetActive(false);
        toPanel.SetActive(true);

        currentPanel = toPanel;
        isChanging = false;
    }

    private void Start()
    {
        safeArea.gameObject.SetActive(true);
        loadingPanel.SetActive(true);
        currentPanel = loadingPanel;

        NewAccount = new Account();

        StartCoroutine(DelayedStart());
    }

    /// <summary>
    ///     Waits until after the FileManager has finished loading.
    /// </summary>
    /// <returns>null</returns>
    private IEnumerator DelayedStart()
    {
        FileManager fileManager = FileManager.Instance;
        yield return new WaitUntil(() => fileManager.FinishedLoading == true);

        InitialData initialData = fileManager.InitialDataFile.Data;
        if(initialData.SignedIn)
        {
            fileManager.LoadAccount(initialData.AccountID);
            Debug.Log(fileManager.AccountFile.Data.AccountType);
            Debug.Log((int)fileManager.AccountFile.Data.AccountType + 1);
            TransitionManager.Instance.BasicTransition((int)fileManager.AccountFile.Data.AccountType + 1);
        }
        else
        {
            fileManager.AccountDictionaryFile.Load();
            ChangePanel(choosePanel);
        }
    }
}
