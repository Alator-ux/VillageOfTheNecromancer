using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedUse : ItemUse
{
    private Seed seed;

    private Inventory inventory;

    private Soil highlightedSoil = null;

    private float maxDistance = 2.0f;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        Cursor.visible = true;
    }

    private void OnEnable()
    {
        seed = item as Seed;
        if (seed == null)
        {
            Debug.Log("seed is null");
            return;
        }
    }

    private void FixedUpdate()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        LayerMask mask = 1 << LayerMask.NameToLayer("Soil");
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, mask);

        if (hit.collider == null) return;

        Soil soil = hit.collider.GetComponent<Soil>();
        if (soil == null) return;

        if (highlightedSoil != null)
        {
            MouseLeaveSoil(highlightedSoil);
        }

        highlightedSoil = soil;
        MouseHoverSoil(highlightedSoil);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (highlightedSoil == null) return;

            MouseDownOnSoil(highlightedSoil);
        }
    }

    private void OnDisable()
    {
        if (highlightedSoil != null)
        {
            highlightedSoil.ResetColor();
            Debug.Log(highlightedSoil);
        }
    }

    public void MouseHoverSoil(Soil soil)
    {
        bool available = CanPlant(soil);
        soil.SetIndicatonColor(available);
    }

    public void MouseLeaveSoil(Soil soil)
    {
        soil.ResetColor();
    }

    public void MouseDownOnSoil(Soil soil)
    {
        if (!CanPlant(soil)) return;

        inventory.RemoveItem(seed);
        soil.Plant(seed);

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
