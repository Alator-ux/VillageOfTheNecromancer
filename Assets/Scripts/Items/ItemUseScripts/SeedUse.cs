using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedUse : ItemUse
{
    private Seed seed;

    private Inventory inventory;

    private Color canPlantColor = Color.green;
    private Color cannotPlantColor = Color.red;

    private Soil highlightedSoil = null;

    private float maxDistance = 2.0f;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        seed = (Seed)item;
        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        Debug.Log("On destroy!");

        if (highlightedSoil != null)
        {
            highlightedSoil.ResetColor();
            Debug.Log(highlightedSoil);
        }
    }

    public void SoilHover(Soil soil)
    {
        highlightedSoil = soil;
        if (CanPlant(soil))
        {
            soil.SpriteRenderer.color = canPlantColor;
        }
        else
        {
            soil.SpriteRenderer.color = cannotPlantColor;
        }
    }

    public void SoilLeave(Soil soil)
    {
        soil.ResetColor();
    }

    public void SoilClick(Soil soil)
    {
        if (!CanPlant(soil)) return;
        
        inventory.RemoveItem(seed);
        soil.Plant(seed);
        inventory.LogItems();

        if (!inventory.Contains(seed))
        {
            StopUsing();
        }
    }

    private bool CanPlant(Soil soil)
    {
        float distance = Vector3.Distance(transform.position, soil.transform.position);
        return distance <= maxDistance && soil.IsFree;
    }
}
