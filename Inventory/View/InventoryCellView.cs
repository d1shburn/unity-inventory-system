using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCellView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _amount;

    private InventoryCell _model;

    public void Initialize(InventoryCell model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        if (_model != null)
            _model.Changed -= Render;

        _model = model;

        if (isActiveAndEnabled)
            Subscribe();

        Render();
    }

    private void OnEnable()
    {
        if (_model == null)
            return;

        Subscribe();
        Render();
    }

    private void OnDisable()
    {
        if (_model != null)
            _model.Changed -= Render;
    }

    private void Subscribe()
    {
        _model.Changed -= Render;
        _model.Changed += Render;
    }

    private void Render()
    {
        if (_model.IsEmpty)
        {
            Clear();
            return;
        }

        _icon.sprite = _model.Item.Icon;
        _title.text = _model.Item.Title;
        _amount.text = _model.Amount.ToString();
    }

    private void Clear()
    {
        _icon.sprite = null;
        _title.text = string.Empty;
        _amount.text = string.Empty;
    }
}