using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerCompanion : MonoBehaviour
{
    // variables
    [SerializeField] CanvasGroup questGroup; // the UI canvas group for our quests
    float targetAlpha, fadeRate = 1;

    // public functions for showing / hiding the active quests
    public void ShowQuests()
    {
        targetAlpha = 1f;
    }

    // hide the quests
    public void HideQuests()
    {
        targetAlpha = 0;
    }

    public void ToggleQuests()
    {
        if (targetAlpha == 1)
            HideQuests();
        if (targetAlpha == 0)
            ShowQuests();
    }

    // run the fixed update 60 times per second
    void FixedUpdate()
    {
        // fade to our desired canvas alpha state
        questGroup.alpha = questGroup.alpha == targetAlpha ? Mathf.MoveTowards(questGroup.alpha, targetAlpha, fadeRate * Time.deltaTime) : questGroup.alpha;
    }
}
