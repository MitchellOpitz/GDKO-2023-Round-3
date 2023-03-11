using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplier : Upgrade
{

    public ScoreMultiplier()
    {
        upgradeName = "Score x2";
        description = "Doubles the score of every enemy";
    }

    public override void ActivateUpgrade()
    {
        FindObjectOfType<GameManager>().scoreMultiplier *= 2;
        GameObject.Find("UpgradeHolder").GetComponent<ScoreMultiplier>().currentRank++;
        Instantiate(penaltiesMenu);
        Destroy(FindObjectOfType<UpgradesShop>().gameObject);
    }
}
