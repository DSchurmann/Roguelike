using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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
    private int surroundingTiles = 0;

    private float moveDelay = 0.15f;
    private float moveTimer = 0;

    private Dictionary<Dir, Position> directions = new Dictionary<Dir, Position>()
    {
        {Dir.up, new Position(0,1)},
        {Dir.down, new Position(0,-1)},
        {Dir.left, new Position(-1,0)},
        {Dir.right, new Position(1,0)},
        {Dir.leftUp, new Position(-1,1)},
        {Dir.rightUp, new Position(1,1)},
        {Dir.leftDown, new Position(-1,-1)},
        {Dir.rightDown, new Position(1,-1)},
    };

    protected override void Start()
    {
        base.Start();
        controllable = true;
        inv = GetComponent<Inventory>();
    }

    private void Update()
    {
        if(GameController.Instance.GetCurrent() == this)
            moveTimer -= Time.deltaTime;

        if (input.Run || input.AllMovementFalse())
        {
            moveTimer = 0;
            if (ValidTiles() > 2 && surroundingTiles <= 2)
                input.SetAllMovementFalse();
        }
    }

    public override Position HandleMovement()
    {
        if (moveTimer <= 0)
        {
            Position pos = this.Position;

            Dir d = dir;
            if (stats.CheckStatus())
            {
                //Misc inputs
                if (input.Attack)
                {
                    input.SetAttackFalse();
                    int range = 1;
                    if (inv.Equips.Weapon != null)
                        range = inv.Equips.Weapon.Range;
                    Attack(range);
                }
                if (input.Menu)
                {
                    //activate UI menu - call from game controller?
                    GameController.Instance.MenuInput();
                    //change input type to menu
                    input.SetMenuFalse();
                }
                if (Keyboard.current[Key.Space].wasPressedThisFrame) //pass turn
                {
                    GameController.Instance.NextTurn();
                }

                //movement related inputs
                if (input.Face)
                {
                    ChangeFacingDir();
                }
                else if (!input.Diagonal)
                {
                    if (input.Up)
                    {
                        d = Dir.up;
                        CheckMovement(d);
                    }
                    else if (input.Down)
                    {
                        d = Dir.down;
                        CheckMovement(d);
                    }
                    else if (input.Right)
                    {
                        d = Dir.right;
                        CheckMovement(d);
                    }
                    else if (input.Left)
                    {
                        d = Dir.left;
                        CheckMovement(d);
                    }
                }

                if (input.Up && input.Right)
                {
                    d = Dir.rightUp;
                    if(DiagonalIsValid(d))
                        CheckMovement(d);
                }
                if (input.Up && input.Left)
                {
                    d = Dir.leftUp;
                    if (DiagonalIsValid(d))
                        CheckMovement(d);
                }
                if (input.Down && input.Right)
                {
                    d = Dir.rightDown;
                    if (DiagonalIsValid(d))
                        CheckMovement(d);
                }
                if (input.Down && input.Left)
                {
                    d = Dir.leftDown;
                    if (DiagonalIsValid(d))
                        CheckMovement(d);
                }
            }

            //change everything
            FacingDirection(d);
            dir = d;
            Position newPos = new Position(xPos, yPos);
            if (pos != newPos)
            {
                moveTimer = moveDelay;
            }
        }
        surroundingTiles = ValidTiles();
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

        vert += directions[dir].y * range;
        hor += directions[dir].x * range;

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

    private void ChangeFacingDir()
    {
        if (input.Up && input.Left)
        {
            dir = Dir.leftUp;
        }
        else if (input.Up && input.Right)
        {
            dir = Dir.rightUp;
        }
        else if (input.Down && input.Left)
        {
            dir = Dir.leftDown;
        }
        else if (input.Down && input.Right)
        {
            dir = Dir.rightDown;
        }
        else if (input.Up)
        {
            dir = Dir.up;
        }
        else if (input.Down)
        {
            dir = Dir.down;
        }
        else if (input.Left)
        {
            dir = Dir.left;
        }
        else if (input.Right)
        {
            dir = Dir.right;
        }
    }

    private void CheckMovement(Dir d)
    {
        int x = xPos + directions[d].x;
        int y = yPos + directions[d].y;

        TileData t = map.GetTile(x, y);
        if (t != null)
        {
            if (t.Walkable)
            {
                xPos = x;
                yPos = y;
            }
        }
    }

    private bool DiagonalIsValid(Dir d)
    {
        int x = xPos + directions[d].x;
        int y = yPos + directions[d].y;
        TileData tile1 = map.GetTile(x, yPos);
        TileData tile2 = map.GetTile(xPos, y);
        if (tile1 != null)
        {
            if (!tile1.Walkable)
                return false;
        }
        if (tile2 != null)
        {
            if (!tile2.Walkable)
                return false;
        }
        return true;
    }

    private int ValidTiles()
    {
        int num = 0;

        foreach(var d in directions)
        {
            int x = xPos + d.Value.x;
            int y = yPos + d.Value.y;
            if (map.GetTile(x, y).Walkable)
                num++;
        }

        return num;
    }
}