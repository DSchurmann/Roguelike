using UnityEngine;
using UnityEngine.UI;

public class StatusUI : UIState
{
    private PlayerStats stats;

    [SerializeField] private GameObject messageBox;
    [SerializeField] private GameObject menuOptions;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject gold;
    [SerializeField] private GameObject floor;

    [SerializeField] private Image protrait;
    [SerializeField] private Text level;
    [SerializeField] private Text status;
    [SerializeField] private Text exp;
    [SerializeField] private Text hp;
    [SerializeField] private Text mp;
    [SerializeField] private Text def;
    [SerializeField] private Text attack;
    [SerializeField] private Text iceRes;
    [SerializeField] private Text fireRes;
    [SerializeField] private Text litRes;
    [SerializeField] private Text earthRes;
    [SerializeField] private Text lightRes;
    [SerializeField] private Text darkRes;
    [SerializeField] private Text waterRes;
    [SerializeField] private Text windRes;

    [SerializeField] private Text weapon;
    [SerializeField] private Text head;
    [SerializeField] private Text armour;
    [SerializeField] private Text accessory;

    public override void Display()
    {
        display.SetActive(true);
        display.transform.Find("Status").gameObject.SetActive(true);
        menuOptions.SetActive(false);
        cursor.SetActive(false);
        messageBox.SetActive(false);
        gold.SetActive(false);
        floor.SetActive(false);


        stats = GameController.Instance.GetCurrent().GetComponent<PlayerStats>();
        level.text = stats.Level();
        switch(stats.StatusState)
        {
            case Status.normal:
                status.text = "Healthy";
                break;
            case Status.poison:
                status.text = "Poisoned";
                break;
            case Status.stun:
                status.text = "Stunned";
                break;
            case Status.sleep:
                status.text = "Asleep";
                break;
        }
        exp.text = stats.Exp();
        hp.text = stats.HealthNumbers();
        mp.text = stats.ManaNumbers();
        def.text = stats.Defence.ToString();
        attack.text = stats.Attack.ToString();
        iceRes.text = stats.Resistances.Ice.ToString();
        fireRes.text = stats.Resistances.Fire.ToString();
        litRes.text = stats.Resistances.Lit.ToString();
        earthRes.text = stats.Resistances.Earth.ToString();
        lightRes.text = stats.Resistances.Light.ToString();
        darkRes.text = stats.Resistances.Dark.ToString();
        waterRes.text = stats.Resistances.Water.ToString();
        windRes.text = stats.Resistances.Wind.ToString();

        if (stats.GetInventory.Equips.Weapon != null)
            weapon.text = stats.GetInventory.Equips.Weapon.Name;
        else
            weapon.text = "[None]";
        if (stats.GetInventory.Equips.Head != null)
            head.text = stats.GetInventory.Equips.Head.Name;
        else
            head.text = "[None]";
        if (stats.GetInventory.Equips.Armour != null)
            armour.text = stats.GetInventory.Equips.Armour.Name;
        else
            armour.text = "[None]";
        if (stats.GetInventory.Equips.Accessory != null)
            accessory.text = stats.GetInventory.Equips.Accessory.Name;
        else
            accessory.text = "[None]";
    }

    public override void HideDisplay()
    {
        base.HideDisplay();
        display.transform.Find("Status").gameObject.SetActive(false);
        menuOptions.SetActive(true);
        cursor.SetActive(true);
        messageBox.SetActive(true);
        gold.SetActive(true);
        floor.SetActive(true);
    }
}