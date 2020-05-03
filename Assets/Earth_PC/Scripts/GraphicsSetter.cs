using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsSetter : MonoBehaviour
{

    private void Start()
    {

        QualitySettings.SetQualityLevel(2, false);
    }
    // Start is called before the first frame update
    public void SetQuality(int i)
    {

        QualitySettings.SetQualityLevel(i, false);
    }


}
