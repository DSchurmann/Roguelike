using UnityEngine;

public class CollisionManager
{
    private TileMap map;

    public CollisionManager(TileMap m)
    {
        map = m;
    }

    public void CheckTile(Position current, Position newPos, UnitMovement um, bool isPlayer)
    {
        TileData cur = map.GetTile(current.x, current.y);
        TileData target = map.GetTile(newPos.x, newPos.y);
        if (target.Unit == null)
        {
            target.Unit = um.gameObject;
            cur.Unit = null;

            if(isPlayer && target.Item)
            {
                Inventory inv = um.GetComponent<Inventory>();
                //need to check if player inv is full
                if(!inv.IsFull())
                {
                    inv.AddItem(target.Item.GetComponent<ItemObject>());
                    Object.Destroy(target.Item);
                    target.Item = null;
                }
            }
            else if(!isPlayer && target.Item)
            {
                EnemyStats stats = um.GetComponent<EnemyStats>();
                if(!stats.HasItem())
                {
                    stats.SetItem(target.Item.GetComponent<ItemObject>().GetItem);
                    Object.Destroy(target.Item);
                    target.Item = null;
                }
            }
            um.SetPosition(newPos);
            um.transform.position = target.transform.position;
        }
        else
        {
            UnitStats stats = target.Unit.GetComponent<UnitStats>();
            if(stats)
            {
                stats.TakeDamage(um.GetComponent<UnitStats>().Attack);
            }
            um.SetPosition(current);
        }
    }
}