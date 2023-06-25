using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectImage : MonoBehaviour
{
    public Sprite ImageSprite { get; private set; }
    public Texture2D ImageTexture2D { get; private set; }
    public byte[] ImageRaw { get; private set; }

    public bool NewImage { get; private set; }
    public bool DoneLoading { get; private set; }

    public void Select()
    {
        DoneLoading = false;
        NewImage = true;

        CheckPermissions();
        
        if (NativeGallery.IsMediaPickerBusy()) return;

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                ImageRaw = File.ReadAllBytes(path);
                ImageTexture2D = NativeGallery.LoadImageAtPath(path);
                ImageSprite = Sprite.Create(ImageTexture2D, new Rect(0, 0, ImageTexture2D.width, ImageTexture2D.height), new Vector2(0.5f, 0.5f));
            }
        });

        DoneLoading = true;
        NewImage = false;
    }
    public Sprite ConvertSprite(byte[] raw)
    {
        Texture2D image;
        image = new Texture2D(1, 1);
        image.LoadImage(raw);

        return Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
    }

    private void Start()
    {
        CheckPermissions();
        NewImage = false;
        DoneLoading = false;
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
