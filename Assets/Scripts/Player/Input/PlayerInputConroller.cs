using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MovingObject
{
    private UIManager uiManager;
    private PlayerInteractionManager interactionManager;
    private MouseController mouseController;
    protected override void Start()
    {
        interactionManager = GetComponent<PlayerInteractionManager>();
        mouseController = GetComponent<MouseController>();

        uiManager = GameObject.FindObjectOfType<UIManager>();

        base.Start();
    }

    void Update()
    {
        HandleKeyboardEvents();
        HandleMouseEvents();
    }
    void HandleKeyboardEvents()
    {
        Vector2 moveDirection = new Vector2(0f, 0f);
        moveDirection.x = (float)Input.GetAxisRaw("Horizontal");
        moveDirection.y = (float)Input.GetAxisRaw("Vertical");
        Move(moveDirection);

        if (Input.GetButtonDown("Interact"))
        {
            interactionManager.Interact();
        }

        if (Input.GetButtonDown("Craft"))
        {
            uiManager.ProcessCraftButtonInput();
        }

        for (int column = 0; column < 9; column++)
        {
            var keyCode = KeyCode.Alpha1 + column;
            if (Input.GetKeyDown(keyCode)) // keyboard buttons 1-9
            {
                uiManager.ProcessNumInput(keyCode);
            }
        }
    }
    void HandleMouseEvents()
    {
        var mouseInfo = mouseController.GetMouseState();
        uiManager.ProcessMouseInput(mouseInfo);
    }
}
