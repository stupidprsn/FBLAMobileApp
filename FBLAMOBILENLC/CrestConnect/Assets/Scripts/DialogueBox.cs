using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
///     Manages dialogue boxes.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/16/2023
/// </remarks>
public class DialogueBox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject dialogueBox, miscGameObject;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button button, miscButton;

    private bool hasAction;

    /// <summary>
    ///     Shows the dialogue box;
    /// </summary>
    /// <param name="msg">The message to display.</param>
    public void Enable(string msg)
    {
        dialogueText.SetText(msg);
        dialogueBox.SetActive(true);
    }

    /// <summary>
    ///     Shows the dialogue box and adds functionality to the confirm button.
    /// </summary>
    /// <param name="msg">The message to display.</param>
    /// <param name="call">The action to perform.</param>
    public void Enable(string msg, UnityAction call)
    {
        hasAction = true;
        button.onClick.AddListener(call);
        dialogueText.SetText(msg);
        dialogueBox.SetActive(true);
    }
    
    public void EnableMisc(Sprite image, UnityAction call)
    {
        miscGameObject.SetActive(true);
        miscGameObject.GetComponent<Image>().sprite = image;
        miscButton.onClick.AddListener(call);
    }

    public void Disable()
    {
        if(hasAction)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                Disable();
            });
        }
        miscGameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
