using UnityEngine;
using UnityEngine.Rendering;

public class DayNightPostEffect : MonoBehaviour
{
    public Volume morningVolume;

    public Volume middayVolume;

    public Volume eveningVolume;

    public Volume nightVolume;

    // Subscribe to minute tick updates.
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
        morningVolume.weight = GameManager.Instance.clockManager.morning.GetCurrentVolume();
        middayVolume.weight = GameManager.Instance.clockManager.midday.GetCurrentVolume();
        eveningVolume.weight = GameManager.Instance.clockManager.evening.GetCurrentVolume();
        nightVolume.weight = GameManager.Instance.clockManager.night.GetCurrentVolume();
    }
}