using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugReturnToHouseButton : MonoBehaviour
{
    public void TeleportToHouse()
    {
        DebugMover.instance.Teleport(DebugMover.instance.teleportLocations[0]);
    }
}
