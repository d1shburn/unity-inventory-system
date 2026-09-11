using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isStackable;
    [SerializeField] private int _maxStackSize;

    public string Title => _title;

    public Sprite Icon => _icon;

    public bool IsStackable => _isStackable;

    public int MaxStackSize => _isStackable ? Mathf.Max(1, _maxStackSize) : 1;

    private void OnValidate() =>
        _maxStackSize = _isStackable ? Mathf.Max(1, _maxStackSize) : 1;
}