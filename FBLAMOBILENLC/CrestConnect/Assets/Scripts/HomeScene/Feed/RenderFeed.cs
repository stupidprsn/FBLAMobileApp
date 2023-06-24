using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Manages rendering the feed.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/21/2023
/// </remarks>
public class RenderFeed : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HomeManager homeManager;
    [SerializeField] private SelectImage selectImage;
    [SerializeField] private GameObject imgPrefab;
    [SerializeField] private GameObject noImgPrefab;
    [SerializeField] private Transform content;

    private FileManager fileManager;
    private AccountManager accountManager;

    /// <summary>
    ///     Loads the feed.
    /// </summary>
    public void UpdateScreen()
    {
        // Clear past feed (except for loading text).
        foreach(Transform t in content)
        {
            Destroy(t.gameObject);
        }

        FeedList feedList = fileManager.FeedListFile.Data;
        
        FeedPost feedPost;
        GameObject newPost;
        PostReferences postReferences;
        bool renderPost;
        int siblingIndex = 0;

        // Start from latest feed.
        for(int i = feedList.CurrentIndex - 1; i >= 0; i--)
        {
            feedPost = fileManager.GetPost(i);

            // Check that the post applies to the user.
            renderPost = false;
            foreach(int j in accountManager.AllClasses)
            {
                if(j == feedPost.ClassID)
                {
                    renderPost = true;
                    break;
                }
            }
            if (!renderPost) continue;


            if(feedPost.HasImage)
            {
                newPost = Instantiate(imgPrefab, content);
                newPost.SetActive(false);
                postReferences = newPost.GetComponent<PostReferences>();
                postReferences.PostImage.sprite = selectImage.ConvertSprite(feedPost.ImgID);

            }
            else
            {
                newPost = Instantiate(noImgPrefab, content);
                newPost.SetActive(false);
                postReferences = newPost.GetComponent<PostReferences>();
            }
            newPost.transform.SetSiblingIndex(siblingIndex);
            postReferences.AuthorText.SetText(feedPost.TeacherName + " | " + fileManager.ClassDictionaryFile.Data.GetClassName(feedPost.ClassID));
            postReferences.TimeText.SetText(feedPost.Time);
            postReferences.PostText.SetText(feedPost.Content);
            Debug.Log(feedPost.Username);
            postReferences.ProfilePicture.sprite = fileManager.GetProfilePicture(feedPost.Username);
            newPost.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(newPost.GetComponent<RectTransform>());
            siblingIndex++;
        }
    }

    public void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        accountManager = SingletonManager.Instance.AccountManagerInstance;
        UpdateScreen();
    }
}
