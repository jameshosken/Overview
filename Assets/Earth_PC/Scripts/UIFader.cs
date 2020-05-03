using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFader : MonoBehaviour
{
    public bool StartOff;
    public float fadeTime = .25f;
    public bool playOnAwake = false;

    
    Image img;
    Button button;
    Text text;


    Coroutine currentProcess;

    Color originalUIColour;
    Color originalTextColour;
    bool isButton = false;

    // Start is called before the first frame update
    void Start()
    {

        img = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        button = GetComponent<Button>();

        if (button != null) isButton = true;

        originalTextColour = text.color;
        originalUIColour = img.color;

        if (StartOff)
        {
            img.color = Color.clear;
            text.color = Color.clear;
            if (isButton) button.interactable = false;

        }

        if (playOnAwake)
        {
            StartFadeIn();
        }

    }

    public void SetText(string _text)
    {
        if(text != null) text.text = _text;
    }
    public string GetText()
    {
        return text.text;
    }
    public void StartFadeIn()
    {
        img = GetComponent<Image>();
        text = GetComponentInChildren<Text>();

        if (currentProcess != null)
        {
            StopCoroutine(currentProcess);
        }
        currentProcess = StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        img.alphaHitTestMinimumThreshold = 0f;
        img = GetComponent<Image>();
        text = GetComponentInChildren<Text>();

        if (currentProcess != null)
        {
            StopCoroutine(currentProcess);
        }
        if(this.gameObject.activeInHierarchy) currentProcess = StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        //button.gameObject.SetActive(true);
        if (isButton) button.interactable = true;

        Color startCol = img.color;
        Color startTextCol = text.color;
        float c = 0;

        while (c < fadeTime)
        {
            c += Time.deltaTime;
            img.color = Color.Lerp(startCol, originalUIColour, c/fadeTime);
            text.color = Color.Lerp(startTextCol, originalTextColour, c / fadeTime);
            yield return null;
        }

        img.color = originalUIColour;
        text.color = originalTextColour;
        currentProcess = null;
    }

    IEnumerator FadeOut()
    {
        Color startCol = img.color;
        Color startTextCol = text.color;
        float c = 0;

        while (c < fadeTime)
        {
            c += Time.deltaTime;
            img.color = Color.Lerp(startCol, Color.clear, c / fadeTime);
            text.color = Color.Lerp(startTextCol, Color.clear, c / fadeTime);
            yield return null;
        }

        img.color = Color.clear;
        text.color = Color.clear;

        if (isButton) button.interactable = false;
        currentProcess = null;
    }
}