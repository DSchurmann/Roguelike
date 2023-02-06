using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvItem
{
    private string name;
    private GameObject prefab;

    public InvItem(string n, GameObject p)
    {
        name = n;
        prefab = p;
    }

    public string Name => name;
    public GameObject Prefab => prefab;
}
