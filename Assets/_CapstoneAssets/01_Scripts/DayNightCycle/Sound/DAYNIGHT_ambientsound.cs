using UnityEngine;
using DarkTonic.MasterAudio;

public class DAYNIGHT_ambientsound : MonoBehaviour
{
    public MasterAudioGroup morningGroup;

    public MasterAudioGroup middayGroup;

    public MasterAudioGroup eveningGroup;

    public MasterAudioGroup nightGroup;
   

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
        MasterAudio.SetBusVolumeByName(morningGroup.BusForGroup.busName, GAME_manager.Instance.clockManager.morning.GetCurrentVolume());
        Debug.Log(GAME_manager.Instance.clockManager.morning.GetCurrentVolume() + " Morning Volume");
        MasterAudio.SetBusVolumeByName(middayGroup.BusForGroup.busName, GAME_manager.Instance.clockManager.midday.GetCurrentVolume());
        Debug.Log(GAME_manager.Instance.clockManager.midday.GetCurrentVolume() + " Midday Volume");
        MasterAudio.SetBusVolumeByName(eveningGroup.BusForGroup.busName, GAME_manager.Instance.clockManager.evening.GetCurrentVolume());
        Debug.Log(GAME_manager.Instance.clockManager.evening.GetCurrentVolume() + " Evening Volume");
        MasterAudio.SetBusVolumeByName(nightGroup.BusForGroup.busName, GAME_manager.Instance.clockManager.night.GetCurrentVolume());
        Debug.Log(GAME_manager.Instance.clockManager.night.GetCurrentVolume() + " Night Volume");
    }
}
