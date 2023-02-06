using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerInputs : UnitMovement
{
    private Inventory inv;
    private PlayerInputManager input;
    private Dir dir = Dir.down;

    protected override void Start()
    {
        base.Start();
        controllable = true;
        inv = GetComponent<Inventory>();
    }

    public override Position HandleMovement()
    {
        Dir d = dir;
        if (stats.CheckStatus())
        {
            if(input.Attack)
            {
                input.SetAttackFalse();
                int range = 1;
                if (inv.Equips.Weapon != null)
                    range = inv.Equips.Weapon.Range;
                Attack(range);
            }

            if (!input.Diagonal)
            {
                if (input.Up)
                {
                    //get tile
                    TileData t = map.GetTile(xPos, yPos + 1);
                    //move north
                    if (t != null)
                    {
                        if (t.Walkable)
                        {
                            yPos++;
                        }
                        d = Dir.up;
                    }
                    if (!input.Run)
                    {
                        input.SetUpFalse();
                    }
                }
                else if (input.Down)
                {
                    //get tile
                    TileData t = map.GetTile(xPos, yPos - 1);
                    //move south
                    if (t != null)
                    {
                        if (t.Walkable)
                        {
                            yPos--;
                        }
                        d = Dir.down;
                    }
                    if (!input.Run)
                    {
                        input.SetDownFalse();
                    }
                }
                else if (input.Right)
                {
                    //get tile
                    TileData t = map.GetTile(xPos + 1, yPos);
                    //move east
                    if (t != null)
                    {
                        if (t.Walkable)
                        {
                            xPos++;
                        }
                        d = Dir.right;
                    }
                    if (!input.Run)
                    {
                        input.SetRightFalse();
                    }
                }
                else if (input.Left)
                {
                    //get tile
                    TileData t = map.GetTile(xPos - 1, yPos);
                    //move west
                    if (t != null)
                    {
                        if (t.Walkable)
                        {
                            xPos--;
                        }
                        d = Dir.left;
                    }
                    if (!input.Run)
                    {
                        input.SetLeftFalse();
                    }
                }
                else if (input.Menu)
                {
                    //activate UI menu - call from game controller?
                    GameController.Instance.MenuInput();
                    //change input type to menu
                    input.SetMenuFalse();
                }
                else if (Keyboard.current[Key.Space].wasPressedThisFrame) //pass turn
                {
                    GameController.Instance.NextTurn();
                }
            }

            if (input.Up && input.Right)
            {
                //move north east
                TileData t = map.GetTile(xPos + 1, yPos + 1);
                if (t != null)
                {
                    if (t.Walkable)
                    {
                        xPos++;
                        yPos++;
                    }
                    d = Dir.rightUp;
                }
                if (!input.Run)
                {
                    input.SetUpFalse();
                    input.SetRightFalse();
                }
            }
            if (input.Up && input.Left)
            {
                //move north west
                TileData t = map.GetTile(xPos - 1, yPos + 1);
                if (t != null)
                {
                    if (t.Walkable)
                    {
                        xPos--;
                        yPos++;
                    }
                    d = Dir.leftUp;
                }
                if (!input.Run)
                {
                    input.SetUpFalse();
                    input.SetLeftFalse();
                }
            }
            if (input.Down && input.Right)
            {
                //move south east
                TileData t = map.GetTile(xPos + 1, yPos - 1);
                if (t != null)
                {
                    if (t.Walkable)
                    {
                        xPos++;
                        yPos--;
                    }
                    d = Dir.rightDown;
                }
                if (!input.Run)
                {
                    input.SetDownFalse();
                    input.SetRightFalse();
                }
            }
            if (input.Down && input.Left)
            {
                //move south west
                TileData t = map.GetTile(xPos - 1, yPos - 1);
                if (t != null)
                {
                    if (t.Walkable)
                    {
                        xPos--;
                        yPos--;
                    }
                    d = Dir.leftDown;
                }
                if (!input.Run)
                {
                    input.SetDownFalse();
                    input.SetLeftFalse();
                }
            }
        }

        FacingDirection(d);
        dir = d;
        return new Position(xPos, yPos);
    }

    public void SetInput(PlayerInputManager pim)
    {
        input = pim;
    }

    private void Attack(int range)
    {
        int x = xPos;
        int y = yPos;
        int vert = y;
        int hor = x;
        switch (dir)
        {
            case Dir.down:
                vert -= range;
                Debug.Log("down called-vert:" + vert);
                break;
            case Dir.up:
                vert += range;
                break;
            case Dir.left:
                hor -= range;
                break;
            case Dir.right:
                hor += range;
                break;
            case Dir.leftUp:
                hor -= range;
                vert += range;
                break;
            case Dir.rightUp:
                hor += range;
                vert += range;
                break;
            case Dir.leftDown:
                hor -= range;
                vert -= range;
                break;
            case Dir.rightDown:
                hor += range;
                vert -= range;
                break;
        }

        while (x != hor || y != vert)
        {
            if (x < hor)
                x++;
            else if(x > hor)
                x--;

            if (y < vert)
                y++;
            else if (y > vert)
                y--;

            TileData t = map.GetTile(x, y);
            if (t.Unit != null)
                t.Unit.GetComponent<UnitStats>().TakeDamage(stats.Attack);  
        }

        GameController.Instance.NextTurn();
    }
}