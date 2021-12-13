using Opsive.UltimateInventorySystem.Core.DataStructures;
using Opsive.UltimateInventorySystem.ItemActions;
using UnityEngine;

public class UseShovelItemAction : ItemAction
{
    protected override bool CanInvokeInternal(ItemInfo itemInfo, ItemUser itemUser)
    {
        return true;
    }

    protected override void InvokeActionInternal(ItemInfo itemInfo, ItemUser itemUser)
    {
        int layermask = LayerMask.GetMask("DiggableGround");

        Ray ray = new Ray(PlayerManager.Instance.playerMovement.playerMovementModule.transform.position + new Vector3(0, 0, -1), Vector3.forward);

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layermask);

        if (rayHit.collider != null)
        {
            Debug.Log("Dig on this spot.");
            PlayerManager.Instance.playerAnimation.PlayAnimationState("Shovel");
        }
        else
        {
            Debug.Log("Found nothing to dig.");
            PlayerManager.Instance.playerAnimation.PlayAnimationState("ToolShrug");
        }
    }
}
