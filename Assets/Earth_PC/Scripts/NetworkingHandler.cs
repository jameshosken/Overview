using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;


public class NetworkingHandler : MonoBehaviour
{
    AudioFilesManager audioFilesManager;
    AudioUIManager audioUIManager;
    StoriesPlaybackManager playback;

    [SerializeField] bool downloadOnStart = true;

    List<string> onlineAudioFiles = new List<string>();
    static string uriPath = "https://jameshosken.com/subs/thesis/";
    static string downloadPath = uriPath + "audio/";

    List<UnityWebRequest> audioRequests = new List<UnityWebRequest>();



    // Start is called before the first frame update
    void Start()
    {
        audioFilesManager = FindObjectOfType<AudioFilesManager>();
        audioUIManager = FindObjectOfType<AudioUIManager>();
        playback = FindObjectOfType<StoriesPlaybackManager>();
        if (downloadOnStart) StartCoroutine(GetAudioFileNames());
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = audioRequests.Count - 1; i >= 0; i--)
        {
            UnityWebRequest audioReq = audioRequests[i];
            if (audioReq.isDone)
            {
                
            }
        }
    }

    IEnumerator GetAudioFileNames()
    {
        print("Getting audio file names");
        string uri = uriPath + "information.txt";


        print(uri);
        UnityWebRequest audioInformationRequest = UnityWebRequest.Get(uri);
        audioInformationRequest.downloadHandler = new DownloadHandlerBuffer();


        yield return audioInformationRequest.SendWebRequest();
        
        if (audioInformationRequest.error != null)
        {
            print("Failed to get audio information");
            print(audioInformationRequest.error);
        }
        else
        {
            print("SUCCESS!");
            print(audioInformationRequest.downloadHandler.text.Length);
            print(audioInformationRequest.downloadHandler.data.Length);

            

            string[] names = audioInformationRequest.downloadHandler.text.Split('\n');
            print("Found " + names.Length + " names");

            foreach(string name in names)
            {
                
                if (name == "") continue;
                if (name.Contains(".mp3"))
                {
                    print("Adding " + name + " to list of files");
                    onlineAudioFiles.Add(name);
                }
            }

            StartCoroutine(DownloadAllAudioFiles());
        }
        //audioInformationRequest.downloadHandler.Dispose();

    }



    IEnumerator DownloadAllAudioFiles()
    {
        print("Downloading Audio Assets via WWW");

        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            print("Downloads only work in WebGL");
            StopAllCoroutines();
        }

        foreach (string fileName in onlineAudioFiles)
        {
            string uri = downloadPath + fileName;
            print(uri);


            WWW www = new WWW(uri);

            yield return www;

            if (www.error != null)
            {
                print("Failed to download audio");
            }
            else
            {

                AudioClip clip = www.GetAudioClip(false, false, AudioType.MPEG);

                Debug.Log("Loading audio over");
                print("Successfully downloaded: " + fileName);
                playback.AddClip(clip, fileName);
            }
        }
    }

    //public void SendBytesToConvert(byte[] wav)
    //{
    //    StartCoroutine(ConvertWavBytes(wav));
    //}

    //internal void SendFileToConvert(string wavFile)
    //{
    //    StartCoroutine(ConvertWavFile(wavFile));
    //}

    //private IEnumerator ConvertWavFile(string wavFile)
    //{

    //    string convertURL = "https://parallel-responsible-snowboard.glitch.me/convert";

    //    WWWForm form = new WWWForm();

    //    byte[] data = File.ReadAllBytes(wavFile);

    //    form.AddBinaryData("wavBytes", data, "wavByteFile.wav", "audio/wav");

    //    using (UnityWebRequest uwr = UnityWebRequest.Post(convertURL, form))
    //    {
    //        uwr.SetRequestHeader("Access-Control-Allow-Credentials", "true");
    //        uwr.SetRequestHeader("Access-Control-Allow-Headers", "Accept, Content-Type, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
    //        uwr.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, PUT, OPTIONS");
    //        uwr.SetRequestHeader("Access-Control-Allow-Origin", "*");
    //        uwr.downloadHandler = new DownloadHandlerBuffer();

    //        yield return uwr.SendWebRequest();

    //        if (uwr.isNetworkError || uwr.isHttpError)
    //        {
    //            print(uwr.error);
    //        }
    //        else
    //        {
    //            print("Finished Uploading Wave File, Converted MP3:");
    //            print("Length of Data: " + uwr.downloadHandler.text.Length.ToString());
    //            print(uwr.downloadHandler.data);
    //            print(uwr.downloadHandler.text);

    //            MP3Info mp3inf = JsonUtility.FromJson<MP3Info>(uwr.downloadHandler.text);

    //            print("JSON Parsed");
    //            print(mp3inf.status);
    //            print(mp3inf.buffer.data);

    //            audioFilesManager.SetMP3Bytes(mp3inf.buffer.data);

    //        }
    //    }
    //}

    //private IEnumerator ConvertWavBytes(byte[] wav)
    //{
    //    yield return null;
    ///*
    //    string convertURL = "https://parallel-responsible-snowboard.glitch.me/convert";

    //    WWWForm form = new WWWForm();

        
    //    form.AddBinaryData("wavBytes", wav, "wavByteFile.wav", "audio/wav");

    //    using (UnityWebRequest uwr = UnityWebRequest.Post(convertURL, form))
    //    {
    //        uwr.downloadHandler = new DownloadHandlerBuffer();

    //        yield return uwr.SendWebRequest();

    //        if (uwr.isNetworkError || uwr.isHttpError)
    //        {
    //            print(uwr.error);
    //        }
    //        else
    //        {
    //            print("Finished Uploading Wave File, Converted MP3:");
    //            //print(uwr.downloadHandler.data);
    //            print("Length of Data: " + uwr.downloadHandler.text.Length.ToString()) ;


    //            MP3Info mp3inf = JsonUtility.FromJson<MP3Info>(uwr.downloadHandler.text);

    //            print("JSON Parsed");
    //            print(mp3inf.status);
    //            print(mp3inf.data.Length);


    //            audioNetwork.SetMP3Bytes(mp3inf.data);
    //        }
    //    }
    //*/
    //}

    //public void SendAudioFileToServer(byte[] audioBytes, string fileName)
    //{
    //    StartCoroutine(SendBytesAsPostRequestToServer(audioBytes, fileName));
    //}

    //IEnumerator SendBytesAsPostRequestToServer(byte[] audioBytes, string fileName)
    //{

    //    print("Attempting to upload audio bytes");
    //    string uri = uriPath + "file-upload.php";
    //    print("URI: " + uri);
    //    // Create a Web Form
    //    WWWForm form = new WWWForm();
    //    form.AddBinaryData("fileToUpload", audioBytes, fileName, "audio/mp3");

    //    print(form.headers.ToString());
    //    print(form.data.ToString());

    //    using (UnityWebRequest uwr = UnityWebRequest.Post(uri, form))
    //    {
    //        StartCoroutine(TrackUploadProgress(uwr));
    //        print("Sending: " + uwr.url);
    //        yield return uwr.SendWebRequest();
    //        if (uwr.isNetworkError || uwr.isHttpError)
    //        {
    //            print(uwr.error);
    //        }
    //        else
    //        {
    //            print("Finished Uploading Audio File");
    //            audioFilesManager.OnSuccessfulUpload();
    //        }
    //    }
    //}

    //IEnumerator TrackUploadProgress(UnityWebRequest uwr)
    //{
    //    audioUIManager.UpdateUploadProgress(uwr.uploadProgress);
    //    yield return null;
    //}
}


[System.Serializable]
public class MP3Info
{
    public int status;
    public MP3DataBuffer buffer;
}

[Serializable]
public class MP3DataBuffer
{
    public string type;
    public byte[] data;
}