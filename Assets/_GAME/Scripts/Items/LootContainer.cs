using System.Collections.Generic;
using UnityEngine;

public class LootContainer : MonoBehaviour
{
    [SerializeField] private List<ItemData> items = new List<ItemData>();
    public List<ItemData> Items => items;

    public ItemData TakeItem(int index)
    {
        if (index < 0 || index >= items.Count) return null;
        var item = items[index];
        items.RemoveAt(index);
        return item;
    }

    public void AddLootList(List<ItemData> loot)
    {
        items.AddRange(loot);
    }

    public void UseItem(string itemName)
    {
        ItemData item = items.Find(i => i.ItemName == itemName);
        if (item == null) {
            Debug.LogWarning("Item not found in loot container: " + itemName);
            return;
        }
        item.UseItem();
    }
}
