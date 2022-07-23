using System;

[Serializable]
public class Resistance
{
    private int iceRes;
    private int fireRes;
    private int windRes;
    private int earthRes;
    private int waterRes;
    private int litRes;
    private int lightRes;
    private int darkRes;

    private int pierceRes;
    private int slashRes;
    private int bluntRes;

    public Resistance()
    {
        iceRes = fireRes = windRes = earthRes = waterRes = litRes = lightRes = darkRes = 0;
    }
}