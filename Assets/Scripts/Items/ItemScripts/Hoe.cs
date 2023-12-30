using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Hoe", menuName = "Items/Hoe")]
public class Hoe : Item
{
    [SerializeField]
    private SoilTile soilTile;

    public SoilTile SoilTile => soilTile;

    private void OnEnable()
    {
        UseScriptName = "HoeUse";
    }
}
