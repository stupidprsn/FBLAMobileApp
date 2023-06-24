using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Manages the panel that allows the user to customize their account.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/21/2023
/// </remarks>
public class CustomizeAccount : MonoBehaviour
{
    [Header("Text")]
    [SerializeField, TextArea] private string displaynameError, imageError;

    [Header("References")]
    [SerializeField] private GameObject nextPanel;
    [SerializeField] private LoginManager loginManager;
    [SerializeField] private TMP_InputField displayNameInput;
    [SerializeField] private GameObject uploadButton, imagePreview;
    [SerializeField] private SelectImage selectImage;
    [SerializeField] private Sprite circle;
    [SerializeField] private DialogueBox dialogueBox;

    private byte[] profilePicture;

    /// <summary>
    ///     Functionality for the upload image button.
    /// </summary>
    public void UploadImageButton()
    {
        StartCoroutine(UploadImageCoroutine());
    }

    private IEnumerator UploadImageCoroutine()
    {
        profilePicture = selectImage.Select();
        yield return new WaitUntil(() => profilePicture != null);

        Sprite profile = selectImage.ConvertSprite(profilePicture);
        imagePreview.GetComponent<Image>().sprite = profile;
        imagePreview.SetActive(true);

        uploadButton.GetComponent<Image>().sprite = circle;
        uploadButton.GetComponent<Mask>().enabled = true;
    }

    /// <summary>
    ///     Check if a display name is between 4-16 characters.
    /// </summary>
    /// <param name="displayName">The name to check.</param>
    /// <returns>If the display name is acceptable</returns>
    private bool VerifyDisplayName(string displayName)
    {
        int len = displayName.Length;
        if (len > 3 && len < 17)
        {
            return true;
        }
        else
        {
            dialogueBox.Enable(displaynameError);
            return false;
        }
    }

    private bool VerifyProfile(Sprite profile)
    {
        if(profile != null)
        {
            return true;
        }
        else
        {
            dialogueBox.Enable(imageError);
            return false;
        }
    }

    public void NextButton()
    {
        string displayName = displayNameInput.text;
        if (!VerifyDisplayName(displayName) || profilePicture == null) return;
        loginManager.NewAccount.DisplayName = displayName;
        loginManager.NewAccount.ProfilePicture = profilePicture;

        loginManager.ChangePanel(nextPanel);
    }
}
