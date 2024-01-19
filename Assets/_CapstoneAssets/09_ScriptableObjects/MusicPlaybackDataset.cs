using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Josh;

namespace Josh
{
    /// <summary>
    /// This is our scriptable object which holds every single: Room Cluster, Time Pool, and MusicClip
    /// </summary>
    [CreateAssetMenu(fileName = "MusicPlaybackDataset", menuName = "Music System/MusicPlaybackData", order = 1)]
    public class MusicPlaybackDataset : ScriptableObject
    {
        // build our list of room clusters, this will auto-populate
        public List<RoomCluster> roomClusters = new List<RoomCluster>();

        void Awake()
        {
            ValidateDataset();
        }

        // when we validate this object
        void OnValidate()
        {
            ValidateDataset();
        }

        void ValidateDataset()
        {
            if (roomClusters.Count < (int)MusicPlayer.PossibleRoomClusters.Count)
            {
                UnityEngine.Debug.LogWarning("Music System Clusters lower than four, rebuilding asset...");
                roomClusters.Clear();
                // then
                for (int i = 0; i < (int)MusicPlayer.PossibleRoomClusters.Count; i++)
                {
                    // create one new RoomCluster with the correct header per room cluster area
                    roomClusters.Add(new RoomCluster(Enum.GetName(typeof(MusicPlayer.PossibleRoomClusters), i)));
                }
            }
        }
    }
}