using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisccoverButton : MonoBehaviour
{
    [SerializeField] private string clubName;
    [SerializeField, TextArea] private List<string> text;
    [SerializeField] private List<string> socialLinksURLS;
    [SerializeField] private List<SocialTypes> socialTypes;

    public List<SocialLink> socialLinks;
    public AClass aClass;

    private void Start()
    {
        aClass = new AClass(0, "debug", clubName);
        socialLinks = new();
        for(int i = 0; i < socialTypes.Count; i++)
        {
            socialLinks.Add(new SocialLink(socialTypes[i], socialLinksURLS[i]));
        }
        aClass.socials = socialLinks;
        aClass.text = text;

        GetComponent<Button>().onClick.AddListener(() => FindObjectOfType<ViewClass>().LoadViewClass(aClass));
    }

}
