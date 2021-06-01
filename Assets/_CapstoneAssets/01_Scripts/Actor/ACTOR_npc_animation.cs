using UnityEngine;

public class ACTOR_npc_animation : MonoBehaviour {

	public Animator anim;
	ACTOR_pathfinding_ai pathfinder;

	Vector2 inputVector;
	Vector2 lastMove;

	// Deadzone for movement
	float movementThreshold = .4f;


	// Use this for initialization
	void Start () {
		pathfinder = GetComponent<ACTOR_pathfinding_ai> ();
	}
	
	// Update is called once per frame
	void Update () {
		inputVector = pathfinder.GetMovementValues();
		if (pathfinder.pathIsEnded) {
			anim.Play ("IdleStanding");
		} else {
			anim.Play ("Walk");
		}

		if (!pathfinder.pathIsEnded) {
			lastMove = new Vector2 (0f, 0f);
			if (inputVector.x > movementThreshold || inputVector.x < movementThreshold * -1f) {
				//lastMove = new Vector2 (playerMovement.x, 0f);
				//if (playerMovement.x != 0) {
				lastMove.x = inputVector.x;
				//}

			}
			if (inputVector.y > movementThreshold || inputVector.y < movementThreshold * -1f) {
				//lastMove = new Vector2 (0f, playerMovement.y);
				//if (playerMovement.y != 0) {
				lastMove.y = inputVector.y;
				//	}
			}
		}

		anim.SetFloat ("DirectionX", lastMove.x);
		anim.SetFloat ("DirectionY", lastMove.y);
	}
}
