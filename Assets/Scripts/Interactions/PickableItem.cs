using System;
using UnityEngine;

public class PickableItem : Interactable
{
    [SerializeField] Item item;
    [SerializeField] int count = 1;

    PickableItem() :
        base(colliderSizeMultiplier: 1f)
    { }

    public override void Interact(GameObject interactor) { }

    public override void EnterInteractionArea(GameObject interactor)
    {
        if (interactor.tag != "Player") return;

        Inventory inventory = interactor.GetComponent<Inventory>();
        if (inventory == null) return;

        inventory.AddItem(item, count);
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
