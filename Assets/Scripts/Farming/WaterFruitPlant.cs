using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterFruitPlant : Plant
{
    protected override void OnPickUp(GameObject interactor)
    {
        var neighbourSoils = SoilManager.Instance.Neighbours(Soil);
        foreach (var neighbour in neighbourSoils) {
            Debug.Log("Water neighbour!");
            neighbour.Water();
        }
    }

    protected override void AfterPickUp()
    {
        CurrentState = PlantState.Child;
        GrowPoints = childGrowPointsThreshold;
        UpdateSprite();
    }
}
