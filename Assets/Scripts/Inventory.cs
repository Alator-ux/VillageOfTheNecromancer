using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int gridRowsCount = 5;
    [SerializeField]
    private int gridColsCount = 10;

    private Dictionary<Item, List<InventoryItem>> itemsToInventoryItems = new();
    private InventoryItem[,] grid = null;

    private void Start()
    {
        grid = new InventoryItem[gridRowsCount, gridColsCount];
    }

    public InventoryItem[,] Grid
    {
        get { return grid; }
    }

    public (int,int) GridSize()
    {
        return (gridRowsCount, gridColsCount);
    }

    public IEnumerable<Item> Items => itemsToInventoryItems.Keys;

    public Dictionary<Item, int> ItemsCounts()
    {
        var itemsCounts = new Dictionary<Item, int>();
        foreach (var item in Items)
        {
            itemsCounts[item] = Count(item);
        }

        return itemsCounts;
    }

    public int Count(Item item)
    {
        return itemsToInventoryItems[item].Select(l => l.Count).Sum();
    }

    public bool Contains(Item item)
    {
        return itemsToInventoryItems.ContainsKey(item);
    }

    // returns added count
    public int AddItem(Item item, int count)
    {
        if (count < 1)
            return 0;

        if (itemsToInventoryItems.ContainsKey(item))
        {
            return AddExistingItem(item, count);
        }

        return AddToFreeCells(item, count);
    }

    private int AddExistingItem(Item item, int count)
    {
        var addedCount = AddToExistingCells(item, count);
        
        if (addedCount < count)
        {
            var remaining = count - addedCount;
            addedCount += AddToFreeCells(item, remaining);
        }

        return addedCount;
    }

    private int AddToExistingCells(Item item, int count)
    {
        var addedCount = 0;
        var remaining = count;
        while (addedCount < count && remaining != 0)
        {
            var lastItemStack = FirstNotFullStack(item);
            if (lastItemStack == null)
                return addedCount;

            var addedToStack = lastItemStack.AddToStack(remaining);
            remaining -= addedToStack;
            addedCount += addedToStack;
        }

        return addedCount;
    }

    private InventoryItem FirstNotFullStack(Item item) 
    {
        return itemsToInventoryItems[item].First((inventoryItem) => !inventoryItem.IsStackFull);
    }

    private int AddToFreeCells(Item item, int count)
    {
        var added = 0;
        while (count > item.StackSize)
        {
            var addedToCell = AddToFreeCell(item, item.StackSize);
            if (addedToCell == 0)
                return added;
            count -= addedToCell;
        }
        added += AddToFreeCell(item, count);
        return added;
    }

    private int AddToFreeCell(Item item, int count)
    {
        if (count < 1 || count > item.StackSize) return 0;

        if (!itemsToInventoryItems.ContainsKey(item))
            itemsToInventoryItems[item] = new List<InventoryItem>();
        
        var (row, col) = FreeGridCell();
        
        if (row == -1)
            return 0;

        var inventoryItem = new InventoryItem(item, count, row, col);

        grid[row, col] = inventoryItem;
        itemsToInventoryItems[item].Add(inventoryItem);
        
        return count;
    }

    // returns (-1, -1) when there are no free cells in grid
    private (int, int) FreeGridCell()
    {
        for (var i = 0; i < gridRowsCount; i++)
        {
            for (var j = 0; j < gridColsCount; j++)
            {
                var cell = grid[i, j];
                if (cell == null)
                    return (i, j);
            }
        }

        return (-1, -1);
    }

    public void LogItems()
    {
        var items = Items.Select(i => (i.Name, Count(i)));
        Debug.Log("[" + string.Join("; ", items) + "]");
    }

    public void LogGrid()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < gridRowsCount; i++)
        {
            sb.Append("|");
            for (var j = 0; j < gridColsCount; j++)
            {
                sb.Append(CellString(i, j));
                sb.Append("|");
            }
            sb.Append(Environment.NewLine);
        }

        Debug.Log(sb.ToString());
    }

    private string CellString(int row, int column, int len=10)
    {
        if (row > gridRowsCount || column > gridColsCount) return null;

        if (grid[row, column] == null)
        {
            return "".PadRight(len, ' ');
        }

        return grid[row, column].ToString().PadRight(len, ' ');
    }

    // TODO: Implement this
    public void RemoveItem(Item item, int count)
    {
    }
}
