using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class AudioUIManager : MonoBehaviour
{


    [DllImport("__Internal")]
    private static extern void StartRecording();

    [DllImport("__Internal")]
    private static extern void StopRecording();

    [DllImport("__Internal")]
    private static extern void StartRecordingPlayback();

    [DllImport("__Internal")]
    private static extern void StopRecordingPlayback();


    [DllImport("__Internal")]
    private static extern void SetFileNameAndSend(string str);


    /// <summary>
    /// Handles what happens when buttons are pushed, 
    /// and handles displaying buttons.
    /// </summary>
    /// 
    //[SerializeField] Dropdown audioDevices;
    [SerializeField] Dropdown playableClips;
    [Space]
    [SerializeField] UIFader playButton;
    [SerializeField] UIFader recordButton;
    [SerializeField] UIFader stopButton;
    [SerializeField] UIFader uploadButton;
    [SerializeField] UIFader otherPlayPause;
    [SerializeField] UIFader OtherSkip;
    
    [Space]
    [SerializeField] UIFader statusMessageFader;

    [SerializeField] UIToggler recordingPanelButton;
    [SerializeField] UIFader UploadMessage;
    [Space]
    //[SerializeField] SoundFader soundtrack;

    //RecordingHandler recordingHandler;
    StoriesPlaybackManager storiesPlaybackManager;
    AudioFilesManager fileManager;

    bool isRecording = false;
    float recStartTime = 0;
    float maxRecTime = 120;

    // Start is called before the first frame update
    void Start()
    {
        SetupComponents();
    }

    private void Update()
    {
        TrackRecordingProgress();
    }

    private void SetupComponents()
    {
        //recordingHandler = GetComponent<RecordingHandler>();
        storiesPlaybackManager = GetComponent<StoriesPlaybackManager>();
        fileManager = GetComponent<AudioFilesManager>();
    }


    public void AddAudioClipToMenu(AudioClip clip)
    {
        string clipName = "Clip " + playableClips.options.Count.ToString();
  

        List<string> option = new List<string>();
        option.Add(clipName);
        playableClips.AddOptions(option);

        print("New clip added! " + clip.name);

        if (playableClips.options.Count <= 1)
        {
            storiesPlaybackManager.SetAudioClip(0);
        }

    }


    public void OnPlayPressed()
    {
        print("Play Clicked");
        if (!Application.isEditor) StartRecordingPlayback();
        if (Application.isEditor) OnStartPlaybackRecording();
    }
    public void OnRecordPressed()
    {
        if (!Application.isEditor) StartRecording();
        if (Application.isEditor) OnStartRecording();
    }

    public void OnStopPressed()
    {
        if (isRecording)
        {
            if (!Application.isEditor) StopRecording();

            if (Application.isEditor)
            {
                OnStopRecording();
                OnStartSave();
                Invoke("OnEndSave", 2f);
            }
        }
        else
        {
            if (!Application.isEditor) StopRecordingPlayback();

            if (Application.isEditor) OnStopPlaybackRecording();
        }
        
        
    }

    

    public void OnUploadPressed()
    {
        //Tell web audio interface to send file
        if (!Application.isEditor) SetFileNameAndSend(fileManager.ConstructName() + ".mp3");


        if (Application.isEditor)
        {
            OnStartUpload();
            Invoke("OnEndUpload", 2f);
        }
    }



    public void ForcePauseStories()
    {
        print("Pausing Stories Playback");
        storiesPlaybackManager.PauseAudio();

        print("Done force pausing stories");
        if(otherPlayPause.isActiveAndEnabled) otherPlayPause.SetText("Play");
    }

    public void ForceEndRecording()
    {
        if (isRecording)
        {
            OnStopPressed();
        }
    }

    public void OnPlayPauseStoriesPressed()
    {
        if(otherPlayPause.GetText() == "Play")
        {
            storiesPlaybackManager.PlayAudio();
            otherPlayPause.SetText("Pause");
        }
        else
        {
            storiesPlaybackManager.PauseAudio();
            otherPlayPause.SetText("Play");
        }
    }




    public void OnNextStoriesPressed()
    {
        storiesPlaybackManager.CycleToNextClip();
    }

    public void TrackRecordingProgress()
    {
        if (isRecording)
        {
            float timeLeft = (maxRecTime) - (Time.time - recStartTime);
            statusMessageFader.SetText("Time Left: " + timeLeft.ToString("F0"));
        }
    }


    /* MESSAGES FROM WEB AUDIO INTERFACE */

    void OnStartRecording()
    {
        //soundtrack.StartFadeOut();

        print("On Start Rec");
        isRecording = true;
        stopButton.StartFadeIn();
        playButton.StartFadeOut();
        recordButton.StartFadeOut();

        //ForcePauseStories();

        recStartTime = Time.time;
        statusMessageFader.SetText("Recording...");
        statusMessageFader.StartFadeIn();
    }

    void OnStopRecording()
    {
        //soundtrack.StartFadeIn();

        print("On Stop Rec");
        isRecording = false;

        recordButton.SetText("Re Record");

        stopButton.StartFadeOut();
        //playButton.StartFadeIn();
        //recordButton.StartFadeIn();


    }
    void OnStartPlaybackRecording()
    {
        print("Stop Button Fade In");
        stopButton.StartFadeIn();

        print("Play Button Fade Out");
        playButton.StartFadeOut();

        print("Rec button Fade Out");
        recordButton.StartFadeOut();

        print("Force Pause Stories");
        //ForcePauseStories();

        print("Done playback start");
    }

    void OnStopPlaybackRecording()
    {
        print("On Stop Play Rec");

        stopButton.StartFadeOut();
        playButton.StartFadeIn();
        recordButton.StartFadeIn();
    }

    void OnStartSave()
    {
        statusMessageFader.SetText("Saving...");
    }

    void OnEndSave()
    {
        recordButton.StartFadeIn();
        playButton.StartFadeIn();
        uploadButton.StartFadeIn();

        recordingPanelButton.GetComponentInChildren<Text>().text = "Close without uploading";

        statusMessageFader.SetText("Save Complete");
        statusMessageFader.Invoke("StartFadeOut", 1f);
    }

    void OnStartUpload()
    {
        statusMessageFader.SetText("Uploading...");
        statusMessageFader.StartFadeIn();

        stopButton.StartFadeOut();
        recordButton.StartFadeOut();
        //playButton.StartFadeOut();
        uploadButton.StartFadeOut();

        //playButton.StartFadeOut();
        //stopButton.StartFadeOut();
        //recordButton.StartFadeIn();
        //uploadButton.StartFadeOut();
    }

    void OnEndUpload()
    {
        statusMessageFader.SetText("Complete");
        statusMessageFader.Invoke("StartFadeOut", 1f);
        

        recordButton.StartFadeIn();
        stopButton.StartFadeIn();

        recordingPanelButton.ForceToggleOff();
        recordingPanelButton.TurnOffForever();

        StartCoroutine(CloseAndDisableRecordingPanel());
        
    }

    IEnumerator CloseAndDisableRecordingPanel()
    {
        recordingPanelButton.GetComponentInChildren<Text>().text = "Message recorded";

        UploadMessage.gameObject.SetActive(true);
        UploadMessage.StartFadeIn();
        yield return new WaitForSeconds(6);
        UploadMessage.StartFadeOut();
        yield return new WaitForSeconds(1);
        UploadMessage.gameObject.SetActive(false);

    }


}
