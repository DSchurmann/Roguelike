using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private List<GameObject> actors;
    private GameObject currentActor;

    [SerializeField] private Tile[] tileTypes; //0 = floor, 1 = wall

    private int[,] tiles;

    private int sizeX = 10;
    private int sizeY = 10;

    void Start()
    {
        GenerateMap();
        DisplayMap();
    }

    private void GenerateMap()
    {
        tiles = new int[sizeX, sizeY];
        for(int x = 0; x < sizeX; x++) //x coord
        {
            for(int y = 0; y < sizeY; y++) //y coord
            {
                if (Random.Range(0, 100) < 50)
                {
                    tiles[x, y] = 0;
                }
                else
                {
                    tiles[x, y] = 1;
                }
            }
        }
    }

    private void DisplayMap()
    {
        for( int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                Tile tile = tileTypes[tiles[x, y]];
                GameObject g = (GameObject)Instantiate(tile.tilePrefab, new Vector3(x, 0, y), Quaternion.identity);

                TileData data = g.GetComponent<TileData>();
                data.xLocation = x;
                data.yLocation = y;
                data.map = this;

            }
        }
    }
}
