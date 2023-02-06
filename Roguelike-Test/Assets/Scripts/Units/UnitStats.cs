using UnityEngine;
using Random = UnityEngine.Random; 

public class UnitStats : MonoBehaviour
{
    protected int currentHP;
    protected int currentMP;
    [SerializeField] protected int maxHP;
    [SerializeField] protected int maxMP;
    [SerializeField] protected int attack;
    [SerializeField] protected int armour;

    protected int speed;
    protected int exp;

    [SerializeField] protected Status status;
    protected bool takenPoison = false;
    protected int poisonCounter = 0;
    protected int poisonTarget = 10;
    protected string actorName;

    [SerializeField] protected Resistance res;

    protected virtual void Start()
    { 
        currentHP = maxHP;
        currentMP = maxMP;
        res = new Resistance();
        status = Status.normal;
    }

    public virtual void TakeDamage(int damage)
    {
        int dmgToTake = damage - armour;

        if(dmgToTake < 0)
        {
            dmgToTake = 0;
        }
        Debug.Log("Damage: " + dmgToTake);
        currentHP -= dmgToTake;
    }

    public virtual void Heal(int h, bool range)
    {
        //range 10% for testing
        if (range)
        {
            int amt = (int)(h / 0.1f);
            int heal = Random.Range(h - amt, h + amt);
            currentHP += heal;
        }
        else
        {
            currentHP += h;
        }
    }

    public void ReduceMana(int d)
    {
        currentMP -= d;
        if (currentMP <= 0)
        {
            currentMP = 0;
        }
    }

    public void RestoreMana(int m, bool range)
    {
        //range 10% for testing
        if (range)
        {
            int amt = (int)(m / 0.1f);
            int heal = Random.Range(m - amt, m + amt);
            currentMP += heal;
        }
        else
        {
            currentMP += m;
        }
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

    public virtual void DropItem(Item i) { }

    public void ResetPoison()
    {
        takenPoison = false;
    }

    public float HealthPercent()
    {
        return (float)currentHP / (float)maxHP;
    }

    public float ManaPercent()
    {
        return (float)currentMP / (float)maxMP;
    }

    public virtual int Attack => attack;

    public virtual int Armour => armour;
}