using UnityEngine;
using UnityEngine.UI;

public class GroundUI : UIState
{
    [SerializeField] private GameObject stairMenu;
    [SerializeField] private GameObject itemMenu;
    private bool menuActive = false;

    public override void Display(TileData td)
    {
        if (td is Stairs)
        {
            stairMenu.SetActive(true);
            stairMenu.GetComponentInChildren<Button>().Select();
            menuActive = true;
        }
        else if (td.Item != null)
        {
            itemMenu.SetActive(true);
            Button b = itemMenu.GetComponentInChildren<Button>();
            itemMenu.GetComponentInChildren<Button>().Select();
            menuActive = true;
        }
        else
        {
            menuActive = false;
            //error noise or something?
        }
    }

    public override void HideSubMenu()
    {
        stairMenu.SetActive(false);
        itemMenu.SetActive(false);
        menuActive = false;
    }

    public void NextFloor()
    {
        GameController.Instance.NextFloor();
        ReturnToGameplay();
    }

    public void PickUp()
    {
        if(GameController.Instance.PickUpItem())
        {
            ReturnToGameplay();
            GameController.Instance.NextTurn();
        }
    }

    public void Use()
    {

    }

    public bool MenuActive => menuActive;

}