using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDInventoryInputScript : MonoBehaviour
{
    private HUDInventory hudInventory;
    void Start()
    {
        hudInventory = GetComponent<HUDInventory>();
    }
    void ProcessMouseHover(MouseInfo mouseInfo)
    {
        var hovered = UITools.RaycastGameObjects(mouseInfo.Position, LayerMask.NameToLayer("UI"));
        if (UITools.GameObjectWithTag(hovered, "HUDItemOptionsMenu") != null)
        {
            return;
        }
        var itemCell = UITools.GameObjectWithTag(hovered, "HUDInventoryItemCell");
        if (itemCell != null)
        {
            hudInventory.CreateTooltip(itemCell.GetComponent<HUDInventoryCell>(), mouseInfo.Position);
        }
    }
    void ProcessMouseClick(MouseInfo mouseInfo)
    {
        var clickedGameObjects = UITools.RaycastGameObjects(mouseInfo.ClickPosition, LayerMask.NameToLayer("UI"));
        if (UITools.GameObjectWithTag(clickedGameObjects, "HUDItemOptionsMenu") != null)
        {
            return;
        }

        hudInventory.DestroyOptionsMenu();

        if(mouseInfo.ClickButton == MouseButton.Right && mouseInfo.ClickAction == ClickAction.Click)
        {
            var hudInventoryCell = UITools.GameObjectWithTag(clickedGameObjects, "HUDInventoryItemCell");
            if (hudInventoryCell != null)
            {
                hudInventory.CreateOptionsMenu(hudInventoryCell.GetComponent<HUDInventoryCell>());
            }
        }
    }

    public void ProcessMouseInput(MouseInfo mouseInfo)
    {
        if (mouseInfo.Hover)
        {
            ProcessMouseHover(mouseInfo);
            return;
        }

        hudInventory.DestroyTooltip();

        if(mouseInfo.Clicked)
        {
            ProcessMouseClick(mouseInfo);
        }
    }

    public void ProcessNumInput(KeyCode keyCode)
    {
        DestroyOptionalElements();
        if (KeyCode.Alpha0 < keyCode && keyCode <= KeyCode.Alpha9)
        {
            hudInventory.ToggleItemActivation(keyCode - KeyCode.Alpha1);
        }
    }

    public void DestroyOptionalElements()
    {
        hudInventory.DestroyOptionsMenu();
        hudInventory.DestroyTooltip();
    }
}
