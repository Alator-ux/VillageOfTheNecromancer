using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Watering Can", menuName = "Items/WateringCan")]
public class WateringCan : Item
{
    public GameObject WateringCanInHandPrefab;
    private void OnEnable()
    {
        UseScriptName = "WateringCanUse";
    }
}
