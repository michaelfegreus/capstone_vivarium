using UnityEngine;

public class DisablePlayerShadow : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        PLAYER_manager.Instance.playerAnimation.DisablePlayerShadow();
    }
}