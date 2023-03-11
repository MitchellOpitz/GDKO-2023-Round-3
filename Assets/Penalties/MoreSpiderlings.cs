using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreSpiderlings : Penalty
{
    public MoreSpiderlings()
    {
        penaltyName = "More Spiderling Minions";
        description = "Increases the number of Spiderlings that spawn by 10%";
    }

    public override void ActivatePenalty()
    {
        GameObject.Find("SpiderSpawner(Clone)").GetComponent<SpiderSpawner>().spawnInterval *= .9f;
        GameObject.Find("PenaltyHolder").GetComponent<MoreSpiderlings>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
