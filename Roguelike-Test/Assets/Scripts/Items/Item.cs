using System;
using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField] private string itemName;
    [SerializeField] private ItemType type;

    public Item(string n, ItemType t)
    {
        itemName = n;
        type = t;
    }

    public string GetName
    {
        get { return itemName; }
    }
    public ItemType GetItemType
    {
        get { return type; }
    }
}

public enum ItemType
{
    weapon,
    head,
    chest,
    arms,
    legs,
    feet,
    projectile,
    consumable,
    gold
}