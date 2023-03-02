using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateDown : Penalty
{

    public FireRateDown()
    {
        penaltyName = "Fire Rate Down";
        description = "Decreases fire rate by 10%";
    }

    public override void ActivatePenalty()
    {
        FindObjectOfType<PlayerShoot>().fireRate *= 1.1f;
        GameObject.Find("PenaltyHolder").GetComponent<FireRateDown>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
