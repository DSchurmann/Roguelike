using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Gold", order = 2)]
public class Gold : Item
{
    private int amount;

    public int Amount => amount;

    public void GenerateValue()
    {
        amount = Random.Range(0, 100);
    }
}