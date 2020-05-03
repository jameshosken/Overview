using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersioningManager : MonoBehaviour
{
    [SerializeField] float version;
    // Start is called before the first frame update
    void Start()
    {
        print(">>>>>>>>>>>>>>>>>>\n\n\n");

        print("VERSION: " + version.ToString());
        print("\n\n\n<<<<<<<<<<<<<<<<<<");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
