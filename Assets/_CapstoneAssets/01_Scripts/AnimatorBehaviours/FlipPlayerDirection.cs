using UnityEngine;

public class FlipPlayerDirection : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        // So this is a little hack-y...
        // This checks to see if the player is inputing controls because if they are, don't have this script turn the character, let the controls do it.

        Vector2 movementInput = PlayerManager.Instance.playerInput.FreeControl.Move.ReadValue<Vector2>();

        if (movementInput.x == 0f && movementInput.y == 0f)
        {
            PlayerManager.Instance.playerMovement.SetStandingFaceDirection(animator.GetFloat("DirectionX") * -1, animator.GetFloat("DirectionY") * -1);
        }

    }
}