using UnityEngine;
using DarkTonic.MasterAudio;

public class DayNightAmbientSound : MonoBehaviour
{
    public MasterAudioGroup morningGroup;

    public MasterAudioGroup middayGroup;

    public MasterAudioGroup eveningGroup;

    public MasterAudioGroup nightGroup;
   

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
        MasterAudio.SetBusVolumeByName(morningGroup.BusForGroup.busName, GameManager.Instance.clockManager.morning.GetCurrentVolume());
        MasterAudio.SetBusVolumeByName(middayGroup.BusForGroup.busName, GameManager.Instance.clockManager.midday.GetCurrentVolume());
        MasterAudio.SetBusVolumeByName(eveningGroup.BusForGroup.busName, GameManager.Instance.clockManager.evening.GetCurrentVolume());
        MasterAudio.SetBusVolumeByName(nightGroup.BusForGroup.busName, GameManager.Instance.clockManager.night.GetCurrentVolume());
    }
}
