using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory Item")]
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
    private GameObject prefab;

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

    public void Drop(Vector3 position, Quaternion? rotation = null)
    {
        Quaternion actualRotation = rotation ?? Quaternion.identity;

        Instantiate(prefab, position, actualRotation);
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
