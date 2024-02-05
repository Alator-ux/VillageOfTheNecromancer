using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanUse : ItemUse
{
    private Soil highlightedSoil = null;

    private float maxDistance = 2.0f;

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
        bool available = CanWater(soil);
        soil.SetIndicatonColor(available);
    }

    public void MouseLeaveSoil(Soil soil)
    {
        soil.ResetColor();
    }

    public void MouseDownOnSoil(Soil soil)
    {
        if (!CanWater(soil)) return;

        soil.Water();
    }

    private bool CanWater(Soil soil)
    {
        float distance = Vector3.Distance(transform.position, soil.transform.position);
        return distance <= maxDistance && !soil.Wet;
    }
}
