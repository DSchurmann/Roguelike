using System;
using UnityEngine;

public class InvWeapon : InvItem, Equipable
{
    private int damage;
    private int range;
    private bool equipped;

    public InvWeapon(string n, GameObject p, int d, int r) : base(n, p)
    {
        damage = d;
        range = r;
        equipped = false;
    }

    public void Equip()
    {
        equipped = !equipped;
    }

    public bool Equipped() => equipped;
    public int Damage => damage;
    public int Range => range;

}