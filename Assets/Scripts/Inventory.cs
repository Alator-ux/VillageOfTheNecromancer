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

    private Dictionary<Item, List<ItemStack>> itemsToStacks = new();
    private ItemStack[,] grid = null;

    private Action<int, int> onChanged;

    private void Start()
    {
        grid = new ItemStack[gridRowsCount, gridColsCount];
        SetOnChangedCallback(delegate (int row, int col) { }); // ��������
    }

    public ItemStack[,] Grid
    {
        get { return grid; }
    }

    public (int,int) GridSize()
    {
        return (gridRowsCount, gridColsCount);
    }

    public IEnumerable<Item> Items => itemsToStacks.Keys;

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
        return itemsToStacks[item].Select(l => l.Count).Sum();
    }

    public bool Contains(Item item, int count=1)
    {
        if (count == 1)
            return itemsToStacks.ContainsKey(item);

        return Count(item) >= count;
    }
    public void SetOnChangedCallback(Action<int, int> callback)
    {
        onChanged = callback;
    }

    // returns added count
    public int AddItem(Item item, int count = 1)
    {
        if (count < 1)
            return 0;

        if (itemsToStacks.ContainsKey(item))
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
            Debug.Log(lastItemStack);
            if (lastItemStack == null)
                return addedCount;

            var addedToStack = lastItemStack.Add(remaining);
            remaining -= addedToStack;
            addedCount += addedToStack;

            onChanged(lastItemStack.GridRow, lastItemStack.GridColumn);
        }

        return addedCount;
    }

    private ItemStack FirstNotFullStack(Item item) 
    {
        try
        {
            var first = itemsToStacks[item].First((inventoryItem) => !inventoryItem.IsStackFull);
            return first;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
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

        if (!itemsToStacks.ContainsKey(item))
            itemsToStacks[item] = new List<ItemStack>();
        
        var (row, col) = FreeGridCell();
        
        if (row == -1)
            return 0;

        var inventoryItem = new ItemStack(item, count, row, col);

        grid[row, col] = inventoryItem;
        itemsToStacks[item].Add(inventoryItem);

        onChanged(row, col);

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

    // TODO: Implement this
    public int RemoveItem(Item item, int count = 1)
    {
        if (!itemsToStacks.ContainsKey(item)) return 0;
        var (removedItemGridRow, removedItemGridCol) = (-1, -1);

        int removed = 0;
        while (removed < count && itemsToStacks.ContainsKey(item))
        {
            var lastStack = itemsToStacks[item].Last();
            (removedItemGridRow, removedItemGridCol) = (lastStack.GridRow, lastStack.GridColumn);
            removed += RemoveFromStack(lastStack, count);
        }

        if(removed > 0)
        {
            onChanged(removedItemGridRow, removedItemGridCol);
        }

        return removed;
    }

    // TODO: Implement this
    public int AddToCell(Item item, int row, int column, int count = 1)
    {
        if (row < 0 || column < 0 || row > gridRowsCount || column > gridColsCount)
            return 0;

        return 0;
    }

    // returns removed count
    public int RemoveFromCell(int row, int column, int count = 1)
    {
        if (row < 0 || column < 0 || row > gridRowsCount || column > gridColsCount)
            return 0;

        var cell = grid[row, column];
        if (cell == null)
            return 0;

        return RemoveFromStack(cell, count);
    }

    private int RemoveFromStack(ItemStack stack, int count)
    {
        var (removedItemGridRow, removedItemGridCol) = (stack.GridRow, stack.GridColumn);
        var removed = stack.Remove(count);
        if (stack.IsStackEmpty)
        {
            itemsToStacks[stack.Item].Remove(stack);

            Debug.Log(itemsToStacks[stack.Item]);
            
            if (itemsToStacks[stack.Item].Count() == 0)
            {
                Debug.Log($"{stack.Item} list is empty");
                itemsToStacks.Remove(stack.Item);
                grid[stack.GridRow, stack.GridColumn] = null;
                Debug.Log("Remove that list");
            }
        }

        onChanged(stack.GridRow, stack.GridColumn);
        return removed;
    }
    // TODO ������ ���-��. ���� �����, ���������� ��.
    public void SwapStacks(int row, int col, int otherRow, int otherCol)
    {
        var itemStack = grid[row, col];

        grid[row, col] = grid[otherRow, otherCol];
        if (grid[row, col] != null)
        {
            grid[row, col].GridRow = row;
            grid[row, col].GridColumn = col;
        }

        grid[otherRow, otherCol] = itemStack;
        if (grid[otherRow, otherCol] != null)
        {
            grid[otherRow, otherCol].GridRow = otherRow;
            grid[otherRow, otherCol].GridColumn = otherCol;
        }

        onChanged(row, col);
        onChanged(otherRow, otherCol);
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

    private string CellString(int row, int column, int len = 10)
    {
        if (row > gridRowsCount || column > gridColsCount) return null;

        if (grid[row, column] == null)
        {
            return "".PadRight(len, ' ');
        }

        return grid[row, column].ToString().PadRight(len, ' ');
    }
}
