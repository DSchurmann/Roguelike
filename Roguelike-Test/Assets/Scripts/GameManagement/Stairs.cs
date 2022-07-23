using System;

public class Stairs : TileData
{
    private void Update()
    {
        if(unit && unit.tag == "Player")
        {
            GameController.Instance.NextFloor();
            unit = null;
        }
    }
}