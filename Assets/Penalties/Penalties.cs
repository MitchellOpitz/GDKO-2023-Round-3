using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty : MonoBehaviour
{
    public string penaltyName;
    public string description;
    public int currentRank = 0;
    public int maxRank;

    public virtual void ActivatePenalty()
    {
        Debug.Log("Penalty activated.");
    }
}