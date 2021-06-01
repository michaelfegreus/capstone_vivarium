using UnityEngine;
using System.Collections;

public class GAME_audio_manager : Singleton<GAME_audio_manager> {

    /*public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioSource sfxAmbientLoopSource;

    // Pitch randomizer multiplier for the RandomizeSFX function
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    // Standard audio fade duration
    public float audioFadeTime = 1f;

    // Playlist of music for current level
    [SerializeField]
    LevelAudioPlaylist currentLevelMusicPlaylist;
    // Playlist of ambient SFX 
    [SerializeField]
    LevelAudioPlaylist currentLevelAmbientSFXPlaylist;

    // Set up event subscription to the time of day blocks
    private void OnEnable() {
        WORLD_time_manager.OnTimeBlockChange += PlayRandomLevelMusic;
        WORLD_time_manager.OnTimeBlockChange += PlayLevelAmbientSFX;
    }

    // Unsubscribe events if this script gets disabled to avoid errors.
    private void OnDisable() {
        WORLD_time_manager.OnTimeBlockChange -= PlayRandomLevelMusic;
        WORLD_time_manager.OnTimeBlockChange -= PlayLevelAmbientSFX;
    }

    public void PlaySingleSFX(AudioClip clip) {
        sfxSource.clip = clip;
        sfxSource.PlayOneShot(clip);
    }

    public void RandomizeSFX(params AudioClip[] clips) {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        sfxSource.pitch = randomPitch;
        sfxSource.clip = clips[randomIndex];
        sfxSource.PlayOneShot(sfxSource.clip);
    }

    public void PlayMusicTrack(AudioClip track) {
        musicSource.clip = track;
        musicSource.Play();
    }

    void FadeInAudioSource(AudioSource fadingAudio) {
        StartCoroutine(FadeIn(fadingAudio, audioFadeTime));
    }

    void FadeOutAudioSource(AudioSource fadingAudio) {
        if (musicSource.isPlaying) {
            StartCoroutine(FadeOut(fadingAudio, audioFadeTime));
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }

    IEnumerator FadeIn(AudioSource audioSource, float FadeTime) {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1) {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

    public void MuteLevelMusic() {
        currentLevelMusicPlaylist = null;
        FadeOutAudioSource(musicSource);
    }

    public void MuteLevelAmbientSFX() {
        currentLevelAmbientSFXPlaylist = null;
        sfxAmbientLoopSource.Stop();
        //FadeOutAudioSource(sfxAmbientLoopSource);
    }

    public void AddLevelMusicPlaylist(LevelAudioPlaylist playlist) {
        // If the new playlist is not the same as the old, then roll for a random level song to play.
        if (playlist != currentLevelMusicPlaylist || playlist == null) {
            currentLevelMusicPlaylist = playlist;
            PlayRandomLevelMusic();
        }
    }

    public void PlayRandomLevelMusic() {
        // If there isn't currently a song playing.
        if (musicSource.isPlaying == false && currentLevelMusicPlaylist != null) {
            // Roll for random chance of playing a song, if there's at least one song in that time of day block.
            AudioClip myTrack = SelectTrackFromPlaylist(currentLevelMusicPlaylist);
            if (myTrack != null) {
                // Fade in the randomly selected track
                musicSource.clip = myTrack;
                FadeInAudioSource(musicSource);
            }
        }
    }

    public void AddLevelAmbientSFX(LevelAudioPlaylist sfxLoop) {
        if (currentLevelMusicPlaylist != sfxLoop) {
            currentLevelAmbientSFXPlaylist = sfxLoop;
            PlayLevelAmbientSFX();
        }
    }

   
    
    // Plays level AmbientSFX
    void PlayLevelAmbientSFX() {

        if (currentLevelAmbientSFXPlaylist != null) {
            // Pick track depending on time of day and play it (if it's not a null option)
            AudioClip selectedTrack = SelectTrackFromPlaylist(currentLevelAmbientSFXPlaylist);

            if (selectedTrack != null) {
                sfxAmbientLoopSource.clip = selectedTrack;
                sfxAmbientLoopSource.volume = 1f; // Make sure volume is turned up, in case the audio source was faded out previously.

                // Start the audio at a random time.
                float randomStartingTime = Random.Range(0f, selectedTrack.length);
                sfxAmbientLoopSource.time = randomStartingTime;

                sfxAmbientLoopSource.Play();
            }
        }
    }

   / AudioClip SelectTrackFromPlaylist(LevelAudioPlaylist playlist) {
        AudioClip playlistTrack = null;
        
        if (Random.Range(0, 100) <= playlist.trackFrequency) {
            switch (WORLD_manager.Instance.timeManager.currentTimeBlock) {
                case WORLD_time_manager.TimeBlock.Morning:
                    if (playlist.morningTracks.Length > 0) {
                        playlistTrack = playlist.morningTracks[Random.Range(0, playlist.morningTracks.Length)];
                    }
                    break;
                case (WORLD_time_manager.TimeBlock.Midday):
                    if (playlist.dayTracks.Length > 0) {
                        playlistTrack = playlist.dayTracks[Random.Range(0, playlist.dayTracks.Length)];
                    }
                    break;
                case (WORLD_time_manager.TimeBlock.Evening):
                    if (playlist.eveningTracks.Length > 0) {
                        playlistTrack = playlist.eveningTracks[Random.Range(0, playlist.eveningTracks.Length)];
                    }
                    break;
                case (WORLD_time_manager.TimeBlock.Night):
                    if (playlist.nightTracks.Length > 0) {
                        playlistTrack = playlist.nightTracks[Random.Range(0, playlist.nightTracks.Length)];
                    }
                    break;
            }
        }
        return playlistTrack;
    }*/
}