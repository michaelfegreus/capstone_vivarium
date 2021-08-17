using UnityEngine;

public class ClimbLedgeReposition : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        PLAYER_manager.Instance.playerMovement.LedgeReposition();
    }
}