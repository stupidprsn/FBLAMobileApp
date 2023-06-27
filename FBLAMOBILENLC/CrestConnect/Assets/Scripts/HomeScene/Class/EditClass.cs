using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Creates functionality for the edit class panel.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/20/23
/// </remarks>
public class EditClass : MonoBehaviour
{
    [SerializeField] private TMP_Text joinCodeText;
    [SerializeField] private ViewClass viewClass;
    int classCode;
    //[Header("References")]
    //[SerializeField] private TMP_Text title, joinCodeTitle;
    //[SerializeField] private TMP_InputField nameInput, descriptionInput, joinInput;
    //[SerializeField] private Toggle publicToggle;
    //[SerializeField] private List<GameObject> textInputs;
    //private DataFile<AClass> aClassFile;
    //private AClass aClass;
    //public void OpenEditClass(int joinCode)
    //{
    //    aClassFile = SingletonManager.Instance.FileManagerInstance.GetClassFile(joinCode);
    //    title.SetText(aClass.name);
    //    joinCodeTitle.SetActive()
    //    foreach(GameObject textField in textInputs)
    //    {
    //        textField.SetActive(false);
    //    }
    //    for (int i = 0; i < aClass.text.Count; i++)
    //    {
    //        textInputs[i].GetComponent<TMP_InputField>().text = aClass.text[i];
    //        textInputs[i].SetActive(true);
    //    }
    //}

    //public void FinalizeButton()
    //{
    //    // Manage text

    //    // Manage socials
    //}


    public void OpenEditClass(int joinCode)
    {
        classCode = joinCode;
        joinCodeText.SetText(joinCode.ToString());
    }

    public void FinalizeButton()
    {
        DataFile<AClass> fblaClass = SingletonManager.Instance.FileManagerInstance.GetClassFile(classCode);
        fblaClass.Data.socials.Add(new SocialLink(SocialTypes.Instagram, "https://www.instagram.com/crestFBLA/"));
        fblaClass.Data.socials.Add(new SocialLink(SocialTypes.Twitter, "https://twitter.com/crest_fbla"));
        fblaClass.Save();
        FindObjectOfType<HomeManager>().ResetScreens();
        FindObjectOfType<HomeManager>().ChangePanel(viewClass.gameObject);
        viewClass.LoadViewClass(classCode);
    }
}
