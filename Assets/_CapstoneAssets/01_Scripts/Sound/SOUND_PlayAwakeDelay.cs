using UnityEngine;

public class SOUND_PlayAwakeDelay : MonoBehaviour
{

    public AudioSource myAudio;

    public float secondsDelayed;

    // Start is called before the first frame update
    void OnEnable()
    {
        myAudio.PlayDelayed(secondsDelayed);
    }
    
}
