using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void OnEnable()
    {
        TimeManager.OnHourChange += UpdateTime;
        TimeManager.OnMinuteChange += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnHourChange -= UpdateTime;
        TimeManager.OnMinuteChange  -= UpdateTime;
    }


    
    void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
    }
}
