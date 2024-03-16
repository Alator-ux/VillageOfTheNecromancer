using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MovingObject
{
    private Animator animator;

    [SerializeField]
    private GameObject hudGameObject;
    private HUDInventory hudInventory;
    private PlayerInteractionManager interactionManager;
    private MouseController mouseController;
    [SerializeField]
    private GameObject QuestLogWindow;
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        interactionManager = GetComponent<PlayerInteractionManager>();
        mouseController = GetComponent<MouseController>();
        hudInventory = hudGameObject.GetComponent<HUDInventory>();
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

        if (Input.GetKeyDown(KeyCode.L))
        {
            QuestLogWindow.SetActive(!QuestLogWindow.activeSelf);
        }

        for (int column = 0; column < 9; column++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + column)) // keyboard buttons 1-9
            {
                hudInventory.ToggleItemActivation(column);
            }
        }
    }
    void HandleMouseEvents()
    {
        var mouseInfo = mouseController.GetMouseState();
        if (mouseInfo.Hover)
        {
            var hovered = mouseController.RaycastGameObjects(mouseInfo.Position, LayerMask.NameToLayer("UI"));
            if(GameObjectWithTag(hovered, "HUDItemOptionsMenu") == null)
            {
                var itemCell = GameObjectWithTag(hovered, "HUDInventoryItemCell");
                if (itemCell != null)
                {
                    hudInventory.OnItemCellMouseEnter(itemCell.GetComponent<HUDInventoryCell>(), mouseInfo.Position);
                }
            }
        }
        else
        {
            hudInventory.OnItemCellMouseExit();
        }
        if(mouseInfo.ClickButton == MouseButton.None)
        {
            return;
        }
        var clickedGameObjects = mouseController.RaycastGameObjects(mouseInfo.ClickPosition, LayerMask.NameToLayer("UI"));
        if (GameObjectWithTag(clickedGameObjects, "HUDItemOptionsMenu") != null)
        {
            return;
        }
        switch (mouseInfo.ClickButton)
        {
            case MouseButton.Left:
                {
                    hudInventory.OnClick();
                    if (mouseInfo.ClickAction == ClickAction.DoubleClick)
                    {
                        var hudInventoryCell = GameObjectWithTag(clickedGameObjects, "HUDInventoryItemCell");
                        if (hudInventoryCell != null)
                        {
                            hudInventory.ToggleItemActivation(hudInventoryCell.GetComponent<HUDInventoryCell>());
                        }
                        Debug.Log("Left double click");
                    }
                }
                break;
            case MouseButton.Right:
                {
                    hudInventory.OnClick();
                    if (mouseInfo.ClickAction == ClickAction.Click)
                    {
                        var hudInventoryCell = GameObjectWithTag(clickedGameObjects, "HUDInventoryItemCell");
                        if (hudInventoryCell != null)
                        {
                            hudInventory.OnItemCellRightClick(hudInventoryCell.GetComponent<HUDInventoryCell>());
                        }
                        Debug.Log("Right click");
                    }
                }
                break;
            case MouseButton.Middle:
                {
                    hudInventory.OnClick();
                }
                break;
        }
    }
    GameObject GameObjectWithTag(List<GameObject> gameObjects, string tag)
    {
        foreach(var gameObject in gameObjects)
        {
            if (gameObject.CompareTag(tag))
            {
                return gameObject;
            }
        }
        return null;
    }

}
