using System;

public class InventoryPresenter
{
    private readonly Inventory _model;
    private readonly InventoryCellView[] _views;

    public InventoryPresenter(Inventory model, InventoryCellView[] views)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _views = views ?? throw new ArgumentNullException(nameof(views));

        if (_views.Length != _model.Capacity)
            throw new ArgumentException("View count must match inventory capacity", nameof(views));
    }

    public void Initialize()
    {
        for (int i = 0; i < _views.Length; i++)
        {
            if (_views[i] == null)
                throw new ArgumentException("A cell view cannot be null", nameof(_views));

            _views[i].Initialize(_model.Cells[i]);
        }
    }
}