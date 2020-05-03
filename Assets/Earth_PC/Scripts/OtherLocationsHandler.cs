using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherLocationsHandler : MonoBehaviour
{
    [SerializeField] GameObject blipTemplate;
    [SerializeField] Transform planet;

    List<GameObject> otherBlips = new List<GameObject>();
    public GameObject CreateBlipFromName(string name)
    {

        Vector3 localPos = NameUtility.GetCoordFromName(name);

        GameObject blip = Instantiate(blipTemplate, Vector3.zero, Quaternion.identity);
        
        blip.transform.parent = planet;
        blip.transform.localPosition = localPos;
        blip.transform.up = planet.rotation * localPos.normalized;
        blip.name = name;

        return blip;
    }
    

}
