using UnityEngine;
using DarkTonic.MasterAudio;
using System;

[Serializable]
public class DayCycleAmbientSound : DayCycleRange
{
    public DayCycleAmbientSound()
    {

    }

    public MasterAudioGroup ambientGroup;
}
