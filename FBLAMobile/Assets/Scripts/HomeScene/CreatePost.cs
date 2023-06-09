using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System;

public class CreatePost : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_InputField textInput;
    [SerializeField] private HomeManager homeManger;
    [SerializeField] private GameObject feedPanel;
    private FileManager fileManager;
    private Texture2D image;
    private string imagePath;
    public void SelectImage()
    {
        switch(NativeFilePicker.CheckPermission())
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

        if (NativeFilePicker.IsFilePickerBusy()) return;

        string[] fileTypes = new string[] { "image/*" };

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            Debug.Log(path);
            imagePath = path;
        }, fileTypes);

        byte[] imageBytes = File.ReadAllBytes(imagePath);
        image = new Texture2D(1, 1);
        image.LoadImage(imageBytes);

        Sprite newSprite = Sprite.Create(image, new Rect(0,0, image.width, image.height), new Vector2(0.5f, 0.5f));
        buttonImage.sprite = newSprite;
    }

    public void FinalizePost()
    {
        Account account = fileManager.AccountFile.Data;
        string text = textInput.text;
        FeedPost feedPost = new FeedPost(account.ID, "", account.DisplayName, text, DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt"), imagePath);
        fileManager.feedList.Data.list.Add(feedPost);
        fileManager.feedList.Save();
        homeManger.ResetScreens();
        homeManger.ChangePanel(feedPanel);
    }

    private void Start()
    {
        fileManager = FileManager.Instance;
    }
}
