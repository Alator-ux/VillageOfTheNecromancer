using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MovingObject
{
    [SerializeField]
    private GameObject hudGameObject;
    private HUDInventory hudInventory;
    private PlayerInteractionManager interactionManager;
    private ClickController clickController;
    protected override void Start()
    {
        interactionManager = GetComponent<PlayerInteractionManager>();
        clickController = GetComponent<ClickController>();
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

        if (Input.GetButtonDown("Interact"))
        {
            interactionManager.Interact();
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
        var mouseInfo = clickController.ClickButtonNAction();
        if(mouseInfo.MouseButton == MouseButton.None)
        {
            return;
        }
        var clickedGameObjects = clickController.ClickedGameObjects(mouseInfo.MousePosition, LayerMask.NameToLayer("UI"));
        if (GameObjectWithTag(clickedGameObjects, "HUDItemOptionsMenu"))
        {
            return;
        }
        switch (mouseInfo.MouseButton)
        {
            case MouseButton.Left:
                {
                    hudInventory.DestroyItemOptionsMenu();
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
                    hudInventory.DestroyItemOptionsMenu();
                    if (mouseInfo.ClickAction == ClickAction.Click)
                    {
                        var hudInventoryCell = GameObjectWithTag(clickedGameObjects, "HUDInventoryItemCell");
                        if (hudInventoryCell != null)
                        {
                            hudInventory.CreateItemOptionsMenu(hudInventoryCell.GetComponent<HUDInventoryCell>());
                        }
                        Debug.Log("Right click");
                    }
                }
                break;
            case MouseButton.Middle:
                {
                    hudInventory.DestroyItemOptionsMenu();
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
