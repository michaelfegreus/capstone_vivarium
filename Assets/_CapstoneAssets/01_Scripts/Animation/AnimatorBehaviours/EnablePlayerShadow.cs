using UnityEngine;

public class EnablePlayerShadow : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        PLAYER_manager.Instance.playerAnimation.EnablePlayerShadow();
    }
}