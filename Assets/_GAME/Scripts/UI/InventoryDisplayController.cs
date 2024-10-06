using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplayController : MonoBehaviour
{
    [SerializeField] private RectTransform _inventoryContent;
    [SerializeField] private InventoryItemDisplayManager _inventoryDisplay;
    [SerializeField] List<InventoryItemDisplayManager> _inventoryItemDisplayManagers = new List<InventoryItemDisplayManager>();

    public void Initialize()
    {
        ClearInventory();
        foreach (var item in PawnController.Instance.Inventory.Items)
        {
            var itemDisplay = Instantiate(_inventoryDisplay, _inventoryContent);
            itemDisplay.Initialize(item);
            _inventoryItemDisplayManagers.Add(itemDisplay);
        }
    }

    void ClearInventory()
    {
        foreach (var item in _inventoryItemDisplayManagers) Destroy(item.gameObject);
        _inventoryItemDisplayManagers.Clear();
    }
}