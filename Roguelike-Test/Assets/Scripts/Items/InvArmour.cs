using UnityEngine;

public class InvArmour : InvItem, Equipable
{
    private int armour;
    private bool equipped;

    public InvArmour(string n, GameObject p, int a) : base(n, p)
    {
        armour = a;
        equipped = false;
    }

    public void Equip()
    {
        equipped = !equipped;
    }

    public bool Equipped() => equipped;
}