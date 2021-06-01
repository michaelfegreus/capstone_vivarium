using UnityEngine;

public class BespokeActionAnimation : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (PLAYER_manager.Instance.GetPlayerState().GetType() == typeof(PLAYER_STATE_FreeControl)){
            PLAYER_manager.Instance.EnterBespokeActionState();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(PLAYER_manager.Instance.GetPlayerState().GetType() == typeof(PLAYER_STATE_BespokeAction)){
            PLAYER_manager.Instance.EnterFreeState();
        }
    }
}