﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Josh
{
    [RequireComponent(typeof(AudioSource))]
    /// <summary>
    /// This is the MusicPlayer class. It plays the music in the scene to a 2D Audio source. 
    /// This script plays songs based on time of day, the current room, and manually triggered events.
    /// It fades between songs. You can also manually trigger the start and stop of music, or silence.
    /// </summary>
    public class MusicPlayer : MonoBehaviour
    {
        /// <summary>
        /// The public static instance of the MusicPlayer class
        /// </summary>
        public static MusicPlayer instance;
        void Awake() => instance = this;
        public bool initialized = false;

        /// <summary>
        /// Our current music playback dataset. This must be set in the editor!
        /// </summary>
        public MusicPlaybackDataset currentPlaybackDataset;

        [Header("Core Variables")]
        [SerializeField] private AudioSource musicSource;
        public PossibleRoomClusters currentRoomCluster;
        [SerializeField] PossibleRoomClusters previousRoomCluster;
        public PossibleTimes currentTime;
        [SerializeField] PossibleTimes previousTime;
        public float targetSourceVolume;
        [Tooltip("How quickly songs fade in and out")]
        public float targetSourceFadeRate;
        /// <summary>
        /// What possible times are there in the environment?
        /// Used by the TimePool class to mark what time the pool represents
        /// </summary>
        public enum PossibleTimes
        {
            Morning, Midday, Evening, Night
        }

        [Header("Audio Tracks Loaded Up")]
        [SerializeField] Queue<AudioClip> playbackQueue = new Queue<AudioClip>();

        /// <summary>
        /// What possible Room Clusters are there in the game?
        /// Used by the RoomCluster class to mark which room cluster is associated with it in the game
        /// </summary>
        public enum PossibleRoomClusters
        {
            House, Town, TreeHollow, Forest,

            // leave this here so that we can get our maximum count of room clusters
            Count
        }

        // runs on the initialization of the object on the first frame
        void Start()
        {
            // ensure the music player's settings are all good to go
            InitializeMusicPlayer();
        }

        /// <summary>
        /// Local function to setup the settings for our player
        /// </summary>
        void InitializeMusicPlayer()
        {
            // let's get our music source before doing anything else
            musicSource = GetComponent<AudioSource>(); // this is required by the editor, we will always have one.
            // if we don't throw an error
            if (musicSource == null)
            {
                Debug.LogError("MusicPlayer does not have an audio source, this means something has broken in the project. Please check the console.");
                initialized = false;
                return;
            }

            // make sure our volume is ready to be set
            targetSourceVolume = 0;
            musicSource.volume = targetSourceVolume;
            // make sure this is a 2D mixer
            musicSource.spatialBlend = 0;
            
            // once we are initialized, complete the function
            initialized = true;
        }

        // Runs once per physics tick, 50 times per second
        void FixedUpdate()
        {
            if (!initialized)
            {
                Debug.Log("Not initialized");
                return;
            }

            ProcessRoomClusterChange();
            ProcessTimeChange();
            ProcessVolumeLevel();
            ProcessPlayingState();

        }

        /// <summary>
        /// Checks our Current RoomCluster compared to what it was last frame to see if it has changed.
        /// </summary>
        void ProcessRoomClusterChange()
        {
            // check if the room cluster has changed
            if (previousRoomCluster != currentRoomCluster)
            {
                Debug.Log("Cluster Change Processed");
                // a change has occured!
                OnAnyMusicChange();
                // at the end of the frame set our previous room cluster to the current one
                previousRoomCluster = currentRoomCluster;
            }
        }

        /// <summary>
        /// Processes Time Changes
        /// </summary>
        void ProcessTimeChange()
        {
            if (previousTime != currentTime)
            {
                Debug.Log("Time has changed!");
                // a change has occured!
                OnAnyMusicChange();
                // set our previous time
                previousTime = currentTime;
            }
        }

        /// <summary>
        /// Performs actions based on whether the audio source is currently playing
        /// </summary>
        void ProcessPlayingState()
        {
            // if we are not playing, play the next song
            if (!musicSource.isPlaying && playbackQueue.Count > 0)
            {
                ExecuteMusicPlaybackFromQueue();
            }
        }

        /// <summary>
        /// Run when the any Room Cluster or Times Change
        /// </summary>
        void OnAnyMusicChange()
        {
            // clear the queue
            ClearMusicQueue();
            // whenever the cluster changes, run a coroutine to lower fade out the music before fading in
            RoomClusterMusicTransition();
        }

        /// <summary>
        /// Removes all songs in the queue
        /// </summary>
        public void ClearMusicQueue()
        {
            // this clears the queue of songs we had lined up
            playbackQueue.Clear();
            // whenever this runs we have to cancel any coroutines happening so that we do not overload the queue
            StopAllCoroutines();
        }

        // function wrapper for ease of readability
        void RoomClusterMusicTransition()
        {
            StartCoroutine(RoomClusterMusicTransitionCoroutine());
        }

        /// <summary>
        /// Fade out our music
        /// </summary>
        IEnumerator RoomClusterMusicTransitionCoroutine()
        {
            // fade our music out
            targetSourceVolume = 0;
            yield return new WaitForSecondsRealtime(1f);
            // after our music has faded out, populate the queue and then set the target volume to 1
            AutoPopulatePlaybackQueue();
            targetSourceVolume = 1;
        }

        // function wrapper for manual audio fade in / out
        void ManualMusicFadeInOut(AudioClip song, int loops)
        {
            // clear our queue
            ClearMusicQueue();
            // run our new song
            StartCoroutine(ManualMusicFadeInOutCoroutine(song, loops));
        }

        // manually fades music in and out
        IEnumerator ManualMusicFadeInOutCoroutine(AudioClip song, int loops)
        {
            // fade our music out
            targetSourceVolume = 0;
            yield return new WaitForSecondsRealtime(1f);
            // after our music has faded out, populate the queue and then set the target volume to 1
            ExecuteMusicPlaybackFromManual(song, loops);
            targetSourceVolume = 1;
        }

        /// <summary>
        /// Local function to process the current volume of the Music Player
        /// </summary>
        void ProcessVolumeLevel()
        {
            // if we are not close to our music volume, transition towards it at our rate
            if (musicSource.volume != targetSourceVolume)
                musicSource.volume = Mathf.MoveTowards(musicSource.volume, targetSourceVolume, targetSourceFadeRate * Time.deltaTime);
        }

        /// <summary>
        /// Local function to populate our music queue
        /// </summary>
        void AutoPopulatePlaybackQueue()
        { 

            Debug.Log("Automatically populating the playback queue...");

            // make sure we have a playback dataset before continuing
            if (currentPlaybackDataset == null)
            {
                Debug.Log("MusicPlayer object does not have a MuscPlaybackDataset, you need to set this in the inspector or music will not play.");
                Debug.LogError("MusicPlayer object does not have a MuscPlaybackDataset, you need to set this in the inspector or music will not play.");
                return;
            }

            // based on our current room cluster pick a series of songs to add to the playback queue
            MusicClip selectedClip = currentPlaybackDataset?.roomClusters[(int)currentRoomCluster].timePools[(int)currentTime].musicClips[Random.Range(0, currentPlaybackDataset.roomClusters[(int)currentRoomCluster].timePools[(int)currentTime].musicClips.Count-1)];

            // if this music clip comes up null, don't do anything and return this function
            if (selectedClip == null)
            {
                Debug.Log("Automatically Selected Music Clip was null, no music will play.");
                return;
            }

            // depending on how many times this clip can loop, add a specific amount to the queue
            int loopCount = selectedClip.maxLoops == 0 ? 10 : Random.Range(1, selectedClip.maxLoops);

            // queue the loop up to the count
            for (int i = 0; i < loopCount; i++)
            {
                Debug.Log("Enqueuing : " + selectedClip.audioClip.name);
                playbackQueue.Enqueue(selectedClip.audioClip);
            }

            // after we finish population, run our music execution function and begin playback
            if (playbackQueue.Count > 0)
                ExecuteMusicPlaybackFromQueue();

        }

        /// <summary>
        /// Local function to populate our music queue manually
        /// </summary>
        /// <param name="song"></param>
        /// <param name="loops"></param>
        void ManuallyPopulatePlaybackQueue(AudioClip song, int loops)
        {
            // depending on how many times this clip can loop, add a specific amount to the queue
            int loopCount = loops == 0 ? 10 : Random.Range(1, loops);

            // queue the loop up to the count
            for (int i = 0; i < loopCount; i++)
            {
                playbackQueue.Enqueue(song);
            }

            // after we finish population, run our music execution function and begin playback
            if (playbackQueue.Count > 0)
                ExecuteMusicPlaybackFromQueue();
        }

        /// <summary>
        /// Function plays music based off of: Time of day, current room, and previously played song.
        /// Calling this function without any arguments causes it to automatically determine these features.
        /// </summary>
        public void PlayMusic()
        {
            // clear and then auto populate the queue
            ClearMusicQueue();
            AutoPopulatePlaybackQueue();
        }

        /// <summary>
        /// Function plays music based off of: Time of day, current room, and previously played song.
        /// Calling this function with an AudioClip overrides the current song and plays that clip instead, once.
        /// </summary>
        /// <param name="song"></param>
        public void PlayMusic(AudioClip song)
        {
            ClearMusicQueue();
            ManualMusicFadeInOut(song, 1);
        }        
        
        /// <summary>
        /// Function plays music based off of: Time of day, current room, and previously played song.
        /// Calling this function without any arguments causes it to automatically determine these features.
        /// Calling this function with an AudioClip overrides the current song and plays that clip instead.
        /// Calling this function with maxLoops sets the maximum randomized loops that this clip can play in a row.
        /// </summary>
        /// <param name="song"></param>
        public void PlayMusic(AudioClip song, int maxLoops)
        {
            ClearMusicQueue();
            ManualMusicFadeInOut(song, maxLoops);
        }

        /// <summary>
        /// Function plays music based off of: Time of day, current room, and previously played song.
        /// Calling this function without any arguments causes it to automatically determine these features.
        /// Calling this function with a MusicClip plays the audio using the properties of that MusicClip
        /// </summary>
        /// <param name="musicClip"></param>
        public void PlayMusic(MusicClip musicClip)
        {
            ClearMusicQueue();
            ManualMusicFadeInOut(musicClip.audioClip, musicClip.maxLoops);
        }

        /// <summary>
        /// Local function used to play music by all of the PlayMusic public functions.
        /// Running this function with no arguments plays the most recent song in the queue.
        /// </summary>
        void ExecuteMusicPlaybackFromQueue()
        {

            musicSource.clip = playbackQueue.Dequeue();
            musicSource.Play();

            Debug.Log("Executing Playback from Queue: " + musicSource.clip.name);   
        }

        /// <summary>
        /// Local function used to play music manually
        /// </summary>
        /// <param name="audioClip">The audio file to be played</param>
        /// <param name="maxLoops">How many times this song is looped</param>
        void ExecuteMusicPlaybackFromManual(AudioClip audioClip, int maxLoops)
        {
            ManuallyPopulatePlaybackQueue(audioClip, maxLoops);
        }

        //Trent-Added Public Functions

        public void StopAllMusic()
        {
            targetSourceVolume = 0;
        }

    }

    /// <summary>
    /// RoomClusters hold the selection of Time Pools for each cluster of rooms in the game.
    /// </summary>
    [System.Serializable]
    public class RoomCluster
    {
        [HideInInspector] public string header;
        public List<TimePool> timePools = new List<TimePool>()
        {
            new TimePool(MusicPlayer.PossibleTimes.Morning, new List<MusicClip>(), "Morning"),
            new TimePool(MusicPlayer.PossibleTimes.Midday, new List<MusicClip>(), "Midday"),
            new TimePool(MusicPlayer.PossibleTimes.Evening, new List<MusicClip>(), "Afternoon"),
            new TimePool(MusicPlayer.PossibleTimes.Night, new List<MusicClip>(), "Night")
        };

        public RoomCluster(string header) { this.header = header; }
    }
    /// <summary>
    /// TimePools hold collections of songs for each time of day, to be held by RoomClusters
    /// </summary>
    [System.Serializable]
    public class TimePool
    {
        [HideInInspector] public string header;
        /// <summary>
        /// Referenced by other scripts to determine which time this pool represents.
        /// Should only have one TimePool per time 
        /// </summary>
        [HideInInspector] public MusicPlayer.PossibleTimes setTime;
        public List<MusicClip> musicClips = new List<MusicClip>();

        public TimePool(MusicPlayer.PossibleTimes setTime, List<MusicClip> musicClips, string header)
        {
            // set our attributes on construct
            this.setTime = setTime;
            this.musicClips = musicClips;
            this.header = header;
        }
    }

    /// <summary>
    /// This is a custom class which holds the information for each Music track to be played back correctly.
    /// This information is a public class which holds a series of public variables to playback music
    /// </summary>
    [System.Serializable]
    [SerializeField] public class MusicClip 
    {
        public string customHeader;
        public AudioClip audioClip; // what is the audio we want to play?
        [Tooltip("Set to 0 for infinite")]
        public int maxLoops; // how many times can this loop at most?

        public MusicClip(AudioClip musicClip, int maxLoops)
        {
            this.audioClip = musicClip;
            this.maxLoops = maxLoops;
        }
    }




}
