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
    [SerializeField] private TMP_Text classTitle;
    [SerializeField] private Transform socialsContent, content, socialTitle;
    [SerializeField] private GameObject socialPrefab, editButton, titlePrefab, textPrefab;
    [SerializeField] private EditClass editClass;

    private FileManager fileManager;

    int joinCode;

    public void EditButton()
    {
        editClass.OpenEditClass(joinCode);
        FindObjectOfType<HomeManager>().ChangePanel(editClass.gameObject);
    }

    /// <summary>
    ///     Loads the panel.
    /// </summary>
    /// <param name="classCode">Join code</param>
    public void LoadViewClass(int classCode)
    {
        joinCode = classCode;
        fileManager = SingletonManager.Instance.FileManagerInstance;
        AClass aClass = fileManager.GetClassFile(classCode).Data;
        Debug.Log(classCode);
        Debug.Log(aClass.socials);
        LoadViewClass(aClass);
    }

    public void LoadViewClass(AClass aClass)
    {
        joinCode = aClass.joinCode;
        foreach (Transform t in content)
        {
            if(t.CompareTag("Delete"))
            {
                Destroy(t.gameObject);
            }
        }

        // If user is class owner, enable the edit button
        //if (aClass.owner.Equals(fileManager.AccountFile.Data.Username)) editButton.SetActive(true);

        // Update text
        classTitle.SetText(aClass.name);

        List<string> text = aClass.text;
        GameObject newTitle;
        for (int i = 0; i < text.Count; i++)
        {
            // title
            if (i % 2 == 0)
            {
                newTitle = Instantiate(titlePrefab, content);
                newTitle.GetComponent<TMP_Text>().SetText(text[i]);
            }
            else
            {
                newTitle = Instantiate(textPrefab, content);
                newTitle.GetComponent<TMP_Text>().SetText(text[i]);
            }
        }
        socialTitle.SetAsLastSibling();
        socialsContent.SetAsLastSibling();

        foreach (Transform t in socialsContent)
        {
            Destroy(t.gameObject);
        }

        foreach (SocialLink s in aClass.socials)
        {
            GameObject newSocial = Instantiate(socialPrefab, socialsContent);

            newSocial.GetComponent<Image>().sprite = socialImgs[(int)s.SocialType];
            newSocial.GetComponent<Button>().onClick.AddListener(() =>
            {
                Application.OpenURL(s.SocialURL);
            });
        }

        //membersContent.SetAsLastSibling();

        //foreach (Transform t in membersContent)
        //{
        //    Destroy(t.gameObject);
        //}

        //foreach (string s in aClass.members)
        //{
        //    GameObject newMember = Instantiate(memberPrefab, membersContent);
        //    // Code
        //}
    }

    private void Awake()
    {
        fileManager = SingletonManager.Instance.FileManagerInstance;
    }
}
