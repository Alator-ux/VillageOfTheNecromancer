using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DayNightController : MonoBehaviour
{
    public bool Day { get; private set; }

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
        TimeManager.OnHourChange += HourChanged;
    }

    private void HourChanged() {
        if (TimeManager.Hour == nightTimeHour) {
            SwitchDayTime();
        }
    }

    private void SwitchDayTime()
    {
        Debug.Log("Daytime switch!");
        Day = !Day;

        var newColor = Day? dayColor : nightColor;
        defaultMaterial.color = newColor;
    }
}
