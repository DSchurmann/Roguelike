using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class TileMap : MonoBehaviour
{
    public struct Room
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
            if (Right < other.Left + 1)
            {
                return false;
            }
            if(Bottom < other.Top + 1)
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

    public struct Floor
    {
        private int[,] tilesTypes;
        private int x;
        private int y;

        public Floor(int[,] tt, int sX, int sY)
        {
            tilesTypes = tt;
            x = sX;
            y = sY;
        }

        public int[,] Tiles => tilesTypes;
        public int X => x;
        public int Y => y;
    }

    public GameObject player;
    public GameObject enemy;
    private GameObject floor;
    private int startEnemies = 5;
    private int currentFloor = 1;
    private List<Floor> floors = new List<Floor>();

    [SerializeField] private List<GameObject> items;
    private int itemsToPlace = 10;

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

    public void SetupMap()
    {
        floor = Instantiate(new GameObject());
        floors = new List<Floor>();
        tiles = new TileData[sizeX, sizeY];
        rooms = new List<Room>();
        GenerateMap();
        DisplayMap();
        PlaceItems();
    }

    private void SetupNextFloor()
    {
        DestroyFloor();
        tiles = new TileData[sizeX, sizeY];
        rooms = new List<Room>();
        floors.Add(new Floor(tilesType, sizeX, sizeY));
        currentFloor++;
        GenerateMap();
        DisplayMap();
        PlaceItems();
    }

    private void PreviousFloor()
    {
        DestroyFloor();
        currentFloor--;
        tilesType = floors[currentFloor].Tiles;
        tiles = new TileData[floors[currentFloor].X, floors[currentFloor].Y];
        DisplayMap();
        PlaceItems();
    }

    private void DestroyFloor()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                Destroy(tiles[i, j].gameObject);
            }
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

        //zig-zag if target is beyond a distance
        int xDist = Mathf.Abs(x1 - x2);
        while(xDist > 5)
        {
            tilesType[x1, y1] = 0;
            x1 += x1 < x2 ? 1 : -1;
            xDist--;
            if (tilesType[x1, y1] == 0 && xDist == 5 && !TileWithinRoom(x1, y1))
            {
                break;
            }
        }
        int yDist = Mathf.Abs(y1 - y2);
        while(yDist > 5)
        {
            tilesType[x1, y1] = 0;
            y1 += y1 < y2 ? 1 : -1;
            yDist--;
            if(tilesType[x1, y1] == 0 && !TileWithinRoom(x1, y1))
            {
                break;
            }
        }

        //point to point corridor
        while(x1 != x2)
        {
            tilesType[x1, y1] = 0;
            x1 += x1 < x2 ? 1 : -1;
            if(tilesType[x1, y1] == 0 && !TileWithinRoom(x1, y1))
            {
                break;
            }
        }
        while(y1 != y2)
        {
            tilesType[x1, y1] = 0;
            y1 += y1 < y2 ? 1 : -1;
            if(tilesType[x1, y1] == 0 && !TileWithinRoom(x1, y1))
            {
                break;
            }
        }

        r1.IsConnected = true;
        //r2.IsConnected = true;
    }

    public bool TileWithinRoom(int x, int y)
    {
        for(int i = 0; i < rooms.Count; i++)
        {
            if((x < rooms[i].Left && x > rooms[i].Right) && (y < rooms[i].Bottom && y > rooms[i].Top))
            {
                return true;
            }
        }

        return false;
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
        floor.name = "floor " + currentFloor;
        for( int x = 0; x < sizeX; x++)
        {
            for(int y = 0; y < sizeY; y++)
            {
                Tile tile = tileTypes[tilesType[x, y]];
                GameObject g = Instantiate(tile.tilePrefab, new Vector3(x, 0, y), Quaternion.identity);

                TileData data = g.GetComponent<TileData>();
                data.SetX(x);
                data.SetY(y);
                data.SetMap(this);

                tiles[x, y] = data;
                g.transform.parent = floor.transform;
            }
        }
    }

    public List<GameObject> PlacePlayer(PlayerInputManager pim)
    {
        List<GameObject> players = new List<GameObject>();
        Room room = rooms[Random.Range(0, rooms.Count)];
        int x = Random.Range(room.Left + 1, room.Right - 1);
        int y = Random.Range(room.Bottom - 1, room.Top + 1);

        do
        {
            room = rooms[Random.Range(0, rooms.Count)];
            x = Random.Range(room.Left + 1, room.Right - 1);
            y = Random.Range(room.Bottom - 1, room.Top + 1);
        } while (tiles[x, y].Unit != null);

        GameObject g = Instantiate(player, tiles[x, y].transform.position, Quaternion.identity);
        PlayerInputs um = g.GetComponent<PlayerInputs>();
        um.PosX = x;
        um.PosY = y;
        um.SetMap(this);
        um.SetInput(pim);

        GameObject item = Instantiate(items[1], tiles[x, y].transform.position, Quaternion.identity);
        tiles[x, y].Item = item;


        players.Add(g);
        tiles[x, y].Unit = g;
        return players;
    }

    public void MovePlayer(List<GameObject> g)
    {
        Room room = rooms[Random.Range(0, rooms.Count)];
        int x = Random.Range(room.Left + 1, room.Right - 1);
        int y = Random.Range(room.Bottom - 1, room.Top + 1);

        for (int i = 0; i < g.Count; i++)
        {
            do
            {
                x = Random.Range(room.Left + 1, room.Right - 1);
                y = Random.Range(room.Bottom - 1, room.Top + 1);
            } while (tiles[x, y].Unit != null);

            UnitMovement um = g[i].GetComponent<UnitMovement>();
            um.PosX = x;
            um.PosY = y;
            um.transform.position = tiles[x, y].transform.position;
            tiles[x, y].Unit = um.gameObject;
        }
    }

    public List<GameObject> PlaceUnits()
    {
        List<GameObject> result = new List<GameObject>();

        for(int i = 0; i < startEnemies; i++)
        {
            Room room = rooms[Random.Range(0, rooms.Count)];
            int x = Random.Range(room.Left + 1, room.Right - 1);
            int y = Random.Range(room.Bottom - 1, room.Top + 1);
            do
            {
                x = Random.Range(room.Left + 1, room.Right - 1);
                y = Random.Range(room.Bottom - 1, room.Top + 1);
            } while (tiles[x, y].Unit != null);

            GameObject g = Instantiate(enemy, tiles[x, y].transform.position, Quaternion.identity);
            UnitMovement um = g.GetComponent<UnitMovement>();
            um.PosX = x;
            um.PosY = y;
            um.SetMap(this);
            result.Add(g);
            tiles[x, y].Unit = g;
        }

        return result;
    }

    private void PlaceItems()
    {
        for(int i = 0; i < itemsToPlace; i++)
        {
            bool placed = false;
            do
            {
                Room room = rooms[Random.Range(0, rooms.Count)];
                int x = Random.Range(room.Left + 1, room.Right - 1);
                int y = Random.Range(room.Bottom - 1, room.Top + 1);

                if (tiles[x, y].Item == null && tiles[x,y].CanHaveItem)
                {
                    GameObject item = Instantiate(items[0], tiles[x, y].transform.position, Quaternion.identity);
                    tiles[x, y].Item = item;
                    placed = !placed;
                }
            } while (!placed);
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

    public TileData[,] Tiles
    {
        get { return tiles; }
    }

    public void RemoveActor(UnitMovement um)
    {
        tiles[um.PosX, um.PosY].Unit = null;
    }
    
    public void NextFloor()
    {
        SetupNextFloor();
        currentFloor++;
    }

    public int CurrentFloor => currentFloor;
}
