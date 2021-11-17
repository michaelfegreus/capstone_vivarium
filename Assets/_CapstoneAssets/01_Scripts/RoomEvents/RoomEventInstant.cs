using UnityEngine;
using PixelCrushers.DialogueSystem;

public class RoomEventInstant : RoomEvent
{
    [Header("This event will take place once instantly when its time conditions are met.")]

    [SerializeField] DialogueSystemTrigger eventDialogueSystemTrigger;

    [Tooltip("The singular time this event will fire off at.")]
    [SerializeField] GameTime eventStartTime;

    // Format GameTime properly in the inspector by running the custom class' own validation code.
    [ExecuteInEditMode]
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            eventStartTime.OnValidate();
        }
    }

    // Be sure to check events OnEnable so you dont walk into a room and the event starts after waiting for the minute tick
    private void OnEnable()
    {
        CheckEventTime();
    }

    public void SetEventTime(GameTime newTime)
    {
        eventStartTime = newTime;
    }

    public override void CheckEventTime()
    {
        base.CheckEventTime();

        // If the time of the event matches the current game time, run the Dialogue System Trigger's OnUse() function to check relevant Quest and DS variabes.
        if (gameClockManager.inGameTime.TimeEquals(eventStartTime))
        {
            eventDialogueSystemTrigger.OnUse();

            EventDebugLog();
        }
    }
}