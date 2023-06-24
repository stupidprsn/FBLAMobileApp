using TMPro;
using UnityEngine;

public class SettingsHome : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    private FileManager fileManager;

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        title.SetText(
            "Hello, " + fileManager.AccountFile.Data.DisplayName + "!");
    }
}
