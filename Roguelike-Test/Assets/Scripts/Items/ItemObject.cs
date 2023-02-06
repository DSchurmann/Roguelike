using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private Item item;

    public Item GetItem => item;
}