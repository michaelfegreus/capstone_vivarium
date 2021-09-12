using UnityEngine;

public class BespokeActionAnimation : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (PlayerManager.Instance.GetPlayerState().GetType() == typeof(PlayerStateFreeControl)){
            PlayerManager.Instance.EnterBespokeActionState();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(PlayerManager.Instance.GetPlayerState().GetType() == typeof(PlayerStateBespokeAction)){
            PlayerManager.Instance.EnterFreeState();
        }
    }
}