using UnityEngine;

[CreateAssetMenu(fileName = "Armour", menuName = "ScriptableObjects/Equipment/Armour", order = 3)]
public class Armour : Item, Equipable
{
    [SerializeField] private int armour;
    private bool equip = false;

    public override InvItem ConvertToInvItem()
    {
        return new InvArmour(itemName, prefab, armour);
    }

    public int Defense => armour;

    public void Equip()
    {
        equip = !equip;
    }

    public bool Equipped()
    {
        return equip;
    }
}