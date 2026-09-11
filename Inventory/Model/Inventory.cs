using System;
using System.Collections.Generic;

public sealed class Inventory
{
    private readonly InventoryCell[] _cells;
    private readonly IReadOnlyList<InventoryCell> _cellsView;

    public IReadOnlyList<InventoryCell> Cells => _cellsView;

    public int Capacity => _cells.Length;

    public Inventory(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity));

        _cells = new InventoryCell[capacity];

        for (int i = 0; i < capacity; i++)
            _cells[i] = new InventoryCell();

        _cellsView = Array.AsReadOnly(_cells);
    }

    public void Add(Item item, int amount)
    {
        if (TryAdd(item, amount) == false)
            throw new InvalidOperationException("Not enough space in the inventory");
    }

    public bool TryAdd(Item item, int amount)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (CanAdd(item, amount) == false)
            return false;

        int leftAmount = amount;

        foreach (InventoryCell cell in _cells)
        {
            if (leftAmount == 0)
                break;

            leftAmount -= cell.TryAdd(item, leftAmount);
        }

        return true;
    }

    public bool CanAdd(Item item, int amount)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        long availableSpace = 0;

        foreach (InventoryCell cell in _cells)
            availableSpace += cell.GetAvailableSpaceFor(item);

        return availableSpace >= amount;
    }

    public int Remove(Item item, int amount)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        int leftAmount = amount;

        foreach (InventoryCell cell in _cells)
        {
            if (cell.Item != item)
                continue;

            leftAmount -= cell.Remove(leftAmount);
            
            if (leftAmount == 0)
                break;
        }

        return amount - leftAmount;
    }

    public bool Contains(Item item)
    {
        if (item == null)
            return false;

        foreach (InventoryCell cell in _cells)
            if (cell.Item == item)
                return true;

        return false;
    }

    public int Count(Item item)
    {
        if (item == null)
            return 0;

        int totalAmount = 0;

        foreach (InventoryCell cell in _cells)
            if (cell.Item == item)
                totalAmount += cell.Amount;

        return totalAmount;
    }
}