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

    public GameObject inventoryCellPrefab;
    public GameObject topLevelObject;

    private HUDInventoryCell[,] cells;
    void Start()
    {
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
        for (var col = 0; col < rowWidth; col++)
        {
            var itemStack = playerInventory.Grid[row, col];

            var cell = Instantiate(inventoryCellPrefab);
            cell.transform.SetParent(parent.transform);
            cells[row, col] = cell.GetComponent<HUDInventoryCell>();
            cells[row, col].Row = row;
            cells[row, col].Col = col;
            cells[row, col].TopLevelObject = topLevelObject;

            int currentRow = row;
            int currentCol = col;
            cells[row, col].OnItemSwapped = delegate(int otherRow, int otherCol) {
                playerInventory.SwapStacks(currentRow, currentCol, otherRow, otherCol);
            };
            cells[row, col].OnItemUsed = delegate ()
            {
                playerUseItemController.UseItem(playerInventory.Grid[currentRow, currentCol].Item);
            };
            cells[row, col].OnItemRemoved = delegate () {
                var cell = playerInventory.Grid[currentRow, currentCol];
                if (cell != null)
                {
                    cell.Drop(1, player.transform.position);
                }
                playerInventory.RemoveFromCell(currentRow, currentCol);
            };
            cells[row, col].OnAllItemsRemoved = delegate () {
                var cell = playerInventory.Grid[currentRow, currentCol];
                if (cell != null)
                {
                    cell.Drop(cell.Count, player.transform.position);
                }
                playerInventory.RemoveFromCell(currentRow, currentCol, cell.Count);
            };

            cells[row, col].SetItemStack(itemStack);
        }
    }
    public void ChangeInventoryVisibility()
    {
        inventory.SetActive(!inventory.activeSelf);
    }
    void Update()
    {
        
    }
}
