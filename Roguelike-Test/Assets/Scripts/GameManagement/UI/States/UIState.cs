using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIState : MonoBehaviour
{
    [SerializeField] protected GameObject display;
    protected Display sub;
    protected bool inSubMenu = false;
    public virtual void Display() { }
    public virtual void Display(List<InvItem> inv) { }
    public virtual void Display(TileData td) { }

    public virtual void HideDisplay()
    {
        if (display)
        {
            display.SetActive(false);
        }
    }
    
    public void ReturnToGameplay()
    {
        inSubMenu = false;
        HideSubMenu();
        HideDisplay();
        GetComponent<Button>().Select();
        GameController.Instance.MenuInput();
    }

    public bool InSub
    {
        get { return inSubMenu; }
        set { inSubMenu = value; }
    }

    public virtual void ActiveSub(Display di) { }

    public virtual void HideSubMenu() { }
}