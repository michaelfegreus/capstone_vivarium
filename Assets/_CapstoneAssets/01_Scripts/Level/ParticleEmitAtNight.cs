using UnityEngine;
using System.Collections;

public class ParticleEmitAtNight : MonoBehaviour
{
    // TODO: KILL THIS EXTREMELY HACKY SCRIPT

    public ParticleSystem myParticle;

    public bool disableAtNight;

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
        if (!disableAtNight)
        {
            if (GameManager.Instance.clockManager.night.GetCurrentVolume() == 1)
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
            if (GameManager.Instance.clockManager.night.GetCurrentVolume() == 1)
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
