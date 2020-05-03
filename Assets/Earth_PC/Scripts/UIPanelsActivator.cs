using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelsActivator : MonoBehaviour
{

    [SerializeField] UIFader[] faders;

    public void ActivateStartingPanels()
    {
        Debug.Log("Activating PAnels!");
        foreach(UIFader fader in faders)
        {
            fader.StartFadeIn();
        }
    }
   
}
