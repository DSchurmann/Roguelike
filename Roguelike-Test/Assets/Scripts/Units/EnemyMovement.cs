using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : UnitMovement
{
    protected override void Start()
    {
        base.Start();
    }

    public override Position HandleMovement()
    {
        int dir = Random.Range(0, 4);
        TileData t = null;

        switch (dir)
        {
            case 0:
                t = map.GetTile(xPos, yPos + 1);
                //move north
                if (t != null)
                {
                    if (t.Walkable)
                    {
                        //move up
                        yPos++;
                    }
                }
                break;
            case 1:
                t = map.GetTile(xPos, yPos - 1);
                //move south
                if (t != null)
                {
                    if (t.Walkable)
                    {
                        //move down
                        yPos--;
                    }
                }
                break;
            case 2:
                t = map.GetTile(xPos + 1, yPos);
                //move east
                if (t != null)
                {
                    if (t.Walkable)
                    {
                        //move left
                        xPos++;
                    }
                }
                break;
            case 3:
                t = map.GetTile(xPos - 1, yPos);
                //move west
                if (t != null)
                {
                    if (t.Walkable)
                    {
                        //move right
                        xPos--;
                    }
                }
                break;
        }
        return new Position(xPos, yPos);
    }
}