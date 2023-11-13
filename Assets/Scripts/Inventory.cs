using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int gridRowsCount = 5;
    [SerializeField]
    private int gridColsCount = 10;

    private Dictionary<Item, InventoryItem> itemsToInventoryItems = new();
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

    public bool Contains(Item item)
    {
        return itemsToInventoryItems.ContainsKey(item);
    }

    public void AddItem(Item item, int count)
    {
        if (count < 1)
            return;

        if (itemsToInventoryItems.ContainsKey(item))
        {
            itemsToInventoryItems[item].AddToStack(count);
            return;
        }

        var (row, col) = (0, 0); // TODO: Find empty cell in grid, otherwise don't add
        var inventoryItem = new InventoryItem(item, count, row, col);
        itemsToInventoryItems[item] = inventoryItem;
        grid[row, col] = inventoryItem;
    }

    public void LogItems()
    {
        var items = Items.Select(i => (i.Name, itemsToInventoryItems[i].Count));
        Debug.Log("[" + string.Join("; ", items) + "]");
    }

    public void RemoveItem(Item item, int count)
    {
    }
}
