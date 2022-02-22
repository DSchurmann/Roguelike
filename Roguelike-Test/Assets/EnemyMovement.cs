using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMovement : UnitMovement
{
    protected override void Start()
    {
        base.Start();
    }

    public override void HandleMovement()
    {
        int dir = Random.Range(0, 4);
        TileData t = null;
        Vector2 pos = new Vector2(xPos, yPos);

        switch (dir)
        {
            case 0:
                t = map.GetTile(xPos, yPos + 1);
                //move north
                if (t != null)
                {
                    if (t.Walkable && t.Unit == null)
                    {
                        //move up
                        transform.position = t.transform.position;
                        yPos++;
                    }
                }
                break;
            case 1:
                t = map.GetTile(xPos, yPos - 1);
                //move south
                if (t != null)
                {
                    if (t.Walkable && t.Unit == null)
                    {
                        //move down
                        transform.position = t.transform.position;
                        yPos--;
                    }
                }
                break;
            case 2:
                t = map.GetTile(xPos + 1, yPos);
                //move east
                if (t != null)
                {
                    if (t.Walkable && t.Unit == null)
                    {
                        //move left
                        transform.position = t.transform.position;
                        xPos++;
                    }
                }
                break;
            case 3:
                t = map.GetTile(xPos - 1, yPos);
                //move west
                if (t != null)
                {
                    if (t.Walkable && t.Unit == null)
                    {
                        //move right
                        transform.position = t.transform.position;
                        xPos--;
                    }
                }
                break;
            default:
                map.NextTurn();
                break;
        }

        if(pos == new Vector2(xPos,yPos))
        {
            map.NextTurn();
        }
    }
}