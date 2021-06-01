using UnityEngine;

public class CAMERA_vcam_trigger : MonoBehaviour
{

    public GameObject myCam;

    [Tooltip("Enable if the script should return to the previous camera on exiting the trigger collider.")]
    public bool returnToLastCam;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == ("Player"))
        {
            GAME_manager.Instance.cameraManager.SetNewLevelCamera(myCam);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == ("Player"))
        {
            if (returnToLastCam)
            {
                GAME_manager.Instance.cameraManager.SwapToLastLevelCamera();
            }
        }
    }
}
