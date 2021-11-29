using Opsive.UltimateInventorySystem.Core;
using Opsive.UltimateInventorySystem.ItemActions;
using Opsive.UltimateInventorySystem.ItemObjectBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShovelBehavior : ItemObjectBehaviour
{
    public override void Use(ItemObject itemObject, ItemUser itemUser)
    {
        int layermask = LayerMask.GetMask("DiggableGround");

        Ray ray = new Ray(PlayerManager.Instance.playerMovement.playerMovementModule.transform.position + new Vector3(0, 0, -1), Vector3.forward);

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layermask);

        if (rayHit.collider != null)
        {
            Debug.Log("Dig on this spot.");
        }
        else
        {
            Debug.Log("Found nothing to dig.");
        }
    }
}
