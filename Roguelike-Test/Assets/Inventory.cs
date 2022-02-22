using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> inv;

    private void Start()
    {
        inv = new List<Item>();
    }

    public void AddItem(ItemObject i)
    {
        inv.Add(new Item(i.GetItem.GetName, i.GetItem.GetItemType));
        Destroy(i.gameObject);
    }

    public void RemoveItem(Item i)
    {
        inv.Remove(i);
    }

    public List<Item> GetInventory
    {
        get { return inv; }
    }
}