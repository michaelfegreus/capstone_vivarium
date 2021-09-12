using UnityEngine;

public class FlipPlayerDirection : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //animator.SetFloat("DirectionX", animator.GetFloat("DirectionX") * -1);
        //animator.SetFloat("DirectionY", animator.GetFloat("DirectionY") * -1);
        PlayerManager.Instance.playerMovement.SetStandingFaceDirection(animator.GetFloat("DirectionX") * -1, animator.GetFloat("DirectionY") * -1);

    }
}