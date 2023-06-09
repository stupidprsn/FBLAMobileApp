using UnityEngine;

/// <summary>
///     Creates functionality for the navigation bar.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/06/2023
/// </remarks>
public class NavBar : MonoBehaviour
{
    // The current active panel.
    private GameObject currentPanel;
    // Prevents multiple transitions from happening at once.
    private bool isTransitioning;

    FileManager fileManager;

    /// <summary>
    ///     Used by the navigation buttons to change
    ///     the current panel.
    /// </summary>
    /// <param name="toPanel">
    ///     The panel to set active.
    /// </param>
    public void ChangePanel(GameObject toPanel)
    {
        if((currentPanel == toPanel) || isTransitioning) return;
        
        isTransitioning = true;
        currentPanel.SetActive(false);
        toPanel.SetActive(true);
        currentPanel = toPanel;
        isTransitioning = false;
    }

    public void SignOut()
    {
        fileManager.InitialDataFile.Save(new InitialData());
        TransitionManager.Instance.BasicTransition(Scenes.Login);
    }

    private void Start()
    {
        fileManager = FileManager.Instance;
    }
}
