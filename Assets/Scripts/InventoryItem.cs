public class InventoryItem
{
    private Item item;
    private int count;
    private int gridRow;
    private int gridColumn;

    public Item Item { get => item; set => item = value; }
    public int Count { get => count; }
    public int GridRow { get => gridRow; set => gridRow = value; }
    public int GridColumn { get => gridColumn; set => gridColumn = value; }

    public InventoryItem(Item item, int count, int gridRow, int gridColumn)
    {
        this.item = item;
        this.count = count;
        this.gridRow = gridRow;
        this.gridColumn = gridColumn;
    }

    public void AddToStack(int count)
    {
        if (count <= 0 || (this.count + count) > item.StackSize)
            return;

        this.count += count;
    }

    public void RemoveFromStack(int count)
    {
        if (count <= 0 || (this.count - count) <= item.StackSize)
            return;

        this.count -= count;
    }

    public bool IsStackFull => count == item.StackSize;

    public bool IsStackEmpty => count == 0;
}
