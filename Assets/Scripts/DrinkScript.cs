using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkScript : Interactable
{
    public GameObject prefab;
    DrinkScript() :
        base(createBoxColider2D: true, colliderSizeMultiplier: 2f, createMessageObject: true, message:"Выпей меня")
    { }

    public override void Interact(GameObject _interactor)
    {
        if (!canInteract)
        {
            return;
        }
        canInteract = false;
        var clone = Instantiate(prefab);
        clone.transform.position = clone.transform.position + new Vector3(0f, 1f, 0f);
    }
}
