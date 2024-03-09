using UnityEngine;
using PixelCrushers.DialogueSystem;

public class RoomEventStartWindow : RoomEvent
{

    [SerializeField] private bool runOnlyOnceDaily;
    [Header("This event will start anytime within this A-to-B time frame window. After it's started, the event will not fire again until the component is disabled and re-enabled.")]

    [SerializeField] DialogueSystemTrigger eventDialogueSystemTrigger;

    // TODO: Set up a system to make event windows that rollover with the clock into the next day.
    [Tooltip("The start of the window of time this event will fire off at.")]
    [SerializeField] GameTime startTimeWindowA;
    [Tooltip("The end of the window of time this event will fire off at.")]
    [SerializeField] GameTime startTimeWindowB;

    bool ranEvent;
    bool clockRollover;

    // Format GameTime properly in the inspector by running the custom class' own validation code.
    [ExecuteInEditMode]
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            startTimeWindowA.OnValidate();
            startTimeWindowB.OnValidate();

            CheckClockRollover();
        }
    }

    private void Start()
    {

    }

    // Be sure to check events OnEnable so you dont walk into a room and the event starts after waiting for the minute tick
    private void OnEnable()
    {
        if (gameClockManager == null)
        {
            gameClockManager = GameManager.Instance.clockManager;
            CheckClockRollover();
        }
        CheckEventTime();
    }

    void CheckClockRollover()
    {
        // If the start of the window is later in the day than the end of the window, assume an event that rolls over into the next day.
        if (startTimeWindowA.TimeMet(startTimeWindowB))
        {
            clockRollover = true;
        }
    }

    public void SetEventTime(GameTime newTimeA, GameTime newTimeB)
    {
        startTimeWindowA = newTimeA;
        startTimeWindowB = newTimeB;
    }

    public void CheckEvent()
    {
        if (gameClockManager == null)
        {
            gameClockManager = GameManager.Instance.clockManager;
            CheckClockRollover();
        }
        CheckEventTime();
    }

    public override void CheckEventTime()
    {
        base.CheckEventTime();

        // If the time of the event matches the current game time, run the Dialogue System Trigger's OnUse() function to check relevant Quest and DS variabes.
        if (!clockRollover)
        {
            if (!ranEvent)
            {
                Debug.Log("Checked time event with clock rollover FALSE");
                if (gameClockManager.inGameTime.TimeMet(startTimeWindowA))
                {
                    if (!gameClockManager.inGameTime.TimeMet(startTimeWindowB))
                    {
                        eventDialogueSystemTrigger.OnUse();
                        if (runOnlyOnceDaily)
                        {
                            ranEvent = true;
                        }

                        EventDebugLog();
                    }
                }
            }
            else
            {
                // Reset the ranEvent flag if we're past the window of time.
                if (gameClockManager.inGameTime.TimeMet(startTimeWindowB))
                {
                    ranEvent = false;
                }
            }
        }
        // This is what we should do instead if the event rolls into the next day.
        else
        {
            if (!ranEvent)
            {
                Debug.Log("Checked time event with clock rollover TRUE");
                if (gameClockManager.inGameTime.TimeMet(startTimeWindowA))
                {
                    eventDialogueSystemTrigger.OnUse();
                    ranEvent = true;

                    EventDebugLog();
                }
                // Alternatively, if time hasn't reached the end of the rolled over window.
                else if (gameClockManager.inGameTime.TimeMet(startTimeWindowB) == false) 
                {
                    eventDialogueSystemTrigger.OnUse();
                    ranEvent = true;

                    EventDebugLog();
                }
            }
            else
            {
                // If the game time is between the two clock windows, now reset the ran event flag.
                if(gameClockManager.inGameTime.TimeMet(startTimeWindowB) && !gameClockManager.inGameTime.TimeMet(startTimeWindowA))
                {
                    ranEvent = false;
                }
            }
        }
    }
}