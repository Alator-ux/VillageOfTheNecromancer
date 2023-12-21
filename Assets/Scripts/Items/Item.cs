using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private int stackSize = 10;
    [SerializeField]
    private GameObject droppedItemPrefab;

    [SerializeField]
    protected MonoScript itemUseScript;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public Sprite Image { get => image; set => image = value; }
    public int StackSize { 
        get => stackSize;
        set {
            if (value < 0)
                return;
            stackSize = value;
        }
    }
    public Type ItemUseScriptType { 
        get
        {
            if (itemUseScript == null)
                return null;

            var itemUseScriptType = itemUseScript.GetClass();

            if (!itemUseScriptType.IsSubclassOf(typeof(ItemUse))) return null;

            return itemUseScriptType;
        }
    }


    public void Drop(Vector3 position, Quaternion? rotation = null)
    {
        Quaternion actualRotation = rotation ?? Quaternion.identity;

        GameObject droppedItemObject = Instantiate(droppedItemPrefab, position, actualRotation);

        var pickableItemScript = droppedItemObject.GetComponent<PickableItem>();
        pickableItemScript.ItemWasDropped();
    }

    public override bool Equals(object other)
    {
        Item otherItem = other as Item;
        if (otherItem == null)
            return false;

        return name == otherItem.Name;
    }

    public override int GetHashCode()
    {
        return name.GetHashCode();
    }
}
