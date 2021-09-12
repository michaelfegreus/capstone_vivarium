using UnityEngine;

public class JumpLedgeReposition : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        PlayerManager.Instance.playerMovement.LedgeJumpReposition();
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        PlayerManager.Instance.playerMovement.InitiateLedgeFall();
    }
}