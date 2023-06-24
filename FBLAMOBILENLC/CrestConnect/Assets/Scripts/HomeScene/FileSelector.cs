//using UnityEngine;
//using System.IO;

///// <summary>
/////     Manages selecting files.
///// </summary>
///// <remarks>
/////     Hanlin Zhang
/////     Last Modified: 6/21/2023
///// </remarks>
//public class FileSelector : MonoBehaviour
//{
//    /// <summary>
//    ///     Selects a single image.
//    /// </summary>
//    public Sprite SelectImage()
//    {
//        Texture2D image;
//        string imagePath = "";


//        if (NativeFilePicker.IsFilePickerBusy()) return;

//        CheckPermission();

//        // File types to look for
//        string[] fileTypes = new string[] { "image/*" };

//        // Finds File
//        NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
//        {
//            Debug.Log(path);
//            imagePath = path;
//        }, fileTypes);

//        // Convert raw data to usable image.
//        byte[] imageBytes = File.ReadAllBytes(imagePath);
//        image = new Texture2D(1, 1);
//        image.LoadImage(imageBytes);

//        return Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        
//    }

//    private void CheckPermission()
//    {
//        switch (NativeFilePicker.CheckPermission())
//        {
//            case (NativeFilePicker.Permission.Denied):
//                NativeFilePicker.OpenSettings();
//                break;
//            case (NativeFilePicker.Permission.ShouldAsk):
//                NativeFilePicker.RequestPermission();
//                break;
//            case (NativeFilePicker.Permission.Granted):
//                break;
//        }
//    }
//}
