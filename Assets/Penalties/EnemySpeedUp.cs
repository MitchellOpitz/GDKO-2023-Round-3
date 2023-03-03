using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeedUp : Penalty
{
    public EnemySpeedUp()
    {
        penaltyName = "Enemy Speed UP";
        description = "Increases enemy movement speed by 10%";
    }

    public override void ActivatePenalty()
    {
        GameObject.Find("PenaltyHolder").GetComponent<EnemySpeedUp>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
