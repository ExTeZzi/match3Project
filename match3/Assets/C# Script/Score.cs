using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    public GameObject panelWin;
    //public WinLoseGame winlosegame;

    public static Score Instance 
    { 
        get; private set; 
    }
    private int _score;
    public int _Score
    {
        get => _score;
        set
        {
            if (_score == value) return;
            _score = value;

            ScoreGame.SetText($"Score = {_score}");

            if (_score >= 600)
            {
                panelWin.SetActive(true);
            }
        }
    }

    [SerializeField] private TextMeshProUGUI ScoreGame;
    private void Awake()
    {
        Instance = this;
    }
}
