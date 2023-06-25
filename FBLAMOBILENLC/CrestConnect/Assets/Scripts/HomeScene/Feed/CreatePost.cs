using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
///     Creates functionality for posting to the feed.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     last Modified: 6/21/2023
/// </remarks>
public class CreatePost : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform feedContent;
    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_InputField textInput;
    [SerializeField] private HomeManager homeManger;
    [SerializeField] private GameObject feedPanel;
    [SerializeField] private TMP_Dropdown selectClassDropdown;
    [SerializeField] private SelectImage selectImage;
    [SerializeField] private DialogueBox dialogueBox; 

    private bool imageSelected = false;
    private FileManager fileManager;
    private byte[] rawImage;
    private List<int> ownedClasses;
    private Sprite newSprite;

    /// <summary>
    ///     Loads all of the user's owned classes into the select class dropdown.
    /// </summary>
    public void LoadClassDropdown()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        selectClassDropdown.ClearOptions();
        ownedClasses = fileManager.AccountFile.Data.OwnedClasses;

        List<string> ownedClassesString = ownedClasses.ConvertAll<string>(x => fileManager.ClassDictionaryFile.Data.GetClassName(x));
        selectClassDropdown.AddOptions(ownedClassesString);
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

    public void SelectImageButton()
    {
        CheckPermissions();

        if (NativeGallery.IsMediaPickerBusy()) return;

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Texture2D texture;
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                rawImage = File.ReadAllBytes(path);
                texture = NativeGallery.LoadImageAtPath(path);
                buttonImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                imageSelected = true;
            }
        });


    }

    public void FinalizePost()
    {
        Account account = SingletonManager.Instance.FileManagerInstance.AccountFile.Data;
        string text = textInput.text;

        if(text.Equals(string.Empty) && !imageSelected)
        {
            dialogueBox.Enable("Please fill out the post information.");
        }

        int classID = account.OwnedClasses[selectClassDropdown.value];
        FeedPost feedPost;
        if (imageSelected)
        {
            feedPost = new FeedPost(
                classID,
                account.DisplayName,
                account.Username,
                text,
                DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"),
                rawImage
                );
        }
        else
        {
            feedPost = new FeedPost(
                classID,
                account.DisplayName,
                account.Username,
                text,
                DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt")
            );
        }
        imageSelected = false;
        fileManager.CreatePost( feedPost );

        homeManger.ResetScreens();
        homeManger.ChangePanel(feedPanel);
        LayoutRebuilder.ForceRebuildLayoutImmediate(feedContent.GetComponent<RectTransform>());

    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        selectImage = SingletonManager.Instance.SelectImageInstance;
        LoadClassDropdown();
    }
}
