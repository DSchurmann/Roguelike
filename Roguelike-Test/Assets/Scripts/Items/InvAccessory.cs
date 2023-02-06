using UnityEngine;

public class InvAccessory : InvItem, Equipable
{
    private bool equipped;

    public InvAccessory(string n, GameObject p) : base(n,p)
    {
        equipped = false;
    }

    public void Equip()
    {
        equipped = !equipped;
    }

    public bool Equipped() => equipped;
}