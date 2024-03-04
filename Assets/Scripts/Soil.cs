using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Plant CurrentPlant { get; private set; }
    public bool Wet { get; private set; } = false;

    private Color baseColor;

    [SerializeField]
    private Color wetColor = new Color(0, 0, 0);

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
        spriteRenderer.color = Wet ? wetColor : baseColor;
    }

    public bool IsFree => CurrentPlant == null;

    public void Plant(Seed seed)
    {
        if (!IsFree)
            return;

        if (seed == null || seed.PlantPrefab == null) return;

        GameObject plantPrefab = seed.PlantPrefab.gameObject;

        GameObject plantedObject = Instantiate(plantPrefab, transform);

        Vector3 plantPosition = new(transform.position.x, transform.position.y, transform.position.z);
        plantedObject.transform.position = plantPosition;

        CurrentPlant = plantedObject.GetComponent<Plant>();
        CurrentPlant.Soil = this;
    }

    public void RemovePlant()
    {
        CurrentPlant = null;
    }

    public void Water()
    {
        Wet = true;
        spriteRenderer.color = wetColor;
        Debug.Log("Set color");
        StartCoroutine(DryAfter10Seconds());
    }

    public void Dry()
    {
        spriteRenderer.color = baseColor;
        Wet = false;
    }

    private IEnumerator DryAfter10Seconds()
    {
        yield return new WaitForSeconds(10);

        Dry();
    }
}
