using UnityEngine;
using UnityEngine.InputSystem;
using PixelCrushers.DialogueSystem;

public class PlayerStateFreeControl : IState {

	private GameObject playerObject;

	PlayerMovement movementScript;
	PlayerInteraction interactionScript;
	PlayerAnimation animationScript;
    PlayerToolUse toolUseScript;

    InputAction playerMovementDirection; // Cache the movement since it will continously be referenced.
    InputAction playerDash;

	// Constructor:
	public PlayerStateFreeControl(GameObject player){
		playerObject = player;
		movementScript = playerObject.GetComponent<PlayerMovement>();
		interactionScript = playerObject.GetComponent<PlayerInteraction>();
		animationScript = playerObject.GetComponent<PlayerAnimation>();
        toolUseScript = playerObject.GetComponent<PlayerToolUse>();
	}

	public void Enter(){
		Debug.Log ("Player entered free move state");
		// Now go ahead and enable the scripts that the player should have when they are free to move around.

        if(DialogueManager.instance.IsConversationActive == false)
        {
            movementScript.enabled = true;
            interactionScript.enabled = true;
            toolUseScript.enabled = true;

            animationScript.SetFreeStateBool(true);

            //PlayerManager.Instance.playerInput.Enable();
            playerMovementDirection = PlayerManager.Instance.playerInput.FreeControl.Move;
            playerDash = PlayerManager.Instance.playerInput.FreeControl.Dash;
        }

		

    }

    public void Execute()
    {
        // This serves as the current hub for controlling the player character in the game's action.

        // Interaction button check: (REMOVED for Pixel Crushers Dialogue System plugin
        /*if (Input.GetKeyDown (GAME_input_manager.Instance.actionButton1) || Input.GetKeyDown (GAME_input_manager.Instance.actionButton2)) {
			interactionScript.InteractInput ();
		}
		// Item Use button check:
		if (Input.GetKeyDown (GAME_input_manager.Instance.itemUseButton1) || Input.GetKeyDown (GAME_input_manager.Instance.itemUseButton2)) {
			interactionScript.UseItemInput (GAME_inventory_manager.Instance.equippedItem);
		}*/

        // Set player movement:

        // Run button check: ()
        // Might want to create some sort of dashing sub-state later to unify this. This will get animation, game mechanics, etc, on the same page.
        // (Sub-states might also be used for things like controlling the character while she's tired, walking with umbrella, etc.)

        /*if (Input.GetKey(GameInputManager.Instance.dashButton1) || Input.GetKey(GameInputManager.Instance.dashButton2)) {
			movementScript.SetDashInput (true);
		} else {
			movementScript.SetDashInput (false);
		}*/
        if (DialogueManager.instance.IsConversationActive == false)
        {
            movementScript.SetMovementAxes(playerMovementDirection.ReadValue<Vector2>().x, playerMovementDirection.ReadValue<Vector2>().y);

            if (playerDash.ReadValue<float>() > 0f)
            {
                movementScript.SetDashInput(true);
            }
            else
            {
                movementScript.SetDashInput(false);
            }
        }
    }

    public void Exit(){		
		// Reset analog stick input when exiting FreeControl state.
		movementScript.SetMovementAxes(0f, 0f);
		movementScript.SetDashInput (false);
        movementScript.StopMomentum();
        animationScript.SetFreeStateBool(false);

        interactionScript.enabled = false;
        toolUseScript.enabled = false;

    }

}