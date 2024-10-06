using UnityEngine;

public enum ItemType
{
    Consumable,
    Weapon,
    Armor,
    Quest,
    Key,
    Junk
}

[CreateAssetMenu(menuName = "Items/ItemData", fileName = "New Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Item Data")]
    [SerializeField] string itemName;
    [SerializeField] string itemDescription;
    [SerializeField] string itemUseDescription;
    [SerializeField] ItemType itemType;

    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public string ItemUseDescription => itemUseDescription;
    public ItemType ItemType => itemType;

    [Header("Feedback")]
    [SerializeField] GameObject _useItemFeedbackPrefab;
    [SerializeField] GameObject _dropItemFeedbackPrefab;

    public virtual void UseItem()
    {
        Debug.Log("Using item: " + itemName);
        HandleUseItemFeedback();
    }

    public virtual void UseItem(GameObject target)
    {
        Debug.Log("Using item: " + itemName + " on " + target.name);
        HandleUseItemFeedback();
    }

    public virtual void UseItem(Vector3 target)
    {
        Debug.Log("Using item: " + itemName + " on " + target);
        HandleUseItemFeedback();
    }

    public virtual void DropItem()
    {
        Debug.Log("Dropping item: " + itemName);
        HandleDropItemFeedback();
    }

    void HandleUseItemFeedback()
    {
        if (_useItemFeedbackPrefab)
        {
            Instantiate(_useItemFeedbackPrefab);
        }
    }

    void HandleDropItemFeedback()
    {
        if (_dropItemFeedbackPrefab)
        {
            Instantiate(_dropItemFeedbackPrefab);
        }
    }
}
