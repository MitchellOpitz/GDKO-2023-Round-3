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
        int currentDamage = FindObjectOfType<PlayerShoot>().damage;
        if (Mathf.Floor(currentDamage) == Mathf.Floor(currentDamage * 1.1f))
        {
            FindObjectOfType<PlayerShoot>().damage += 1;
        } else
        {
            FindObjectOfType<PlayerShoot>().damage = (int)Mathf.Floor(currentDamage * 1.1f);
        }
        GameObject.Find("UpgradeHolder").GetComponent<Damage>().currentRank++;
        Instantiate(penaltiesMenu);
        Destroy(FindObjectOfType<UpgradesShop>().gameObject);
    }
}
