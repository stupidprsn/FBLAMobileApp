using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultPanel;
    [SerializeField] private RenderFeed renderFeed;
    [SerializeField] private ClassesPage classesPage;
    [SerializeField] private ChatManager chatManager;
    private FileManager fileManager;

    private void Start()
    {
        fileManager = FileManager.Instance;
        fileManager.LoadClasses();
        currentPanel = defaultPanel;
        prevPanel = defaultPanel;
    }

    public void ResetScreens()
    {
        renderFeed.UpdateScreen();
        classesPage.ShowClasses();
        chatManager.LoadChats();
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
        TransitionManager.Instance.BasicTransition(Scenes.Login);
    }

}
