using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightLight2DIntensity : MonoBehaviour
{
    /*
    // Format DayTimePostProfile properly in the inspector by running the custom class' own validation code.
    [ExecuteInEditMode]
    void OnValidate()
    {
        if (!Application.isPlaying)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].startTime.OnValidate();
                lights[i].peakStartTime.OnValidate();
                lights[i].peakEndTime.OnValidate();
                lights[i].endTime.OnValidate();
            }

        }
    }*/

    // Subscribe to minute tick updates.
    
    private void OnEnable()
    {
        ClockManager.OnMinuteTick += DaylightMinuteUpdate;
    }

    private void OnDisable()
    {
        ClockManager.OnMinuteTick -= DaylightMinuteUpdate;
    }


    Light2D[] morningLights;
    Light2D[] middayLights;
    Light2D[] eveningLights;
    Light2D[] nightLights;

    public Transform morningLightParent;
    public Transform middayLightParent;
    public Transform eveningLightParent;
    public Transform nightLightParent;

    private void Start()
    {
        morningLights = morningLightParent.GetComponentsInChildren<Light2D>();
        middayLights = middayLightParent.GetComponentsInChildren<Light2D>();
        eveningLights = eveningLightParent.GetComponentsInChildren<Light2D>();
        nightLights = nightLightParent.GetComponentsInChildren<Light2D>();
    }

    void DaylightMinuteUpdate()
    {
        if (morningLights.Length != 0)
        {
            foreach (Light2D light in morningLights)
            {
                light.intensity = GameManager.Instance.clockManager.morning.GetCurrentVolume();
            }
        }
        if (middayLights.Length != 0)
        {
            foreach (Light2D light in middayLights)
            {
                light.intensity = GameManager.Instance.clockManager.midday.GetCurrentVolume();
            }
        }
        if (eveningLights.Length != 0)
        {
            foreach (Light2D light in eveningLights)
            {
                light.intensity = GameManager.Instance.clockManager.evening.GetCurrentVolume();
            }
        }
        if (nightLights.Length != 0)
        {
            foreach (Light2D light in nightLights)
            {
                light.intensity = GameManager.Instance.clockManager.night.GetCurrentVolume();
            }
        }
    }

    /*
       // NOTE: I'm coding this in a way that's not super scalable right now. So you may want to create a more generic way to affect or override post. Anyway, let us begin.
       public DayCycleRangeLights2D[] lights;

       // Floats to divide as a proportion to find the weight of the profile.
       float start;
       float goal;
       float current;

       //DayTimePostProfile.DayTimePostProfileActiveHours currentProfileActivity;


       public void DaylightMinuteUpdate()
       {
           //Debug.Log("Post script delegate called.");

           for (int i = 0; i < lights.Length; i++)
           {
               if (lights[i].IsActiveHours())
               {
                   // Debug.Log(daylightProfiles[i].postVolume.sharedProfile.name + " profile is active");

                   if (lights[i].IsCurrentlyPeakTime())
                   {
                       foreach(Light2D item in lights[i].roomLights)
                       {
                           item.intensity = 1f;
                       }
                   }
                   else
                   {
                       if (lights[i].IsRisingHours())
                       {
                           // Set up proportion based on Peak Time
                           // ((CURRENT - START) / (GOAL - START))
                           start = start = lights[i].startTime.ThisTimeInMinutes();
                           goal = lights[i].peakStartTime.ThisTimeInMinutes();
                           current = GAME_clock_manager.Instance.inGameTime.ThisTimeInMinutes();

                           foreach (Light2D item in lights[i].roomLights)
                           {
                               item.intensity = (current - start) / (goal - start);
                           }

                       }
                       else if (lights[i].IsFallingHours())
                       {
                           // Set up proportion based on End Time
                           // 1- ((CURRENT - START) / (GOAL - START))
                           start = lights[i].peakEndTime.ThisTimeInMinutes();
                           goal = lights[i].endTime.ThisTimeInMinutes();
                           current = GAME_clock_manager.Instance.inGameTime.ThisTimeInMinutes();

                           foreach (Light2D item in lights[i].roomLights)
                           {
                               item.intensity = 1 - (current - start) / (goal - start);
                           }
                       }
                   }
               }
               else
               {
                   foreach (Light2D item in lights[i].roomLights)
                   {
                       item.intensity = 0f;
                   }
               }
           }
       }

   }
   */

}