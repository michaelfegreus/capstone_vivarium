using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Plays looping game music. If any game music is already playing, it is stopped. Game music will continue playing across scene loads.
    /// </summary>
    [CommandInfo("Audio",
                 "Play Vivarium Level Music",
                 "Plays looping game music. If any game music is already playing, then don't play this. Game music will continue playing across scene loads.")]
    [AddComponentMenu("")]
    public class PlayVivariumLevelMusic : Command
    {
        // Hours of the day to begin playing associated music tracks.
        protected int morningMusicHour = 6;
        protected int dayMusicHour = 12;
        protected int eveningMusicHour = 17;
        protected int nightMusicHour = 20;

		[Tooltip("How often a song gets called from the playlist.")]
		[Range(0.0f, 1.0f)]
		[SerializeField] protected float playbackRate = 1f;

        [Tooltip("Music sound clips to pick from at Morning.")]
        [SerializeField] protected AudioClip[] morningTracks;

        [Tooltip("Music sound clips to pick from at Day.")]
        [SerializeField] protected AudioClip[] dayTracks;

        [Tooltip("Music sound clips to pick from at Evening.")]
        [SerializeField] protected AudioClip[] eveningTracks;

        [Tooltip("Music sound clips to pick from at Night.")]
        [SerializeField] protected AudioClip[] nightTracks;

        [Tooltip("Time to begin playing in seconds. If the audio file is compressed, the time index may be inaccurate.")]
        [SerializeField] protected float atTime;

        [Tooltip("The music will start playing again at end.")]
        [SerializeField] protected bool loop;

        [Tooltip("Length of time to fade out previous playing music.")]
        [SerializeField] protected float fadeDuration = 1f;

        // Randomly selected audioclip
        protected AudioClip musicClip;


        #region Public members

        public override void OnEnter()
        {
            var musicManager = FungusManager.Instance.MusicManager;

            if (!musicManager.IsMusicPlaying())
            {
				if (playbackRate != 0f && Random.Range(0f, 1f) < playbackRate) { // Make sure it hits the playback rate RNG

					int gameHour = WORLD_manager.Instance.timeManager.inGameTime.hour;

					float startTime = Mathf.Max(0, atTime);


					if (gameHour >= nightMusicHour || gameHour < morningMusicHour)
					{
						musicClip = PickFromClipArray(nightTracks);
					}
					else if (gameHour >= eveningMusicHour)
					{
						musicClip = PickFromClipArray(eveningTracks);
					}
					else if (gameHour >= dayMusicHour)
					{
						musicClip = PickFromClipArray(dayTracks);
					}
					else if (gameHour >= morningMusicHour)
					{
						musicClip = PickFromClipArray(morningTracks);
					}

					musicManager.PlayMusic(musicClip, loop, fadeDuration, startTime);

				}
            }

            Continue();
        }

        protected AudioClip PickFromClipArray(AudioClip[] clipArray)
        {
            AudioClip selectedClip = clipArray[Random.Range(0, clipArray.Length)];

            return selectedClip;
        }
/*
        public override string GetSummary()
        {
            if (musicClip == null)
            {
                return "Error: No music clip selected for this time of day period.";
            }

            return musicClip.name;
        }*/

        public override Color GetButtonColor()
        {
            return new Color32(242, 209, 176, 255);
        }

        #endregion
    }
}