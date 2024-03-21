using System;
using System.Collections;
using System.Collections.Generic;
using Articy.Villageofthenecrofarmer.GlobalVariables;
using UnityEngine;

public class PlayerInputController : MovingObject
{
    private Animator animator;

    [SerializeField] private GameObject hudGameObject;
    private HUDInventory hudInventory;
    private UIManager uiManager;
    private PlayerInteractionManager interactionManager;
    private MouseController mouseController;
    [SerializeField] private GameObject QuestLogWindow;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
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

        animator.SetFloat("Speed", Math.Min(moveDirection.magnitude, speed));

        if (Input.GetButtonDown("Interact"))
        {
            interactionManager.Interact();
        }

        if (Input.GetKeyDown(KeyCode.C))
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
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            QuestLogWindow.SetActive(!QuestLogWindow.activeSelf);

            
        }
        
    }
    void HandleMouseEvents()
    {
        var mouseInfo = mouseController.GetMouseState();
        uiManager.ProcessMouseInput(mouseInfo);
    }
}
