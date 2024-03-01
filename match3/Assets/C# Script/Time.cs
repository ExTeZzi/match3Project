using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class time : MonoBehaviour
{
    public float repeat_time;
    private float curr_time;
    public float timeStart = 60;
    public Text timerText;

    public GameObject panelLose;
    public GameObject Return;



    void Start()
    {
        timerText.text = timeStart.ToString();

        curr_time = repeat_time * 10f;
    }

    void Update()
    {
        timeStart -= Time.deltaTime;
        timerText.text = Mathf.Round(timeStart).ToString();
        curr_time -= Time.deltaTime; /* Вычитаем из 10 время кадра (оно в миллисекундах) */
        if (curr_time <= 0) /* Время вышло пишем */
        {
            panelLose.SetActive(true); //Degug.Log("Прошло 10 сек!");
            Time.timeScale = 0;
        }
        
    }

    public void backinReturn()
    {
        Return.SetActive(true);
    }
}
