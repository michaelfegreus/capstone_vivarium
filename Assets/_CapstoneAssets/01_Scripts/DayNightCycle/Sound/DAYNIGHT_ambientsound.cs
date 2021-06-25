using UnityEngine;

public class DAYNIGHT_ambientsound : MonoBehaviour
{
    // Format DayTimePostProfile properly in the inspector by running the custom class' own validation code.
    [ExecuteInEditMode]
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            for (int i = 0; i < ambientLoops.Length; i++)
            {
                ambientLoops[i].startTime.OnValidate();
                ambientLoops[i].peakStartTime.OnValidate();
                ambientLoops[i].peakEndTime.OnValidate();
                ambientLoops[i].endTime.OnValidate();
            }

        }
    }

    // Subscribe to minute tick updates.
    private void OnEnable()
    {
        GAME_clock_manager.OnMinuteTick += DaylightMinuteUpdate;
    }

    private void OnDisable()
    {
        GAME_clock_manager.OnMinuteTick -= DaylightMinuteUpdate;
    }


    // NOTE: I'm coding this in a way that's not super scalable right now. So you may want to create a more generic way to affect or override post. Anyway, let us begin.
    public DayCycleAmbientSound[] ambientLoops;

    // Floats to divide as a proportion to find the weight of the profile.
    float start;
    float goal;
    float current;

    //DayTimePostProfile.DayTimePostProfileActiveHours currentProfileActivity;

    public void DaylightMinuteUpdate()
    {
        //Debug.Log("Post script delegate called.");

        for (int i = 0; i < ambientLoops.Length; i++)
        {
            if (ambientLoops[i].IsActiveHours())
            {
                // Debug.Log(daylightProfiles[i].postVolume.sharedProfile.name + " profile is active");

                if (ambientLoops[i].IsCurrentlyPeakTime())
                {
                    ambientLoops[i].ambientGroup.groupMasterVolume = 1f;
                }
                else
                {
                    if (ambientLoops[i].IsRisingHours())
                    {
                        // Set up proportion based on Peak Time
                        // ((CURRENT - START) / (GOAL - START))
                        start = start = ambientLoops[i].startTime.ThisTimeInMinutes();
                        goal = ambientLoops[i].peakStartTime.ThisTimeInMinutes();
                        current = GAME_clock_manager.Instance.inGameTime.ThisTimeInMinutes();

                        ambientLoops[i].ambientGroup.groupMasterVolume = (current - start) / (goal - start);
                        
                    }
                    else if (ambientLoops[i].IsFallingHours())
                    {
                        // Set up proportion based on End Time
                        // 1- ((CURRENT - START) / (GOAL - START))
                        start = ambientLoops[i].peakEndTime.ThisTimeInMinutes();
                        goal = ambientLoops[i].endTime.ThisTimeInMinutes();
                        current = GAME_clock_manager.Instance.inGameTime.ThisTimeInMinutes();

                        ambientLoops[i].ambientGroup.groupMasterVolume = 1 - (current - start) / (goal - start);
                    }
                }
            }
            else
            {
                ambientLoops[i].ambientGroup.groupMasterVolume = 0f;
            }
        }
    }

}
