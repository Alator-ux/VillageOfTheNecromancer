using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Watering Can", menuName = "Items/WateringCan")]
public class WateringCan : Item
{
    private void OnEnable()
    {
        // Stupidest shit in the world.
        string assetPath = "Assets/Scripts/Items/ItemUseScripts/WateringCanUse.cs";
        itemUseScript = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);
    }
}
