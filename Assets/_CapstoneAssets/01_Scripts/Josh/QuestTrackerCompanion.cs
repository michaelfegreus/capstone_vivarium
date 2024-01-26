using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerCompanion : MonoBehaviour
{
    // variables
    [SerializeField] CanvasGroup questGroup; // the UI canvas group for our quests
    [SerializeField] float targetAlpha = 1, fadeRate = 1;
    public bool desireShowQuests = true; // this is a public boolean which tracks the status of the menu checkbox

    // public functions for showing / hiding the active quests
    public void ShowQuests()
    {
        // only show our quests if we have told the game we want to see them
        if (desireShowQuests)
            targetAlpha = 1f;
    }

    // hide the quests
    public void HideQuests()
    {
        targetAlpha = 0;
    }

    public void ToggleQuests()
    {
        Debug.Log("setting quest toggle...");

        if (targetAlpha == 1)
            HideQuests();
        else if (targetAlpha == 0)
            ShowQuests();
    }

    // run the fixed update 60 times per second
    void FixedUpdate()
    {
        // fade to our desired canvas alpha state
        questGroup.alpha = Mathf.MoveTowards(questGroup.alpha, targetAlpha, fadeRate * Time.deltaTime);
    }
}
