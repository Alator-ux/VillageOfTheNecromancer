using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    [SerializeField]
    private int initialEnergyPoints = 2;

    private int maxEnergyPoints;

    public int EnergyPoints { get; private set; }

    [SerializeField]
    private DayNightController dayNightController;

    void Start()
    {
        maxEnergyPoints = initialEnergyPoints;

        EnergyPoints = 0;

        dayNightController.DaytimeChanged += OnDaytimeChanged;
    }

    void OnDaytimeChanged() {
        if (dayNightController.Night) {
            EnergyPoints = maxEnergyPoints;
        } else {
            EnergyPoints = 0;
        }
    }

    public bool ConsumeEnergy(int points = 1) {
        Debug.Log("Current energy: " + EnergyPoints);

        if (points > EnergyPoints) return false;

        EnergyPoints -= points;
        return true;
    }

    public bool HasEnergy { get => EnergyPoints > 0; }
}
