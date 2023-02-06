using UnityEditor;

public class Stats
{
    private Inventory inv;
    private int hp;
    private int mp;
    protected int def;
    protected int atk;
    protected Resistance res;

    public Stats(Inventory i, int h, int m, int d, int a, Resistance r)
    {
        inv = i;
        hp = h;
        mp = m;
        def = d;
        atk = a;
        res = r;
    }

    public Inventory Inventory => inv;
    public int HP => hp;
    public int MP => mp;
    public int Defence => def;
    public int Attack => atk;
    public Resistance Resistances => res;
}