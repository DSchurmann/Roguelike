using UnityEngine;

public class PlayerStats: UnitStats
{
    private Inventory inv;

    private int level = 1;
    private int skillPoints;

    protected override void Start()
    {
        base.Start();
        inv = GetComponent<Inventory>();
    }

    public Inventory GetInventory
    {
        get { return inv; }
    }

    public void AddExp(int xp)
    {
        exp += xp;
        //if(exp over threshold)
        {
            level++;
            //increase threshold?
            skillPoints += 3;
        }
    }
}