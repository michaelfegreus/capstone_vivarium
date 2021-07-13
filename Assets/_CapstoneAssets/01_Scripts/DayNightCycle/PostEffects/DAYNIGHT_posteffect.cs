using UnityEngine;
using UnityEngine.Rendering;

public class DAYNIGHT_posteffect : MonoBehaviour
{
    public Volume morningVolume;

    public Volume middayVolume;

    public Volume eveningVolume;

    public Volume nightVolume;

    // Subscribe to minute tick updates.
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
        morningVolume.weight = GAME_manager.Instance.clockManager.morning.GetCurrentVolume();
        middayVolume.weight = GAME_manager.Instance.clockManager.midday.GetCurrentVolume();
        eveningVolume.weight = GAME_manager.Instance.clockManager.evening.GetCurrentVolume();
        nightVolume.weight = GAME_manager.Instance.clockManager.night.GetCurrentVolume();
    }
}