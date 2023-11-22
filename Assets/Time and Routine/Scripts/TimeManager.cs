using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChange;
    public static Action OnHourChange;
    
    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    private float minuteToRealTime = 1f;
    private float timer;
    void Start()
    {
        Minute = 0;
        Hour = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Minute++;
            OnMinuteChange?.Invoke();
            if (Minute >= 60)
            {
                Hour++;
                OnHourChange?.Invoke();
                Minute = 0;
            }

            timer = minuteToRealTime;
        }
    }
}
