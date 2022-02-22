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

    public GameObject player;
    public GameObject enemy;
    private int startEnemies = 5;
    private int currentFloor = 1;

    //need turn order variable - list?
    private List<GameObject> turnOrder; //for now, player moves first, iterate through all enemies
    private int currentTurn = 0;
    private UnitMovement current;

    private List<GameObject> items;

    [SerializeField] private Tile[] tileTypes; //0 = floor, 1 = wall, 2 = stairs

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
        SetupMap();
    }

    private void Update()
    {
        Vector2 currentPos = new Vector2(current.PosX, current.PosY);
        UnitMovement um = current;
        current.HandleMovement();
        Vector2 newPos = new Vector2(current.PosX, current.PosY);
        if (current == um)
        {
            if (currentPos != newPos)
            {
                GetTile((int)newPos.x, (int)newPos.y).Unit = current.gameObject;
                GetTile((int)currentPos.x, (int)currentPos.y).Unit = null;
                Debug.Log("unset tile for " + current.name);
                //next unit's turn;
                NextTurn();
            }
        }
    }

    private void SetupMap()
    {
        tiles = new TileData[sizeX, sizeY];
        rooms = new List<Room>();
        turnOrder = new List<GameObject>();
        GenerateMap();
        DisplayMap();
        PlacePlayer();
        PlaceUnits();
        current = turnOrder[currentTurn].GetComponent<UnitMovement>();
    }

    private void SetupNextFloor(GameObject player)
    {
        foreach(GameObject g in turnOrder)
        {
            if(g != player)
            {
                Destroy(g);
            }
        }
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                Destroy(tiles[i, j].gameObject);
            }
        }
        tiles = new TileData[sizeX, sizeY];
        rooms = new List<Room>();
        turnOrder = new List<GameObject>();
        GenerateMap();
        DisplayMap();
        MovePlayer(player);
        PlaceUnits();
        current = turnOrder[currentTurn].GetComponent<UnitMovement>();
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

        PlaceStairs();
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

    private void PlaceStairs()
    {
        Room room = rooms[Random.Range(0, rooms.Count)];
        int x = Random.Range(room.Left + 1, room.Right - 1);
        int y = Random.Range(room.Bottom - 1, room.Top + 1);
        tilesType[x, y] = 2;
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
                data.SetX(x);
                data.SetY(y);
                data.SetMap(this);

                tiles[x, y] = data;
            }
        }
    }

    private void PlacePlayer()
    {
        Room room = rooms[Random.Range(0, rooms.Count)];
        int x = Random.Range(room.Left + 1, room.Right - 1);
        int y = Random.Range(room.Bottom - 1, room.Top + 1);

        GameObject g = (GameObject)Instantiate(player, tiles[x, y].transform.position, Quaternion.identity);
        UnitMovement um = g.GetComponent<UnitMovement>();
        um.PosX = x;
        um.PosY = y;
        um.SetMap(this);
        turnOrder.Add(g);
    }

    private void MovePlayer(GameObject g)
    {
        Room room = rooms[Random.Range(0, rooms.Count)];
        int x = Random.Range(room.Left + 1, room.Right - 1);
        int y = Random.Range(room.Bottom - 1, room.Top + 1);

        UnitMovement um = g.GetComponent<UnitMovement>();
        um.PosX = x;
        um.PosY = y;
        um.transform.position = tiles[x, y].transform.position;
        turnOrder.Add(g);
    }


    private void PlaceUnits()
    {
        for(int i = 0; i < startEnemies; i++)
        {
            Room room = rooms[Random.Range(0, rooms.Count)];
            int x = Random.Range(room.Left + 1, room.Right - 1);
            int y = Random.Range(room.Bottom - 1, room.Top + 1);

            GameObject g = (GameObject)Instantiate(enemy, tiles[x, y].transform.position, Quaternion.identity);
            UnitMovement um = g.GetComponent<UnitMovement>();
            um.PosX = x;
            um.PosY = y;
            um.SetMap(this);
            turnOrder.Add(g);
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

    public void NextTurn()
    {
        currentTurn++;
        if (currentTurn >= turnOrder.Count)
        {
            currentTurn = 0;
        }
        current = turnOrder[currentTurn].GetComponent<UnitMovement>();
    }

    public void RemoveFromTurnOrder(UnitMovement um)
    {
        tiles[um.PosX, um.PosY].Unit = null;
        turnOrder.Remove(um.gameObject);
    }
    
    public void NextFloor(GameObject player)
    {
        SetupNextFloor(player);
        currentFloor++;
    }
}
