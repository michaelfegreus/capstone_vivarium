using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;


public class SOUND_Footsteps : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    public void Step_Tip()
    {
        if (PLAYER_manager.Instance.playerMovement.dashInput) //Play running sound
        {
            switch (PLAYER_manager.Instance.playerMovement.playerMovementDelegateScript.currentSurface)
            {
                case LEVEL_surfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Run_Tip", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Run_Tip", targetTransform.position);
                    break;
            }
        } else //Play walking sound
        {
            switch (PLAYER_manager.Instance.playerMovement.playerMovementDelegateScript.currentSurface)
            {
                case LEVEL_surfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Walk_Tip", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Walk_Tip", targetTransform.position);
                    break;
            }
        }

    }

    public void Step_Tap()
    {
        if (PLAYER_manager.Instance.playerMovement.dashInput) //Play running sound
        {
            switch (PLAYER_manager.Instance.playerMovement.playerMovementDelegateScript.currentSurface)
            {
                case LEVEL_surfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Run_Tap", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Run_Tap", targetTransform.position);
                    break;
            }
        }
        else //Play walking sound
        {
            switch (PLAYER_manager.Instance.playerMovement.playerMovementDelegateScript.currentSurface)
            {
                case LEVEL_surfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Walk_Tap", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Walk_Tap", targetTransform.position);
                    break;
            }
        }
    }

}
