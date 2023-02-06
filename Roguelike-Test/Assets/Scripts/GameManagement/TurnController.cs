using UnityEngine;
using System.Collections.Generic;

public class TurnController
{
    private List<GameObject> actors;
    private List<GameObject> players;
    private List<GameObject> turnOrder;
    private int current = 0;

    public List<GameObject> Players
    {
        get { return players; }
        set { players = value; }
    }


    public void SetActors(List<GameObject> newActors)
    {
        actors = newActors;
    }

    public void CreateTurnOrder()
    {
        turnOrder = new List<GameObject>();
        turnOrder.AddRange(players);
        turnOrder.AddRange(actors);
        current = 0;
    }

    public UnitMovement GetCurrent()
    {
        return turnOrder[current].GetComponent<UnitMovement>();
    }

    public void NextTurn()
    {
        current++;
        if(current > turnOrder.Count - 1)
        {
            current = 0;
        }
    }

    public void NextFloor()
    {
        for(int i = 0; i < turnOrder.Count; i++)
        {
            if(!players.Contains(turnOrder[i]))
            {
                Object.Destroy(turnOrder[i]);
            }
        }
    }

    public void RemoveActor(GameObject a)
    {
        actors.Remove(a);
        turnOrder.Remove(a);
    }

    public void GivePlayersExp(int exp)
    {
        for(int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerStats>().AddExp(exp);
        }
    }
}
