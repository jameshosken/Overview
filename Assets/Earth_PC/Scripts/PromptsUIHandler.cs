using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptsUIHandler : MonoBehaviour
{

    Prompts prompts;
    public Text text;
    string current = "";
    // Start is called before the first frame update
    void Start()
    {
        if(text == null)
        {
            text = GetComponentInChildren<Text>();
        }
        prompts = FindObjectOfType<Prompts>();
        current = prompts.GetRandomPrompt();
        //text.text = current;
    }



    public void NextPrompt()
    {
        current = prompts.GetNextPrompt();
        text.text = current;
    }

    public void PreviousPrompt()
    {
        current = prompts.GetPreviousPrompt();
        text.text = current;
    }

    public void GetNewPrompt()
    {
        string newPrompt = prompts.GetRandomPrompt();
        while (newPrompt == current)
        {
            newPrompt = prompts.GetRandomPrompt();
        }

        current = newPrompt;
        text.text = current;
    }
}
