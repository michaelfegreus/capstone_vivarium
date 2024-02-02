using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFadeUpAndDown : MonoBehaviour
{

    private Light2D light;

    [SerializeField]private float minIntensity;
    [SerializeField] private float maxIntensity;

    [SerializeField] private float increment;

    private bool fadingUp = true;



    private void Start()
    {
        light = GetComponent<Light2D>();
    }

    void Update()
    {
        if (fadingUp)
        {
            if(light.intensity <= maxIntensity)
            {
                light.intensity += increment*Time.deltaTime;
            }
            else
            {
                fadingUp = false;
            }
        }
        else
        {
            if (light.intensity >= minIntensity)
            {
                light.intensity -= increment * Time.deltaTime;
            }
            else
            {
                fadingUp = true;
            }
        }
    }

}
