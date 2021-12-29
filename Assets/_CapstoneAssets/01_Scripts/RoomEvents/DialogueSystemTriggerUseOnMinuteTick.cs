using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class DialogueSystemTriggerUseOnMinuteTick : MonoBehaviour
{
    [Tooltip("This will auto-populate with TimeDialogueSystemTriggers that are set as children to this object, so no need to manually fill it.")]
    [SerializeField] TimeDialogueSystemTrigger[] dialogueSystemTriggers;

    [Tooltip("Once the Lua conditions have been met in the Conditions section of the component, stop checking them on the Minute Tick.")]
    [SerializeField] bool disableCheckOnSuccessfulFire = true;

    // Cache a reference to the GameManager's clock
    protected ClockManager gameClockManager;

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

    private void Start()
    {
        gameClockManager = GameManager.Instance.clockManager;

        dialogueSystemTriggers = GetComponentsInChildren<TimeDialogueSystemTrigger>();
    }

    // TODO: You should reset all successful fire flags on the DS Triggers when the clock proceeeds to the next day, in case the player stands in a room overnight and
    // the room events need to reset for the next day.

    public void TryStartDialogueSystemTriggers()
    {
        foreach(TimeDialogueSystemTrigger trigger in dialogueSystemTriggers)
        {
            // Make sure it hasn't already successfully fired before trying to start it up again, if that's a relevant check
            if (disableCheckOnSuccessfulFire)
            {
                if (!trigger.firedSuccessfully)
                {
                    trigger.TryStart(null);
                }
            }
            else
            {
                trigger.TryStart(null);
            }
        }
    }
}
