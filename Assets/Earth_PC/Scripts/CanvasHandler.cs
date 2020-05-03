using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanvasHandler : MonoBehaviour
{

    [SerializeField] UICanvasGroupFader introGroup;
    [SerializeField] UICanvasGroupFader placementGroup;
    [SerializeField] UICanvasGroupFader recordingGroup;
    [SerializeField] UICanvasGroupFader listeningGroup;


    [Space]

    [SerializeField] UnityEvent OnIntroActivation;
    [SerializeField] UnityEvent OnPlacementActivation;
    [SerializeField] UnityEvent OnRecordingActivation;
    [SerializeField] UnityEvent OnListeningActivation;
    public void ChangeCanvasToIntro()
    {
        OnIntroActivation.Invoke();

    }
    public void ActivatePlacementUI()
    {
        OnPlacementActivation.Invoke();
    }

    public void ActivateRecordingUI()
    {
        OnRecordingActivation.Invoke();
    }

    public void ActivateListeningUI()
    {
        OnListeningActivation.Invoke();
    }

    // Update is called once per frame
    void Start()
    {
        ChangeCanvasToIntro();
    }
}
