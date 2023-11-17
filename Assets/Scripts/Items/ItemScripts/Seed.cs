using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Seed Item", menuName = "Items/Seed")]
public class Seed : Item
{
    [SerializeField]
    private Plant plantPrefab;
    public Plant PlantPrefab { get => plantPrefab; }

    private void OnEnable()
    {
        // Stupidest shit in the world.
        string assetPath = "Assets/Scripts/Items/ItemUseScripts/SeedUse.cs";
        itemUseScript = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath);
    }
}
