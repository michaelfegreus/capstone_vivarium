using UnityEngine;

public class DisableOnExit : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        animator.gameObject.SetActive(false);
    }
}