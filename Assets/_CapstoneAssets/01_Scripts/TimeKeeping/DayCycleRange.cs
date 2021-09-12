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
    /*
    // Format DayTimePostProfile properly in the inspector by running the custom class' own validation code.
    [ExecuteInEditMode]
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
                startTime.OnValidate();
                peakStartTime.OnValidate();
                peakEndTime.OnValidate();
                endTime.OnValidate();
        }
    }
    */
    public GameTime startTime;
    public GameTime peakStartTime;
    public GameTime peakEndTime;
    public GameTime endTime;

    //[SerializeField]
    [Tooltip("Set this on if the clock rolls over from one day to the next. NOTE: You need to check both of these at the moment to make this work.")]
    private bool rolloverClock = false;
    [Tooltip("Set this if the peak rolls over from one day to the next. NOTE: You need to check both of these at the moment to make this work.")]
    private bool rolloverPeak = false;

    [SerializeField]
    [Tooltip("How close to the peak of the day time period.")]
    private float currentDaytimeVolume;
    //[SerializeField]
    private bool activeHours = false;

    // Subscribe to minute tick updates.
    public void SubscribeToMinuteTick()
    {
        ClockManager.OnMinuteTick += DaylightMinuteUpdate;
    }

    public void UnsubscribeFromMinuteTick()
    {
        ClockManager.OnMinuteTick -= DaylightMinuteUpdate;
    }

    public float GetCurrentVolume()
    {
        return currentDaytimeVolume;
    }

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
            if (ClockManager.Instance.inGameTime.TimeMet(startTime))
            {
                // If End Time not reached
                if (!ClockManager.Instance.inGameTime.TimeMet(endTime))
                {
                    activeHours = true;
                }
            }
        }
        else
        {
            if (ClockManager.Instance.inGameTime.TimeMet(startTime))
            {
                activeHours = true;
            }
            else if (!ClockManager.Instance.inGameTime.TimeMet(endTime))
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
            if (ClockManager.Instance.inGameTime.TimeMet(peakStartTime) && !ClockManager.Instance.inGameTime.TimeMet(peakEndTime))
            {
                atPeakTime = true;
            }
        }
        else
        {
            if (ClockManager.Instance.inGameTime.TimeMet(peakStartTime))
            {
                atPeakTime = true;
            }
            else if (!ClockManager.Instance.inGameTime.TimeMet(peakEndTime))
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
        if (ClockManager.Instance.inGameTime.TimeMet(startTime) && !ClockManager.Instance.inGameTime.TimeMet(peakStartTime))
        {
            risingHours = true;
        }
        return risingHours;
    }
    public bool IsFallingHours()
    {
        bool fallingHours = false;
        // Check if it's greater than peak time, less than end time.
        if (ClockManager.Instance.inGameTime.TimeMet(peakEndTime) && !ClockManager.Instance.inGameTime.TimeMet(endTime))
        {
            fallingHours = true;
        }
        return fallingHours;
    }

    // Floats to divide as a proportion to find the weight of the profile.
    float start;
    float goal;
    float current;

    public void DaylightMinuteUpdate()
    {

        if (IsActiveHours())
        {

            if (IsCurrentlyPeakTime())
            {
                currentDaytimeVolume = 1f;
            }
            else
            {
                if (IsRisingHours())
                {
                    // Set up proportion based on Peak Time
                    // ((CURRENT - START) / (GOAL - START))
                    start = start = startTime.ThisTimeInMinutes();
                    goal = peakStartTime.ThisTimeInMinutes();
                    current = ClockManager.Instance.inGameTime.ThisTimeInMinutes();
                   // Debug.Log(current);

                    currentDaytimeVolume = (current - start) / (goal - start);
                }
                else if (IsFallingHours())
                {
                    //Debug.Log("Falliong hours");

                    // Set up proportion based on End Time
                    // 1- ((CURRENT - START) / (GOAL - START))
                    start = peakEndTime.ThisTimeInMinutes();
                    goal = endTime.ThisTimeInMinutes();
                    current = ClockManager.Instance.inGameTime.ThisTimeInMinutes();

                    currentDaytimeVolume = 1 - (current - start) / (goal - start);
                }
            }
        }
        else
        {
            currentDaytimeVolume = 0f;
        }
    }
}
