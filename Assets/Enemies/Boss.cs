using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject upgradesMenu;

    public void StartUpgrades()
    {
        Boss[] bosses = FindObjectsOfType<Boss>();
        if (!GameObject.Find("Upgrades(Clone)") && bosses.Length == 1)
        {
            Instantiate(upgradesMenu);
            FindObjectOfType<PlayerToggle>().ToggleAbilities(false);
        }
    }
}
