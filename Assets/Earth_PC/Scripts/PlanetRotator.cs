using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotator : MonoBehaviour
{

    [SerializeField] float interpolationSpeed = 0.5f;
    [SerializeField] float rotateToBlipSpeed = 0.01f;
    bool isRotationEnabled = false;

    int positionBufferSize = 10;
    int positionBufferIndex = 0;

    Vector3[] positionBuffer;

    Vector3 onEntryVector;
    Quaternion onEntryRotation;
    Quaternion releasedRotation = Quaternion.identity;

    Camera cam;

    Transform previousClosestTransform = null;

    List<Collider> currentColliders = new List<Collider>();

    Material planetMat;

    bool hasTargetBlip = false;
    GameObject targetBlip;

    void Start()
    {
        positionBuffer = new Vector3[positionBufferSize];
        planetMat = GetComponent<Renderer>().material;
        cam = Camera.main;
    }

    private void FixedUpdate()
    {
        AddToPositionBuffer();
        
        //To do with user rotation
        if (isRotationEnabled)  
        {
            if (currentColliders.Count == 0)
            {
                DisableRotation();
            }
            else
            {

                float closestDistance = float.MaxValue;
                Transform closestTransform = null;
                foreach (Collider col in currentColliders)
                {
                    float dist = Vector3.Distance(col.transform.position, transform.position);
                    if (dist < closestDistance)
                    {
                        closestTransform = col.transform;
                        closestDistance = dist;
                    }
                }

                if (closestTransform != previousClosestTransform)
                {
                    previousClosestTransform = closestTransform;
                    ResetRotationTrackers();
                }

                RotateToHandVector();
            }
            
        }
        else
        {
            //Check if we should enable rotation:
            if(currentColliders.Count > 0) EnableRotation();
            else ResetRotationTrackers(); 
        }
        
        currentColliders.Clear();
        

        Vector3 pos = Vector3.zero;
        if (previousClosestTransform)
        {
            pos = previousClosestTransform.position;
        }

        planetMat.SetVector("HandPosition", pos);


        //To do with auto rotations

        if (hasTargetBlip)
        {
            RotateToBlipVector();
        }
        
    }

    public void SetHasTargetBlip(bool b)
    {
        hasTargetBlip = b;

    }

    public void SetTargetBlip(GameObject blip)
    {
        targetBlip = blip;
    }

    void RotateToBlipVector()
    {
        Vector3 targetBlipWorldVector = targetBlip.transform.position;
        Quaternion camRotation = Quaternion.LookRotation(cam.transform.position, Vector3.up);
        Quaternion blipRotation = Quaternion.LookRotation(targetBlipWorldVector, Vector3.up);

        Quaternion rotationToCamera = camRotation * Quaternion.Inverse(blipRotation);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToCamera * transform.rotation, rotateToBlipSpeed);
    }

    void RotateToHandVector()
    {
        Vector3 toHand = GetAveragePosition();

        Quaternion handsRotation = GetHandsRotation(toHand);

        Quaternion offset = handsRotation * Quaternion.Inverse(onEntryRotation);

        transform.rotation = Quaternion.Slerp(transform.rotation, offset * releasedRotation, interpolationSpeed);

   
    }


    private void AddToPositionBuffer()
    {
        if (previousClosestTransform == null) return; 
        positionBuffer[positionBufferIndex] = previousClosestTransform.position;
        positionBufferIndex = (positionBufferIndex + 1) % positionBufferSize;
    }

    private Quaternion GetHandsRotation(Vector3 toHand)
    {

        Quaternion lookRotation = Quaternion.LookRotation(toHand, Vector3.up);
        return lookRotation;

    }

    Vector3 GetAveragePosition()
    {
        Vector3 payload = Vector3.zero;
        foreach(Vector3 v in positionBuffer)
        {
            Vector3 fromCenter = v - transform.position;
            payload += fromCenter;
        }

        return payload.normalized;
    }

    public void EnableRotation()
    {
        isRotationEnabled = true;
    }

    void ResetRotationTrackers()
    {
        
        releasedRotation = transform.rotation;

        if (previousClosestTransform == null) return;
        FlushPositionBuffer();

        onEntryVector = GetAveragePosition();
        onEntryRotation = GetHandsRotation(onEntryVector);
    }

    private void FlushPositionBuffer()
    {
        for (int i = 0; i < positionBuffer.Length; i++)
        {
            AddToPositionBuffer();
        }
    }

    public void DisableRotation()
    {
        ResetRotationTrackers();
        releasedRotation = transform.rotation;
        isRotationEnabled = false;
        previousClosestTransform = null;
    }

  

    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.activeSelf) currentColliders.Add(other);

        //AddToCollisionBuffer(true);
    }

}
