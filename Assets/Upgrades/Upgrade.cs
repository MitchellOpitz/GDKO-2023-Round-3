using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public string upgradeName;
    public string description;
    public virtual void ActivateUpgrade()
    {
        Debug.Log("Upgrade activated.");
    }
}