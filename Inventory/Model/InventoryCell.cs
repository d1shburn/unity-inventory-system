using System;
using UnityEngine;

public sealed class InventoryCell
{
    private Item _item;
    private int _amount;

    public Item Item => _item;

    public int Amount => _amount;

    public bool IsEmpty => _item == null;

    public event Action Changed;

    internal InventoryCell() { }

    internal int TryAdd(Item item, int amount)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (CanAccept(item) == false)
            return 0;

        int acceptedAmount = Mathf.Min(amount, item.MaxStackSize - _amount);

        if (IsEmpty)
            _item = item;

        _amount += acceptedAmount;

        Changed?.Invoke();
        return acceptedAmount;
    }

    internal int Remove(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        int removedAmount = Mathf.Min(amount, _amount);

        if (removedAmount == 0)
            return 0;

        _amount -= removedAmount;

        if (_amount == 0)
            _item = null;

        Changed?.Invoke();
        return removedAmount;
    }

    internal bool CanAccept(Item item)
    {
        if (item == null)
            return false;

        if (IsEmpty)
            return true;

        return _item == item && item.IsStackable && _amount < item.MaxStackSize;
    }

    internal int GetAvailableSpaceFor(Item item)
    {
        if (CanAccept(item) == false)
            return 0;

        return IsEmpty ? item.MaxStackSize : item.MaxStackSize - _amount;
    }
}