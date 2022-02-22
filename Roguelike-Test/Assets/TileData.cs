using System;
using UnityEngine;

public class TileData : MonoBehaviour
{
    protected int xLocation;
    protected int yLocation;

    protected TileMap map;

    [SerializeField] protected bool isWalkable;
    [SerializeField] private bool canHaveItem = false;
    private GameObject item;
    [SerializeField] protected GameObject unit = null;

    public bool Walkable
    {
        get { return isWalkable; }
    }

    public GameObject Unit
    {
        get { return unit; }
        set { unit = value; }
    }

    public void SetMap(TileMap m)
    {
        map = m;
    }

    public void SetX(int x)
    {
        xLocation = x;
    }

    public void SetY(int y)
    {
        yLocation = y;
    }
}