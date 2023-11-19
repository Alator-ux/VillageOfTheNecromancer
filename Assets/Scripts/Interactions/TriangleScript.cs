using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleScript : InteractableWithMessage
{
    TriangleScript() :
        base(message: "�������� �� ����", colliderSizeMultiplier: 1f)
    { }

    protected bool canInteract = true;

    public bool CanInteract
    {
        get
        {
            return canInteract;
        }
    }
    

    public override void Interact(GameObject _interactor)
    {
        if (!canInteract)
        {
            return;
        }
        canInteract = false;
        Message = "��������� ���!";
    }
}
