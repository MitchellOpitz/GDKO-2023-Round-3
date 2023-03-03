using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerMinions : Penalty
{
    public GameObject spawner;

    public CrawlerMinions()
    {
        penaltyName = "Crawler Minions";
        description = "Spawns crawler minions. Crawlers travel from one side of the map to other in a straight line.";
    }

    public override void ActivatePenalty()
    {
        Instantiate(spawner);
        GameObject.Find("PenaltyHolder").GetComponent<CrawlerMinions>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
