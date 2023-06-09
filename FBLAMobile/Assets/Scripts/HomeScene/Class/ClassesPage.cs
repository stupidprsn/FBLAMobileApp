using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClassesPage : MonoBehaviour
{
    [SerializeField] HomeManager homeManager;
    [SerializeField] GameObject classPrefab;
    [SerializeField] Transform classContent;

    [SerializeField] GameObject joinClassPanel;
    [SerializeField] GameObject[] joinDialogues;

    FileManager fileManager;

    public void ShowClasses()
    {
        //errorText.gameObject.SetActive(false);

        foreach (Transform child in classContent)
        {
            Destroy(child.gameObject);
        }

        if (fileManager.AccountFile.Data.AccountType == AccountType.Teacher)
        {
            foreach (DataFile<AClass> aClass in fileManager.OwnClassList)
            {
                aClass.Load();
                NewClassView(false, aClass.Data);
            }
        }
        else
        {
            foreach (DataFile<AClass> aClass in fileManager.InClassList)
            {
                aClass.Load();
                NewClassView(false, aClass.Data);
            }

        }
    }

    private void NewClassView(bool isOwn, AClass aClass)
    {
        Transform newPrefab = Instantiate(classPrefab, classContent).transform;
        newPrefab.Find("ClassName").GetComponent<TMP_Text>().SetText(aClass.name);
        newPrefab.Find("ClassCode").GetComponent<TMP_Text>().SetText(aClass.joinCode.ToString());
        newPrefab.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            fileManager.AccountFile.Data.LeaveClass(isOwn, aClass.joinCode);
            ShowClasses();
        });
    }

    public void JoinClass()
    {
        foreach (GameObject dialogue in joinDialogues)
        {
            dialogue.SetActive(false);
        }
        homeManager.ChangePanel(joinClassPanel);
    }

    private void Awake()
    {
        fileManager = FileManager.Instance;
    }

    private void Start()
    {
        ShowClasses();
    }
}
