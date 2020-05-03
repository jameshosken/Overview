using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using System.IO;
using System;
using UnityEngine.Events;

[Serializable]
public class ProgressUpdate : UnityEvent<float> { }

public class AudioFilesManager : MonoBehaviour 
{

    [SerializeField] UnityEvent OnStartListeningStage;

    //FTP FTPHandler;

    //AudioSource source;
    MyLocationHandler myLocation;

    AudioUIManager uiManager;

    bool checkIfConversionDone = false;

    byte[] wavBytes;

    byte[] mp3Bytes;


    // Start is called before the first frame update
    void Start()
    {

        //source = GetComponent<AudioSource>();
        //clip = source.clip;

        myLocation = FindObjectOfType<MyLocationHandler>();

        uiManager = GetComponent<AudioUIManager>();

    }

    //private void Update()
    //{
    //    TrackUploadProgress();
    //    TrackSaveProgess();
    //}

    //public void SetMP3Bytes(byte[] _mp3Bytes)
    //{
    //    mp3Bytes = _mp3Bytes;
    //}


    //https://answers.unity.com/questions/737002/wav-byte-to-audioclip.html
    static float bytesToFloat(byte firstByte, byte secondByte)
    {
        // convert two bytes to one short (little endian)
        short s = (short)((secondByte << 8) | firstByte);
        // convert to range from -1 to (just below) 1
        return s / 32768.0F;
    }

    

    //public void SaveFile(AudioClip clip)
    //{

    //    print("SAVING");

    //    uiManager.StartSave();



    //    string wavFile = SaveWav.Save("recording.wav", clip);

    //    mp3Bytes = null;

    //    FindObjectOfType<NetworkingHandler>().SendFileToConvert(wavFile);

    //    checkIfConversionDone = true;


    //}

    //public void UploadAudioFile()
    //{
    //    string name = ConstructName() + ".mp3";
        
    //    FindObjectOfType<NetworkingHandler>().SendAudioFileToServer(mp3Bytes, name);

    //    uiManager.StartUpload();
    //    print("UPLOAD AWAY! >>>>>>>>>>>>>>>>>>>>>>>");

    //}

    public string ConstructName()
    {

        Vector3 localLocation = myLocation.GetLocalLocation();
        string name = NameUtility.CreateNameFromCoord(localLocation, DateTime.UtcNow.ToLongTimeString());
            
        return name; 
    }


    //private void TrackSaveProgess()
    //{
    //    if (checkIfConversionDone)
    //    {
    //        if(mp3Bytes != null)
    //        {
    //            OnSuccessfulSave();
    //        }
    //    }
    //}

    //private void OnSuccessfulSave()
    //{
    //    print("SAVED SUCCESSFULLY");
    //    checkIfConversionDone = false;

    //    uiManager.EndSave();
    //}

    //private void TrackUploadProgress()
    //{
    //    return; 
    //}

    //public void SetUploadProgressIndicator(float p)
    //{
    //}

    //public void OnSuccessfulUpload()
    //{
    //    uiManager.EndUpload();
    //}



}
