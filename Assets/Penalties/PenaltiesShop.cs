using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PenaltiesShop : MonoBehaviour
{
    public GameObject penaltyPanel1;
    public GameObject penaltyPanel2;
    public GameObject penaltyPanel3;
    public Penalty[] penalties;

    private Penalty penalty1;
    private Penalty penalty2;
    private Penalty penalty3;

    private PlayerManager playerManager;

    void OnEnable()
    {
        GetPenalties();

    }

    private void GetPenalties()
    {
        // Upgrade 1
        penalty1 = RollAbility();
        UpdatePanel(1, penalty1);

        // Upgrade 2
        do
        {
            penalty2 = RollAbility();
            UpdatePanel(2, penalty2);
        } while (penalty1 == penalty2);

        // Upgrade 3
        do
        {
            penalty3 = RollAbility();
            UpdatePanel(3, penalty3);
        } while (penalty1 == penalty3 || penalty2 == penalty3);

    }

    private Penalty RollAbility()
    {
        float randomNumber = Mathf.Floor(Random.Range(0f, penalties.Length));
        playerManager = GameObject.Find("PenaltyHolder").GetComponent<PlayerManager>();

        // If Crawlers spawner not started
        if(randomNumber == 4)
        {
            if(playerManager.penalties[2].currentRank == 0)
            {
                return RollAbility();
            }
        }

        // If Hornet spawner not started
        if (randomNumber == 5)
        {
            if (playerManager.penalties[0].currentRank == 0)
            {
                return RollAbility();
            }
        }

        if (playerManager.penalties[(int)randomNumber].currentRank != penalties[(int)randomNumber].maxRank)
        {
            return penalties[(int)randomNumber];
        }
        else
        {
            return RollAbility();
        }
    }

    private void UpdatePanel(int panel, Penalty penalty)
    {
        switch (panel)
        {
            case 1:
                penaltyPanel1.transform.Find("PenaltyName").GetComponent<TMP_Text>().text = penalty.penaltyName;
                penaltyPanel1.transform.Find("PenaltyDescription").GetComponent<TMP_Text>().text = penalty.description;
                break;
            case 2:
                penaltyPanel2.transform.Find("PenaltyName").GetComponent<TMP_Text>().text = penalty.penaltyName;
                penaltyPanel2.transform.Find("PenaltyDescription").GetComponent<TMP_Text>().text = penalty.description;
                break;
            case 3:
                penaltyPanel3.transform.Find("PenaltyName").GetComponent<TMP_Text>().text = penalty.penaltyName;
                penaltyPanel3.transform.Find("PenaltyDescription").GetComponent<TMP_Text>().text = penalty.description;
                break;
        }
    }

    public void PurchasePenalty(int upgradeNumber)
    {
        Time.timeScale = 1;
        switch (upgradeNumber)
        {
            case 1:
                penalty1.ActivatePenalty();
                break;
            case 2:
                penalty2.ActivatePenalty();
                break;
            case 3:
                penalty3.ActivatePenalty();
                break;
        }
    }
}
