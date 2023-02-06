using UnityEngine;

public class Stairs : TileData
{
    public void Check()
    {
        if(unit && unit.tag == "Player")
        {
            GameController.Instance.Ground();
        }
    }
}