using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPerLevel: Upgrade
{

    public HealthPerLevel()
    {
        upgradeName = "Health Per Level";
        description = "Restores 1 health each time you defeat a boss";
    }

    public override void ActivateUpgrade()
    {
        GameObject.Find("UpgradeHolder").GetComponent<HealthPerLevel>().currentRank++;
        Instantiate(penaltiesMenu);
        Destroy(FindObjectOfType<UpgradesShop>().gameObject);
    }
}
