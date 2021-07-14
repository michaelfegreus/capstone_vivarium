using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DAYNIGHT_globallights2D : MonoBehaviour
{
    [SerializeField] Light2D globalLight;

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
        globalLight.color = (morning * GAME_manager.Instance.clockManager.morning.GetCurrentVolume()) +
                            (day * GAME_manager.Instance.clockManager.midday.GetCurrentVolume()) +
                            (evening * GAME_manager.Instance.clockManager.evening.GetCurrentVolume()) +
                            (night * GAME_manager.Instance.clockManager.night.GetCurrentVolume());
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
