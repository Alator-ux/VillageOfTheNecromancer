using System;
using UnityEngine;

public class PickableItem : Interactable
{
    [SerializeField] Item item;
    [SerializeField] static float pickUpDelay = 3f;
    float spawnTime;
    PickableItem() :
        base(colliderSizeMultiplier: 1f)
    { }

    private void Awake()
    {
        spawnTime = Time.time - pickUpDelay; // has no delay by default
    }

    public void ItemWasDropped()
    {
        spawnTime = Time.time; // and only if item was dropped we should wait
    }

    public override void Interact(GameObject interactor) { }

    public override void EnterInteractionArea(GameObject interactor)
    {
        if (interactor.tag != "Player") return;

        Inventory inventory = interactor.GetComponent<Inventory>();
        if (inventory == null) return;

        if (Time.time - spawnTime < pickUpDelay) return;

        int added = inventory.AddItem(item, 1);
        if (added == 0)
            return;

        // Debugging
        //inventory.LogItems();
        //inventory.LogGrid();
        PlayerUseItemController playerUseItemController = interactor.GetComponent<PlayerUseItemController>();
        playerUseItemController.UseItem(item);
        // End debugging
        Destroy(gameObject);
    }

    public override void LeaveInteractionArea(GameObject interactor) { }
}
