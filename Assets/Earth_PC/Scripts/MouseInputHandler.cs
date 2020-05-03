using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseInputHandler : MonoBehaviour
{
    [SerializeField] Transform mousePhysicsObject;
    Camera cam;

    [SerializeField] float mouseClickTime = 0.2f;

    [SerializeField] Vector2 zoomMinMax;
    //[SerializeField] GameObject ObjectToPlace;

    StoriesPlaybackManager stories;
    MyLocationHandler myLocation;
    float mouseDownTimestamp;

    float cameraZoom;

    // Start is called before the first frame update
    void Start()
    {
        stories = FindObjectOfType<StoriesPlaybackManager>();
        myLocation = FindObjectOfType<MyLocationHandler>();

        mousePhysicsObject.gameObject.SetActive(false);
        cam = Camera.main;

        cameraZoom = cam.fieldOfView;
    }

    

    // Update is called once per frame
    void Update()
    {
        
        

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (EventSystem.current.gameObject.GetComponent<CanvasGroup>() != null)
                {
                    //ignore canvas groups
                }
                else
                {
                    return;
                }
            }

            mouseDownTimestamp = Time.time;
            mousePhysicsObject.gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            mousePhysicsObject.gameObject.SetActive(false);

            if(Time.time - mouseDownTimestamp < mouseClickTime)
            {
                RegisterClick();
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        cameraZoom = Mathf.Clamp(cameraZoom + scroll * 10f, zoomMinMax.x, zoomMinMax.y);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, cameraZoom, 1f * Time.deltaTime);


        if (Input.GetMouseButton(0))
        {
            TransformToRaycast();
        }
    }


    public void SetZoomLevel(float z)
    {
        print("Setting Zoom Level: " + z);
        cameraZoom = z;
    }
    private void RegisterClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, 10f);
        foreach (RaycastHit hit in hits)
        {
            //if (hit.collider.gameObject.tag != "Planet") continue;

            if (hit.collider.gameObject.tag == "Blip")
            {
                stories.SetAudioClipByBlip(hit.collider.gameObject);
            }
            if (hit.collider.gameObject.tag == "Planet")
            {
                myLocation.OnLocationClick(hit);
                //Stop zoom in on click
                cameraZoom = cam.fieldOfView;
            }

            //return;
        }
    }

    private void TransformToRaycast()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, 10f);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.tag != "Planet") continue;
            mousePhysicsObject.position = hit.point;
            return;
        }
    }
}
