using UnityEngine;

[CreateAssetMenu(fileName = "Accessory", menuName = "ScriptableObjects/Equipment/Accessory", order = 4)]
public class Accessory : Item, Equipable
{
    private bool equip;

    public override InvItem ConvertToInvItem()
    {
        return new InvAccessory(itemName, prefab);
    }

    public void Equip()
    {
        equip = !equip;
    }

    public bool Equipped()
    {
        return equip;
    }
}