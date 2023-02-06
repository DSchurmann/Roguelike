using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Equipment/Weapon", order = 1)]
public class Weapon : Item
{
    [SerializeField] private int damage;
    [SerializeField] private int range = 1;
    [SerializeField] private Element element;

    public override InvItem ConvertToInvItem()
    {
        return new InvWeapon(itemName, prefab, damage, range);
    }

    public int Damage => damage;
    public int Range => range;
    public Element Element => element;
}