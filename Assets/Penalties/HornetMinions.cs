using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornetMinions : Penalty
{
    public GameObject spawner;

    public HornetMinions()
    {
        penaltyName = "Hornet Minions";
        description = "Spawns hornet minions. Hornets travel from one side of the map to other in a wave pattern.";
    }

    public override void ActivatePenalty()
    {
        Instantiate(spawner);
        GameObject.Find("PenaltyHolder").GetComponent<HornetMinions>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
