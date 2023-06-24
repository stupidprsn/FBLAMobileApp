using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Holds references regarding components of feed posts.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/22/2023
/// </remarks>
public class PostReferences : MonoBehaviour
{
    [SerializeField] public Image ProfilePicture;
    [SerializeField] public TMP_Text AuthorText;
    [SerializeField] public TMP_Text TimeText;
    [SerializeField] public Image PostImage;
    [SerializeField] public TMP_Text PostText;
}
