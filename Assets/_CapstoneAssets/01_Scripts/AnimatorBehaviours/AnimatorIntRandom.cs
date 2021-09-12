using UnityEngine;

public class AnimatorIntRandom : StateMachineBehaviour
{
    [Tooltip("Upper bounds of the random int selection. (Lower bounds is 0.)")]
    [SerializeField]
    int randomLimit;

    [SerializeField]
    public string parameterIntegerToSet;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        // animator.gameObject.SetActive(false);

        //		anim.SetFloat ("DirectionX", lastMove.x);

        animator.SetInteger(parameterIntegerToSet, Random.Range(0, randomLimit + 1));
    }
}
