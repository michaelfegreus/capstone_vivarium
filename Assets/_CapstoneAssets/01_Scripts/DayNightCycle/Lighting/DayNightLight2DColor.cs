using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Linq;

public class DayNightLight2DColor : MonoBehaviour
{
    [Tooltip("Defaults to this Game Object at runtime if you didn't enter a different parent.")]
    [SerializeField] Transform lightParent;

    [SerializeField] private Light2D[] lights;

    [SerializeField] Color morning;
    [SerializeField] Color day;
    [SerializeField] Color evening;
    [SerializeField] Color night;

    

    private void Awake()
    {
        if(lightParent == null)
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
