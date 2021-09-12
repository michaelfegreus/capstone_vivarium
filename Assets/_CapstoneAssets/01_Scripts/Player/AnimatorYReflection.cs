using UnityEngine;

public class AnimatorYReflection : MonoBehaviour
{

    Animator myAnimator;
    [Tooltip("Animation to read currently playing clips off of and reflect.")]
    public Animator sourceAnimator;

    AnimatorClipInfo[] currentSourceClipInfo;
    string currentClipName;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        int w = sourceAnimator.GetCurrentAnimatorClipInfo(0).Length;
        for (int i = 0; i < w; i += 1)
        {
            currentClipName = myAnimator.GetCurrentAnimatorClipInfo(0)[i].clip.name;
        }

        currentSourceClipInfo = sourceAnimator.GetCurrentAnimatorClipInfo(0);
        currentClipName = currentSourceClipInfo[0].clip.name;

        myAnimator.Play(currentClipName, 0);
    }
}
