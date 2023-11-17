using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerUseItemController playerUseItemController;
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Plant CurrentPlant { get; private set; }

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerUseItemController = playerObject.GetComponent<PlayerUseItemController>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        SeedUse seedUse = playerUseItemController.ItemInUse as SeedUse;

        if (seedUse == null)
            return;

        seedUse.SoilEnter(this);
    }

    private void OnMouseOver()
    {
        SeedUse seedUse = playerUseItemController.ItemInUse as SeedUse;

        if (seedUse == null)
            return;

        seedUse.SoilHover(this);
    }

    private void OnMouseExit()
    {
        SeedUse seedUse = playerUseItemController.ItemInUse as SeedUse;

        if (seedUse == null)
            return;

        seedUse.SoilLeave(this);
    }

    private void OnMouseDown()
    {
        SeedUse seedUse = playerUseItemController.ItemInUse as SeedUse;
        
        if (seedUse == null) return;

        seedUse.SoilClick(this);
    }

    public bool IsFree => CurrentPlant == null;

    public void Plant(Seed seed)
    {
        if (!IsFree)
            return;

        GameObject plantPrefab = seed.PlantPrefab?.gameObject;
        if (plantPrefab == null) return;

        GameObject plantedObject = Instantiate(plantPrefab, transform);
        
        // It should be a bit closer to camera
        Vector3 plantPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        plantedObject.transform.position = plantPosition;

        CurrentPlant = plantedObject.GetComponent<Plant>();
    }
}
