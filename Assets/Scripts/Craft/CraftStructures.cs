using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
{
    public Item item;
    public int count = 1;
    public Ingredient(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
}