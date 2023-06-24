using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Holds references for the select class prefab.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/18/2023
/// </remarks>
public class SelectClassReferences : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text className, classCode;
    [SerializeField] private Button button;

    public TMP_Text ClassName { get { return className; } }
    public TMP_Text ClassCode { get { return classCode; } }
    public Button Button { get { return button; } }
}
