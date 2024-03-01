using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLvl : MonoBehaviour
{
    public void Start_Scene_Transform()
    {
        StartCoroutine(LoadAcync());
    }
    private IEnumerator LoadAcync()
    {
        AsyncOperation Loadass = SceneManager.LoadSceneAsync("Game lvl 2");
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
