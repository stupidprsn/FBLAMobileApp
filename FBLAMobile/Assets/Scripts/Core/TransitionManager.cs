using UnityEngine.SceneManagement;

/// <summary>
///     Manages transitions between scenes.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/05/2023
/// </remarks>
public class TransitionManager : Singleton<TransitionManager>
{
    /// <summary>
    ///     If a transition is currently being played.
    /// </summary>
    private bool isTransitioning = false;

    /// <summary>
    ///     Loads a new scene and unloads the current scene without
    ///     any transition.
    /// </summary>
    /// <param name="scene">The scene to load.</param>
    public void BasicTransition(Scenes scene)
    {
        if (isTransitioning) return;
        isTransitioning = true;

        SceneManager.LoadScene((int)scene);
        isTransitioning = false;
    }

    public void BasicTransition(int scene)
    {
        if (isTransitioning) return;
        isTransitioning = true;

        SceneManager.LoadScene(scene);
        isTransitioning = false;
    }

    private void Awake()
    {
        SingletonCheck(this);
    }
}