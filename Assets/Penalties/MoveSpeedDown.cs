using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedDown : Penalty
{

    public MoveSpeedDown()
    {
        penaltyName = "Movement Speed Down";
        description = "Decreases movement speed by 10%";
    }

    public override void ActivatePenalty()
    {
        FindObjectOfType<PlayerMovement>().moveSpeed *= .9f;
        GameObject.Find("PenaltyHolder").GetComponent<MoveSpeedDown>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
