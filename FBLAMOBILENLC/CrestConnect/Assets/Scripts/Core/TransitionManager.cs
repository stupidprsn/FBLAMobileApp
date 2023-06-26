using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     Manages transitions between scenes.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 06/12/2023
/// </remarks>
public class TransitionManager : MonoBehaviour
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

        FindObjectOfType<CrossfadeAnimation>().LoadNextScene();
        isTransitioning = false;
    }
}
