using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : Interactable
{
    FoodScript() :
        base(createBoxColider2D: true, bcSizeMult: 1f, createMessageObject: true, message:"������� ����")
    { }

    public override void Interact()
    {
        Destroy(gameObject);
    }
}
