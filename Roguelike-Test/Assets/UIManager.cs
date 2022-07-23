using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool gameplay = true;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject messageUI;
    [SerializeField] private GameObject cursor;

    public bool Menu()
    {
        if(gameplay)
        {
            //show menu ui
            menuUI.SetActive(true);
            gameplayUI.SetActive(false);
            //change input map
            gameplay = !gameplay;
        }
        else
        {
            //show gameplay ui
            menuUI.SetActive(false);
            gameplayUI.SetActive(true);
            gameplay = !gameplay;
        }
        return gameplay;
    }

    public void MoveCursor()
    {

    }

    public void Items()
    {

    }

    public void Skills()
    {

    }

    public void Ground()
    {

    }

    public void Status()
    {

    }

    public void Journal()
    {

    }

    public void Options()
    {

    }
}
