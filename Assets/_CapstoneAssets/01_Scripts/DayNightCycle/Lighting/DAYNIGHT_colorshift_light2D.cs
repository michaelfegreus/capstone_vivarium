using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DAYNIGHT_colorshift_light2D : MonoBehaviour
{
    [SerializeField] Light2D[] lights;

    [SerializeField] Color morning;
    [SerializeField] Color day;
    [SerializeField] Color evening;
    [SerializeField] Color night;

    private void OnEnable()
    {
        GAME_clock_manager.OnMinuteTick += DaylightMinuteUpdate;
    }

    private void OnDisable()
    {
        GAME_clock_manager.OnMinuteTick -= DaylightMinuteUpdate;
    }

    void DaylightMinuteUpdate()
    {
        foreach (Light2D currentLight in lights)
        {
            currentLight.color = (morning * GAME_manager.Instance.clockManager.morning.GetCurrentVolume()) +
                                (day * GAME_manager.Instance.clockManager.midday.GetCurrentVolume()) +
                                (evening * GAME_manager.Instance.clockManager.evening.GetCurrentVolume()
                                ) +
                                (night * GAME_manager.Instance.clockManager.night.GetCurrentVolume());
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
