using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {

    CinemachineVirtualCamera currentLevelVCam;
    CinemachineVirtualCamera lastLevelVCam;

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

    public void SetNewLevelCamera(CinemachineVirtualCamera newCamera)
    {
        if (currentLevelVCam != null)
        {
            lastLevelVCam = currentLevelVCam;
            lastLevelVCam.gameObject.SetActive(false);
        }
        currentLevelVCam = newCamera;
        currentLevelVCam.gameObject.SetActive(true);
        if (OnLevelScreenChange != null)
        {
            OnLevelScreenChange();
        }
    }

    public void SwapToLastLevelCamera()
    {
        if (lastLevelVCam != null)
        {
            currentLevelVCam.gameObject.SetActive(false);
            lastLevelVCam.gameObject.SetActive(true);
            CinemachineVirtualCamera swappingCam = currentLevelVCam;
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