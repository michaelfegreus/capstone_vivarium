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
                PlayerManager.Instance.EnterFreeState();
                break;
            case PlayerState.menu:
                PlayerManager.Instance.EnterMenuState();
                break;
        }
    }

}
