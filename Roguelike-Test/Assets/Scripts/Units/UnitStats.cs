using System;
using UnityEngine;
using Random = UnityEngine.Random; 

public class UnitStats : MonoBehaviour
{
    protected int currentHP;
    protected int currentMP;
    [SerializeField] protected int maxHP;
    [SerializeField] protected int mapMP;
    [SerializeField] protected int attack;
    [SerializeField] protected int armour;

    protected int speed;
    protected int exp;

    protected Status status;
    protected bool takenPoison = false;
    protected int poisonCounter = 0;
    protected int poisonTarget = 10;
    protected string actorName;

    protected Resistance res;

    protected virtual void Start()
    {
        currentHP = maxHP;
        res = new Resistance();
        status = Status.normal;
    }

    public virtual int TakeDamage(int damage)
    {
        int dmgToTake = damage - armour;

        if(dmgToTake < 0)
        {
            dmgToTake = 0;
        }
        Debug.Log("Damage: " + dmgToTake);
        currentHP -= dmgToTake;
        if(currentHP <= 0)
        {
            //die
            Destroy(gameObject);
            return exp;
        }
        return 0;
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Armour
    {
        get { return armour; }
    }

    public bool CheckStatus()
    {
        bool canMove = true;
        //do something if status is not normal
        //stun = NextTurn(), poison = % maxHP as damage, sleep = NextTurn() with % chance to wake up
        switch(status)
        {
            case Status.normal:
                break;
            case Status.poison:
                int dmg = maxHP / 16;
                currentHP -= dmg;
                takenPoison = true;
                poisonCounter++;
                if(poisonCounter < poisonTarget)
                {
                    status = Status.normal;
                }
                break;
            case Status.sleep:
                float rand = Random.Range(0, 100);
                if(rand <= 25)
                {
                    status = Status.normal;
                }
                else
                {
                    GameController.Instance.NextTurn();
                    canMove = false;
                }
                break;
            case Status.stun:
                status = Status.normal;
                GameController.Instance.NextTurn();
                canMove = false;
                break;
        }

        return canMove;
    }

    public void ResetPoison()
    {
        takenPoison = false;
    }
}