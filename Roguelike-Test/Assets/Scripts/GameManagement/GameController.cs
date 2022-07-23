using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    //need game states

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        map = GameObject.FindGameObjectWithTag("Map").GetComponent<TileMap>();
        mh = new MovementHandler(map);
        tc = new TurnController();
        pim = GetComponent<PlayerInputManager>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        uim = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

        //for now (without game states) - just load map
        SetupMap();
        cam.SetTarget(tc.GetCurrent().gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            NextFloor();
        }
        mh.Update(tc.GetCurrent());
    }

    private void SetupMap()
    {
        map.SetupMap();
        tc.Players = map.PlacePlayer(pim);
        tc.SetActors(map.PlaceUnits());
        tc.CreateTurnOrder();
    }

    public void NextFloor()
    {
        map.NextFloor();
        tc.SetActors(map.PlaceUnits());
        tc.SetActors(map.PlaceUnits());
        tc.CreateTurnOrder();
        map.MovePlayer(tc.Players);
    }

    public void NextTurn()
    {
        tc.NextTurn();
        if(tc.GetCurrent().GetComponent<PlayerInputs>())
        {
            cam.SetTarget(tc.GetCurrent().gameObject);
        }
        tc.GetCurrent().GetComponent<UnitStats>().ResetPoison();
    }

    public void RemoveActor(UnitMovement um)
    {
        map.RemoveActor(um);
        tc.RemoveActor(um.gameObject);
    }

    public void MenuInput()
    {
        Debug.LogError(input.currentActionMap);
        if(uim.Menu())
        {
            input.SwitchCurrentActionMap("Gameplay");
        }
        else
        {
            input.SwitchCurrentActionMap("Menus");
        }
    }
}
