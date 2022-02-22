using UnityEngine.InputSystem;

public class PlayerMovement : UnitMovement
{
    private Inventory inv;

    protected override void Start()
    {
        base.Start();
        controllable = true;
        inv = GetComponent<Inventory>();
    }

    public override void HandleMovement()
    {
        if (Keyboard.current[Key.W].wasPressedThisFrame)
        {
            //get tile
            TileData t = map.GetTile(xPos, yPos + 1);
            //move north
            if (t != null)
            {
                if (t.Walkable && t.Unit == null)
                {
                    transform.position = t.transform.position;
                    yPos++;
                }
                else if (t.Unit != null)
                {
                    if (t.Unit.GetComponent<UnitStats>())
                    {
                        t.Unit.GetComponent<UnitStats>().TakeDamage(stats.Attack);
                        map.NextTurn();
                    }
                }
            }
        }
        if (Keyboard.current[Key.S].wasPressedThisFrame)
        {
            //get tile
            TileData t = map.GetTile(xPos, yPos - 1);
            //move south
            if (t != null)
            {
                if (t.Walkable && t.Unit == null)
                {
                    transform.position = t.transform.position;
                    yPos--;
                }
                else if (t.Unit != null)
                {
                    if (t.Unit.GetComponent<UnitStats>())
                    {
                        t.Unit.GetComponent<UnitStats>().TakeDamage(stats.Attack);
                        map.NextTurn();
                    }
                }
            }
        }
        if (Keyboard.current[Key.D].wasPressedThisFrame)
        {
            //get tile
            TileData t = map.GetTile(xPos + 1, yPos);
            //move east
            if (t != null)
            {
                if (t.Walkable && t.Unit == null)
                {
                    transform.position = t.transform.position;
                    xPos++;
                }
                else if (t.Unit != null)
                {
                    if (t.Unit.GetComponent<UnitStats>())
                    {
                        t.Unit.GetComponent<UnitStats>().TakeDamage(stats.Attack);
                        map.NextTurn();
                    }
                }
            }
        }
        if (Keyboard.current[Key.A].wasPressedThisFrame)
        {
            //get tile
            TileData t = map.GetTile(xPos - 1, yPos);
            //move west
            if (t != null)
            {
                if (t.Walkable && t.Unit == null)
                {
                    transform.position = t.transform.position;
                    xPos--;
                }
                else if (t.Unit != null)
                {
                    if (t.Unit.GetComponent<UnitStats>())
                    {
                        t.Unit.GetComponent<UnitStats>().TakeDamage(stats.Attack);
                        map.NextTurn();
                    }
                }
            }
        }
        if (Keyboard.current[Key.Space].wasPressedThisFrame) //pass turn
        {
            map.NextTurn();
        }

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
}