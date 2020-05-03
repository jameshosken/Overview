using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverLight : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point;
        }
    }
}
