using UnityEngine;
using Cinemachine;

public class RoomTriggerVirtualCamera : MonoBehaviour, IRoomTrigger
{

    public CinemachineVirtualCamera myCam;

    /*[Tooltip("Enable if you just want to activate a camera separate from the room system, using this separate from the room system. It will turn on the camera ")]
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
    }*/

    public void OnEnterRoom()
    {
        GameManager.Instance.cameraManager.SetNewLevelCamera(myCam);
    }
    public void OnExitRoom()
    {
        myCam.gameObject.SetActive(false);
    }
}
