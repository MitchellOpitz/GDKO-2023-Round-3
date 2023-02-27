using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Upgrade
{

    public SpeedUp()
    {
        upgradeName = "Fire Rate Up";
        description = "Increases fire rate to 0.1";
    }

    public float fireRate = 0.1f;

    public override void ActivateUpgrade()
    {
        FindObjectOfType<PlayerShoot>().fireRate = fireRate;
        Instantiate(penaltiesMenu);
        Destroy(FindObjectOfType<UpgradesShop>().gameObject);
    }
}
