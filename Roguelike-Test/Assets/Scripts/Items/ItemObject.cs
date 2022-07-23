using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Item item;

    public Item GetItem
    {
        get { return item; }
    }
}