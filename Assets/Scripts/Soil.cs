using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerUseItemController playerUseItemController;
    public SpriteRenderer SpriteRenderer { get; private set; }

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

    public void Plant(SeedItem seed)
    {
        Debug.Log("Seed");
    }
}
