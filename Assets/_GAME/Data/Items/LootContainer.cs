using System.Collections.Generic;
using UnityEngine;

public class LootContainer : MonoBehaviour
{
    [SerializeField] private List<ItemData> items = new List<ItemData>();
    public List<ItemData> Items => items;
}
