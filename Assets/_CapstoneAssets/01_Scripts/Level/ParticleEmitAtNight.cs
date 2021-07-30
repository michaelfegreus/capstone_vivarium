using UnityEngine;
using System.Collections;

public class ParticleEmitAtNight : MonoBehaviour
{
    // TODO: KILL THIS EXTREMELY HACKY SCRIPT

    public ParticleSystem myParticle;

    public bool disableAtNight;

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
        if (!disableAtNight)
        {
            if (GAME_manager.Instance.clockManager.night.GetCurrentVolume() == 1)
            {
                var emission = myParticle.emission;
                emission.enabled = true;
            }
            else
            {
                var emission = myParticle.emission;
                emission.enabled = false;
            }
        }
        else
        {
            if (GAME_manager.Instance.clockManager.night.GetCurrentVolume() == 1)
            {
                var emission = myParticle.emission;
                emission.enabled = false;
            }
            else
            {
                var emission = myParticle.emission;
                emission.enabled = true;
            }
        }
    }
}
