using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Slider health;
    [SerializeField] private Slider mana;
    [SerializeField] private Image protrait;
    [SerializeField] private Text hp;
    [SerializeField] private Text mp;

    public void UpdateDisplay()
    {
        PlayerStats ps = GameController.Instance.GetCurrent().GetComponent<PlayerStats>();
        if (ps != null)
        {
            health.value = ps.HealthPercent();
            mana.value = ps.ManaPercent();
            hp.text = ps.HealthNumbers();
            mp.text = ps.ManaNumbers();
        }
    }
}
