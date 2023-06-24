using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Fills the view class panel.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/20/23
/// </remarks>
public class ViewClass : MonoBehaviour
{
    [Header("Socials")]
    [SerializeField] private Sprite[] socialImgs;

    [Header("References")]
    [SerializeField] private TMP_Text classTitle, descriptionTitle, descriptionText, JoinTitle, joinText;
    [SerializeField] private Transform socialsContent;
    [SerializeField] private GameObject socialPrefab, editButton;

    private FileManager fileManager;
    
    /// <summary>
    ///     Loads the panel.
    /// </summary>
    /// <param name="classCode">Join code</param>
    public void LoadViewClass(int classCode)
    {
        // Get class data.
        AClass aClass = fileManager.GetClassFile(classCode).Data;

        // Update text
        classTitle.SetText(aClass.name);
        descriptionText.SetText(aClass.description);
        joinText.SetText(aClass.howToJoin);

        // If user is class owner, enable the edit button
        if(aClass.owner.Equals(fileManager.AccountFile.Data.Username)) editButton.SetActive(true);

        foreach(Transform t in socialsContent)
        {
            Destroy(t.gameObject);
        }

        foreach(SocialLink s in aClass.socials)
        {
            GameObject newSocial = Instantiate(socialPrefab, socialsContent);

            newSocial.GetComponent<Image>().sprite = socialImgs[(int)s.SocialType];
            newSocial.GetComponent<Button>().onClick.AddListener(() =>
            {
                Application.OpenURL(s.SocialURL);
            });
        }

    }

    private void Start()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
    }
}
