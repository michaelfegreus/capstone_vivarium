using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Josh
{
    public class MusicRoomClusterDefinition : MonoBehaviour, IRoomTrigger
    {
        public MusicPlayer.PossibleRoomClusters assignedCluster;
        public float newTransitionFadeRate = 1f;

        void OnValidate()
        {
            if (newTransitionFadeRate <= 0f)
                newTransitionFadeRate = 1f;
        }

        public void OnEnterRoom() 
        {
            MusicPlayer.instance.targetSourceFadeRate = newTransitionFadeRate;
        }
        public void OnExitRoom() { }
    }
}
