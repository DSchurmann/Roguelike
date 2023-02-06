using System;
using UnityEngine;

[Serializable]
public class Resistance
{
    [SerializeField] private int iceRes;
    [SerializeField] private int fireRes;
    [SerializeField] private int windRes;
    [SerializeField] private int earthRes;
    [SerializeField] private int waterRes;
    [SerializeField] private int litRes;
    [SerializeField] private int lightRes;
    [SerializeField] private int darkRes;

    public Resistance()
    {
        iceRes = fireRes = windRes = earthRes = waterRes = litRes = lightRes = darkRes = 0;
    }

    public Resistance(int iceRes, int fireRes, int windRes, int earthRes, int waterRes, int litRes, int lightRes, int darkRes)
    {
        this.iceRes = iceRes;
        this.fireRes = fireRes;
        this.windRes = windRes;
        this.earthRes = earthRes;
        this.waterRes = waterRes;
        this.litRes = litRes;
        this.lightRes = lightRes;
        this.darkRes = darkRes;
    }

    public int GetResistance(Element e) => e switch
    {
        Element.Ice => iceRes,
        Element.Fire => fireRes,
        Element.Wind => windRes,
        Element.Earth => earthRes,
        Element.Water => waterRes,
        Element.Lit => litRes,
        Element.Light => lightRes,
        Element.Dark => darkRes,
        Element.None => 0,
        _ => 0
    };

    public int Ice => iceRes;
    public int Fire => fireRes;
    public int Lit => litRes;
    public int Water => waterRes;
    public int Earth => earthRes;
    public int Light => lightRes;
    public int Dark => darkRes;
    public int Wind => windRes;
}