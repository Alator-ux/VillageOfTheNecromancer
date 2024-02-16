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

    [SerializeField]
    private float gameMinuteToRealTimeSeconds = 0.2f;

    [SerializeField]
    private int startingHour = 12;

    private float timer;

    void Start()
    {
        Minute = 0;
        Hour = startingHour;
        timer = gameMinuteToRealTimeSeconds;

        OnHourChange?.Invoke();
        OnMinuteChange?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
            return;

        UpdateMinute();
        timer = gameMinuteToRealTimeSeconds;
    }

    private void UpdateMinute() {
        Minute++;

        if (Minute % 10 == 0) {
            OnMinuteChange?.Invoke();
        }

        if (Minute >= 60)
        {
            UpdateHour();
        }
    }

    private void UpdateHour() {
        Hour++;
        Minute = 0;

        if (Hour == 24) {
            Hour = 0;
        }

        OnHourChange?.Invoke();
    }
}
