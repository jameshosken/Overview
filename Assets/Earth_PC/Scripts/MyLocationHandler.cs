using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLocationHandler : MonoBehaviour
{

    bool isPlaced;
    bool isPlacable = true;
    
    [SerializeField] GameObject myLocationObjectTemplate;

    [SerializeField] Material activeMat;
    [SerializeField] Material inactiveMat;


    GameObject myLocationObject;

    public void TogglePlacement(bool b)
    {
        isPlacable = b;
        myLocationObject.GetComponent<BlipMaterialModifier>().SetActiveMaterial(b);
    }

    public void EnablePlacement()
    {
        isPlacable = true;
    }
    public bool IsLocationPlaced()
    {
        return isPlaced;
    }

    public Vector3 GetLocalLocation()
    {
        return myLocationObject.transform.localPosition;
    }
    public void OnLocationClick(RaycastHit hit)
    {

        if (Application.isEditor)
        {
            CreateNewLocationObject(hit);
            return;
        }
        
        
        if (!isPlacable)
        {
            return;
        }

        if (!isPlaced)
        {
            CreateNewLocationObject(hit);
        }
        else
        {
            UpdateLocationObject(hit);
        }
        
    }

    private void UpdateLocationObject(RaycastHit hit)
    {
        myLocationObject.transform.position = hit.point;
        myLocationObject.transform.up = hit.normal;
        myLocationObject.transform.parent = hit.collider.transform;
    }

    private void CreateNewLocationObject(RaycastHit hit)
    {
        Quaternion rot = Quaternion.Euler(hit.normal);
        myLocationObject = Instantiate(myLocationObjectTemplate, hit.point, rot);
        myLocationObject.transform.up = hit.normal;
        myLocationObject.transform.parent = hit.collider.transform;
        isPlaced = true;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
