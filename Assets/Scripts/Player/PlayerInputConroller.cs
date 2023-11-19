using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MovingObject
{
    private PlayerInteractionManager interactionManager;
    protected override void Start()
    {
        interactionManager = GetComponent<PlayerInteractionManager>();
        base.Start();
    }

    void Update()
    {
        Vector2 moveDirection = new Vector2(0f, 0f);
        moveDirection.x = (float)Input.GetAxisRaw("Horizontal");
        moveDirection.y = (float)Input.GetAxisRaw("Vertical");
        Move(moveDirection);

        if (Input.GetButtonDown("Interact"))
        {
            interactionManager.Interact();
        }
    }
}
