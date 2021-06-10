using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;


public class SOUND_Footsteps : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    public LEVEL_surfaceType.surface currentSurface;
    private string currSurf;

    public void Step_Tip()
    {

        SetCurrentSurface();

        if (PLAYER_manager.Instance.playerMovement.dashInput) //Play running sound
        {
            switch (currentSurface)
            {
                case LEVEL_surfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Run_Tip", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Run_Tip", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Tile:
                    MasterAudio.PlaySound3DAtVector3("Tile_Run_Tip", targetTransform.position);
                    break;
            }
        }
        else //Play walking sound
        {
            switch (currentSurface)
            {
                case LEVEL_surfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Walk_Tip", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Walk_Tip", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Tile:
                    MasterAudio.PlaySound3DAtVector3("Tile_Walk_Tip", targetTransform.position);
                    break;
            }
        }

    }

    public void Step_Tap()
    {

        SetCurrentSurface();

        if (PLAYER_manager.Instance.playerMovement.dashInput) //Play running sound
        {
            switch (currentSurface)
            {
                case LEVEL_surfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Run_Tap", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Run_Tap", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Tile:
                    MasterAudio.PlaySound3DAtVector3("Tile_Run_Tap", targetTransform.position);
                    break;
            }
        }
        else //Play walking sound
        {
            switch (currentSurface)
            {
                case LEVEL_surfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Walk_Tap", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Walk_Tap", targetTransform.position);
                    break;
                case LEVEL_surfaceType.surface.Tile:
                    MasterAudio.PlaySound3DAtVector3("Tile_Walk_Tap", targetTransform.position);
                    break;
            }
        }
    }

    public void SkidSound()
    {

        SetCurrentSurface();

        switch (currentSurface)
        {
            case LEVEL_surfaceType.surface.Wood:
                MasterAudio.PlaySound3DAtVector3("Wood_Skid", targetTransform.position);
                break;
            case LEVEL_surfaceType.surface.Carpet:
                MasterAudio.PlaySound3DAtVector3("Carpet_Skid", targetTransform.position);
                break;
            case LEVEL_surfaceType.surface.Tile:
                MasterAudio.PlaySound3DAtVector3("Tile_Skid", targetTransform.position);
                break;
        }

    }

    public void SetCurrentSurface()
    {
        int layermask = LayerMask.GetMask("Surfaces");

        Ray ray = new Ray(transform.position + new Vector3(0, 0, -1), Vector3.forward);

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layermask);

        if (rayHit.collider!=null) { 
        currSurf = rayHit.collider.gameObject.tag.Trim();
        }

        if (currSurf.Equals("Carpet"))
        {
            currentSurface = LEVEL_surfaceType.surface.Carpet;
        }
        if (currSurf.Equals("Wood"))
        {
            currentSurface = LEVEL_surfaceType.surface.Wood;
        }
        if (currSurf.Equals("Tile"))
        {
            currentSurface = LEVEL_surfaceType.surface.Tile;
        }
    }

}
