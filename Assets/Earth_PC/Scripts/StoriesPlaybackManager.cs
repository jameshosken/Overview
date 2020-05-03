using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StoriesPlaybackManager : MonoBehaviour
{

    
    List<AudioClip> clips = new List<AudioClip>();
    OtherLocationsHandler locationsHandler;
    Playlist playlist;
    PlanetRotator planetRotation;

    AudioSource source;
    AudioUIManager ui;

    int currentClipIndex;

    bool continueOnEnd = false;

    float fadeDuration = 0.5f;


    Coroutine currentCoroutine = null;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = false;
        locationsHandler = FindObjectOfType<OtherLocationsHandler>();
        ui = GetComponent<AudioUIManager>();

        playlist = FindObjectOfType<Playlist>();
        planetRotation = FindObjectOfType<PlanetRotator>();

    }

    private void Update()
    {
        if(!source.isPlaying && continueOnEnd)
        {
            print("End of clip! Cycling to next");
            CycleToNextClip();
            PlayAudio();
        }

        if( source.isPlaying && 
            source.time > source.clip.length - fadeDuration && 
            currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(FadeOutAudio());
        }
    }

    public void SetAudioClipByBlip(GameObject blip)
    {

        foreach(KeyValuePair<AudioClip, GameObject> kvpair in playlist.playlist)
        {
            if(kvpair.Value == blip)
            {
                print("Setting new audio clip");
                source.clip = kvpair.Key;
                currentClipIndex = clips.IndexOf(kvpair.Key);
            }
        }
    }

    public void PlayAudio()
    {
        planetRotation.SetHasTargetBlip(true);
        source.Play();
        continueOnEnd = true;
        currentCoroutine = StartCoroutine(FadeInAudio());
    }

    IEnumerator FadeInAudio()
    {
        print("Fading In Audio");
        source.volume = 0;
        float fadeDur = fadeDuration;
        float c = 0;
        while (c < fadeDur) {
            c += Time.deltaTime;
            source.volume = Mathf.Lerp(0, 1, c / fadeDur);
            yield return null;
        }


        currentCoroutine = null;
    }

    IEnumerator FadeOutAudio()
    {
        print("Fading Out Audio");
        source.volume = 1;
        float fadeDur = fadeDuration;
        float c = 0;
        while (c < fadeDur)
        {
            c += Time.deltaTime;
            source.volume = Mathf.Lerp(1, 0, c / fadeDur);
            yield return null;
        }
        currentCoroutine = null;
    }

    public void PauseAudio()
    {
        try
        {
            planetRotation.SetHasTargetBlip(false);

            print("Pausing if playing");

            print("Source is playing: " + source.isPlaying.ToString());
            if (source.isPlaying) source.Pause();

            print("Done pausing");
            continueOnEnd = false;
        }
        catch (Exception e)
        {
            print("Trouble pausing audio");
            print(e.Message);
        }
    }

 

    public void StopAudio()
    {
        planetRotation.SetHasTargetBlip(false);
        if (source.isPlaying) source.Pause();
        //source.Stop();
        continueOnEnd = false;
    }

    public void CycleToNextClip()
    {
        if (clips.Count == 0) return;

        print("Cycling clip!");
        print("Current idx: " + currentClipIndex);
        print("Clips Length: " + clips.Count);

        //int idx = (currentClipIndex + 1) % playlist.playlist.Count;
        int idx = (currentClipIndex + 1) % clips.Count;
        UpdateCurrentClip(idx);
    }

    public void UpdateCurrentClip(int value)
    {
        currentClipIndex = value;

        print("New idx: " + currentClipIndex);
        if (source.isPlaying)
        {
            StopAudio();
            SetAudioClip(value);
            PlayAudio();
        }
        else
        {
            SetAudioClip(value);
        }
    }

    public void SetAudioClip(int i)
    {
        source.clip = clips[i];
        planetRotation.SetTargetBlip(playlist.playlist[source.clip]);

    }

    public void AddClip(AudioClip clip, string name)
    {
       
        print("Adding new clip: " + name);
        
        clips.Add(clip);

        print("clips length now: " + clips.Count);

        GameObject blip = locationsHandler.CreateBlipFromName(name);


        playlist.playlist.Add(clip, blip);

        //ui.AddAudioClipToMenu(clip);
    }
   
}
