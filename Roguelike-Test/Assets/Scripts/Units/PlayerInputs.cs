using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : UnitMovement
{
    private Inventory inv;
    private PlayerInputManager input;

    protected override void Start()
    {
        base.Start();
        controllable = true;
        inv = GetComponent<Inventory>();
    }

    public override Position HandleMovement()
    {
        if (stats.CheckStatus())
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
                }
                input.SetUpFalse();
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
                }
                input.SetDownFalse();
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
                }
                input.SetRightFalse();
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
                }
                input.SetLeftFalse();
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
        return new Position(xPos, yPos);

        //if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.D))
        //{
        //    //move north east
        //}
        //if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.A))
        //{
        //    //move north west
        //}
        //if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.D))
        //{
        //    //move south east
        //}
        //if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.A))
        //{
        //    //move south west
        //}
    }

    public void SetInput(PlayerInputManager pim)
    {
        input = pim;
    }
}