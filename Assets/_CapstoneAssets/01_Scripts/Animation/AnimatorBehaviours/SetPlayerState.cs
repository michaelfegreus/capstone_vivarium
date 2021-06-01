using UnityEngine;

public class SetPlayerState : StateMachineBehaviour
{
    enum PlayerState
    {
        free,
        menu
    }

    [SerializeField]
    PlayerState goToState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        switch (goToState)
        {
            case PlayerState.free:
                PLAYER_manager.Instance.EnterFreeState();
                break;
            case PlayerState.menu:
                PLAYER_manager.Instance.EnterMenuState();
                break;
        }
    }

}
