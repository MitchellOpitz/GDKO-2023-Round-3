using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHealth : Penalty
{
    public MinionHealth()
    {
        penaltyName = "Minion Health UP";
        description = "Increases enemy minion health by 10%";
    }

    public override void ActivatePenalty()
    {
        GameObject.Find("PenaltyHolder").GetComponent<MinionHealth>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
