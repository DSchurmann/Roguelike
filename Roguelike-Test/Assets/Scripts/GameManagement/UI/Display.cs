using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject menu;
    [SerializeField] private List<Button> options;
    private UIState previous;

    public void DisplayMenu()
    {
        menu.SetActive(true);
        options[0].Select();
        previous.InSub = true;
        previous.ActiveSub(this);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
        previous.InSub = false;
        button.Select();
    }

    public void Previous(UIState g) => previous = g;
    public List<Button> Options => options;
}