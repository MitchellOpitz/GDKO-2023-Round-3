using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject upgradesMenu;

    public void StartUpgrades()
    {
        Instantiate(upgradesMenu);
        FindObjectOfType<PlayerToggle>().ToggleAbilities(false);
    }
}
