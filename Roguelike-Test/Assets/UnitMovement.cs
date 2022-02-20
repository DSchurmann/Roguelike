using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitMovement : MonoBehaviour
{
    public TileMap map;
    public int xPos;
    public int yPos;
    public bool controllable = false;

    private void Start()
    {
        TileData data = map.GetTile(xPos, yPos);
        data.unit = this.gameObject;
        transform.position = data.transform.position;

    }

    public void HandleMovement()
    {
        if (Keyboard.current[Key.W].wasPressedThisFrame)
        {
            //get tile
            TileData t = map.GetTile(xPos, yPos+1);
            //move north
            if (t != null)
            {
                if (t.isWalkable && t.unit == null)
                {
                    //move up
                    transform.position = t.transform.position;
                    yPos++;
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
                if (t.isWalkable && t.unit == null)
                {
                    //move down
                    transform.position = t.transform.position;
                    yPos--;
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
                if (t.isWalkable && t.unit == null)
                {
                    //move left
                    transform.position = t.transform.position;
                    xPos++;
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
                if (t.isWalkable && t.unit == null)
                {
                    //move right
                    transform.position = t.transform.position;
                    xPos--;
                }
            }
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

    public void SetMap(TileMap m)
    {
        map = m;
    }
}
