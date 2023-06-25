using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EditAccount : MonoBehaviour
{
    [SerializeField, TextArea] private string displaynameError, imageChanged, nameChanged, bothChanged, noneChanged;


    public TMP_InputField displayNameInput;
    public Image buttonImage;
    public DialogueBox dialogueBox;
    public GameObject settingsHome;
    public HomeManager homeManager;
    private FileManager fileManager;

    private byte[] profilePicture;
    bool imageSelected;

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

                buttonImage.GetComponent<Image>().sprite = ImageSprite;
                imageSelected = true;
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

    private void OnEnable()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        displayNameInput.text = fileManager.AccountFile.Data.DisplayName;
        buttonImage.sprite = fileManager.GetProfilePicture(fileManager.AccountFile.Data.Username);
    }

    public void SaveButton()
    {
        string newName = displayNameInput.text;
        
        if(!VerifyDisplayName(newName))
        {
            return;
        }

        bool nameSelected = newName != fileManager.AccountFile.Data.DisplayName;

        if(nameSelected && imageSelected)
        {
            fileManager.AccountFile.Data.DisplayName = newName;
            fileManager.AccountFile.Data.ProfilePicture = profilePicture;
            fileManager.AccountFile.Save();
            dialogueBox.Enable(bothChanged, () => homeManager.ChangePanel(settingsHome));
            homeManager.ResetScreens();
        }
        else if (nameSelected)
        {
            fileManager.AccountFile.Data.DisplayName = newName;
            fileManager.AccountFile.Save();
            dialogueBox.Enable(nameChanged, () => homeManager.ChangePanel(settingsHome));
            homeManager.ResetScreens();
        }
        else if (imageSelected)
        {
            fileManager.AccountFile.Data.ProfilePicture = profilePicture;
            fileManager.AccountFile.Save();
            dialogueBox.Enable(imageChanged, () => homeManager.ChangePanel(settingsHome));
            homeManager.ResetScreens();
        }
        else
        {
            dialogueBox.Enable(noneChanged);
        }
    }
}
