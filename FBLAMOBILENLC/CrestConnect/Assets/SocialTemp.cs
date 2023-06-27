using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialTemp : MonoBehaviour
{
    public GameObject prefab;
    public Transform content;
    public Transform button;
    public void AddSocial()
    {
        GameObject obj = Instantiate(prefab, content);
        button.SetAsLastSibling();
    }
}
