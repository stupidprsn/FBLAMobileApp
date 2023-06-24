using System.Collections;
using UnityEngine;

/// <summary>
///     Manages the scene where users log in.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 06/16/2023
/// </remarks>
public class LoginManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform safeArea;
    [SerializeField] private GameObject loadingPanel, choosePanel;

    private FileManager fileManager;
    private TransitionManager transitionManager;

    /// <summary>
    ///     Stores data about an account being registered.
    /// </summary>
    public Account NewAccount { get; set; }

    /// <summary>
    ///     If the user's sign in should be remembered.
    /// </summary>
    public bool Remember { get; set; }

    #region Change Panel

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

    #endregion

    public void Login()
    {
        Scenes scene = Scenes.StudentView;

        transitionManager.BasicTransition(scene);
    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        transitionManager = SingletonManager.Instance.TransitionManagerInstance;

        EnablePanels();

        StartCoroutine(DelayedStart());
    }

    /// <summary>
    ///     Enables the correct panels for application startup. 
    /// </summary>
    private void EnablePanels()
    {
        safeArea.gameObject.SetActive(true);

        foreach (Transform t in safeArea.transform)
        {
            t.gameObject.SetActive(false);
        }

        loadingPanel.SetActive(true);
        currentPanel = loadingPanel;
    }

    /// <summary>
    ///     Waits until after the FileManager has finished loading.
    /// </summary>
    /// <returns>null</returns>
    private IEnumerator DelayedStart()
    {
        yield return new WaitUntil(() => fileManager.FinishedLoading == true);

        InitialData initialData = fileManager.InitialDataFile.Data;
        if (initialData.SignedIn)
        {
            fileManager.LoadAccount(initialData.Username);
            Login();
        }
        else
        {
            NewAccount = new Account();
            fileManager.AccountDictionaryFile.Load();
            ChangePanel(choosePanel);
        }
    }
}
