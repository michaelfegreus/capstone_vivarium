using Opsive.UltimateInventorySystem.Utility;
using System;
using System.Collections;
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

        /// <summary>
        /// Our current music playback dataset. This must be set in the editor!
        /// </summary>
        public MusicPlaybackDataset currentPlaybackDataset;

        [Header("Debug Readouts")]
        [SerializeField] string currentRoom;
        [SerializeField] PossibleRoomClusters currentRoomCluster;
        [SerializeField] PossibleTimes currentTime;

        /// <summary>
        /// What possible times are there in the environment?
        /// Used by the TimePool class to mark what time the pool represents
        /// </summary>
        public enum PossibleTimes
        {
            Morning, Midday, Afternoon, Night
        }

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

        /// <summary>
        /// Function plays music based off of: Time of day, current room, and previously played song.
        /// Calling this function without any arguments causes it to automatically determine these features.
        /// </summary>
        public void PlayMusic()
        {

        }

        /// <summary>
        /// Function plays music based off of: Time of day, current room, and previously played song.
        /// Calling this function without any arguments causes it to automatically determine these features.
        /// Calling this function with an AudioClip overrides the current song and plays that clip instead.
        /// </summary>
        /// <param name="song"></param>
        public void PlayMusic(AudioClip song)
        {

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

        }

        /// <summary>
        /// Function plays music based off of: Time of day, current room, and previously played song.
        /// Calling this function without any arguments causes it to automatically determine these features.
        /// Calling this function with a MusicClip plays the audio using the properties of that MusicClip
        /// </summary>
        /// <param name="musicClip"></param>
        public void PlayMusic(MusicClip musicClip)
        {

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
            new TimePool(MusicPlayer.PossibleTimes.Afternoon, new List<MusicClip>(), "Afternoon"),
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
        public AudioClip musicClip; // what is the audio we want to play?
        [Tooltip("Set to 0 for infinite")]
        public int maxLoops; // how many times can this loop at most?

        public MusicClip(AudioClip musicClip, int maxLoops)
        {
            this.musicClip = musicClip;
            this.maxLoops = maxLoops;
        }
    }


}
