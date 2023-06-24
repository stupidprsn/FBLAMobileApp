using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Creates functionality for the edit class panel.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/20/23
/// </remarks>
public class EditClass : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_InputField nameInput, descriptionInput, joinInput;
    [SerializeField] private Toggle publicToggle;
}
