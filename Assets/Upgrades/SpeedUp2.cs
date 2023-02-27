using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp2 : Upgrade
{

    public SpeedUp2()
    {
        upgradeName = "Fire Rate Up";
        description = "Increases fire rate to 0.1";
    }

    public float fireRate = 0.1f;

    public override void ActivateUpgrade()
    {
        FindObjectOfType<PlayerShoot>().fireRate = fireRate;
        ResumeGame();
    }

    private void ResumeGame()
    {
        FindObjectOfType<UpgradeShop>().gameObject.SetActive(false);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
