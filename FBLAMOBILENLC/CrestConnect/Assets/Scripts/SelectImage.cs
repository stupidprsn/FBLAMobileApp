using UnityEngine;
using System.IO;

/// <summary>
///     Allows the user to select an image.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/21/2023
/// </remarks>
public class SelectImage : MonoBehaviour
{
    /// <summary>
    ///     Selects an image and returns it.
    /// </summary>
    /// <returns>The image as a sprite.</returns>
    public byte[] Select()
    {
        bool pathEmpty = false;
        string imagePath = "";

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

        if (NativeFilePicker.IsFilePickerBusy()) return null;

        string[] fileTypes = new string[] { "image/*" };

#if UNITY_ANDROID
        fileTypes = new string[] { "image/*" };
#elif UNITY_IOS
        fileTypes = new string[] { "public.image" };
#endif

        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
        {
            Debug.Log(path);

            if (path is null) pathEmpty = true;

            imagePath = path;
        }, fileTypes);
        if (pathEmpty) return null;
        return File.ReadAllBytes(imagePath);

    }

    public Sprite ConvertSprite(byte[] raw)
    {
        Texture2D image;
        image = new Texture2D(1, 1);
        image.LoadImage(raw);

        return Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
    }
}
