using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : Upgrade
{

    public Damage()
    {
        upgradeName = "Damage Up";
        description = "Increases damage done by 1";
    }

    public override void ActivateUpgrade()
    {
        FindObjectOfType<PlayerShoot>().damage += 1;
        GameObject.Find("UpgradeHolder").GetComponent<Damage>().currentRank++;
        Instantiate(penaltiesMenu);
        Destroy(FindObjectOfType<UpgradesShop>().gameObject);
    }
}
