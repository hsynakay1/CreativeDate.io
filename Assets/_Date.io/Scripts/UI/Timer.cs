using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timer;

    public static Timer Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InvokeRepeating("UpdateTimer", 0f, 1f);
    }

    void UpdateTimer()
    {
        if (GameManager.Instance.gameStades == GameStades.Draw)
        {
            timer -= 1f;

            if(timer <= 0f)
            {
                timer = 0f;
            }
            UpdateUI();
        }
        
    }

    void UpdateUI()
    {
        timerText.text = "Time Left: " + Mathf.RoundToInt(timer);
    }
}
