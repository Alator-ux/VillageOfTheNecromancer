using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventory : MonoBehaviour
{
    private Inventory playerInventory;
    private PlayerUseItemController playerUseItemController;
    private TooltipManager tooltipManager;
    private OptionsMenuManager optionsMenuManager;
    [SerializeField]
    private GameObject quickBar, inventory;

    public GameObject inventoryCellPrefab, topLevelObject;

    private HUDInventoryCell[,] cells;
    private HUDInventoryCell activeItemCell;

    void Start()
    {
        var player = GameObject.Find("Player");
        playerUseItemController = player.GetComponent<PlayerUseItemController>();
        tooltipManager = GetComponent<TooltipManager>();

        optionsMenuManager = GetComponent<OptionsMenuManager>();
        optionsMenuManager.TopLevelObject = topLevelObject;

        playerInventory = player.GetComponent<Inventory>();
        playerInventory.SetOnChangedCallback((int row, int column) =>
        {
            cells[row, column].SetItemStack(playerInventory.Grid[row, column]);
        });

        var gridSize = playerInventory.GridSize();
        var fullInventoryGLG = inventory.GetComponent<GridLayoutGroup>();
        fullInventoryGLG.constraintCount = gridSize.Item2;

        cells = new HUDInventoryCell[gridSize.Item1, gridSize.Item2];
        FillItemGrid(gridSize);

        activeItemCell = cells[0, 0];
        ToggleItemActivation(activeItemCell);

        ChangeInventoryVisibility();
    }

    void FillItemGrid((int, int) gridSize)
    {
        CreateItemRowForObject(0, gridSize.Item2, quickBar);
        for (var row = 1; row < gridSize.Item1; row++)
        {
            CreateItemRowForObject(row, gridSize.Item2, inventory);
        }
    }
    void CreateItemRowForObject(int row, int rowWidth, GameObject parent)
    {
        for (var column = 0; column < rowWidth; column++)
        {
            var itemStack = playerInventory.Grid[row, column];

            var cell = Instantiate(inventoryCellPrefab);
            cell.transform.SetParent(parent.transform);
            cells[row, column] = cell.GetComponent<HUDInventoryCell>();
            cells[row, column].Row = row;
            cells[row, column].Column = column;
            cells[row, column].TopLevelObject = topLevelObject;

            int currentRow = row;
            int currentColumn = column;
            cells[row, column].OnItemSwapped = (int otherRow, int otherColumn) => {
                playerInventory.SwapStacks(currentRow, currentColumn, otherRow, otherColumn);
            };
            cells[row, column].OnItemRemoved = () => {
                DropItem(currentRow, currentColumn);
            };

            cells[row, column].SetItemStack(itemStack);
        }
    }
    void UseItem(int row, int column)
    {
        var stack = playerInventory.Grid[row, column];
        playerUseItemController.UseItem(stack == null ? null : stack.Item);
    }
    void DropItem(int row, int column)
    {
        playerInventory.DropFromCell(row, column, position: playerInventory.transform.position, count: 1);
        if(activeItemCell.IsEqual(cells[row, column]) && playerInventory.IsCellEmpty(row, column))
        {
            playerUseItemController.UseItem(null);
        }
    }
    void DropAllItems(int row, int column)
    {
        var stack = playerInventory.Grid[row, column];
        if(stack != null)
        {
            playerInventory.DropFromCell(row, column, position: playerInventory.transform.position, count: stack.Count);
            if (activeItemCell.IsEqual(cells[row, column]))
            {
                playerUseItemController.UseItem(null);
            }
        }
    }
    public void ToggleItemActivation(int column)
    {
        activeItemCell.SetInactiveColor();
        UseItem(0, column);
        activeItemCell = cells[0, column];
        activeItemCell.SetActiveColor();
    }
    public void ToggleItemActivation(HUDInventoryCell inventoryCell)
    {
        activeItemCell.SetInactiveColor();
        UseItem(inventoryCell.Row, inventoryCell.Column);
        activeItemCell = inventoryCell;
        activeItemCell.SetActiveColor();
    }

    public void CreateOptionsMenu(HUDInventoryCell inventoryCell)
    {
        if(playerInventory.IsCellEmpty(inventoryCell.Row, inventoryCell.Column)) {
            return;
        }
        var menuScript = optionsMenuManager.
            CreateOptionsMenu(inventoryCell.transform.position) as HUDItemOptionsMenu;
        menuScript.SetOnButtonClickCallback((HUDItemOptionsMenuAction choosenAction) =>
        {
            switch (choosenAction)
            {
                case HUDItemOptionsMenuAction.Use:
                    ToggleItemActivation(inventoryCell);
                    break;
                case HUDItemOptionsMenuAction.Drop:
                    DropItem(inventoryCell.Row, inventoryCell.Column);
                    break;
                case HUDItemOptionsMenuAction.DropAll:
                    DropAllItems(inventoryCell.Row, inventoryCell.Column); ;
                    break;
            }
            optionsMenuManager.DestroyOptionsMenu();
        });    
    }
    public void DestroyOptionsMenu()
    {
        optionsMenuManager.DestroyOptionsMenu();
    }
    public void CreateTooltip(HUDInventoryCell inventoryCell, Vector2 mousePosition)
    {
        if (playerInventory.IsCellEmpty(inventoryCell.Row, inventoryCell.Column))
        {
            return;
        }
        var item = playerInventory.Grid[inventoryCell.Row, inventoryCell.Column].Item;
        var text = $"{item.Name}:\n{item.Description}";
        tooltipManager.CreateTooltip(mousePosition, text);
    }
    public void DestroyTooltip()
    {
        tooltipManager.DestroyTooltip();
    }

    public void ChangeInventoryVisibility()
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}
