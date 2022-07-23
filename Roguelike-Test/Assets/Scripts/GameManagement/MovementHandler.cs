using UnityEngine;

public class MovementHandler
{
    private TileMap map;
    private CollisionManager cs;

    public MovementHandler(TileMap m)
    {
        map = m;
        cs = new CollisionManager(map);
    }

    public void Update(UnitMovement current)
    {
        UnitStats stat = current.GetComponent<UnitStats>();
        Position currentPos = new Position(current.PosX, current.PosY);
        UnitMovement um = current;

        Position newPos = current.HandleMovement();
        if (currentPos != newPos)
        {
            cs.CheckTile(currentPos, newPos, um, current is PlayerInputs ? true : false);
            //next unit's turn;
            GameController.Instance.NextTurn();
        }
    }
}
    