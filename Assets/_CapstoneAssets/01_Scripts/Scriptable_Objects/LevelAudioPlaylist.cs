using UnityEngine;

[CreateAssetMenu()]
public class LevelAudioPlaylist : ScriptableObject
{
    [Header("Track Play Frequency")]
    // Think I'm going to simplify and just set it to one track play rate per level area.
    [Tooltip("X/100 percent chance that a track will trigger.")]
    [Range(0, 100)]
    public int trackFrequency = 100;

    /*[Tooltip("X/100 percent chance that a morning track will trigger.")]
    public int morningTrackFrequency;
    [Tooltip("X/100 percent chance that a day track will trigger.")]
    public int dayTrackFrequency;
    [Tooltip("X/100 percent chance that an evening track will trigger.")]
    public int eveningTrackFrequency;
    [Tooltip("X/100 percent chance that a night track will trigger.")]
    public int nightTrackFrequency;*/

    [Header("Level Track List")]
    public AudioClip[] morningTracks;
    public AudioClip[] dayTracks;
    public AudioClip[] eveningTracks;
    public AudioClip[] nightTracks;
}
