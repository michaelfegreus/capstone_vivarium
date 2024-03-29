﻿using UnityEngine;
using Cinemachine;

public class VirtualCameraPlayerSetup : MonoBehaviour
{
    // Set up the virtual camera Follow to the Player Manager instance's sprite object.
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = PlayerManager.Instance.playerAnimation.spriteModule;
    }
}