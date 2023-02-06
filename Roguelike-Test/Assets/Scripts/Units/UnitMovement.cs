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
    [SerializeField]
    private Dictionary<Dir, Vector3> rotations = new Dictionary<Dir, Vector3>()
    {
        {Dir.left, new Vector3(0, -90, 0) },
        {Dir.right, new Vector3(0, 90, 0)},
        {Dir.up, new Vector3(0, 0, 0)},
        {Dir.down, new Vector3(0, 180, 0) },
        {Dir.leftUp, new Vector3(0, -45, 0)},
        {Dir.rightUp, new Vector3(0,45, 0) },
        {Dir.leftDown, new Vector3(0, -135, 0) },
        {Dir.rightDown, new Vector3(0, 135, 0) }
    };

    protected virtual void Start()
    {
        TileData data = map.GetTile(xPos, yPos);
        data.Unit = this.gameObject;
        transform.position = data.transform.position;
        stats = GetComponent<UnitStats>();
    }

    private void OnDestroy()
    {
        GameController.Instance.RemoveActor(this);
    }

    protected void FacingDirection(Dir dir)
    {
        transform.eulerAngles = rotations[dir];
    }

    protected virtual void Attack() { }

    public virtual Position HandleMovement() { return new Position(); }

    public void SetMap(TileMap m)
    {
        map = m;
    }

    public TileData GetTile()
    {
        return map.GetTile(xPos, yPos);
    }

    public Position Position
    {
        get => new Position(xPos, yPos);
        set
        {
            xPos = value.x;
            yPos = value.y;
        }
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

    protected enum Dir
    {
        none,
        left,
        right,
        up,
        down,
        leftUp,
        leftDown,
        rightUp,
        rightDown,
    }
}
