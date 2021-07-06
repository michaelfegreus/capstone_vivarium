using UnityEngine;
using System;

[Serializable]
public class DayCycleRange
{
    //This is the first constructor for the class
    //and is not inherited by any derived classes.
    public DayCycleRange()
    {

    }

    public GameTime startTime;
    public GameTime peakStartTime;
    public GameTime peakEndTime;
    public GameTime endTime;


    [SerializeField]
    private bool activeHours = false;
    [SerializeField]
    private bool rolloverClock = false;
    [SerializeField]
    private bool rolloverPeak = false;
    private GameTime gameTime1;
    private GameTime setPeakEndTime1;
    private GameTime gameTime2;
    private GameTime setEndTime1;

    public bool IsActiveHours()
    {
        activeHours = false;

        // To check if the start to end rolls over midnight
        if (!endTime.TimeMet(startTime))
        {
            rolloverClock = true;
        }
        // To check if the peak rolls over midnight
        if (!peakEndTime.TimeMet(peakStartTime))
        {
            rolloverPeak = true;
        }
        if (rolloverClock && !rolloverPeak)
        {
            Debug.LogError("If the event's clock rolls over midnight, the peak should too. I didn't program it to do otherwise.");
        }

        // If Start Time reached
        if (!rolloverClock)
        {
            if (GAME_clock_manager.Instance.inGameTime.TimeMet(startTime))
            {
                // If End Time not reached
                if (!GAME_clock_manager.Instance.inGameTime.TimeMet(endTime))
                {
                    activeHours = true;
                }
            }
        }
        else
        {
            if (GAME_clock_manager.Instance.inGameTime.TimeMet(startTime))
            {
                activeHours = true;
            }
            else if (!GAME_clock_manager.Instance.inGameTime.TimeMet(endTime))
            {
                activeHours = true;
            }
        }


        return activeHours;
    }

    public bool IsCurrentlyPeakTime()
    {
        bool atPeakTime = false;
        // If the world time is at least the start of the Peak, but not at the end of it.
        if (!rolloverPeak)
        {
            if (GAME_clock_manager.Instance.inGameTime.TimeMet(peakStartTime) && !GAME_clock_manager.Instance.inGameTime.TimeMet(peakEndTime))
            {
                atPeakTime = true;
            }
        }
        else
        {
            if (GAME_clock_manager.Instance.inGameTime.TimeMet(peakStartTime))
            {
                atPeakTime = true;
            }
            else if (!GAME_clock_manager.Instance.inGameTime.TimeMet(peakEndTime))
            {
                atPeakTime = true;
            }
        }
        return atPeakTime;
    }

    public bool IsRisingHours()
    {
        bool risingHours = false;

        // Check if it's greater than start time, less than peak time.
        if (GAME_clock_manager.Instance.inGameTime.TimeMet(startTime) && !GAME_clock_manager.Instance.inGameTime.TimeMet(peakStartTime))
        {
            risingHours = true;
        }
        return risingHours;
    }
    public bool IsFallingHours()
    {
        bool fallingHours = false;
        // Check if it's greater than peak time, less than end time.
        if (GAME_clock_manager.Instance.inGameTime.TimeMet(peakEndTime) && !GAME_clock_manager.Instance.inGameTime.TimeMet(endTime))
        {
            fallingHours = true;
        }
        return fallingHours;
    }
}