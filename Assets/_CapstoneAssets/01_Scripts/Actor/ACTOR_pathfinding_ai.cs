using UnityEngine;
using System.Collections;
using Pathfinding;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class ACTOR_pathfinding_ai : MonoBehaviour {

	// Thanks to Brackeys of YouTube for the following :

	// What to move towards?
	public Transform target;
	// Apply target coordinates if not using a GameObject target.
	Vector3 targetCoordinates;

	[Tooltip("How many times a second each second the path will update.")]
	public float updateRate = 2f;

	Seeker seeker;
	Rigidbody2D rb;

	// The calculated path
	public Path myPath;

	// The AI's speed per minute
	public float moveSpeed = 300f;
	public ForceMode2D fMode; // A way to control the way in which force is applied to the rigidbody. Gives a way to control its movement.

	public bool pathIsEnded = true;

	[Tooltip("Max distance from the AI to a waypoint for it to continue to the next waypoint.")]
	public float nextWaypointDistanceLimit = 3;

	// The index of the waypoint currently being moved towards.
	int currentWaypoint = 0; 

	// Movement values -- just like those that would be derrived from an analog stick.
	float yMovementForshortenModifier = .13f;// <-- This modifier value feels pretty good. Change if you want. Copied from Player movement script.
	float inputX;
	float inputY;
	Vector3 moveVec; // To track where the rigidbody should move

	ACTOR_npc_manager.OnCommandComplete moveComplete;

	void Start(){
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		// By default, get the GameObject's coordinates.
		if (target != null) {
			targetCoordinates = target.transform.position;
		}

		if (target == null && targetCoordinates == null) {
			Debug.Log ("No target found");
			return;
		}

		// If a GameObject's Transform is set in the inspector, start pathfinding towareds it.
		if (target != null) {
			// Start a new path to the Target's position, and return the result to the OnPathComplete method.
			seeker.StartPath (transform.position, targetCoordinates, OnPathComplete);

			// Using a Couroutine here because drawing a new path every frame is going to make an unnecessary amount of calls.
			StartCoroutine (UpdatePath ());
		}
	}

	IEnumerator UpdatePath (){
		if (target == null && targetCoordinates == null) {
			// TODO: Search for a target.
			yield return false; // This is a modification of the original script since return false; wasn't working, hope this goes okay.
		}
		// If using a Transform to find a destination, and there's been a change to that transform.
		if (target != null) {
			if (targetCoordinates != target.position) {
				targetCoordinates = target.position;
			}
		}

		//Debug.Log ("Playing UpdatePath couroutine");

		// Start a new path to the Target's position, and return the result to the OnPathComplete method.
		//seeker.StartPath (transform.position, targetCoordinates, OnPathComplete);

		yield return new WaitForSeconds (1f / updateRate);

		// Loop the Coroutine by calling it again. Should disable this if the NPC isn't supposed to be moving currently.
		StartCoroutine (UpdatePath ());

	}

	public void OnPathComplete (Path newPath){
		Debug.Log ("Got a path. Did it have an error? " + newPath.error);
		if (!newPath.error) {
			myPath = newPath;
			currentWaypoint = 0;
		}
	}



	void Update(){
		SetMovementInput ();
	}

	void FixedUpdate(){
		if (target == null && targetCoordinates == null) {
			return;
		}

		// TODO: Show animation of the character looking at the target.

		if (myPath == null) {
			return;
		}

		// If it's reached its final waypoint (aka if the current waypoint index it's on is greater than the amount of waypoints there are), mark that the path has ended.
		if (currentWaypoint >= myPath.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}
			Debug.Log ("End of path reached.");
			pathIsEnded = true;
			// If given a moveComplete delegate from the NPC Manager...
			if (moveComplete != null) {
				moveComplete ();
				moveComplete = null;
			}
			return;
		} 
		pathIsEnded = false;

		// Movement from my "A Signal in Snowfall 2017" game hahah
		rb.velocity = moveVec;

		float waypointDistance = Vector3.Distance (transform.position, myPath.vectorPath [currentWaypoint]);
		if (waypointDistance < nextWaypointDistanceLimit) {
			currentWaypoint++;
			return;
		}
	}

	// To help avoid jitter, provide a buffer:
	public float positionBuffer = .4f;
	// Mimicing the player's controls behavior. This way the NPCs and the Player kind of fit together in the world.
	void SetMovementInput(){
		if (!pathIsEnded && myPath != null){
			if(currentWaypoint >= myPath.vectorPath.Count){
				return;
			}
			// Set X inputs in the direction of the next waypoint.

			// If it's within the buffer range, just set the input to 0f;
			if ((myPath.vectorPath [currentWaypoint].x < transform.position.x + positionBuffer) && (myPath.vectorPath [currentWaypoint].x > transform.position.x - positionBuffer)) {
				inputX = 0f;
			} else {
				if (transform.position.x < myPath.vectorPath [currentWaypoint].x) {
					inputX = 1f;
				} else if (transform.position.x > myPath.vectorPath [currentWaypoint].x) {
					inputX = -1f;
				}
			}
			// Set Y inputs ''

			// If it's within the buffer range, just set the input to 0f;
			if ((myPath.vectorPath [currentWaypoint].y < transform.position.y + positionBuffer) && (myPath.vectorPath [currentWaypoint].y > transform.position.y - positionBuffer)) {
				inputY = 0f;
			} else {
				if (transform.position.y < myPath.vectorPath [currentWaypoint].y) {
					inputY = 1f;
				} else if (transform.position.y > myPath.vectorPath [currentWaypoint].y) {
					inputY = -1f;
				}
			}

			// Tweak >diagonal< input so the NPC moves along the perspective grid.
			if (inputX != 0f && inputY != 0f) {
				float diagonalVerticalAdjust = .5f; // Tweak this value to get diagonal movement to match the perspective grid.
				if (inputY < 0) {
					inputY = inputY * diagonalVerticalAdjust;
				}
				if (inputY > 0) {
					inputY = inputY * diagonalVerticalAdjust;
				}
			}
		} else {
			// If pathIsEnded, just reset the values.
			inputX = 0f;
			inputY = 0f;
		}

		moveVec = transform.up * inputY * ((1f - yMovementForshortenModifier) * moveSpeed) // Foward and backward movement
			+ transform.right * inputX * moveSpeed; // Left and right movement
	}

	// For the animator to get information about what direction the NPC character is currently moving in 
	public Vector2 GetMovementValues(){
		return new Vector2 (inputX, inputY);
	}

	void BrackeysAIMovement(){
		// Now find the direction to the next waypoint.
		Vector3 dir = (myPath.vectorPath[currentWaypoint] - transform.position).normalized; // When working with these Vector3's, subtracting like so gets the direction to the target.

		dir *= moveSpeed * Time.fixedDeltaTime; // fixedDeltaTime because it's in fixed update here.

		// Move the AI
		rb.AddForce(dir, fMode); // Move in the direction of the next waypoint using the selected Force Mode.

		float waypointDistance = Vector3.Distance (transform.position, myPath.vectorPath [currentWaypoint]);
		if (waypointDistance < nextWaypointDistanceLimit) {
			currentWaypoint++;
			return;
		}
	}

	/// For when just using a Vector3 to select a destination.
	/*public void SetDestinationByCoordinates(Vector3 newCoordinates, ACTOR_npc_manager.OnCommandComplete onMoveCompleteCommand){
		moveComplete = onMoveCompleteCommand;
		target = null;
		targetCoordinates = newCoordinates;

		enabled = true;
		pathIsEnded = false;
		//Debug.Log ("Target coordinates: " + targetCoordinates);
		seeker.StartPath (transform.position, targetCoordinates, OnPathComplete);
		StartCoroutine (UpdatePath ());
	}*/
	/// For when using a GameObject in the scene to select a destination.
	public void SetDestinationByTransform(Transform newTarget, ACTOR_npc_manager.OnCommandComplete onMoveCompleteCommand){
		//pathIsEnded = false;
		target = newTarget;
		targetCoordinates = target.position;

		enabled = true;

		seeker.StartPath (transform.position, targetCoordinates, OnPathComplete);
		StartCoroutine (UpdatePath ());
		moveComplete = onMoveCompleteCommand;

	}
}
