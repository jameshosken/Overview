using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlacement : MonoBehaviour
{

    UIToggler uiToggle;

    MyLocationHandler locationHandler;
    // Start is called before the first frame update
    void Start()
    {
        uiToggle = GetComponent<UIToggler>();
        locationHandler = FindObjectOfType<MyLocationHandler>();

    }

    public void TogglePlacableLocation()
    {
        bool status = uiToggle.status;
        print("Placeable: " + status.ToString());
        locationHandler.TogglePlacement(status);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
