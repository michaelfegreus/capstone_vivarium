using UnityEngine;
using Cinemachine;

public class CAMERA_vcam_trigger : MonoBehaviour, IRoomTrigger
{

    public CinemachineVirtualCamera myCam;
    /*
    [Tooltip("Enable if the script should return to the previous camera on exiting the trigger collider.")]
    public bool returnToLastCam;
    */

    [Tooltip("Enable if you just want to activate a camera separate from the room system, using this separate from the room system. It will automatically return to the last camera when you exit the volume. ")]
    [SerializeField] bool affectOnPlayerTrigger = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ("Player"))
        {
            if (affectOnPlayerTrigger)
            {
                GAME_manager.Instance.cameraManager.SetNewLevelCamera(myCam);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == ("Player"))
        {
            if (affectOnPlayerTrigger)
            {
                GAME_manager.Instance.cameraManager.SwapToLastLevelCamera();
            }
        }
    }
    
    public void OnEnterRoom()
    {
        GAME_manager.Instance.cameraManager.SetNewLevelCamera(myCam);
    }
    public void OnExitRoom()
    {

    }
}
