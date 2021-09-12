using UnityEngine;
using DarkTonic.MasterAudio;

public class PlayMasterAudioSound : MonoBehaviour
{

    public void PlaySound(string soundName)
    {
        MasterAudio.PlaySound3DAtVector3(soundName, transform.position);
    }

}
