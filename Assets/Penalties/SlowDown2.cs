using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown2 : Penalty
{

    public SlowDown2()
    {
        penaltyName = "Fire Rate Down";
        description = "Decrease fire rate to 1";
    }

    public float fireRate = 1f;

    public override void ActivatePenalty()
    {
        Debug.Log("Slowed down to: " + fireRate);
        FindObjectOfType<PenaltiesShop>().gameObject.SetActive(false);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
