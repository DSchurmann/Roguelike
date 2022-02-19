using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
    public TileType type;
    public GameObject tilePrefab;
}

public enum TileType
{
    Floor,
    Wall,
    Trap,
}
