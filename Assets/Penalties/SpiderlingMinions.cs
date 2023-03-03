using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderlingMinions : Penalty
{
    public GameObject spawner;

    public SpiderlingMinions()
    {
        penaltyName = "Spiderlings Minions";
        description = "Spawns spiderling minions. Spiderlings constantly move towards the player.";
    }

    public override void ActivatePenalty()
    {
        Instantiate(spawner);
        GameObject.Find("PenaltyHolder").GetComponent<SpiderlingMinions>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
