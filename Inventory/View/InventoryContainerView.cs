using UnityEngine;

public class InventoryContainerView : MonoBehaviour
{
    [SerializeField] private InventoryCellView _prefab;
    [SerializeField, Min(1)] private int _capacity = 1;

    public InventoryCellView[] CreateCells()
    {
        if (_prefab == null)
            throw new MissingReferenceException("Inventory cell prefab is not assigned.");

        InventoryCellView[] views = new InventoryCellView[_capacity];

        for (int i = 0; i < _capacity; i++)
            views[i] = Instantiate(_prefab, transform);

        return views;
    }
}
