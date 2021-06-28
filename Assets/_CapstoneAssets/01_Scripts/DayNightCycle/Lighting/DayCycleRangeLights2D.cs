using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;
using System;

[Serializable]
public class DayCycleRangeLights2D : DayCycleRange
{
    //This is the first constructor for the class.
    //It calls the parent constructor immediately, even
    //before it runs.
    public DayCycleRangeLights2D()
    {

    }

    [Tooltip("Set up the lights affected.")]
    public Light2D[] roomLights;
}