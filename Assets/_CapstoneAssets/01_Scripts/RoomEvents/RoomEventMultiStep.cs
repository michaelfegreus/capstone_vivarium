using UnityEngine;

/// <summary>
/// The purpose of this script is just to be a container for other event scripts.
/// You might want to do something where a character shows up on the start event, plays an animation on the mid event, and then exits scene on conclusion event.
/// </summary>
public class RoomEventMultiStep : MonoBehaviour
{
    [Header("--- Start ---")]
    [Tooltip("Start event that will fire instantly when the current time matches this time.")]
    [SerializeField] RoomEventInstant startEvent;
    [SerializeField] GameTime startEventTime;

    [Header("--- Mid ---")]
    [Tooltip("Event that will fire once during its window.")]
    [SerializeField] RoomEventStartWindow midEvent;
    [Tooltip("The start of the window of time this event will fire off at.")]
    [SerializeField] GameTime midEventTimeWindowA;
    [Tooltip("The end of the window of time this event will fire off at.")]
    [SerializeField] GameTime midEventTimeWindowB;

    [Header("--- Conclusion ---")]
    [Tooltip("End event that will fire instantly when the last.")]
    [SerializeField] RoomEventInstant conclusionEvent;
    [SerializeField] GameTime conclusionEventTime;

    // Format GameTime properly in the inspector by running the custom class' own validation code.
    [ExecuteInEditMode]
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            startEventTime.OnValidate();
            midEventTimeWindowA.OnValidate();
            midEventTimeWindowB.OnValidate();
            conclusionEventTime.OnValidate();

            startEvent.SetEventTime(startEventTime);
            midEvent.SetEventTime(midEventTimeWindowA, midEventTimeWindowB);
            conclusionEvent.SetEventTime(conclusionEventTime);
        }
    }

    private void Start()
    {
        startEvent.SetEventTime(startEventTime);
        midEvent.SetEventTime(midEventTimeWindowA, midEventTimeWindowB);
        conclusionEvent.SetEventTime(conclusionEventTime);
    }
}
