using UnityEngine;

public class PlayerShovel : PlayerTool
{
    Transform playerPosition;

    public override void ToolAction()
    {
        base.ToolAction();

        CheckDiggableGround();
    }

    public void CheckDiggableGround()
    {
        if (playerPosition == null)
        {
            // Cache player position on first use.
            playerPosition = PlayerManager.Instance.playerMovement.playerMovementModule.transform;
        }

        int layermask = LayerMask.GetMask("DiggableGround");

        Ray ray = new Ray(playerPosition.position + new Vector3(0, 0, -1), Vector3.forward);

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layermask);

        if (rayHit.collider != null)
        {
            Debug.Log("Dig on this spot.");
        }
    }
}
