public class EnemyStats : UnitStats
{
    private Item item;

    protected override void Start()
    {
        base.Start();
    }

    public bool HasItem()
    {
        return item is null ? true : false;
    }

    public void SetItem(Item i)
    {
        item = i;
    }

    public void DropItem()
    {

    }
}