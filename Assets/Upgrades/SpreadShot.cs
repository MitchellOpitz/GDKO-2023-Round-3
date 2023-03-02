using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot: Upgrade
{

    public SpreadShot()
    {
        upgradeName = "Spread Shot Up";
        description = "Increases the number of shots fired 1.  Shots fired in a 10° spread.";
    }

    public override void ActivateUpgrade()
    {
        FindObjectOfType<PlayerShoot>().upgradeRank += 1;
        GameObject.Find("UpgradeHolder").GetComponent<SpreadShot>().currentRank++;
        Instantiate(penaltiesMenu);
        Destroy(FindObjectOfType<UpgradesShop>().gameObject);
    }
}
