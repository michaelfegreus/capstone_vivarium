using UnityEngine;

public class SetPlayerDirection : StateMachineBehaviour
{
    [SerializeField]
    [Range(-1, 1f)]
    float xDirection;

    [SerializeField]
    [Range(-1, 1f)]
    float yDirection;

    [SerializeField]
    [Tooltip("Reset the Player Movement to 0, such as in the event of facing downward.")]
    bool resetPlayerRotation;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        PlayerManager.Instance.playerMovement.SetStandingFaceDirection(xDirection, yDirection);
        if (resetPlayerRotation)
        {
            PlayerManager.Instance.playerMovement.playerMovementModule.transform.rotation = Quaternion.identity;
        }
    }

}
