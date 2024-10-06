using TMPro;
using UnityEngine;

public class InventoryItemDisplayManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] ItemData itemData;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI _useButton;
    [SerializeField] TextMeshProUGUI _itemName;
    [SerializeField] TextMeshProUGUI _itemDescription;

    public void Initialize(ItemData itemData)
    {
        this.itemData = itemData;

        if (itemData.ItemType == ItemType.Weapon || itemData.ItemType == ItemType.Armor) {
            _useButton.text = "Equip";
        }
        else _useButton.text = "Use";

        _itemName.text = itemData.ItemName;
        _itemDescription.text = itemData.ItemDescription;
    }

    public void UseItem()
    {
        itemData.UseItem();
    }

    public void DropItem()
    {
        itemData.DropItem();
    }
}
