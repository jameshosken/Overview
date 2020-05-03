using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlaybackOnToggle : MonoBehaviour
{
    AudioUIManager audioUI;
    // Start is called before the first frame update
    void Start()
    {
        audioUI = FindObjectOfType<AudioUIManager>();
    }

    public void DisableAudio()
    {
        audioUI.ForcePauseStories();
        audioUI.ForceEndRecording();
    }
}
