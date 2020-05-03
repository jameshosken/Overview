using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoUtility 
{

    //https://answers.unity.com/questions/189724/polar-spherical-coordinates-to-xyz-and-vice-versa.html
    public static Vector2 LatLongFromXYZ(Vector3 xyz)
    {
        //Vector3 xz = new Vector3(xyz.x, 0, xyz.z);

        //float lon = Vector3.Angle(Vector3.forward, xz);
        //float lat = Vector3.Angle(xz, xyz);



        float lon =  Mathf.Atan2(xyz.x, xyz.z);

        //this is easier to write and read than sqrt(pow(x, 2), pow(y, 2))!
        float xzLen = new Vector2(xyz.x, xyz.z).magnitude;
        //atan2 does the magic
        float lat = Mathf.Atan2(-xyz.y, xzLen);

        return new Vector2(lat, lon);
    }


    public static Vector3 XYZFromLatLong(Vector2 latLong)
    {
        /*
        float lat = latLong.x;
        float lon = latLong.y;
        Vector3 coord = Vector3.forward;

        Quaternion latRotation = Quaternion.Euler(lat, 0, 0);
        coord = latRotation * coord;

        Quaternion lonRotation = Quaternion.Euler(0, lon, 0);
        coord = lonRotation * coord;
        


        return coord;
        */
        
        float lat = latLong.x;
        float lon = latLong.y;

        //Vector3 xyz = Quaternion.AngleAxis(lon, -Vector3.up) * Quaternion.AngleAxis(lat, -Vector3.right) * new Vector3(0, 0, 1);
        
        
        //return xyz;

        float a = Mathf.Cos(lat);
        float x = a * Mathf.Cos(lon);
        float y = Mathf.Sin(lat);
        float z = a * Mathf.Sin(lon);

        return new Vector3(x, y, z);
        
    }

}
