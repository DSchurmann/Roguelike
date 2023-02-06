using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] private bool breakable = false;
    [SerializeField] protected GameObject prefab;

    public string GetName => itemName;

    public virtual InvItem ConvertToInvItem()
    { 
        return new InvItem(itemName, prefab);
    }

    public GameObject Prefab => prefab;
}