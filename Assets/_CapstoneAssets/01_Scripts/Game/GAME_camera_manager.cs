using UnityEngine;
using Cinemachine;

public class GAME_camera_manager : MonoBehaviour {

    GameObject currentLevelVCam;
    GameObject lastLevelVCam;

    public CinemachineBrain mainCamBrain;

    // Event delegate system used to check for level camera changes. Used by things like the Fungs Event Handler.
    public delegate void LevelScreenChange();
    public static event LevelScreenChange OnLevelScreenChange;

    private void Start()
    {
        if (mainCamBrain == null)
        {
            mainCamBrain = Camera.main.GetComponent<CinemachineBrain>();
        }
    }

    public void SetNewLevelCamera(GameObject newCamera)
    {
        if (currentLevelVCam != null)
        {
            lastLevelVCam = currentLevelVCam;
            lastLevelVCam.SetActive(false);
        }
        currentLevelVCam = newCamera;
        currentLevelVCam.SetActive(true);
        if (OnLevelScreenChange != null)
        {
            OnLevelScreenChange();
        }
    }

    public void SwapToLastLevelCamera()
    {
        if (lastLevelVCam != null)
        {
            currentLevelVCam.SetActive(false);
            lastLevelVCam.SetActive(true);
            GameObject swappingCam = currentLevelVCam;
            currentLevelVCam = lastLevelVCam;
            lastLevelVCam = swappingCam;
            if (OnLevelScreenChange != null)
            {
                OnLevelScreenChange();
            }
        }
        else
        {
            Debug.Log("No prior level cam to swap back to.");
        }
    }
}