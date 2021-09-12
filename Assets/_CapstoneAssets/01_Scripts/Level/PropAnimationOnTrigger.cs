using UnityEngine;

public class PropAnimationOnTrigger : MonoBehaviour
{
    public Animator anim;


    // HEADS UP -- you may need to code a check to see if this Animator has an Enter or Exit state when entering or exiting trigger, so it doesn't call a null animation state.

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Trim().Equals("Player".Trim()))
        {
            anim.Play("Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.Trim().Equals("Player".Trim()))
        {
            anim.Play("Exit");
        }
    }
}