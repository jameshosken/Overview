using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipSizeModifier : MonoBehaviour
{
    [SerializeField] Transform icon;
    [SerializeField] float modificationIntervals = .25f; //in seconds
    [SerializeField] float scale = 0.5f;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float d = cam.fieldOfView;

        float mapped = map(d, 1, 60, 1, 10);

        transform.localScale = new Vector3(mapped * scale, 1, mapped * scale);
    }


    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
