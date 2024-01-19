using UnityEngine;
using Josh;

public class PlayerRoomCheck : MonoBehaviour
{
    int layermask;

    Ray ray;
    RaycastHit2D rayHit;

    Collider2D currentRoomVolume;
    IRoomTrigger[] roomVolumeScripts;

    public MusicPlayer.PossibleRoomClusters currentRoomCluster;

    private void Start()
    {
         layermask = LayerMask.GetMask("Rooms");
    }

    private void FixedUpdate()
    {
        // 
        ray = new Ray(transform.position + new Vector3(0, 0, -1), Vector3.forward);

        rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layermask);
    }
    
    // checks every single frame
    void OnTriggerStay2D(Collider2D other)
    {
        if (currentRoomVolume == null)
        {
            // Set up starting room

            currentRoomVolume = rayHit.collider; // use the trigger overlap instead of the raycast
            roomVolumeScripts = currentRoomVolume?.GetComponents<IRoomTrigger>();

            if (roomVolumeScripts?.Length > 0)
            foreach (IRoomTrigger roomTrigger in roomVolumeScripts)
            {
                roomTrigger?.OnEnterRoom();
                Debug.Log("running enter room");
            }

        }
        else if (currentRoomVolume != null && currentRoomVolume != rayHit.collider)
        {
            // Change rooms

            // Run exit functions on current
            if (roomVolumeScripts.Length > 0)
            foreach (IRoomTrigger roomTrigger in roomVolumeScripts)
            {
                roomTrigger?.OnExitRoom();
                Debug.Log("running exit room");
            }

            // Set up new room
            currentRoomVolume = rayHit.collider;
            roomVolumeScripts = currentRoomVolume.GetComponents<IRoomTrigger>();

            // set our current room cluster
            Debug.Log("setting: " + currentRoomVolume.GetComponent<Josh.MusicRoomClusterDefinition>().assignedCluster);
            currentRoomCluster = currentRoomVolume.GetComponent<Josh.MusicRoomClusterDefinition>().assignedCluster;
            MusicPlayer.instance.currentRoomCluster = currentRoomCluster;

            foreach (IRoomTrigger roomTrigger in roomVolumeScripts)
            {
                roomTrigger?.OnEnterRoom();
                Debug.Log("running enter room");
            }
        }
    }
}