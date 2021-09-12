using UnityEngine;

public class DisablePlayerShadow : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        PlayerManager.Instance.playerAnimation.DisablePlayerShadow();
    }
}