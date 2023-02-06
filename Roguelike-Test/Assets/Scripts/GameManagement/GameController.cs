using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField] private PlayerInput input;
    private TileMap map;
    private MovementHandler mh;
    private TurnController tc;
    private PlayerInputManager pim;
    private CameraController cam;
    private UIManager uim;

    //need game states - maybe

    private void Awake()
    {
        //ensure there is only 1 game controller
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        //get references for all controllers
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<TileMap>();
        mh = new MovementHandler(map);
        tc = new TurnController();
        pim = GetComponent<PlayerInputManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        uim = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        uim.SetMIM(GetComponent<MenusInputManager>());

        //for now (without game states) - just load map
        SetupMap();
        cam.SetTarget(tc.GetCurrent().gameObject);
        uim.UpdateGameplayUI();

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        //Get user input or make AI activate
        mh.Update(tc.GetCurrent());
        
        //below is testing only
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            NextFloor();
        }
        if(Keyboard.current.pKey.wasPressedThisFrame)
        {
            tc.GetCurrent().GetComponent<PlayerStats>().TakeDamage(1);
        }
        if(Keyboard.current.oKey.wasPressedThisFrame)
        {
            tc.GetCurrent().GetComponent<PlayerStats>().ReduceMana(1);
        }
    }

    private void SetupMap()
    {
        //create map tiles, actors and items
        map.SetupMap();
        tc.Players = map.PlacePlayer(pim);
        tc.SetActors(map.PlaceUnits());
        tc.CreateTurnOrder();
    }

    public void NextFloor()
    {
        //setup the next floor
        map.NextFloor();
        tc.NextFloor();
        tc.SetActors(map.PlaceUnits());
        tc.CreateTurnOrder();
        map.MovePlayer(tc.Players);
    }

    public void NextTurn()
    {
        //move to next actor's turn
        tc.NextTurn();
        if(tc.GetCurrent().GetComponent<PlayerInputs>())
        {
            cam.SetTarget(tc.GetCurrent().gameObject);
        }
        tc.GetCurrent().GetComponent<UnitStats>().ResetPoison();
        uim.UpdateGameplayUI();
    }

    public void RemoveActor(UnitMovement um)
    {
        //remove actor from the map and turn order
        map.RemoveActor(um);
        tc.RemoveActor(um.gameObject);
    }

    public void MenuInput()
    {
        //change menu displayed and control scheme, gameplay or ingame menu
        if(uim.Menu())
        {
            input.SwitchCurrentActionMap("Gameplay");
            uim.RemoveCurrentState();
        }
        else
        {
            input.SwitchCurrentActionMap("Menus");
        }
    }

    public UnitMovement GetCurrent() => tc.GetCurrent();

    public TileData GetTile(Position pos) => map.GetTile(pos.x, pos.y);
    public TileData GetTile(int x, int y) => map.GetTile(x, y);

    public bool PickUpItem() => mh.PickUpItem(tc.GetCurrent());
    public void CheckTile() => mh.CheckTile(tc.GetCurrent());
    public void Ground()
    {
        MenuInput();
        uim.Ground();
    }

    public int CurrentFloor => map.CurrentFloor;
}
