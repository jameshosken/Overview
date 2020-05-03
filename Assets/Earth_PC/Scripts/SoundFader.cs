using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFader : MonoBehaviour
{
    AudioSource source;
    [SerializeField] float fadeTime = 0.5f;

    float vol;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        vol = source.volume;
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        source.Play();

        float c = 0;

        while (c < fadeTime)
        {
            c += Time.deltaTime;
            source.volume = Mathf.Lerp(0, vol, c / fadeTime);
            yield return null;
        }


    }

    IEnumerator FadeOut()
    {

        float c = 0;

        while (c < fadeTime)
        {
            c += Time.deltaTime;
            source.volume = Mathf.Lerp(vol, 0, c / fadeTime);
            yield return null;
        }

        source.Pause();
    }
}
