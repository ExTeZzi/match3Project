using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public GameObject play;


    public void OnEnable()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
        play.SetActive(false);
    }

    public void OnDisable()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
        play.SetActive(true);
    }
}
