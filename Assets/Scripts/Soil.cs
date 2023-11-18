using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Plant CurrentPlant { get; private set; }

    private Color baseColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
    }

    public void SetIndicatonColor(bool available)
    {
        if (available)
            spriteRenderer.color = Color.green;
        else
            spriteRenderer.color = Color.red;
    }

    public void ResetColor()
    {
        spriteRenderer.color = baseColor;
    }

    public bool IsFree => CurrentPlant == null;

    public void Plant(Seed seed)
    {
        if (!IsFree)
            return;

        if (seed == null || seed.PlantPrefab == null) return;

        GameObject plantPrefab = seed.PlantPrefab.gameObject;

        GameObject plantedObject = Instantiate(plantPrefab, transform);
        
        // It should be a bit closer to camera. TODO: Rewrite using sorting layers.
        Vector3 plantPosition = new(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        plantedObject.transform.position = plantPosition;

        CurrentPlant = plantedObject.GetComponent<Plant>();
        CurrentPlant.Soil = this;
    }

    public void RemovePlant()
    {
        CurrentPlant = null;
    }
}
