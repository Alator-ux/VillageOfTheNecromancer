using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DayNightController : MonoBehaviour
{
    public Action DaytimeChanged;
    public bool Day { get; private set; }
    public bool Night { get => !Day; }

    [SerializeField]
    private int nightTimeHour = 0;

    [Space(4.0f)]
    [SerializeField]
    private Material defaultMaterial;

    [Space(4.0f)]
    [SerializeField]
    private Color dayColor;
    [SerializeField]
    private Color nightColor;

    void Start()
    {
        Day = true;
        defaultMaterial.color = CurrentColor;
        TimeManager.OnHourChange += HourChanged;
    }

    private void HourChanged() {
        if (TimeManager.Hour == nightTimeHour) {
            SwitchDayTime();
        }
    }

    private void SwitchDayTime()
    {
        Day = !Day;
        DaytimeChanged?.Invoke();

        defaultMaterial.color = CurrentColor;
    }

    private Color CurrentColor => Day? dayColor : nightColor;
}
