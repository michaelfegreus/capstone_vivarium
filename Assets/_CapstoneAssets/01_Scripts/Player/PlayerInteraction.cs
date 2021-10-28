﻿using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    // At the moment you could just ditch this script and toggle the module from the State machine directly, but I'll leave it in for now
    // since it's architected and incase I need to play with any other setups using interaction later.

    public SpriteRenderer selectorUI;
    public Transform playerMovementModule; // Also get a reference to the movement module to help set the direction the player is facing on interact.
    public ProximitySelector playerSelector;

    private void OnEnable()
    {
        selectorUI.enabled = true;
        playerSelector.enabled = true;
    }
    private void OnDisable()
    {
        selectorUI.enabled = false;
        playerSelector.enabled = false;
    }

    // You could move this to PLAYER_movement_directional_2d if you decide to get rid of the PLAYER_interaction script
    public void InteractionFaceDirection()
    {
        if (playerSelector.CurrentUsable != null)
        {

            Transform interactedObject = playerSelector.CurrentUsable.transform;
            Debug.Log(interactedObject.name);
            // Also, cause the player character to face what's being interacted with.
            float dirX;
            float dirY;
            if (playerMovementModule.position.x < interactedObject.transform.position.x)
            {
                dirX = 1f;
            }
            else
            {
                dirX = -1f;
            }
            if (playerMovementModule.position.y < interactedObject.transform.position.y)
            {
                dirY = 1f;
            }
            else
            {
                dirY = -1f;
            }
            PlayerManager.Instance.playerMovement.SetFaceDirection(dirX, dirY);
        }
    }
}


    /* Alright fellas pack it up I'm in therapy now and I'm making some changes in my life
     * I'm using the Pixel Crushers Dialogue System plugin and I'm not going to be using my own interaction or dialogue systems
     * So from here this should just accomodate the plugin.
     * Development should be handled minimally in code and mostly in the Editor going forward to make the process of making the game doable.
     * - 1/18/2021


	// This should live on the root of the player with other managing components.
	// This component correlates with an Interaction child object module that holds a triggerbox for checking interactables, transforming around, and snapping to directionals that match where the player animation looks like it's facing.
	// Need to rely on this script as well as a script living in the module because unfortunately Unity does not allow an object to access another object's collider.
	// Glad I left myself thorough notes lol - 9/6/18

	public GameObject playerInteractionModule;
	PLAYER_interaction_module moduleScript;

	public Transform playerMovementModule; // Also get a reference to the movement module to help set the direction the player is facing on interact.

	// An array that keeps track of all the things the trigger collider is colliding with.
	// This prevents the player from interacting with more than one thing at a time, should interactable objects be close enough.
	// This should be copied from the Module object's interactions. This is done because this Game Object can't access the Module's collider data by Unity's design.
	[SerializeField]
	GameObject[] nearbyInteractables;
	// What is currently being interacted with:
	GameObject interactedObject;
	// Cartoon thing that pops up above the player's head to notify that you're in front of something interactable.
	public GameObject exclamationUI;
	// If an item was collected during the interaction:
	[System.NonSerialized]
	public Item collectedItem;

    // Use this for initialization
    void Start () {
		moduleScript = playerInteractionModule.GetComponent<PLAYER_interaction_module> ();
		// Set the amount of interactables based off of how many the module has.
		nearbyInteractables = new GameObject[moduleScript.interactableArraySize];
	}

	// Update is called once per frame
	void Update () {
		
		// Make sure to not run this when paused:
		if(Time.timeScale == 0f) { return; }

		// Check to see if there should be a lightbulb above the character's head when near something interactable.
		CheckExclamationUI (); // Doing this every frame right now because I'm still working out this script.

		// Check to see if the Module has picked up any more interactable objects.
		UpdateInteractableArray();

		// Interact button input:
		//InteractInput ();
	}

	// When interact button is input:
	public void InteractInput(){
		int currentNearestObjectIndex = CheckNearestObjectSlot ();
		// If not null, and there are nearby interactables...
		if (currentNearestObjectIndex > -1) {
			interactedObject = nearbyInteractables [currentNearestObjectIndex];
		
			// Run OnInteract() function in all of the IInteractable Monobehaviors attached to the interactedObject 
			IInteractable[] interactedObjectMonobehaviors = interactedObject.GetComponents<IInteractable> ();
			foreach (IInteractable mb in interactedObjectMonobehaviors) {
				IInteractable interactable = (IInteractable)mb;
				interactable.OnInteract (); // This might need to get triggered at a specific frame of animation. i.e. Only remove item from the ground when the character's hand grasps it.

				// Also, cause the player character to face what's being interacted with.
				float dirX;
				float dirY;
				if (playerMovementModule.position.x < interactedObject.transform.position.x) {
					dirX = 1f;
				} else {
					dirX = -1f;
				}
				if(playerMovementModule.position.y < interactedObject.transform.position.y){
					dirY = 1f;
				}else{
					dirY = -1f;
				}
				PLAYER_manager.Instance.playerMovement.SetFaceDirection (dirX, dirY);
			}
		}
	}

	// If the player chooses to use an item on an interactable, run this.
	public void UseItemInput(Item itemUsed){
		if (itemUsed != null) {
			// Check through the array of interactables objects. Interact with the closest one.
			int currentNearestObjectIndex = CheckNearestObjectSlot ();
			// If not null, and there are nearby interactables...
			if (currentNearestObjectIndex > -1) {
				interactedObject = nearbyInteractables [currentNearestObjectIndex];

				// Run OnItemUse() function in all of the IItemTarget Monobehaviors attached to the interactedObject 
				IItemTarget[] interactedObjectMonobehaviors = interactedObject.GetComponents<IItemTarget> ();
				foreach (IItemTarget mb in interactedObjectMonobehaviors) {
					IItemTarget itemTargetInteractable = (IItemTarget)mb;
					itemTargetInteractable.OnItemUse (itemUsed);
				}
			}
		}
	}

	void UpdateInteractableArray(){
		for (int i = 0; i < nearbyInteractables.Length; i++) {
			nearbyInteractables [i] = moduleScript.moduleInteractables [i];
		}
	}

	int CheckNearestObjectSlot(){

		// Run a check of nearest object's index in the nearbyInteractables array.
		int nearestObjectIndex = -1; // Setting values because Unity asking that they not be empty.
		float nearestObjectDistance = 0; // Setting values because Unity asking that they not be empty.
		for (int i = 0; i < nearbyInteractables.Length; i++) {
			// If not null, check how far from the player object.
			if (nearbyInteractables [i] != null) {
				// If there's nothing in the nearestObjectDistance check yet, just take the first value. -1 means nothing was put in there.
				if (nearestObjectIndex == -1) {
					nearestObjectDistance = Vector3.Distance (nearbyInteractables [i].transform.position, transform.position);
					nearestObjectIndex = i; // The new nearest object is set.
				}
				// If there's a smaller distance between another object and the player, make that the thing to interact with.
				else if (Vector3.Distance (nearbyInteractables [i].transform.position, transform.position) < nearestObjectDistance) {
					nearestObjectDistance = Vector3.Distance (nearbyInteractables [i].transform.position, transform.position);
					nearestObjectIndex = i; // The new nearest object is set.
				}
			}
		}
		return nearestObjectIndex;
	}

	// If there are no nearby interactable objects, turn off the exclamation object UI.
	void CheckExclamationUI(){

		bool interactablesEmpty = true;

		for (int i = 0; i < nearbyInteractables.Length; i++) {
			if (nearbyInteractables [i] != null) {
				// In case the item was deactivated, but not destroyed (i.e. Key Items), take it out of the array.
				if (nearbyInteractables [i].activeInHierarchy == false) {
					nearbyInteractables [i] = null;
				} else {
					interactablesEmpty = false;
				}
				// UI on!
				exclamationUI.SetActive (true);
				return;
			}
		}
		if (interactablesEmpty) {
			// UI off
			exclamationUI.SetActive (false);
		}
	}

	public void DeactivateExclamationUI(){
		exclamationUI.SetActive (false);
	}
	/*
	void ItemCollect(){
		collectedItem = interactedObject.GetComponent<INTERACTABLE_item_pickup> ().GetItemReference();
	}*/

