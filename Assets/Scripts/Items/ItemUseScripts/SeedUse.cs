using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedUse : ItemUse
{
    private Seed seed;

    private Inventory inventory;

    private Color freeSoilColor = Color.green;
    private Color occupiedSoilColor = Color.red;
    private Color baseSoilColor = Color.white;

    private float maxDistance = 2.0f;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        seed = (Seed)item;
        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        Cursor.visible=false;
    }

    public void SoilEnter(Soil soil)
    {
        baseSoilColor = soil.SpriteRenderer.color;
    }

    public void SoilHover(Soil soil)
    {
        
        if (CanPlant(soil))
        {
            soil.SpriteRenderer.color = freeSoilColor;
        }
        else
        {
            soil.SpriteRenderer.color = occupiedSoilColor;
        }
    }

    public void SoilLeave(Soil soil)
    {
        soil.SpriteRenderer.color = baseSoilColor;
    }

    public void SoilClick(Soil soil)
    {
        if (CanPlant(soil))
        {
            Debug.Log("Try plant");
            soil.Plant(seed);
        }
    }

    private bool CanPlant(Soil soil)
    {
        float distance = Vector3.Distance(transform.position, soil.transform.position);
        return distance <= maxDistance && soil.IsFree;
    }
}
