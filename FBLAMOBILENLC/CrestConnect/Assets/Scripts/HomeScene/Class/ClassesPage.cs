using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Manages displaying the user's classes.
/// </summary>
/// <remarks>
///     Hanlin Zhang
///     Last Modified: 6/17/2023
/// </remarks>
public class ClassesPage : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject ClassPrefab, viewClassPanel;
    [SerializeField] private Transform ContentTransform;
    [SerializeField] private ViewClass viewClass;
    [SerializeField] private HomeManager homeManager;

    private FileManager fileManager;
    private AccountManager accountManager;

    // Needs implementation
    public void LoadClassesList()
    {
        accountManager = SingletonManager.Instance.AccountManagerInstance;
        fileManager = SingletonManager.Instance.FileManagerInstance;

        List<int> classes = accountManager.AllClasses;
        if(classes is null)
        {
            classes = new List<int>();
        }

        if (!fileManager.ClassDictionaryFile.IsLoaded) fileManager.ClassDictionaryFile.Load();
        ClassDictionary classDictionary = fileManager.ClassDictionaryFile.Data;

        foreach(Transform t in ContentTransform)
        {
            Destroy(t.gameObject);
        }

        foreach (int i in classes)
        {
            GameObject NewSelectClass = Instantiate(ClassPrefab, ContentTransform);
            SelectClassReferences references = NewSelectClass.GetComponent<SelectClassReferences>();
            string className = classDictionary.GetClassName(i);

            references.ClassName.SetText(className);
            references.ClassCode.SetText(i.ToString());
            references.Button.onClick.AddListener(() =>
            {
                Debug.Log(i);
                ClassDetailButton(i);
            });

        }
    }

    public void ClassDetailButton(int classCode)
    {
        viewClass.LoadViewClass(classCode);
        homeManager.ChangePanel(viewClassPanel);
    }

    private void Awake()
    {
        accountManager = SingletonManager.Instance.AccountManagerInstance;
        fileManager = SingletonManager.Instance.FileManagerInstance;
        LoadClassesList();
    }
}
