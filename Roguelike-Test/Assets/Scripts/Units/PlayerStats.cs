using System.Security.Cryptography;
using UnityEngine;

public class PlayerStats: UnitStats
{
    private Inventory inv;

    private int level = 1;
    private int expTarget = 100;
    private int skillPoints;

    protected void Awake()
    {
        base.Start();
        inv = GetComponent<Inventory>();
    }

    public Inventory GetInventory
    {
        get { return inv; }
    }

	// EXP from things other than killing, (picking up items, returning alive, selling items, etc.)
    public void AddExp(int xp)
    {
        exp += xp;
        if(exp >= expTarget)
        {
            level++;
            exp = 0;
            skillPoints += 3;
        }
    }

    public string Level() => level.ToString();
    public string Exp() => exp + " / " + expTarget;
    public int Defence => armour;
    public override int Attack
    {
        get
        {
            int a = attack;
            if(inv.Equips.Weapon != null)
                a += inv.Equips.Weapon.Damage;
                
            return a;
        }
    }
    //need to adjust to add armour res
    public Resistance Resistances => res;
    public Status StatusState => status;
    public string HealthNumbers()
    {
        return currentHP + "/" + maxHP;
    }

    public string ManaNumbers()
    {
        return currentMP + "/" + maxMP;
    }
}