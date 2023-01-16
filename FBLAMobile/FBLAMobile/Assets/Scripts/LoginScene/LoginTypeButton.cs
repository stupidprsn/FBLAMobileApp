using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Creates functionality for the buttons that allow 
///     user's to choose their account type.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 01/13/2023
/// </remarks>
public class LoginTypeButton : MonoBehaviour
{
    [SerializeField,
        Tooltip("The account type associated with this button.")]
    private AccountType account;

    [Header("References")]
    [SerializeField] private Button button;
    [SerializeField] private LoginManager loginManager;
    [SerializeField] private GameObject createPanel;

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            loginManager.NewAccount.AccountType = account;
            loginManager.ChangePanel(createPanel);
        });
    }
}
