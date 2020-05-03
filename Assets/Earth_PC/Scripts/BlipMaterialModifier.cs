using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipMaterialModifier : MonoBehaviour
{
    [SerializeField] Material activeMat;

    [SerializeField] Material inactiveMat;


    public void SetActiveMaterial(bool b)
    {
        if (b)
        {
            GetComponentInChildren<Renderer>().material = activeMat;
        }
        else
        {
            GetComponentInChildren<Renderer>().material = inactiveMat;
        }
    }
}
