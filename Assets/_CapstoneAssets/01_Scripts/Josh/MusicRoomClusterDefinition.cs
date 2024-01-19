using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Josh
{
    public class MusicRoomClusterDefinition : MonoBehaviour, IRoomTrigger
    {
        public MusicPlayer.PossibleRoomClusters assignedCluster;

        public void OnEnterRoom() { }
        public void OnExitRoom() { }
    }
}
