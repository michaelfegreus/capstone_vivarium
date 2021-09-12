using UnityEngine;

public class ClimbLedgeReposition : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        PlayerManager.Instance.playerMovement.LedgeClimbReposition();
    }
}