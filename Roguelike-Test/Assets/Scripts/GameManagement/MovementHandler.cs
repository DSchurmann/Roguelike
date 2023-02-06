using UnityEngine;
using UnityEngine.InputSystem;

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
        Position currentPos = new Position(current.PosX, current.PosY);
        UnitMovement um = current;
        Position newPos = current.HandleMovement();
        if (currentPos != newPos)
        {
            if (cs.CheckTile(currentPos, newPos, um, current is PlayerInputs ? true : false))
            {
                //next unit's turn;
                GameController.Instance.NextTurn();
            }
            else
                current.Position = currentPos;
        }
    }

    public bool PickUpItem(UnitMovement um)
    {
        return cs.PickUpItem(um);
    }

    public void CheckTile(UnitMovement current)
    {
        Position pos = new Position(current.PosX, current.PosY);
        cs.CheckTile(pos, pos, current, current is PlayerInputs ? true : false);
    }
}
    