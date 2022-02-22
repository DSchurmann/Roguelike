using System;

public class Stairs : TileData
{
    private void Update()
    {
        if(unit && unit.tag == "Player")
        {
            map.NextFloor(unit);
            unit = null;
        }
    }
}