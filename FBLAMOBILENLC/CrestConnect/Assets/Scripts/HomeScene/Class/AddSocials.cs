using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Manages the adding socials function on the edit class panel.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/20/23
/// </remarks>
public class AddSocials : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject addSocialPrefab, addButton;
    [SerializeField] private Transform contentTransform;


    private List<SocialLink> links;
    public List<SocialLink> Links { get { return links; } }

    public void LoadSocials(List<SocialLink> socialLinks)
    {
        links = socialLinks;
        foreach(Transform t in contentTransform)
        {
            if (t.gameObject == addButton) continue;
            Destroy(t.gameObject);
        }

        foreach (SocialLink s in links)
        {
            GameObject newLink = Instantiate(addSocialPrefab, contentTransform);

            Dropdown d = newLink.GetComponentInChildren<Dropdown>();
            d.value = (int)s.SocialType;
            d.onValueChanged.AddListener((int i) =>
            {
                //s.SocialType = SocialTypes
            });

            newLink.GetComponentInChildren<TMP_InputField>().text = s.SocialURL;
            newLink.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                DeleteSocialButton(newLink, s);
            });
        }

        addButton.transform.SetAsLastSibling();
    }

    public void DeleteSocialButton(GameObject social, SocialLink s)
    {
        links.Remove(s);
        Destroy(social);
    }
}
