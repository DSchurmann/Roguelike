using UnityEngine;

[CreateAssetMenu(fileName = "Head", menuName = "ScriptableObjects/Equipment/Head", order = 2)]
public class Head : Item, Equipable
{
    [SerializeField] private int armour;
    private bool equip = false;

    public override InvItem ConvertToInvItem()
    {
        return new InvHead(itemName, prefab, armour);
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