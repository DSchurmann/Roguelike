using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileMap : MonoBehaviour
{
    protected struct Room
    {
        private int left;
        private int top;
        private int width;
        private int height;
        private bool isConnected;

        public Room(int l, int t, int w, int h)
        {
            left = l;
            top = t;
            width = w;
            height = h;
            isConnected = false;
        }

        public bool Intersect(Room other)
        {
            if(left > other.Right - 1)
            {
                return false;
            }
            if(top > other.Bottom - 1)
            {
                return false;
            }
            if (Right < other.left + 1)
            {
                return false;
            }
            if(Bottom < other.top + 1)
            {
                return false;
            }
            return true;
        }

        public int Left
        {
            get { return left; }
        }

        public int Top
        {
            get { return top; }
        }

        public int Bottom
        {
            get { return top + height - 1; }
        }

        public int Right
        {
            get { return left + width - 1; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }
    }

    private List<GameObject> actors;
    public GameObject currentActor;

    //need turn order variable - list?

    [SerializeField] private Tile[] tileTypes; //0 = floor, 1 = wall

    private int[,] tilesType;
    private TileData[,] tiles;
    private List<Room> rooms;

    private int sizeX = 60;
    private int sizeY = 60;
    private int maxRooms = 10;
    private int minRoomHeight = 6;
    private int maxRoomHeight = 10;
    private int minRoomWidth = 6;
    private int maxRoomWidth = 10;

    void Awake()
    {
        tiles = new TileData[sizeX, sizeY];
        rooms = new List<Room>();
        GenerateMap();
        DisplayMap();
    }

    private void Update()
    {
        UnitMovement current = currentActor.GetComponent<UnitMovement>();
        Vector2 currentPos = new Vector2(current.xPos, current.yPos);

        current.HandleMovement();

        Vector2 newPos = new Vector2(current.xPos, current.yPos);
        if(currentPos != newPos)
        {
            GetTile((int)newPos.x, (int)newPos.y).unit = current.gameObject;
            GetTile((int)currentPos.x, (int)currentPos.y).unit = null;
            //next unit's turn;
            Debug.Log("next actor's turn");
        }
    }

    private void GenerateMap()
    {
        tilesType = new int[sizeX, sizeY];
        for(int x = 0; x < sizeX; x++) //x coord
        {
            for(int y = 0; y < sizeY; y++) //y coord
            {
                tilesType[x, y] = 1;
            }
        }

        for(int i = 0; i != maxRooms; i++)
        {
            int rsx = Random.Range(minRoomWidth, maxRoomWidth);
            int rsy = Random.Range(minRoomHeight, maxRoomHeight);
            Room r = new Room(Random.Range(0, sizeX - rsx), Random.Range(0, sizeY - rsy), rsx, rsy);

            if(!RoomIntersect(r))
            {
                rooms.Add(r);
            }
        }

        foreach(Room r in rooms)
        {
            GenerateRoom(r);
        }

        for(int i = 0; i < rooms.Count; i++)
        {
            if(!rooms[i].IsConnected)
            {
                int j = Random.Range(1, rooms.Count);
                ConnectRooms(rooms[i], rooms[(i+j) % rooms.Count]);
            }
        }

    }

    private void GenerateRoom(Room r)
    {
        for(int x = 0; x < r.Width; x++)
        {
            for(int y = 0; y < r.Height; y++)
            {
                if (x == 0 || x == r.Width - 1 || y == 0 || y == r.Height - 1)
                {
                    tilesType[r.Left + x, r.Top + y] = 1;
                }
                else
                {
                    tilesType[r.Left + x, r.Top + y] = 0;
                }
            }
        }
    }

    private bool RoomIntersect(Room r)
    {
        foreach(Room r2 in rooms)
        {
            if(r.Intersect(r2))
            {
                return true;
            }
        }
        return false;
    }

    private void ConnectRooms(Room r1, Room r2)
    {
        int x1 = Random.Range(r1.Left + 1, r1.Right - 1);
        int y1 = Random.Range(r1.Bottom - 1, r1.Top + 1);
        int x2 = Random.Range(r2.Left + 1, r2.Right - 1);
        int y2 = Random.Range(r2.Bottom - 1, r2.Top + 1);

        Debug.Log($"Room1\nLeft {r1.Left}, Right {r1.Right}\nBottom{r1.Bottom}, Top{r1.Top}");
        Debug.Log($"Room2\nLeft {r2.Left}, Right {r2.Right}\nBottom{r2.Bottom}, Top{r2.Top}");
        Debug.Log($"Room 1: {x1}, {y1}\nRoom 2: {x2}, {y2}");


        int xDist = Mathf.Abs(x1 - x2);
        if(xDist > 5)
        {
            while(xDist != 5)
            {
                tilesType[x1, y1] = 0;
                x1 += x1 < x2 ? 1 : -1;
                xDist--;
            }
        }

        int yDist = Mathf.Abs(y1 - y2);
        if(yDist > 5)
        {
            while(yDist != 5)
            {
                tilesType[x1, y1] = 0;
                y1 += y1 < y2 ? 1 : -1;
                yDist--;
            }
        }



        while(x1 != x2)
        {
            tilesType[x1, y1] = 0;
            x1 += x1 < x2 ? 1 : -1;
        }
        while(y1 != y2)
        {
            tilesType[x1, y1] = 0;
            y1 += y1 < y2 ? 1 : -1;
        }
        r1.IsConnected = true;
        r2.IsConnected = true;
    }

    private void DisplayMap()
    {
        for( int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                Tile tile = tileTypes[tilesType[x, y]];
                GameObject g = (GameObject)Instantiate(tile.tilePrefab, new Vector3(x, 0, y), Quaternion.identity);

                TileData data = g.GetComponent<TileData>();
                data.xLocation = x;
                data.yLocation = y;
                data.map = this;

                tiles[x, y] = data;
            }
        }
    }

    public TileData GetTile(int x, int y)
    {
        TileData result = null;
        try
        {
            result = tiles[x, y];
        } 
        catch(IndexOutOfRangeException e)
        {
            Debug.LogError($"{e}\nError at index [{x}, {y}]");
        }
        return result;
    }
}
