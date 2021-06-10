using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class SOUND_PlaySoundOnAnimationEvent : MonoBehaviour
{

    public void PlaySound(string soundName)
    {
        MasterAudio.PlaySound3DAtVector3(soundName, transform.position);
    }

}
