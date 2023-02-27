using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToggle : MonoBehaviour
{
    public Component[] componentsToDisable;

    public void ToggleAbilities(bool enable)
    {
        // Loop through each component and disable them
        foreach (Behaviour component in componentsToDisable)
        {
            component.enabled = enable;
        }
    }
}
