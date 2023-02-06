using UnityEngine;

public class DisplayItem : Display
{
    [SerializeField] private InvItem item;
    public InvItem Item
    {
        get => item;
        set => item = value;
    }
}