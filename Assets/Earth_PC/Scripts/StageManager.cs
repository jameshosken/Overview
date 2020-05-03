using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//public enum Stage
//{
//    INTRO,
//    PLACE,
//    RECORD,
//    LISTEN,
//    WAITINGROOM
//}
public class StageManager : MonoBehaviour
{
    //public Stage stage;

    MyLocationHandler myLocation;

    [SerializeField] UnityEvent StartIntroStage;

    [SerializeField] UnityEvent StartPlacementStage;

    [SerializeField] UnityEvent StartRecordingStage;

    //[SerializeField] UnityEvent StartListeningStage;


    void Start()
    {
        myLocation = FindObjectOfType<MyLocationHandler>();
    }
    public void AttemptIntroStage()
    {
        StartIntroStage.Invoke();
    }
    public void AttemptPlacementStage()
    {
        StartPlacementStage.Invoke();
    }

    //Todo: Update this system to add user message if location not yet placed
    public void AttemptRecordingStage()
    {
        if (myLocation.IsLocationPlaced())
        {
            StartRecordingStage.Invoke();
            //return true;
        }
        //return false;
    }

    //public void AttemptListeningStage()
    //{
    //    StartListeningStage.Invoke();
    //}
}
