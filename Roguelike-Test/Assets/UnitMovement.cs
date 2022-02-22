using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitMovement : MonoBehaviour
{
    protected TileMap map;
    protected int xPos;
    protected int yPos;
    protected bool controllable = false;
    protected UnitStats stats;

    protected virtual void Start()
    {
        TileData data = map.GetTile(xPos, yPos);
        data.Unit = this.gameObject;
        transform.position = data.transform.position;
        stats = GetComponent<UnitStats>();
    }

    private void OnDestroy()
    {
        map.RemoveFromTurnOrder(this);
    }

    public virtual void HandleMovement() { }

    public void SetMap(TileMap m)
    {
        map = m;
    }

    public int PosX
    {
        get { return xPos; }
        set { xPos = value; }
    }
    public int PosY
    {
        get { return yPos; }
        set { yPos = value; }
    }
}
