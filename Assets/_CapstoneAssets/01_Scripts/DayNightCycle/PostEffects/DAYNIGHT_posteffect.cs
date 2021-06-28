using UnityEngine;

public class DAYNIGHT_posteffect : MonoBehaviour
{
    // Format DayTimePostProfile properly in the inspector by running the custom class' own validation code.
    [ExecuteInEditMode]
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            for (int i = 0; i < daylightProfiles.Length; i++)
            {
                daylightProfiles[i].startTime.OnValidate();
                daylightProfiles[i].peakStartTime.OnValidate();
                daylightProfiles[i].peakEndTime.OnValidate();
                daylightProfiles[i].endTime.OnValidate();
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
    public DayCycleRangePostProfile[] daylightProfiles;

    // Floats to divide as a proportion to find the weight of the profile.
    float start;
    float goal;
    float current;

    //DayTimePostProfile.DayTimePostProfileActiveHours currentProfileActivity;

    public void DaylightMinuteUpdate()
    {
        //Debug.Log("Post script delegate called.");
        
        for(int i = 0; i < daylightProfiles.Length; i++)
        {
            if (daylightProfiles[i].IsActiveHours())
            {
               // Debug.Log(daylightProfiles[i].postVolume.sharedProfile.name + " profile is active");

                if (daylightProfiles[i].IsCurrentlyPeakTime())
                {
                    daylightProfiles[i].postVolume.weight = 1f;
                }
                else
                {
                    if (daylightProfiles[i].IsRisingHours())
                    {
                        // Set up proportion based on Peak Time
                        // ((CURRENT - START) / (GOAL - START))
                        start = start = daylightProfiles[i].startTime.ThisTimeInMinutes();
                        goal = daylightProfiles[i].peakStartTime.ThisTimeInMinutes();
                        current = GAME_clock_manager.Instance.inGameTime.ThisTimeInMinutes();

                        daylightProfiles[i].postVolume.weight = (current - start) / (goal - start) ;

                        //profileWeightProportionA = daylightProfiles[i].peakTime.ThisTimeInMinutes() - WORLD_time_manager.Instance.inGameTime.ThisTimeInMinutes(); // end - current
                        //profileWeightProportionB = daylightProfiles[i].peakTime.ThisTimeInMinutes(); // end
                        // Debug.Log(daylightProfiles[i].postVolume.sharedProfile.name + " is rising. Rising hours proportion A: " + profileWeightProportionA + "    Rising hours proportion B: " + profileWeightProportionB);
                        //daylightProfiles[i].postVolume.weight = profileWeightProportionA / profileWeightProportionB;
                    }
                    else if (daylightProfiles[i].IsFallingHours())
                    {
                        // Set up proportion based on End Time
                        // 1- ((CURRENT - START) / (GOAL - START))
                        start = daylightProfiles[i].peakEndTime.ThisTimeInMinutes();
                        goal = daylightProfiles[i].endTime.ThisTimeInMinutes();
                        current = GAME_clock_manager.Instance.inGameTime.ThisTimeInMinutes();

                        daylightProfiles[i].postVolume.weight = 1 - (current - start) / (goal - start);
                        //profileWeightProportionA = WORLD_time_manager.Instance.inGameTime.ThisTimeInMinutes() - daylightProfiles[i].peakTime.ThisTimeInMinutes(); // current - start
                        //profileWeightProportionB = daylightProfiles[i].endTime.ThisTimeInMinutes(); // end
                        // Debug.Log(daylightProfiles[i].postVolume.sharedProfile.name + " is falling. Falling hours proportion A: " + profileWeightProportionA + "    Falling hours proportion B: " + profileWeightProportionB);
                        // daylightProfiles[i].postVolume.weight = 1f - ( profileWeightProportionA / profileWeightProportionB);
                    }
                }
            }
            else { 
                daylightProfiles[i].postVolume.weight = 0f;
            }

            // Disable the Volume component if its weight is zero.
            if (daylightProfiles[i].postVolume.weight == 0f)
            {
                daylightProfiles[i].postVolume.enabled = false;
            }
            else
            {
                daylightProfiles[i].postVolume.enabled = true;
            }

            /*

            // ENUMS Check the activity, set it to do something in that.
            currentProfileActivity = daylightProfiles[i].CurrentActiveHours();
            switch (currentProfileActivity)
            {
                case DayTimePostProfile.DayTimePostProfileActiveHours.Inactive:
                    {
                        // If it's not in its active hours, turn off its weight, and disable it as well.
                        daylightProfiles[i].postVolume.weight = 0f;
                        daylightProfiles[i].postVolume.enabled = false;
                        break;
                    }
                case DayTimePostProfile.DayTimePostProfileActiveHours.Peak:
                    {
                        daylightProfiles[i].postVolume.enabled = true;

                        break;
                    }
            }*/

            /*
            // If it's not in its active hours, turn off its weight, and disable it as well.
            if(daylightProfiles[i].IsActiveHours() == false)
            {
                daylightProfiles[i].postVolume.weight = 0f;
                daylightProfiles[i].postVolume.enabled = false;
            }

            // If it's in active hours, enable it
            if (daylightProfiles[i].IsActiveHours() == true)
            {
                daylightProfiles[i].postVolume.enabled = true;

                // If it's peak time, set weight to full.
                if (daylightProfiles[i].IsCurrentlyPeakTime())
                {
                    daylightProfiles[i].postVolume.weight = 1f;
                }
                // Otherwise, check where in between it is.
            }
            */
            //Debug.Log("Is active hours?: " + daylightProfiles[i].IsActiveHours());

            /*
            // If it's Peak Time --
            if (WORLD_time_manager.Instance.inGameTime.TimeEquals(daylightProfiles[i].peakTime))
            {
                //daylightProfiles[i].postProfile;
            }
            // If Start Time is reached, but not end time
            if (WORLD_time_manager.Instance.inGameTime.TimeMet(daylightProfiles[i].startTime))
            {
                //Debug.Log("Time met.");

            }*/
        }
    }

}
