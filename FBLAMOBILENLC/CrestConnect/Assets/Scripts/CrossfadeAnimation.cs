using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossfadeAnimation : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;



    public void LoadNextScene()
    {
        int sceneToLoad;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            sceneToLoad = 1;
        }
        else
        {
            sceneToLoad = 0;
        }

        StartCoroutine(LoadScene(sceneToLoad));
    }

    IEnumerator LoadScene(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
