using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeed : Upgrade
{

    public MovementSpeed()
    {
        upgradeName = "Movement Speed Up";
        description = "Increases movement speed by 10%";
    }

    public override void ActivateUpgrade()
    {
        FindObjectOfType<PlayerMovement>().moveSpeed *= 1.1f;
        GameObject.Find("UpgradeHolder").GetComponent<MovementSpeed>().currentRank++;
        Instantiate(penaltiesMenu);
        Destroy(FindObjectOfType<UpgradesShop>().gameObject);
    }
}
