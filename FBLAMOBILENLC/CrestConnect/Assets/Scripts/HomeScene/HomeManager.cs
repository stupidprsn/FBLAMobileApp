using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Manages the home screen.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/17/2023
/// </remarks>
public class HomeManager : MonoBehaviour
{
    [HideInInspector] public Sprite profilePicture;

    [SerializeField] private GameObject defaultPanel, safeArea;
    [SerializeField] private SelectImage selectImage;
    [SerializeField] private Image settingsButton;

    [SerializeField] private ClassesPage classesPage;
    [SerializeField] private CreatePost createPost;
    [SerializeField] private RenderFeed renderFeed;

    private FileManager fileManager;
    private TransitionManager transitionManager;
    private AccountManager accountManager;


    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        transitionManager = SingletonManager.Instance.TransitionManagerInstance;
        accountManager = SingletonManager.Instance.AccountManagerInstance;

        currentPanel = defaultPanel;
        prevPanel = defaultPanel;
        EnablePanels();

        //profilePicture = selectImage.ConvertSprite(fileManager.AccountFile.Data.ProfilePicture);
        //settingsButton.sprite = profilePicture;
    }

    public void ResetScreens()
    {
        accountManager.RefreshClasses();
        classesPage.LoadClassesList();
        createPost.LoadClassDropdown();
        renderFeed.UpdateScreen();
    }

    // The current active panel.
    private GameObject currentPanel;
    private GameObject prevPanel;
    // Prevents multiple transitions from happening at once.
    private bool isTransitioning;

    /// <summary>
    ///     Used by the navigation buttons to change
    ///     the current panel.
    /// </summary>
    /// <param name="toPanel">
    ///     The panel to set active.
    /// </param>
    public void ChangePanel(GameObject toPanel)
    {
        if ((currentPanel == toPanel) || isTransitioning) return;

        isTransitioning = true;
        currentPanel.SetActive(false);
        toPanel.SetActive(true);
        prevPanel = currentPanel;
        currentPanel = toPanel;
        isTransitioning = false;
    }

    public void BackButton()
    {
        ChangePanel(prevPanel);
    }

    public void SignOut()
    {
        fileManager.InitialDataFile.Save(new InitialData());
        transitionManager.BasicTransition(Scenes.Login);
    }

    /// <summary>
    ///     Enables the correct panels for application startup. 
    /// </summary>
    private void EnablePanels()
    {
        safeArea.SetActive(true);

        foreach (Transform t in safeArea.transform)
        {
            t.gameObject.SetActive(true);
            foreach(Transform t2 in t)
            {
                t2.gameObject.SetActive(false);
            }
        }

        defaultPanel.SetActive(true);
        currentPanel = defaultPanel;
    }

}
