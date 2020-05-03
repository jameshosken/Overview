using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggler : MonoBehaviour
{
    //[SerializeField] GameObject UIPanel;

    [SerializeField] GameObject[] UIPanels;

    [SerializeField] Button[] buttonsToDisable;
    DisablePlaybackOnToggle audioDisabler; 
    // Start is called before the first frame update

    public bool status = false;

    bool turnOffForever = false;

    Text text;
    string originalText;
    private void Start()
    {
        text = GetComponentInChildren<Text>();
        originalText = text.text;

        foreach (GameObject UIPanel in UIPanels)
        {
            UIPanel.SetActive(status);
        }

        audioDisabler = GetComponent<DisablePlaybackOnToggle>();
    }


    public void Toggle()
    {
        if (turnOffForever) return;


        
        status = !status;

        if (status)
        {
            text.text = "Close";
        }
        else
        {
            text.text = originalText;
        }

        foreach(GameObject UIPanel in UIPanels)
        {
            UIPanel.SetActive(status);
        }

        foreach(Button b in buttonsToDisable)
        {
            b.interactable = !status;
        }

        if(audioDisabler!= null)
        {
            if(status == false)
            {
                audioDisabler.DisableAudio();
            }
        }
    }

    public void ForceToggleOff()
    {
        if (turnOffForever) return;

        status = false;
        foreach (GameObject UIPanel in UIPanels)
        {
            UIPanel.SetActive(status);
        }

        foreach (Button b in buttonsToDisable)
        {
            b.interactable = !status;
        }

        if (audioDisabler != null)
        {
            audioDisabler.DisableAudio();   
        }
    }

    public void ForceToggleOn()
    {
        if (turnOffForever) return;

        status = true;
        foreach (GameObject UIPanel in UIPanels)
        {
            UIPanel.SetActive(status);
        }

        foreach (Button b in buttonsToDisable)
        {
            b.interactable = !status;
        }
    }

    public void TurnOffForever()
    {
        turnOffForever = true;
    }
}
