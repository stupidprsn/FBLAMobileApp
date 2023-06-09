using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class RenderFeed : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform content;
    private FileManager fileManager;

    public void UpdateScreen()
    {
        Texture2D image;
        FeedList feedList = fileManager.feedList.Data;
        foreach(FeedPost post in feedList.list)
        {
            GameObject newPost = Instantiate(prefab, content);
            newPost.transform.SetAsFirstSibling();
            newPost.transform.Find("TeaherName").GetComponent<TMP_Text>().SetText(post.teacherName);
            newPost.transform.Find("Time").GetComponent<TMP_Text>().SetText(post.time);
            newPost.transform.Find("TheText").GetComponent<TMP_Text>().SetText(post.content);
            byte[] imageBytes = File.ReadAllBytes(post.imagePath);
            image = new Texture2D(1, 1);
            image.LoadImage(imageBytes);

            Sprite newSprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
            newPost.transform.Find("TheImage").GetComponent<Image>().sprite = newSprite;
        }
    }

    public void Start()
    {
        fileManager = FileManager.Instance;
    }
}
