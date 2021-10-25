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

    [Tooltip("Default to On State Enter, but tick this to switch to OnStateExit.")]
    [SerializeField]
    bool triggerOnStateExit;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(!triggerOnStateExit)
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

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (triggerOnStateExit)
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
