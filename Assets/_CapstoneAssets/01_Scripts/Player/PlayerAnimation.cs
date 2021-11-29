using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	public Animator anim;
    public Animator animReflectY; // Y Axis reflection Animator. Used for things like reflective floors, puddles, etc.
	[HideInInspector] public Vector2 lastMove;

	bool moving;
	bool running;

	// The following allows the Sprite to follow along with the player's movement module positional transforms, while maintaining 0 rotational values.
	// It used to exist soley as its own monobehavior but I wanted to cut down on the amount of scripts.
	public Transform spriteModule;
	public Transform targetTransform;
	Vector3 spriteModuleOffset;

    public GameObject playerShadow;

	// This preserves the sprite's offset, as if it were a child of the MovementModule.
	void Start(){
		spriteModuleOffset = spriteModule.localPosition;
        animReflectY.fireEvents = false; // Disable animation events on the floor-reflection animator.
	}
	void Update(){
		// Offset the module
		spriteModule.transform.position = targetTransform.position + spriteModuleOffset;

		anim.SetBool("Moving", moving);
		anim.SetBool("DashInput", running);

        animReflectY.SetBool("Moving", moving);
        animReflectY.SetBool("DashInput", running);

        /*if (!moving) {
			anim.Play ("IdleStanding");
		} else {
			if (running)
			{
				anim.Play("Run");
			}
			else
			{
				anim.Play("Walk");
			}
		}*/
    }

	public void SetAnimationMovement(bool playerMoving, Vector2 playerMovement, float movementThreshold, bool _running){

		moving = playerMoving;
		running = _running;

		// lastMove is used for getting information about the last direction the player faced for idle and standing animations.
		if (playerMoving) {
			lastMove = new Vector2 (0f, 0f);
			if (playerMovement.x > movementThreshold || playerMovement.x < movementThreshold * -1f) {
					lastMove.x = playerMovement.x;
			}
			if (playerMovement.y > movementThreshold || playerMovement.y < movementThreshold * -1f) {
				lastMove.y = playerMovement.y;
			}
		}

		anim.SetFloat ("DirectionX", lastMove.x);
		anim.SetFloat ("DirectionY", lastMove.y);

        animReflectY.SetFloat("DirectionX", lastMove.x);
        animReflectY.SetFloat("DirectionY", lastMove.y);
    }
    
    /// Just set the direction the player is facing, but don't move them. Use this with Animator, or for things like cutscenes.
    public void SetFacingDirection(Vector2 playerMovement)
    {
        lastMove = playerMovement;
    }

    /// Jump to a specific animation by string
    public void PlayAnimationState(string animationStateName)
    {
        anim.Play(animationStateName, 0); // Always play from Layer 0 since this is 2D animation and only uses the base layer. I think.

        animReflectY.Play(animationStateName, 0);
    }

    [Tooltip("Sprites affiliated with the Player Character, including the main sprite, the shadow, and reflections.")]
    [SerializeField] SpriteRenderer[] characterSprites;

    // Hide affiliated sprites for cutscenes, interactables, and other non-player controlled situations.
    public void EnableDisableCharacterSprite(bool enable)
    {
        foreach(SpriteRenderer sprite in characterSprites)
        {
            sprite.enabled = enable;
        }
    }



    // More specific / misc. stuff goes below so it doesn't get bogged down here.

    /// Set the origin direction for TurnToWalk animations based on LastMove vector
    public void SetTurnOriginDirection()
    {
        anim.SetFloat("TurnOriginX", lastMove.x);
        anim.SetFloat("TurnOriginY", lastMove.y);

        animReflectY.SetFloat("TurnOriginX", lastMove.x);
        animReflectY.SetFloat("TurnOriginY", lastMove.y);
    }
    /// Set the direction of the Player character's turn rotation.
    /// Counterclockwise: Negative int  Neutral: 0   Clockwise: Positive int
    public void SetTurnAngle(float angleDifference)
    {
        
        anim.SetFloat("TurnAngle", angleDifference);

        animReflectY.SetFloat("TurnAngle", angleDifference);

    }

    public void SetFreeStateBool(bool freeState)
    {
        anim.SetBool("FreeState", freeState);
    }

    public void DisablePlayerShadow()
    {
        playerShadow.SetActive(false);
    }
    public void EnablePlayerShadow()
    {
        playerShadow.SetActive(true);
    }


    // Thanks to Llama_w_2Ls on Unity Answers for the following code for an animation coroutine queue:

    public void PlayTwoAnimationsQueued(string _firstState, string _secondState)
    {
        StartCoroutine(QueueAnimation(_firstState, _secondState));
    }

    /// <summary>
    /// Run this coroutine to play two animations, the second after the first finishes.
    /// </summary>
    /// <param name="First animation clip to play"></param>
    /// <param name="Second animation clip to play"></param>
    /// <returns></returns>
    IEnumerator QueueAnimation(string thisStateName, string nextStateName)
    {
        PlayAnimationState(thisStateName);

        // Wait till it's finished
        while (anim.GetCurrentAnimatorStateInfo(0).IsName(thisStateName))
        {
            Debug.Log("Still in first state.");
            yield return null;
        }
        
        PlayAnimationState(nextStateName);
    }

    /*
     * Here's the basis for a list queue if you end up needing it:

    List<string> QueuedClips;

    /// <summary>
    /// Play Queued Clips list.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator PlayQueuedAnimations(int index)
    {
        // Exit once all queued clips have been played
        if (QueuedClips.Count <= index)
            yield break;

        string thisClipName = QueuedClips[index];

        anim.Play(thisClipName);

        // Wait till it's finished
        while (anim.GetCurrentAnimatorStateInfo(0).IsName(thisClipName))
        {
            yield return null;
        }

        StartCoroutine(PlayQueuedAnimations(index + 1));
    }
    */

}