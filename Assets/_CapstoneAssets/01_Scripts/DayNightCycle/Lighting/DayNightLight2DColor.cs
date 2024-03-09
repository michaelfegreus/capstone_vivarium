using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Linq;
using System;

public class DayNightLight2DColor : MonoBehaviour
{
    [Tooltip("Defaults to this Game Object at runtime if you didn't enter a different parent.")]
    [SerializeField] Transform lightParent;

    [SerializeField] private Light2D[] lights;

    [SerializeField] Color morning;
    [SerializeField] Color day;
    [SerializeField] Color evening;
    [SerializeField] Color night;

    [SerializeField] bool orderBasedOnTimeOfDay;

    [SerializeField]
    enum TimeOfDay
    {
        morning,
        day,
        evening,
        night
    }

    [SerializeField] TimeOfDay myTimeOfDay;

    private void Awake()
    {
        if (lightParent == null)
        {
            lightParent = this.transform;
        }
        Light2D[] lightParentArray = lightParent.GetComponentsInChildren<Light2D>();
        lights = lights.Concat(lightParentArray).ToArray();
    }
    private void OnEnable()
    {
        ClockManager.OnMinuteTick += DaylightMinuteUpdate;
    }

    private void OnDisable()
    {
        ClockManager.OnMinuteTick -= DaylightMinuteUpdate;
    }

    void DaylightMinuteUpdate()
    {
        foreach (Light2D currentLight in lights)
        {
            currentLight.color = (morning * GameManager.Instance.clockManager.morning.GetCurrentVolume()) +
                                (day * GameManager.Instance.clockManager.midday.GetCurrentVolume()) +
                                (evening * GameManager.Instance.clockManager.evening.GetCurrentVolume()
                                ) +
                                (night * GameManager.Instance.clockManager.night.GetCurrentVolume());
        }
        if (orderBasedOnTimeOfDay)
        {

            foreach (Light2D currentLight in lights)
            {
                int lightOrder;
                float currentVolume = 0;

                if (myTimeOfDay == TimeOfDay.morning)
                {
                    currentVolume = GameManager.Instance.clockManager.morning.GetCurrentVolume();
                }
                else if (myTimeOfDay == TimeOfDay.day)
                {
                    currentVolume = GameManager.Instance.clockManager.midday.GetCurrentVolume();
                }
                else if (myTimeOfDay == TimeOfDay.evening)
                {
                    currentVolume = GameManager.Instance.clockManager.evening.GetCurrentVolume();
                }
                else if (myTimeOfDay == TimeOfDay.night)
                {
                    currentVolume = GameManager.Instance.clockManager.morning.GetCurrentVolume();
                }


                lightOrder = (int)Math.Round(currentVolume * 100); //turn that volume into an int for the sorting order.
                                                                   // YE GODS THIS IS JANK
                currentLight.lightOrder = lightOrder;
                if (lightOrder == 0)
                {
                    currentLight.gameObject.SetActive(false);
                }
                else
                {
                    currentLight.gameObject.SetActive(true);
                }
            }

        }


        /* TEST VERSION (it works)
        public Light2D globalLight;

        public Color colorA;
        public Color colorB;

        public float multiplierA;
        public float multiplierB;

        private void Update()
        {
            globalLight.color = (colorA * multiplierA) + (colorB * multiplierB) ;
        }*/
    }
}
