using UnityEngine;

public class DAYNIGHT_ambientsound : MonoBehaviour
{
    // Format DayTimePostProfile properly in the inspector by running the custom class' own validation code.
    [ExecuteInEditMode]
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            for (int i = 0; i < timeProfiles.Length; i++)
            {
                timeProfiles[i].startTime.OnValidate();
                timeProfiles[i].peakStartTime.OnValidate();
                timeProfiles[i].peakEndTime.OnValidate();
                timeProfiles[i].endTime.OnValidate();
            }

        }
    }

    // NOTE: I'm coding this in a way that's not super scalable right now. So you may want to create a more generic way to affect or override post. Anyway, let us begin.
    public DayCycleRangePostProfile[] timeProfiles;

    // Floats to divide as a proportion to find the weight of the profile.
    float start;
    float goal;
    float current;

    //DayTimePostProfile.DayTimePostProfileActiveHours currentProfileActivity;

    public void DaylightMinuteUpdate()
    {
        //Debug.Log("Post script delegate called.");

        for (int i = 0; i < timeProfiles.Length; i++)
        {
            if (timeProfiles[i].IsActiveHours())
            {
                // Debug.Log(daylightProfiles[i].postVolume.sharedProfile.name + " profile is active");

                if (timeProfiles[i].IsCurrentlyPeakTime())
                {
                    timeProfiles[i].postVolume.weight = 1f;
                }
                else
                {
                    if (timeProfiles[i].IsRisingHours())
                    {
                        // Set up proportion based on Peak Time
                        // ((CURRENT - START) / (GOAL - START))
                        start = start = timeProfiles[i].startTime.ThisTimeInMinutes();
                        goal = timeProfiles[i].peakStartTime.ThisTimeInMinutes();
                        current = GAME_clock_manager.Instance.inGameTime.ThisTimeInMinutes();

                        timeProfiles[i].postVolume.weight = (current - start) / (goal - start);
                        
                    }
                    else if (timeProfiles[i].IsFallingHours())
                    {
                        // Set up proportion based on End Time
                        // 1- ((CURRENT - START) / (GOAL - START))
                        start = timeProfiles[i].peakEndTime.ThisTimeInMinutes();
                        goal = timeProfiles[i].endTime.ThisTimeInMinutes();
                        current = GAME_clock_manager.Instance.inGameTime.ThisTimeInMinutes();

                        timeProfiles[i].postVolume.weight = 1 - (current - start) / (goal - start);
                    }
                }
            }
            else
            {
                timeProfiles[i].postVolume.weight = 0f;
            }

            // Disable the Volume component if its weight is zero.
            if (timeProfiles[i].postVolume.weight == 0f)
            {
                timeProfiles[i].postVolume.enabled = false;
            }
            else
            {
                timeProfiles[i].postVolume.enabled = true;
            }
        }
    }

}
