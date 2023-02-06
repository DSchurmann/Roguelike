using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InvItem> inv;
    private int invSize = 10;
    private int gold = 0;
    private Equipment equipment;

    public struct Equipment
    {
        private InvWeapon weapon;
        private InvHead head;
        private InvArmour armour;
        private InvAccessory accessory;

        public InvWeapon Weapon
        {
            get => weapon;
            set => weapon = value;
        }
        public InvHead Head
        {
            get => head;
            set => head = value;
        }
        public InvArmour Armour
        {
            get => armour;
            set => armour = value;
        }
        public InvAccessory Accessory
        {
            get => accessory;
            set => accessory = value;
        }
    }

    private void Start()
    {
        inv = new List<InvItem>();
    }

    public void AddItem(ItemObject i)
    {

        if (i.GetItem is Gold)
        {
            Gold g = (Gold)i.GetItem;
            g.GenerateValue();
            gold += g.Amount;
        }
        else
        {
            InvItem item = i.GetItem.ConvertToInvItem();
            inv.Add(item);
        }
        Destroy(i.gameObject);
    }

    public void Equip(InvItem i)
    {
        if (i is InvWeapon temp)
        {
            if (equipment.Weapon == temp && temp.Equipped())
            {
                print("selected item is equiped");
                equipment.Weapon = null;
                temp.Equip();
            }
            else if (equipment.Weapon != null)
            {
                print("Index is: " + inv.IndexOf(equipment.Weapon));
                InvWeapon w = (InvWeapon)inv[inv.IndexOf(equipment.Weapon)];
                if (w.Equipped())
                {
                    w.Equip();
                }
                w = (InvWeapon)inv[inv.IndexOf(temp)];
                equipment.Weapon = w;
                w.Equip();
            }
            else
            {
                print("nothing should be equiped");
                InvWeapon weapon = temp;
                equipment.Weapon = weapon;
                weapon.Equip();
            }
        }
        //else if (i is Head)
        //{
        //    if (equipment.Head != null)
        //    {
        //        Head h = (Head)inv[inv.IndexOf(i)];
        //        if (h.Equipped())
        //        {
        //            h.Equip();
        //        }
        //    }
        //    Head head = (Head)i;
        //    equipment.Head = head;
        //    head.Equip();
        //}
        //else if (i is Armour)
        //{
        //    if (equipment.Armour != null)
        //    {
        //        Armour a = (Armour)inv[inv.IndexOf(i)];
        //        if (a.Equipped())
        //        {
        //            a.Equip();
        //        }
        //    }
        //    Armour armour = (Armour)i;
        //    equipment.Armour = armour;
        //    armour.Equip();
        //}
        //else if (i is Accessory)
        //{
        //    if (equipment.Accessory != null)
        //    {
        //        Accessory a = (Accessory)inv[inv.IndexOf(i)];
        //        if (a.Equipped())
        //        {
        //            a.Equip();
        //        }
        //    }
        //    Accessory acc = (Accessory)i;
        //    equipment.Accessory = acc;
        //    acc.Equip();
        //}
    }

    public void AddGold(int i) => gold += i;

    public void RemoveItem(InvItem i) => inv.Remove(i);

    public List<InvItem> GetInventory => inv;

    public bool IsFull() => inv.Count == invSize;

    public int Gold => gold;

    public Equipment Equips => equipment;
}