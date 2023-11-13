using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : Interactable
{
    FoodScript() :
        base(createBoxColider2D: true, colliderSizeMultiplier: 1f, createMessageObject: true, message:"������� ����")
    { }

    public override void Interact(GameObject _interactor)
    {
        Destroy(gameObject);
    }
}
