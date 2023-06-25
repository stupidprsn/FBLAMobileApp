using TMPro;
using UnityEngine;

public class ReportBug : MonoBehaviour
{
    public TMP_InputField inputField;
    public DialogueBox dialogueBox;
    public HomeManager homeManager;
    public GameObject settingsHome;
    public void SubmitButton()
    {
        string text = inputField.text;
        if(text.Equals(string.Empty)) {
            dialogueBox.Enable("Please fill out the form.");
        }
        else
        {
            dialogueBox.Enable("Thank you!", () => { homeManager.ChangePanel(settingsHome); });

        }
    }
}
