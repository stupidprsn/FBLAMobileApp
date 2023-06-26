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
    private SelectImage selectImage;
    [SerializeField] private GameObject imgPrefab;
    [SerializeField] private GameObject noImgPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject createPostButton;

    private FileManager fileManager;
    private AccountManager accountManager;

    /// <summary>
    ///     Loads the feed.
    /// </summary>
    public void UpdateScreen()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        if (fileManager.AccountFile.Data.OwnedClasses.Count == 0)
        {
            createPostButton.SetActive(false);
        }
        else
        {
            createPostButton.SetActive(true);
        }

        // Clear past feed (except for loading text).
        foreach (Transform t in content)
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
        for (int i = feedList.CurrentIndex - 1; i >= 0; i--)
        {
            feedPost = fileManager.GetPost(i);

            // Check that the post applies to the user.
            renderPost = false;
            foreach (int j in SingletonManager.Instance.AccountManagerInstance.AllClasses)
            {
                if (j == feedPost.ClassID)
                {
                    renderPost = true;
                    break;
                }
            }
            if (!renderPost) continue;

            float scale;
            float h;
            float w;
            if (feedPost.HasImage)
            {
                newPost = Instantiate(imgPrefab, content);
                newPost.SetActive(false);
                postReferences = newPost.GetComponent<PostReferences>();
                postReferences.PostImage.sprite = selectImage.ConvertSprite(feedPost.ImgID);
                postReferences.PostImage.SetNativeSize();
                h = postReferences.PostImage.rectTransform.rect.height;
                w = postReferences.PostImage.rectTransform.rect.width;

                if (h > 1000 || w > 1000)
                {
                    if (h > w)
                    {
                        scale = 1000 / h;
                        postReferences.PostImage.rectTransform.sizeDelta.Set(w * scale, h * scale);
                    }
                    else
                    {
                        scale = 1000 / w;
                        postReferences.PostImage.rectTransform.sizeDelta = new Vector2(w * scale, h * scale);
                    }
                }

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
            siblingIndex++;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
    }

    public void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
        accountManager = SingletonManager.Instance.AccountManagerInstance;
        selectImage = SingletonManager.Instance.SelectImageInstance;
        UpdateScreen();
    }
}
