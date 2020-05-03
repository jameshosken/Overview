using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvasGroupFader : MonoBehaviour
{

    [SerializeField] bool startOff = false;
    [SerializeField] float fadeTime = 1f;

    CanvasGroup cg;
    Coroutine currentProcess;
    
    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();

        if (startOff)
        {
            cg.alpha = 0;
        }
    }

    public void StartFadeIn()
    {
        print("Fading In " + gameObject.name);
        if (currentProcess != null)
        {
            StopCoroutine(currentProcess);
        }
        currentProcess = StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        print("Fading Out " + gameObject.name);
        if (currentProcess != null)
        {
            StopCoroutine(currentProcess);
        }
        currentProcess = StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {


        float c = 0;

        while (c < fadeTime)
        {
            c += Time.deltaTime;
            cg.alpha = Mathf.Lerp(0, 1, c / fadeTime);
            yield return null;
        }


        cg.alpha = 1;
        cg.blocksRaycasts = true;
    }

    IEnumerator FadeOut()
    {

        float c = 0;

        while (c < fadeTime)
        {
            c += Time.deltaTime;

            cg.alpha = Mathf.Lerp(1, 0, c / fadeTime);
            yield return null;
        }

        cg.alpha = 0;
        cg.blocksRaycasts = false;
    }
}
