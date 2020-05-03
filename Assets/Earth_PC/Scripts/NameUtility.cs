using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameUtility 
{
    public static Vector3 GetCoordFromName(string name)
    {

        Debug.Log("Creating Clip From Name: " + name);
        name = name.Replace(".mp3", "");

        string[] chunks = name.Split('_');

        Debug.Log("Unsplit vec: " + chunks[1]);
        string[] vec = chunks[1].Split('&');
        Debug.Log("X: " + vec[0]);
        Debug.Log("Y: " + vec[1]);
        Debug.Log("Z: " + vec[2]);

        Vector3 v = new Vector3(float.Parse(vec[0]), float.Parse(vec[1]), float.Parse(vec[2]));

        return v.normalized;
    }

    public static string CreateNameFromCoord(Vector3 coord, string timestamp)
    {
        coord = coord.normalized;

        string name = "";
        foreach (char c in timestamp)
        {
            if (c == '/' || c == '\\' || c == ':')
            {
            }
            else
            {
                name += c;
            }
        }

        name += "_";
        name += coord.x.ToString();
        name += '&';
        name += coord.y.ToString(); 
        name += '&';
        name += coord.z.ToString();

        return name;
    }


}
