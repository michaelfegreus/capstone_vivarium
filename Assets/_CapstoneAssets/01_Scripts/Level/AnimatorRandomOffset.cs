using UnityEngine;

public class AnimatorRandomOffset : MonoBehaviour
{
    [SerializeField]
    Animator[] OffsetAnimator;

    // Start is called before the first frame update
    void Start()
    {
        OffsetAnimator = GetComponentsInChildren<Animator>();

        foreach (Animator anim in OffsetAnimator) {
            anim.SetFloat("StartOffset", Random.Range(0f, 1f));
        }
    }
}
