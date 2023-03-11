using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreHornets : Penalty
{
    public MoreHornets()
    {
        penaltyName = "More Hornet Minions";
        description = "Increases the number of Hornets that spawn by 10%";
    }

    public override void ActivatePenalty()
    {
        GameObject.Find("HornetSpawner(Clone)").GetComponent<SpiderSpawner>().spawnInterval *= .9f;
        GameObject.Find("PenaltyHolder").GetComponent<MoreHornets>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
