using UnityEngine;

public class CollisionManager
{
    private TileMap map;

    public CollisionManager(TileMap m)
    {
        map = m;
    }

    public bool CheckTile(Position current, Position newPos, UnitMovement um, bool isPlayer)
    {
        bool result = true;
        TileData cur = map.GetTile(current.x, current.y);
        TileData target = map.GetTile(newPos.x, newPos.y);
        if (target is Stairs && target.Unit == null && isPlayer)
        {
            um.Position = newPos;
            um.transform.position = target.transform.position;
            GameController.Instance.Ground();
            result = false;
        }
        else if (target.Unit == null || target.Unit == um.gameObject)
        {
            target.Unit = um.gameObject;
            cur.Unit = null;

            if (target.Item)
            {
                if (isPlayer)
                {
                    Inventory inv = um.GetComponent<Inventory>();
                    //need to check if player inv is full
                    if (!inv.IsFull())
                    {
                        inv.AddItem(target.Item.GetComponent<ItemObject>());
                        Object.Destroy(target.Item);
                        target.Item = null;
                    }
                }
                else if (!isPlayer)
                {
                    EnemyStats stats = um.GetComponent<EnemyStats>();
                    if (!stats.HasItem())
                    {
                        stats.SetItem(target.Item.GetComponent<ItemObject>().GetItem);
                        Object.Destroy(target.Item);
                        target.Item = null;
                    }
                }
            }
            um.Position = newPos;
            um.transform.position = target.transform.position;
        }
        else if (target.Unit != null && !isPlayer) //KEEP FOR NOW FOR ENEMIES... JUST FOR NOW
        {
            UnitStats stats = target.Unit.GetComponent<UnitStats>();
            if (stats)
            {
                stats.TakeDamage(um.GetComponent<UnitStats>().Attack);
            }
            um.Position = current;
        }
        else if (target.Unit != null)
            result = false;
        return result;
    }

    public bool PickUpItem(UnitMovement um)
    {
        Position pos = um.Position;
        TileData target = map.GetTile(pos.x, pos.y);
        bool pickedUp = false;
        Inventory inv = um.GetComponent<Inventory>();
        //need to check if player inv is full
        if (!inv.IsFull())
        {
            inv.AddItem(target.Item.GetComponent<ItemObject>());
            Object.Destroy(target.Item);
            target.Item = null;
            pickedUp = true;
        }
        return pickedUp;
    }

    public void CheckGround(Position pos, UnitMovement um, bool isPlayer)
    {
        TileData cur = map.GetTile(pos.x, pos.y);
        if (cur is Stairs && isPlayer)
        {
            GameController.Instance.Ground();
        }
        else
        {
            if (isPlayer && cur.Item)
            {
                Inventory inv = um.GetComponent<Inventory>();
                //need to check if player inv is full
                if (!inv.IsFull())
                {
                    inv.AddItem(cur.Item.GetComponent<ItemObject>());
                    Object.Destroy(cur.Item);
                    cur.Item = null;
                }
            }
        }
    }
}