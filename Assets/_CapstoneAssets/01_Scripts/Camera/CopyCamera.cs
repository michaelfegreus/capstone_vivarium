using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCamera : MonoBehaviour
{
    private Camera cam;
    private Camera mainCam;
    [SerializeField] private float fieldOfViewMultipllier = 1;

    private void Start()
    {
        cam = GetComponent<Camera>();
        mainCam = Camera.main;
    }
    void Update()
    {
        cam.transform.position = mainCam.transform.position;
        cam.transform.rotation = mainCam.transform.rotation;
        cam.orthographicSize = mainCam.orthographicSize * fieldOfViewMultipllier;
    }
}