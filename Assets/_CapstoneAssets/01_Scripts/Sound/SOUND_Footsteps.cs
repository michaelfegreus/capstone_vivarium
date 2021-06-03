using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;


public class SOUND_Footsteps : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    

    [SerializeField]
    private enum surface
    {
        Wood,
        Tile,
        Carpet
    }

    private surface currSurface;


    void Start()
    {
        //This is for debug purposes only, delete this eventually
        currSurface = surface.Wood;
    }


    void Update()
    {
        
    }

    public void Step_Tip()
    {
        switch (currSurface)
        {
            case surface.Wood:
                MasterAudio.PlaySound3DAtVector3("Wood_Tip", targetTransform.position);
                break;
        }
    }

    public void Step_Tap()
    {
        switch (currSurface)
        {
            case surface.Wood:
                MasterAudio.PlaySound3DAtVector3("Wood_Tap", targetTransform.position);
                break;
        }
    }

}
