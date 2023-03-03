using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeedUp : Penalty
{
    public GameObject crawlers;
    public GameObject spiderlings;
    public GameObject hornets;
    public GameObject spiderBoss;
    public GameObject hornetBoss;

    public EnemySpeedUp()
    {
        penaltyName = "Enemy Speed UP";
        description = "Increases enemy movement speed by 10%";
    }

    public override void ActivatePenalty()
    {
        crawlers.GetComponent<CrawlerMovement>().speed *= 1.1f;
        spiderlings.GetComponent<Spiderlings>().speed *= 1.1f;
        hornets.GetComponent<HornetMovement>().speed *= 1.1f;
        spiderBoss.GetComponent<Boss1Attacks>().speed *= 1.1f;
        hornetBoss.GetComponent<HornetAttacks>().speed *= 1.1f;

        GameObject.Find("PenaltyHolder").GetComponent<EnemySpeedUp>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
