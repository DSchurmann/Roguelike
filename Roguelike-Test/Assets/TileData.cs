using System;
using UnityEngine;

public class TileData : MonoBehaviour
{
    public int xLocation;
    public int yLocation;

    public TileMap map;

    public bool isWalkable;
    public GameObject unit = null;
}