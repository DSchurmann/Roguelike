using System;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    protected int currentHP;
    [SerializeField] protected int maxHP;
    [SerializeField] protected int attack;
    [SerializeField] protected int armour;

    protected int speed;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        int dmgToTake = damage - armour;

        if(dmgToTake < 0)
        {
            dmgToTake = 0;
        }
        Debug.Log("Daamage: " + dmgToTake);
        currentHP -= dmgToTake;
        if(currentHP <= 0)
        {
            //die
            Destroy(gameObject);
        }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Armour
    {
        get { return armour; }
    }
}