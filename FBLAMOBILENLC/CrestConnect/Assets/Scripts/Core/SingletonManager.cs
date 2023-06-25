using UnityEngine;

/// <summary>
///     Master class for all singletons. 
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/12/2023
/// </remarks>
public class SingletonManager : MonoBehaviour
{
    #region Singleton Set up
    [HideInInspector] 
    public static SingletonManager Instance { get; private set; }

    /// <summary>
    ///     Singleton pattern check to make sure only
    ///     one singleton exists.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    [Header("References")]
    [SerializeField] private TransitionManager transitionManager;
    [SerializeField] private FileManager fileManager;
    [SerializeField] private AccountManager accountManager;
    [SerializeField] private SelectImage selectImage;

    public TransitionManager TransitionManagerInstance 
    { 
        get 
        { 
            return transitionManager; 
        } 
    }

    public FileManager FileManagerInstance 
    { 
        get 
        { 
            return fileManager; 
        } 
    }

    public AccountManager AccountManagerInstance
    {
        get
        {
            return accountManager;
        }
    }

    public SelectImage SelectImageInstance
    {
        get
        {
            return selectImage;
        }
    }
}
