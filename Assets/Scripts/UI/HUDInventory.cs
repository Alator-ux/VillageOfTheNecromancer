using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Inventory playerInventory;
    private PlayerUseItemController playerUseItemController;

    [SerializeField]
    private GameObject quickBar, inventory;

    public GameObject inventoryCellPrefab, itemOptionsMenuPrefab, topLevelObject;

    private HUDInventoryCell[,] cells;
    private HUDInventoryCell activeItemCell;
    private GameObject itemOptionsMenu;
    private CanvasGroup topLevelObjectCG;
    void Start()
    {
        topLevelObjectCG = topLevelObject.GetComponent<CanvasGroup>();

        playerUseItemController = player.GetComponent<PlayerUseItemController>();
        playerInventory = player.GetComponent<Inventory>();
        playerInventory.SetOnChangedCallback(delegate (int row, int col)
        {
            cells[row, col].SetItemStack(playerInventory.Grid[row, col]);
        });

        var gridSize = playerInventory.GridSize();
        var fullInventoryGLG = inventory.GetComponent<GridLayoutGroup>();
        fullInventoryGLG.constraintCount = gridSize.Item2;

        cells = new HUDInventoryCell[gridSize.Item1, gridSize.Item2];
        FillItemGrid(gridSize);

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
            cells[row, column].OnAllItemsRemoved = () => {
                DropAllItems(currentRow, currentColumn);
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
        if(activeItemCell?.IsEqual(cells[row, column]) ?? false && playerInventory.IsCellEmpty(row, column))
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
            if (activeItemCell?.IsEqual(cells[row, column]) ?? false)
            {
                playerUseItemController.UseItem(null);
            }
        }
    }
    public void ToggleItemActivation(int column)
    {
        activeItemCell?.SetInactiveColor();
        UseItem(0, column);
        activeItemCell = cells[0, column];
        activeItemCell.SetActiveColor();
    }
    public void ToggleItemActivation(HUDInventoryCell inventoryCell)
    {
        activeItemCell?.SetInactiveColor();
        UseItem(inventoryCell.Row, inventoryCell.Column);
        activeItemCell = inventoryCell;
        activeItemCell.SetActiveColor();
    }
    public void CreateItemOptionsMenu(HUDInventoryCell inventoryCell)
    {
        if(playerInventory.IsCellEmpty(inventoryCell.Row, inventoryCell.Column)) {
            return;
        }
        Destroy(itemOptionsMenu);
        itemOptionsMenu = Instantiate(itemOptionsMenuPrefab, topLevelObject.transform);
        itemOptionsMenu.transform.position = inventoryCell.transform.position + new Vector3(60, 0, 0);
        topLevelObjectCG.blocksRaycasts = true;
        itemOptionsMenu.GetComponent<HUDItemOptions>()
                .SetOnButtonClickCallback((HUDItemOptions.MenuAction choosenAction) =>
                {
                    switch (choosenAction)
                    {
                        case HUDItemOptions.MenuAction.Use:
                            UseItem(inventoryCell.Row, inventoryCell.Column);
                            break;
                        case HUDItemOptions.MenuAction.Drop:
                            DropItem(inventoryCell.Row, inventoryCell.Column);
                            break;
                        case HUDItemOptions.MenuAction.DropAll:
                            DropAllItems(inventoryCell.Row, inventoryCell.Column); ;
                            break;
                    }
                    topLevelObjectCG.blocksRaycasts = false;
                });
    }
    public void DestroyItemOptionsMenu()
    {
        Destroy(itemOptionsMenu);
    }
    public void ChangeInventoryVisibility()
    {
        inventory.SetActive(!inventory.activeSelf);
    }
    void Update()
    {
        
    }
}
