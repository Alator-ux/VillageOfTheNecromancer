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
        SetOnChangedCallback(delegate (int row, int col) { }); // заглушка
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
        var removed = stack.Remove(count);
        if (stack.IsStackEmpty)
        {
            UpdateStackAndListsOnEmpty(stack);
        }

        onChanged(stack.GridRow, stack.GridColumn);
        return removed;
    }

    public int DropFromCell(int row, int column, Vector3 position, int count = 1, float maxOffset = 1f, Quaternion? rotation = null)
    {
        if (row < 0 || column < 0 || row > gridRowsCount || column > gridColsCount)
            return 0;

        var cell = grid[row, column];
        if (cell == null)
            return 0;

        return DropFromStack(cell, count, position, maxOffset, rotation);
    }
    private int DropFromStack(ItemStack stack, int count, Vector3 position, float maxOffset, Quaternion? rotation)
    {
        var removed = stack.Drop(count, position, maxOffset, rotation);
        if (stack.IsStackEmpty)
        {
            UpdateStackAndListsOnEmpty(stack);
        }

        onChanged(stack.GridRow, stack.GridColumn);
        return removed;
    }
    private void UpdateStackAndListsOnEmpty(ItemStack stack)
    {
        grid[stack.GridRow, stack.GridColumn] = null;

        itemsToStacks[stack.Item].Remove(stack);

        if (itemsToStacks[stack.Item].Count() == 0)
        {
            itemsToStacks.Remove(stack.Item);
        }
    }
    public void SwapStacks(int row, int column, int otherRow, int otherColumn)
    {
        if (row < 0 || column < 0 || row > gridRowsCount || column > gridColsCount ||
            otherRow < 0 || otherColumn < 0 || otherRow > gridRowsCount || otherColumn > gridColsCount)
            return;

        var itemStack = grid[row, column];

        grid[row, column] = grid[otherRow, otherColumn];
        if (grid[row, column] != null)
        {
            grid[row, column].GridRow = row;
            grid[row, column].GridColumn = column;
        }

        grid[otherRow, otherColumn] = itemStack;
        if (grid[otherRow, otherColumn] != null)
        {
            grid[otherRow, otherColumn].GridRow = otherRow;
            grid[otherRow, otherColumn].GridColumn = otherColumn;
        }

        onChanged(row, column);
        onChanged(otherRow, otherColumn);
    }
    public bool IsCellEmpty(int row, int column)
    {
        if (row < 0 || column < 0 || row > gridRowsCount || column > gridColsCount)
            return true;

        if(grid[row, column] == null)
        {
            return true;
        }

        return grid[row, column].IsStackEmpty;
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
