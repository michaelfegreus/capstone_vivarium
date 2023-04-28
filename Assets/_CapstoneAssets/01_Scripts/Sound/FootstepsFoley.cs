using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;


public class FootstepsFoley : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    public SurfaceType.surface currentSurface;
    private string currSurf = "Wood"; // Setting this to default to stop throwing errors.

    public void Step_Tip()
    {

        SetCurrentSurface();

        if (PlayerManager.Instance.playerMovement.dashInput) //Play running sound
        {
            switch (currentSurface)
            {
                case SurfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Run_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Run_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Tile:
                    MasterAudio.PlaySound3DAtVector3("Tile_Run_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Dirt:
                    MasterAudio.PlaySound3DAtVector3("Dirt_Run_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Grass:
                    MasterAudio.PlaySound3DAtVector3("Grass_Run_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Leaves:
                    MasterAudio.PlaySound3DAtVector3("Leaves_Run_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Puddle:
                    MasterAudio.PlaySound3DAtVector3("Puddle_Run_Tip", targetTransform.position);
                    break;
            }
        }
        else //Play walking sound
        {
            switch (currentSurface)
            {
                case SurfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Walk_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Walk_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Tile:
                    MasterAudio.PlaySound3DAtVector3("Tile_Walk_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Dirt:
                    MasterAudio.PlaySound3DAtVector3("Dirt_Walk_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Grass:
                    MasterAudio.PlaySound3DAtVector3("Grass_Walk_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Leaves:
                    MasterAudio.PlaySound3DAtVector3("Leaves_Walk_Tip", targetTransform.position);
                    break;
                case SurfaceType.surface.Puddle:
                    MasterAudio.PlaySound3DAtVector3("Puddle_Walk_Tip", targetTransform.position);
                    break;
            }
        }

    }

    public void Step_Tap()
    {

        SetCurrentSurface();

        if (PlayerManager.Instance.playerMovement.dashInput) //Play running sound
        {
            switch (currentSurface)
            {
                case SurfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Run_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Run_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Tile:
                    MasterAudio.PlaySound3DAtVector3("Tile_Run_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Dirt:
                    MasterAudio.PlaySound3DAtVector3("Dirt_Run_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Grass:
                    MasterAudio.PlaySound3DAtVector3("Grass_Run_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Leaves:
                    MasterAudio.PlaySound3DAtVector3("Leaves_Run_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Puddle:
                    MasterAudio.PlaySound3DAtVector3("Puddle_Run_Tap", targetTransform.position);
                    break;
            }
        }
        else //Play walking sound
        {
            switch (currentSurface)
            {
                case SurfaceType.surface.Wood:
                    MasterAudio.PlaySound3DAtVector3("Wood_Walk_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Carpet:
                    MasterAudio.PlaySound3DAtVector3("Carpet_Walk_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Tile:
                    MasterAudio.PlaySound3DAtVector3("Tile_Walk_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Dirt:
                    MasterAudio.PlaySound3DAtVector3("Dirt_Walk_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Grass:
                    MasterAudio.PlaySound3DAtVector3("Grass_Walk_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Leaves:
                    MasterAudio.PlaySound3DAtVector3("Leaves_Walk_Tap", targetTransform.position);
                    break;
                case SurfaceType.surface.Puddle:
                    MasterAudio.PlaySound3DAtVector3("Puddle_Walk_Tap", targetTransform.position);
                    break;
            }
        }
    }

    public void SkidSound()
    {

        SetCurrentSurface();

        switch (currentSurface)
        {
            case SurfaceType.surface.Wood:
                MasterAudio.PlaySound3DAtVector3("Wood_Skid", targetTransform.position);
                break;
            case SurfaceType.surface.Carpet:
                MasterAudio.PlaySound3DAtVector3("Carpet_Skid", targetTransform.position);
                break;
            case SurfaceType.surface.Tile:
                MasterAudio.PlaySound3DAtVector3("Tile_Skid", targetTransform.position);
                break;
            case SurfaceType.surface.Dirt:
                MasterAudio.PlaySound3DAtVector3("Dirt_Skid", targetTransform.position);
                break;
            case SurfaceType.surface.Grass:
                MasterAudio.PlaySound3DAtVector3("Grass_Skid", targetTransform.position);
                break;
            case SurfaceType.surface.Leaves:
                MasterAudio.PlaySound3DAtVector3("Leaves_Skid", targetTransform.position);
                break;
            case SurfaceType.surface.Puddle:
                MasterAudio.PlaySound3DAtVector3("Puddle_Skid", targetTransform.position);
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
            currentSurface = SurfaceType.surface.Carpet;
        }
        if (currSurf.Equals("Wood"))
        {
            currentSurface = SurfaceType.surface.Wood;
        }
        if (currSurf.Equals("Tile"))
        {
            currentSurface = SurfaceType.surface.Tile;
        }
        if (currSurf.Equals("Dirt"))
        {
            currentSurface = SurfaceType.surface.Dirt;
        }
        if (currSurf.Equals("Grass"))
        {
            currentSurface = SurfaceType.surface.Grass;
        }
        if (currSurf.Equals("Leaves"))
        {
            currentSurface = SurfaceType.surface.Leaves;
        }
        if (currSurf.Equals("Water"))
        {
            currentSurface = SurfaceType.surface.Puddle;
        }
    }

}
