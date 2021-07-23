using UnityEngine;

public class PLAYER_room_check : MonoBehaviour
{
    int layermask;

    Ray ray;
    RaycastHit2D rayHit;

    Collider2D currentRoomVolume;
    IRoomTrigger[] roomVolumeScripts;

    private void Start()
    {
         layermask = LayerMask.GetMask("Rooms");
    }

    private void Update()
    {
        ray = new Ray(transform.position + new Vector3(0, 0, -1), Vector3.forward);

        rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layermask);

        if (rayHit.collider!=null) { 
            if(currentRoomVolume == null)
            {
                // Set up starting room

                currentRoomVolume = rayHit.collider;
                roomVolumeScripts = currentRoomVolume.GetComponents<IRoomTrigger>();

                foreach(IRoomTrigger roomTrigger in roomVolumeScripts)
                {
                    roomTrigger.OnEnterRoom();
                }

            }
            else if (currentRoomVolume != null && currentRoomVolume != rayHit.collider) {
                // Change rooms

                // Run exit functions on current
                foreach (IRoomTrigger roomTrigger in roomVolumeScripts)
                {
                    roomTrigger.OnExitRoom();
                }

                // Set up new room
                currentRoomVolume = rayHit.collider;
                roomVolumeScripts = currentRoomVolume.GetComponents<IRoomTrigger>();

                foreach (IRoomTrigger roomTrigger in roomVolumeScripts)
                {
                    roomTrigger.OnEnterRoom();
                }
            }

        }
        //Currently does not have an option where you can leave all rooms and have none set active, but that's something you could set up later 
    }
    
}