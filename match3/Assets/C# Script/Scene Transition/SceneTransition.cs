using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneTransition : MonoBehaviour
{
    public void Start_Scene_Transform()
    {
        StartCoroutine(LoadAcync());
    }
    IEnumerator LoadAcync()
    {
        AsyncOperation Loadass = SceneManager.LoadSceneAsync("Game");
        Loadass.allowSceneActivation = false;
        while (!Loadass.isDone)
        {
            if (Loadass.progress >= 0.9f && !Loadass.allowSceneActivation)
                if (!Loadass.allowSceneActivation)
                {
                    Loadass.allowSceneActivation = true;
                }
            yield return null;
        }
    }
}


