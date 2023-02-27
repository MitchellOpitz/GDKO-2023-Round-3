using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty : MonoBehaviour
{
    public string penaltyName;
    public string description;
    public virtual void ActivatePenalty()
    {
        Debug.Log("Penalty activated.");
    }
}