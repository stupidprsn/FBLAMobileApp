using System.Collections;
using TMPro;
using UnityEngine;
using System.IO;
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
    [SerializeField] private Sprite circle;
    [SerializeField] private DialogueBox dialogueBox;

    private byte[] profilePicture;

    /// <summary>
    ///     Functionality for the upload image button.
    /// </summary>
    public void UploadImageButton()
    {
        CheckPermissions();

        if (NativeGallery.IsMediaPickerBusy()) return;

        Texture2D ImageTexture2D;
        Sprite ImageSprite;

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                profilePicture = File.ReadAllBytes(path);
                ImageTexture2D = NativeGallery.LoadImageAtPath(path);
                ImageSprite = Sprite.Create(ImageTexture2D, new Rect(0, 0, ImageTexture2D.width, ImageTexture2D.height), new Vector2(0.5f, 0.5f));

                imagePreview.GetComponent<Image>().sprite = ImageSprite;
                imagePreview.SetActive(true);

                uploadButton.GetComponent<Image>().sprite = circle;
                uploadButton.GetComponent<Mask>().enabled = true;
            }
        });
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

    private bool VerifyProfile()
    {
        if (profilePicture != null)
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
        if (!VerifyDisplayName(displayName) || !VerifyProfile())
        {
            return;
        }

        loginManager.NewAccount.DisplayName = displayName;
        loginManager.NewAccount.ProfilePicture = profilePicture;

        loginManager.ChangePanel(nextPanel);
    }

    private void CheckPermissions()
    {
        switch (NativeFilePicker.CheckPermission())
        {
            case (NativeFilePicker.Permission.Denied):
                NativeFilePicker.OpenSettings();
                break;
            case (NativeFilePicker.Permission.ShouldAsk):
                NativeFilePicker.RequestPermission();
                break;
            case (NativeFilePicker.Permission.Granted):
                break;
        }
    }
}
