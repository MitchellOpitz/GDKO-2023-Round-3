using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCrawlers : Penalty
{
    public MoreCrawlers()
    {
        penaltyName = "More Crawler Minions";
        description = "Increases the number of Crawlers that spawn by 10%";
    }

    public override void ActivatePenalty()
    {
        GameObject.Find("CrawlerSpawner(Clone)").GetComponent<SpiderSpawner>().spawnInterval *= .9f;
        GameObject.Find("PenaltyHolder").GetComponent<MoreCrawlers>().currentRank++;
        Destroy(FindObjectOfType<PenaltiesShop>().gameObject);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(true);
    }
}
