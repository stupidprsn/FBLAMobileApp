using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Resizes panels to keep content within the screen's safe area.
/// </summary>
/// <para>
///     This class was inspired by "Optimize UI For Notch Devices - Easy Unity Tutorial" 
///     by "Hooson".
///     
///     <seealso href="https://www.youtube.com/watch?v=VprqsEsFb5w"/>
/// </para>
/// <para>
///     Hanlin Zhang
///     Last Modified: 01/11/2023
/// </para>
public class SafeArea : MonoBehaviour
{
    // The object's rect transform.
    private RectTransform rectTransform;
    // The device's safe area.
    private Rect safeArea;
    private Vector2 minAnchor;
    private Vector2 maxAnchor;

    private void Awake()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }
}
