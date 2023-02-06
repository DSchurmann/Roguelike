using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private bool gameplay = true;
    private MenusInputManager mim;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject groundMenu;
    [SerializeField] private GameObject messageUI;
    [SerializeField] private GameObject statusUI;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject menuDisplay;
    [SerializeField] private Text gold;
    [SerializeField] private Text floor;

    [SerializeField] private GameplayUI gui;


    private UIState current;
    [SerializeField] private ItemUI iUI;
    [SerializeField] private GroundUI gndUI;
    [SerializeField] private StatusUI sUI;

    private void Update()
    {
        if (mim.Decline)
        {
            mim.SetDeclineFalse();
            if (current == null)
            {
                GameController.Instance.MenuInput();
            }
            else if (current.InSub)
            {
                current.HideSubMenu();
            }
            else
            {
                if(gndUI.MenuActive)
                {
                    InGameMenu();
                    current.HideSubMenu();
                }
                current.GetComponent<Button>().Select();
                current.HideDisplay();
                current = null;
            }
        }
    }

    public void UpdateGameplayUI()
    {
        gui.UpdateDisplay();
    }

    public bool Menu()
    {
        if (gameplay)
        {
            //show menu ui
            InGameMenu();
            //change input map
            gameplay = !gameplay;
        }
        else
        {
            //show gameplay ui
            Gameplay();
            gameplay = !gameplay;
        }
        return gameplay;
    }

    private void Gameplay()
    {
        menuUI.SetActive(false);
        gameplayUI.SetActive(true);
        cursor.SetActive(false);
    }

    private void InGameMenu()
    {
        cursor.SetActive(true);
        menuUI.SetActive(true);
        gameplayUI.SetActive(false);
        //menuDisplay.SetActive(false);
        gold.text = "Gold: " + GameController.Instance.GetCurrent().GetComponent<Inventory>().Gold;
        floor.text = "Floor " + GameController.Instance.CurrentFloor;
    }

    public void Items()
    {
        List<InvItem> inv = GameController.Instance.GetCurrent().GetComponent<Inventory>().GetInventory;
        if (inv.Count > 0)
        {
            current = iUI;
            current.Display(inv);
        }
    }

    public void Skills()
    {

    }

    public void Ground()
    {
        //need to make ground state
        // state has 2 menus, 1 for stairs, 1 for items
        //  stairs menu must also be accessable for stairs when player steps on them (without using menu->ground)
        current = gndUI;
        Position pos = GameController.Instance.GetCurrent().Position;
        current.Display(GameController.Instance.GetTile(pos));
        if(gndUI.MenuActive)
        {
            Gameplay();
            cursor.SetActive(true);
        }
    }

    public void Status()
    {
        current = sUI;
        current.Display();
    }

    public void Journal()
    {

    }

    public void Options()
    {

    }

    public void RemoveCurrentState() => current = null;

    public void SetMIM(MenusInputManager m) => mim = m;
}
