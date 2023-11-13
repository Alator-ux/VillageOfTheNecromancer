using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : Interactable
{
    [SerializeField] Item item;
    [SerializeField] int count = 1;

    PickableItem() :
        base(createBoxColider2D: true, colliderSizeMultiplier: 1f, createMessageObject: true, message: "Подбери меня")
    { }

    public override void Interact(GameObject interactor)
    {
        Debug.Log("Interacting here!");
        if (interactor.tag != "Player") return;

        Inventory inventory = interactor.GetComponent<Inventory>();
        if (inventory == null) return;

        inventory.AddItem(item, count);
        // Debugging
        inventory.LogItems();
        Destroy(gameObject);
    }
}
