using UnityEngine;

public class PLAYER_movement_directional_2d : MonoBehaviour {

	// The child object of the player this is actually moving around. This allows for certain this to go off and transform, but the Player manager to remain at the root.
		/* So -- do I really need the Player object to sit at the root? The way I'm building the systems now, it seems like it would be okay to move the main object with the manager.
		The reason it's built this way is because movement is driven by rotation to a point, then acceleration. So it works like I originally built the 3D character.
		It causes issues to rotate the object containing the Sprite and the Interaction collider, however.
		I think this is still a good character controller, but it would be nice if everything was a bit less spread out. */

	public GameObject playerMovementModule;

    // Use a reference because the animation script gets called a lot.
    PLAYER_animation playerAnimation;

	Rigidbody2D rb;

	// Analog stick input
	[System.NonSerialized]
	public float inputX = 0f;
	[System.NonSerialized]
	public float inputY = 0f;
	public float inputDeadzoneThreshold = .01f; // How far you need to move the stick to get this script to register it as movement.
	public bool directionInput; // Is the player moving the analog stick?
    float inputAngle; // Calculated angle of input from the analog stick or keys.
    // Button input
    [System.NonSerialized] public bool dashInput;

    // Movespeed values
    public float walkStartupSpeed;
    public float dashStartupSpeed;
	public float walkSpeed;
	public float dashSpeed;
	float targetMoveSpeed; // Should the Game Object be trying to move at walk speed, dash speed, or another target speed?
    float currentMoveSpeed; // The speed multiplyer actively being applied to the force.
	//float targetMoveSpeed;
	// Acceleration rates
	//public float accelerationRate; // Rate at which the player accelerates to meet the target velocity.
	//public float deccelerationRate;

	// Rotation
	Quaternion desiredRotation; // Calculated r otation of where the player is analog sticks are rotating the player towards.
	//Vector3 movementVector; // Used to calculate direction analog stick is pointing.

    // For checking change in direction
    public float skidAngle; // Angle at which the character plays a skidding animation
    [SerializeField]
    bool skid; // Should the player character be skidding
    [SerializeField]
    float timeBetweenSkidReset = .05f; // How long to wait until the skid is neutralized from standing still.
    float skidResetTimer = 0f;

    public float halfTurnAngle; // Angle at which the character plays a half turn-to-walk animation
    public float fullTurnAngle; // Angle at which the character plays a full turn-to-walk animation
    //bool walkStartTurnCheck; // Flag to see if a "turn-to-walk" animation should play when moving from still

	// Use this to modify vertical movement speed of the player on the screen. This is like a movement for-shortening technique to make the character move in perspective more or less properly.
	// Should prioritize game-feel and what looks natural -- should be subtle.
	public float yMovementForshortenModifier = .13f; // <-- This modifier value feels pretty good. Change if you want.

    [Tooltip("Adjust diagonal input as it feeds into Desired Rotation. Try to get it to move her along the perspective grid.")]
    public float diagonalVerticalAdjust = .6f;

    // Disable and enable based on when you're dashing.
    public ParticleSystem dashParticles;

	void Start () {
		rb = playerMovementModule.GetComponent<Rigidbody2D> ();
        playerAnimation = PLAYER_manager.Instance.playerAnimation;
		targetMoveSpeed = walkSpeed;
	}

    void Update () {
        PlayerControls();
        inputAngle = Mathf.Atan2(inputX, inputY) * Mathf.Rad2Deg;
        // If the analog stick is moved...
        if (directionInput) {
            // If you just started moving, flag a check for a turn to walk animation.
            if(currentMoveSpeed == 0f)
            {
                WalkTurnCheck(inputAngle, true);
            }
            DirectionInputCheck(inputX, inputY);
            if (!skid)
            {
                playerMovementModule.transform.rotation = desiredRotation;
            }
            IncreaseToTargetSpeed();
            if (skidResetTimer != 0f)
            {
                skidResetTimer = 0f;
            }
        }
        else
        {
            // Reset current move speed
            currentMoveSpeed = 0f;
            if (skidResetTimer < timeBetweenSkidReset)
            {
                skidResetTimer += Time.deltaTime;
            }
        }

        // Character dash particles / dust cloud effects
        if(currentMoveSpeed < dashSpeed && dashParticles.isPlaying)
        {
            dashParticles.Stop();
        }
        else if(currentMoveSpeed >= dashSpeed && !dashParticles.isPlaying)
        {
            dashParticles.Play();
        }

        if (!skid)
        {
            playerAnimation.SetMovement(directionInput, new Vector2(inputX, inputY), inputDeadzoneThreshold, dashInput);
        }
    }

	void FixedUpdate (){
		// Move input that pushes the character forward towards the direction faced
		if (directionInput) {
			// Apply force to begin moving!
			rb.AddForce ((playerMovementModule.transform.up * -1f) * (currentMoveSpeed /*substract based on yMovementForshortenMod, if player is drawn from a camera angle needing compensation for moving in perspective*/ - Mathf.Abs(yMovementForshortenModifier * inputY * targetMoveSpeed)));
		}
	}

	void PlayerControls(){

		// Derive player controls from PLAYER_STATE_FreeControl.
		// This method interprets what to do with those controls once input.

		// Is the player moving the analog stick?
		if (inputX < -inputDeadzoneThreshold || inputX > inputDeadzoneThreshold || inputY > inputDeadzoneThreshold || inputY < -inputDeadzoneThreshold) {
			directionInput = true;
		} else {
			directionInput = false;
		}

        if (directionInput)
        {
            if (dashInput)
            {
                targetMoveSpeed = dashSpeed;
            }
            else
            {
                targetMoveSpeed = walkSpeed;
            }
        }
	}

    void IncreaseToTargetSpeed()
    {
        if (currentMoveSpeed < targetMoveSpeed)
        {
            // Pseudo-lerp
            // Increase based on walk or dash. Turn this into case switches or something similar if there expands to be more move speed states.
            if (dashInput)
            {
                currentMoveSpeed += dashStartupSpeed;
            }
            else
            {
                currentMoveSpeed += walkStartupSpeed;
            }
            // Lock it back to the target move speed if it goes over.
            if (currentMoveSpeed > targetMoveSpeed)
            {
                currentMoveSpeed = targetMoveSpeed;
            }
        }
        else
        {
            currentMoveSpeed = targetMoveSpeed;
        }
    }

	// Calculate which way you're facing
	void DirectionInputCheck(float inX, float inY){
		//movementVector = new Vector3 (inputX, inputY, 0.0f);
		//float inputAngle = Mathf.Atan2 (inX, inY) * Mathf.Rad2Deg; // Calculate angle of analog stick input.
		desiredRotation = Quaternion.Euler(new Vector3(0f, 0f, -1f * inputAngle + 180f));

        float deltaAngle = Quaternion.Angle(playerMovementModule.transform.rotation, desiredRotation); // Degree of change between current rotation and desired rotation.

        // For lerped rotation: 
        //angleDifference = Quaternion.Angle (transform.rotation, desiredRotation);

        if (Mathf.Abs(deltaAngle) > skidAngle && directionInput && skidResetTimer < timeBetweenSkidReset)
        {
            WalkTurnCheck(inputAngle, false);
            Debug.Log(deltaAngle);
            playerMovementModule.transform.rotation = desiredRotation;

            
            if (!dashInput)
            {
                playerAnimation.PlayAnimationState("WalkSkid");
            }
            else
            {
                playerAnimation.PlayAnimationState("RunSkid");

            }
            // Set the player into the Bespoke Action state. The Walk Skid animation will enable control again after it finishes playing
            //PLAYER_manager.Instance.EnterBespokeActionState();
            skid = true;
        }
        else
        {
            skid = false;
        }
	}

    void WalkTurnCheck(float inAngle, bool animateNow)
    {
        Vector3 vecPlayer = playerMovementModule.transform.rotation * Vector3.up;
        float anglePlayer = Mathf.Atan2(vecPlayer.x, vecPlayer.z) * Mathf.Rad2Deg;

        float angleDiff = Mathf.DeltaAngle(playerMovementModule.transform.rotation.eulerAngles.z, (-1f * inAngle + 180f)) * -1; // Multiply by -1 because I don't know why. Don't know how the f this worked lol.

        playerAnimation.SetTurnOriginDirection();
        if (!animateNow) {
            playerAnimation.SetTurnAngle(angleDiff);
        }

        Debug.Log(angleDiff);

        if (animateNow)
        {
            if (Mathf.Abs(angleDiff) > fullTurnAngle)
            {

                if (angleDiff > 0)
                {
                    playerAnimation.PlayAnimationState("TurnToWalk Full Clockwise");
                }
                else
                {
                    playerAnimation.PlayAnimationState("TurnToWalk Full Counterclockwise");
                }
            }
            else if (Mathf.Abs(angleDiff) > halfTurnAngle)
            {
                if (angleDiff > 0)
                {
                    playerAnimation.PlayAnimationState("TurnToWalk Half Clockwise");
                }
                else
                {
                    playerAnimation.PlayAnimationState("TurnToWalk Half Counterclockwise");
                }
            }
        }
    }

	public void SetDashInput(bool input){
		dashInput = input;
	}

	// The below could probably be...a lot slimmer in terms of lines of code.
	// But leave it unless it causes an issue, or you plan on adding more depth to input, because it works for now.

	// For use in fixing digital (keys or dpad) diagonal movement.
	bool digitalDiagonalMovement = false; // Is the player moving diagonally
	int digitalDiagonalCheckFrames = 4;
	int digitalDiagonalCheckCurrentFrame;
	float digitalDiagonalStoredInputX;
	float digitalDiagonalStoredInputY;

	public void SetMovementInput(float anX, float anY, float digX, float digY){

		float analogX = anX;
		float analogY = anY;

		// Set player movement with digital input:
		float digitalX = digX;
		float digitalY = digY;

		// Set player movement with digital input:
		SetMovementAxes (digitalX, digitalY);

		// Priorizite with analog stick input, if there is movement on the stick:
		if (analogX != 0f || analogY != 0f) {

			// Set the player movement with analog input:
			SetMovementAxes (analogX, analogY);
		}
		else {
			// The below fixes the digital diagonal movement error where the player character cannot standle in idle
			// at a diagonal angle. The reason for this is because when releasing diagonals on the keyboard,
			// the player will generally release one key a frame or two apart from the other. Using a frame timer,
			// the diagonal input can be stored and used.

			if (digitalX != 0f && digitalY != 0f) {
				digitalDiagonalMovement = true;
				digitalDiagonalCheckCurrentFrame = 0;
				digitalDiagonalStoredInputX = digitalX;
				digitalDiagonalStoredInputY = digitalY;
			}

			if (digitalDiagonalMovement) {
				digitalDiagonalCheckCurrentFrame++;
			}
			if ((digitalX == 0f || digitalY == 0f) && (digitalDiagonalCheckCurrentFrame >= digitalDiagonalCheckFrames)) {
				digitalDiagonalMovement = false;  
			}
			if (digitalDiagonalMovement && digitalDiagonalCheckCurrentFrame < digitalDiagonalCheckFrames) {
				SetMovementAxes (digitalDiagonalStoredInputX, digitalDiagonalStoredInputY);
			}

		}

	}

	// Convert all movement controls to digital input, and send those to the player movement script.
	void SetMovementAxes(float horzInput, float vertInput){

		inputX = 0f;
		inputY = 0f;

		if (horzInput > 0) {
			inputX = 1f;
		} else if (horzInput < 0) {
			inputX = -1f;
		}

		if (vertInput > 0) {
			inputY = 1f;
		} else if (vertInput < 0) {
			inputY = -1f;
		}

		// Tweak >diagonal< input so she moves along the perspective grid.
		if (inputX != 0f && inputY != 0f) {
			if (inputY < 0) {
				inputY = inputY * diagonalVerticalAdjust;
			}
			if (inputY > 0) {
				inputY = inputY * diagonalVerticalAdjust;
			}
		}
	}

	// Give analog directions, then have the character face that way.
	public void SetFaceDirection(float dirFaceX, float dirFaceY){
		// Set the player rotation.
		DirectionInputCheck(dirFaceX, dirFaceY);
		playerMovementModule.transform.rotation = desiredRotation;
		playerAnimation.SetMovement(true, new Vector2(dirFaceX, dirFaceY), inputDeadzoneThreshold, dashInput);
	}

    // Just set the direction the player is facing, but don't move them. Use this with Animator, or for things like cutscenes.
    public void SetStandingFaceDirection(float dirFaceX, float dirFaceY)
    {
        // Set the player rotation.
        DirectionInputCheck(dirFaceX, dirFaceY);
        playerMovementModule.transform.rotation = desiredRotation;
        playerAnimation.SetFacingDirection(new Vector2(dirFaceX, dirFaceY));
    }
    
    public void StopMomentum()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        currentMoveSpeed = 0f;
        targetMoveSpeed = 0f;
    }
}