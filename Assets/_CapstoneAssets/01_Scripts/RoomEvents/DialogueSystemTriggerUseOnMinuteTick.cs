using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueSystemTriggerUseOnMinuteTick : MonoBehaviour
{
    public TimeDialogueSystemTrigger[] dialogueSystemTriggers;

    private void OnEnable()
    {
        foreach (TimeDialogueSystemTrigger trigger in dialogueSystemTriggers)
        {
            // Reset whether the trigger has been fired successfully
            trigger.firedSuccessfully = false;
            // Check everything's conditions right on enabling as well, rather than waiting for the minute tick
            trigger.TryStart(null);
        }
        ClockManager.OnMinuteTick += TryStartDialogueSystemTriggers;
    }

    private void OnDisable()
    {
        ClockManager.OnMinuteTick -= TryStartDialogueSystemTriggers;
    }

    // Cache a reference to the GameManager's clock
    protected ClockManager gameClockManager;

    private void Start()
    {
        gameClockManager = GameManager.Instance.clockManager;

        dialogueSystemTriggers = GetComponentsInChildren<TimeDialogueSystemTrigger>();
    }

    public void TryStartDialogueSystemTriggers()
    {
        foreach(TimeDialogueSystemTrigger trigger in dialogueSystemTriggers)
        {
            // Make sure it hasn't already successfully fired before trying to start it up again
            if (trigger.firedSuccessfully != true)
            {
                trigger.TryStart(null);
            }
        }
    }

}
