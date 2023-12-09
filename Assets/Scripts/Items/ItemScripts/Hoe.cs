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
        // Stupidest shit in the world.
        string assetPath = "Assets/Scripts/Items/ItemUseScripts/HoeUse.cs";
        itemUseScript = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);
    }
}
