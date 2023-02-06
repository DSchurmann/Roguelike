using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : UIState
{
    private List<GameObject> items = new List<GameObject>();
    [SerializeField] private GameObject itemDisplay;
    [SerializeField] private GameObject equipDisplay;

    public override void Display(List<InvItem> inv)
    {
        //display a button for each item
        Vector3 newPos = new Vector3(-795, 240, 0);
        display.SetActive(true);
        for(int i = 0; i < inv.Count; i++)
        {
            GameObject g;
            Equipable e = null;
            if (inv[i] is Equipable)
            {
                g = Instantiate(equipDisplay);
                e = inv[i] as Equipable;
                if(e.Equipped())
                {
                    Button b = g.GetComponent<DisplayItem>().Options[0];
                    b.GetComponentInChildren<Text>().text = "Unequip";
                }
            }
            else
            {
                g = Instantiate(itemDisplay);
                
            }
            g.SetActive(true);
            g.transform.SetParent(display.transform);
            g.GetComponent<RectTransform>().localPosition = newPos;
            g.GetComponent<DisplayItem>().Previous(this);
            g.GetComponent<DisplayItem>().Item = inv[i];
            if (e.Equipped())
            {
                g.GetComponentInChildren<Text>().text = inv[i].Name + "  <b>E</b>";
            }
            else
            {
                g.GetComponentInChildren<Text>().text = inv[i].Name;
            }


            items.Add(g);
            newPos.y -= 140;
        }
        //link buttons to loop when going up and down and not be linked to main in game menu buttons
        if (items.Count > 1)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Button b = items[i].GetComponentInChildren<Button>();
                Navigation nav = b.navigation;
                nav.mode = Navigation.Mode.Explicit;
                if (i == 0)
                {
                    nav.selectOnUp = items[items.Count - 1].GetComponentInChildren<Button>();
                    nav.selectOnDown = items[i + 1].GetComponentInChildren<Button>();
                }
                else if (i == items.Count - 1)
                {
                    nav.selectOnDown = items[0].GetComponentInChildren<Button>();
                    nav.selectOnUp = items[i - 1].GetComponentInChildren<Button>();
                }
                else
                {
                    nav.selectOnDown = items[i + 1].GetComponentInChildren<Button>();
                    nav.selectOnUp = items[i - 1].GetComponentInChildren<Button>();
                }
                b.navigation = nav;
            }
        }
        items[0].GetComponentInChildren<Button>().Select();
    }

    public void Equip()
    {
        DisplayItem s = sub as DisplayItem;
        Inventory inv = GameController.Instance.GetCurrent().GetComponent<Inventory>();
        //Item i = inv.GetInventory[inv.GetInventory.IndexOf(s.Item)];
        inv.Equip(s.Item);
        ReturnToGameplay();
        GameController.Instance.NextTurn();
    }

    public void DropItem()
    {
        DisplayItem s = sub as DisplayItem;
        GameObject g = Instantiate(s.Item.Prefab);
        UnitMovement um = GameController.Instance.GetCurrent().GetComponent<UnitMovement>();
        Inventory inv = GameController.Instance.GetCurrent().GetComponent<Inventory>();
        TileData t = GameController.Instance.GetTile(um.PosX, um.PosY);
        t.Item = g;
        g.transform.position = t.transform.position;
        inv.RemoveItem(s.Item);
        items.Remove(s.Item.Prefab);
        ReturnToGameplay();
        GameController.Instance.NextTurn();
    }

    public override void HideDisplay()
    {
        base.HideDisplay();
        for(int i = 0; i < items.Count; i++)
        {
            Destroy(items[i]);
        }
        items = new List<GameObject>();
    }

    public override void ActiveSub(Display di)
    {
        sub = di;
    }

    public override void HideSubMenu()
    {
        if(sub != null)
        {
            sub.HideMenu();
            sub = null;
        }
    }
}