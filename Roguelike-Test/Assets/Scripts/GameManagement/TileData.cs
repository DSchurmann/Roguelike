using System;
using UnityEngine;

public class TileData : MonoBehaviour
{
    protected int xLocation;
    protected int yLocation;

    protected TileMap map;

    [SerializeField] protected bool isWalkable;
    [SerializeField] private bool canHaveItem = false;
    [SerializeField] private GameObject item;
    [SerializeField] protected GameObject unit = null;

    private void OnDestroy()
    {
        if(item)
        {
            Destroy(item);
        }
    }

    public bool Walkable
    {
        get { return isWalkable; }
    }

    public bool CanHaveItem
    {
        get { return canHaveItem; }
    }

    public GameObject Unit
    {
        get { return unit; }
        set { unit = value; }
    }

    public GameObject Item
    {
        get { return item; }
        set { item = value; }
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