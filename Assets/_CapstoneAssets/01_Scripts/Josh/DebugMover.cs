using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMover : MonoBehaviour
{
    public List<Vector2> teleportLocations;

    public static DebugMover instance;
    void Awake() => instance = this;    

    void Update()
    {
        TeleportCheck();
    }

    void TeleportCheck()
    {
        KeyCode locationCode;

        if (Input.GetKey(KeyCode.T))
        {
            // check through all our keys and see which one is being pressed
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (kcode == KeyCode.T) continue;

                if (Input.GetKey(kcode))
                {
                    locationCode = kcode;
                    // teleport to the correlating location
                    Teleport(teleportLocations[(int)kcode-49]);
                    break;
                }
            }
        }

    }

    public void Teleport(Vector2 target)
    {
        transform.position = target;
    }
}
