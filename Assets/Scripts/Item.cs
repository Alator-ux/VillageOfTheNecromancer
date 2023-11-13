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
    private int count = 1;
    [SerializeField]
    private int stackSize = 10;
    [SerializeField]
    private GameObject prefab;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public Sprite Image { get => image; set => image = value; }
    
    public int Count { get => count; }

    public void AddToStack(int count)
    {
        if (count <= 0 || (this.count + count) > stackSize)
            return;

        this.count += count;
    }

    public void RemoveFromStack(int count)
    {
        if (count <= 0 || (this.count - count) <= stackSize)
            return;

        this.count -= count;
    }

    public void Drop(Vector3 position, Quaternion? rotation = null)
    {
        Quaternion actualRotation = rotation ?? Quaternion.identity;

        Instantiate(prefab, position, actualRotation);
    }

    public bool IsStack => count > 1;

    public bool IsEnded => count == 0;
}
