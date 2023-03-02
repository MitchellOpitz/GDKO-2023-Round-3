using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRate : Upgrade
{

    public FireRate()
    {
        upgradeName = "Fire Rate Up";
        description = "Increases fire rate by 10%";
    }

    public float fireRate = 0.1f;

    public override void ActivateUpgrade()
    {
        FindObjectOfType<PlayerShoot>().fireRate *= .9f;
        GameObject.Find("UpgradeHolder").GetComponent<FireRate>().currentRank++;
        Instantiate(penaltiesMenu);
        Destroy(FindObjectOfType<UpgradesShop>().gameObject);
    }
}
