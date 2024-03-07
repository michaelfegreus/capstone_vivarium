using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMover : MonoBehaviour
{
    [SerializeField] List<Vector2> teleportLocations;

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

    void Teleport(Vector2 target)
    {
        transform.position = target;
    }

    public void TeleportTo(int t)
    {
        Teleport(teleportLocations[t]);
    }
}
