using UnityEngine;

public class EnemyStats : UnitStats
{
    private Item item = null;

    protected override void Start()
    {
        base.Start();
    }

    public override void DropItem(Item i)
    {
        if(HasItem())
        {
            GameObject g = Instantiate(item.Prefab);
            UnitMovement um = GetComponent<UnitMovement>();
            TileData t = GameController.Instance.GetTile(um.PosX, um.PosY);
            t.Item = g;
            g.transform.position = t.transform.position;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHP <= 0)
        {
            //die
            DropItem(item);
            Destroy(gameObject);
        }
    }

    public bool HasItem()
    {
        return item == null ? false : true;
    }

    public void SetItem(Item i)
    {
        item = i;
    }

    public void DropItem()
    {

    }
}