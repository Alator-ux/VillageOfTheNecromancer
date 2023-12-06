public class ItemStack
{
    private Item item;
    private int count;
    private int gridRow;
    private int gridColumn;

    public Item Item { get => item; set => item = value; }
    public int Count { get => count; }
    public int GridRow { get => gridRow; set => gridRow = value; }
    public int GridColumn { get => gridColumn; set => gridColumn = value; }

    public ItemStack(Item item, int count, int gridRow, int gridColumn)
    {
        this.item = item;
        this.count = count;
        this.gridRow = gridRow;
        this.gridColumn = gridColumn;
    }

    // returns added count
    public int Add(int count)
    {
        if (count <= 0) return 0;

        if ((this.count + count) > item.StackSize)
        {
            var added = item.StackSize - this.count;
            this.count = item.StackSize;
            return added;
        }

        this.count += count;
        return count;
    }

    // returns removed count
    public int Remove(int count)
    {
        if (count <= 0) return 0;

        if ((this.count - count) < 0)
        {
            var removed = this.count;
            this.count = 0;
            return removed;
        }

        this.count -= count;
        return count;
    }

    public bool IsStackFull => count == item.StackSize;

    public bool IsStackEmpty => count == 0;

    public override string ToString()
    {
        return $"{item.Name}: {count}";
    }
}
