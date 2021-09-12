using UnityEngine;

public class EnablePlayerShadow : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        PlayerManager.Instance.playerAnimation.EnablePlayerShadow();
    }
}