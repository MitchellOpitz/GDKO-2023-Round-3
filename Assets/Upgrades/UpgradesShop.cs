using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesShop : MonoBehaviour
{
    public GameObject upgradePanel1;
    public GameObject upgradePanel2;
    public GameObject upgradePanel3;
    public Upgrade[] upgrades;

    private Upgrade upgrade1;
    private Upgrade upgrade2;
    private Upgrade upgrade3;

    private PlayerManager playerManager;

    void OnEnable()
    {
        Time.timeScale = 0;
        GetUpgrades();
    }

    private void GetUpgrades()
    {
        // Upgrade 1
        upgrade1 = RollAbility();
        UpdatePanel(1, upgrade1);

        // Upgrade 2
        do
        {
            upgrade2 = RollAbility();
            UpdatePanel(2, upgrade2);
        } while (upgrade1 == upgrade2);

        // Upgrade 3
        do
        {
            upgrade3 = RollAbility();
            UpdatePanel(3, upgrade3);
        } while (upgrade1 == upgrade3 || upgrade2 == upgrade3);

    }

    private Upgrade RollAbility()
    {
        float randomNumber = Mathf.Floor(Random.Range(0f, upgrades.Length));
        playerManager = GameObject.Find("UpgradeHolder").GetComponent<PlayerManager>();
        if (playerManager.upgrades[(int)randomNumber].currentRank != upgrades[(int)randomNumber].maxRank)
        {
            return upgrades[(int)randomNumber];
        } else
        {
            return RollAbility();
        }
    }

    private void UpdatePanel(int panel, Upgrade upgrade)
    {
        switch (panel)
        {
            case 1:
                upgradePanel1.transform.Find("UpgradeName").GetComponent<TMP_Text>().text = upgrade.upgradeName;
                upgradePanel1.transform.Find("UpgradeDescription").GetComponent<TMP_Text>().text = upgrade.description;
                break;
            case 2:
                upgradePanel2.transform.Find("UpgradeName").GetComponent<TMP_Text>().text = upgrade.upgradeName;
                upgradePanel2.transform.Find("UpgradeDescription").GetComponent<TMP_Text>().text = upgrade.description;
                break;
            case 3:
                upgradePanel3.transform.Find("UpgradeName").GetComponent<TMP_Text>().text = upgrade.upgradeName;
                upgradePanel3.transform.Find("UpgradeDescription").GetComponent<TMP_Text>().text = upgrade.description;
                break;
        }
    }

    public void PurchaseUpgrade(int upgradeNumber)
    {
        switch (upgradeNumber)
        {
            case 1:
                upgrade1.ActivateUpgrade();
                break;
            case 2:
                upgrade2.ActivateUpgrade();
                break;
            case 3:
                upgrade3.ActivateUpgrade();
                break;
        }
    }
}
