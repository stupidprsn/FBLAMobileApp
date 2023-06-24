using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IndividualChat : MonoBehaviour
{
    [SerializeField] private GameObject navBar;
    [SerializeField] private HomeManager homeManager;
    [SerializeField] private GameObject chat;
    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_InputField input;
    public void ExitChat()
    {
        navBar.SetActive(true);
        homeManager.ChangePanel(chat);
    }

    public void SendAMessage()
    {
        text.text += "\n" + input.text;
    }
}
