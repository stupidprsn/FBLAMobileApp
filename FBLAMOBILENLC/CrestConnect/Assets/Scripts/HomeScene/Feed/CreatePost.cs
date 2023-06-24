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
    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_InputField textInput;
    [SerializeField] private HomeManager homeManger;
    [SerializeField] private GameObject feedPanel;
    [SerializeField] private TMP_Dropdown selectClassDropdown;
    [SerializeField] private SelectImage selectImage;

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

    public void SelectImageButton()
    {
        rawImage = selectImage.Select();
        if (rawImage is null) return;
        newSprite = selectImage.ConvertSprite(rawImage);
        buttonImage.sprite = newSprite;

        imageSelected = true;
    }

    public void FinalizePost()
    {
        Account account = fileManager.AccountFile.Data;
        string text = textInput.text;
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
    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        LoadClassDropdown();
    }
}
