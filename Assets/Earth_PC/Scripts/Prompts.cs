using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prompts : MonoBehaviour
{
    List<string> AllPrompts = new List<string>();

    int currentIdx = 0;
    private void Awake()
    {

        AllPrompts.Add("Has isolation allowed you to do\nsomething you otherwise wouldn't?");
        AllPrompts.Add("Has anything surprised you about social distancing?");
        AllPrompts.Add("Has anything made you happy today?");
        AllPrompts.Add("What's your most recent 'win'?");
        AllPrompts.Add("What has been your favourite part of your isolated days?");
        AllPrompts.Add("What have you struggled with the most this week?");
        AllPrompts.Add("What has been frustrating\nWhat has been delightful?");
        AllPrompts.Add("Are you where you thought you would be this week?");
        AllPrompts.Add("What has changed for you? \nWhat has stayed the same?");
        AllPrompts.Add("What/who are you grateful for?");
    }

    public string GetRandomPrompt()
    {
        int idx = Mathf.FloorToInt(Random.Range(0, AllPrompts.Count));
        currentIdx = idx;
        return AllPrompts[idx];
    }

    public string GetNextPrompt()
    {
        currentIdx = (currentIdx + 1) % AllPrompts.Count;
        return AllPrompts[currentIdx];
    }

    public string GetPreviousPrompt()
    {
        currentIdx = (currentIdx - 1) % AllPrompts.Count;

        return AllPrompts[currentIdx];
    }


}
