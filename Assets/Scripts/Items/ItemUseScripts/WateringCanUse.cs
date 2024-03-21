using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanUse : ItemUse
{
    private WateringCan wateringCanItem;
    private Soil highlightedSoil = null;
    private Soil wateredSoil = null;
    private float maxDistance = 2.0f;
    private GameObject wateringCanInHand;
    private Animator wateringCanAnimator;

    private void OnEnable()
    {
        wateringCanItem = item as WateringCan;
        if (wateringCanItem == null)
        {
            Debug.Log("watering can is null");
            return;
        }

        wateringCanInHand = Instantiate(wateringCanItem.WateringCanInHandPrefab, transform);
        wateringCanInHand.transform.localPosition = new Vector3(0.7f, 1.0f);

        WateringCanInHand wateringCanInHandScript = wateringCanInHand.GetComponent<WateringCanInHand>();
        wateringCanInHandScript.WateringCanUseScript = this;

        wateringCanAnimator = wateringCanInHand.GetComponent<Animator>();
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
        if (wateringCanInHand != null) {
            Destroy(wateringCanInHand);
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

        Debug.Log("Try water");
        // wateringCanAnimator.Play("Watering");
        wateringCanAnimator.SetBool("Watering", true);
        wateredSoil = soil;
    }

    public void OnWateringAnimationEnded() {
        Debug.Log("OnWateringAnimationEnded");
        wateringCanAnimator.SetBool("Watering", false);
        wateredSoil.Water();
        wateredSoil = null;
    }

    private bool CanWater(Soil soil)
    {
        float distance = Vector3.Distance(transform.position, soil.transform.position);
        return distance <= maxDistance && !soil.Wet;
    }
}
