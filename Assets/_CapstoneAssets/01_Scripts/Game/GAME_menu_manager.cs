using UnityEngine.UI;
using UnityEngine;

public class GAME_menu_manager : Singleton<GAME_menu_manager>  {

	// Menu box from the UI canvas
	public Image menuBox;

	// Is the menu currently in use?
	bool inMenu;

	// Has something been selected? Run this delegate function (instead of setting a boolean flag for other scripts to look for)
	public delegate void OnReturnSelection(int selectedIndex);
	OnReturnSelection currentOnReturnSelection;

	// Should the menu close on selecting an item?
	bool _closeMenuOnSelect;

	//[System.NonSerialized]
	//public bool menuItemSelected = false;
	// Text Fields for options to appear in

	public SuperTextMesh[] optionSlots; // 02/19/2019 - Used to be Unity Text, now is Super Text Mesh.

	int optionsCount; // How many options there are for the current menu

	// UI cursor to select things in the menu
	public Image cursorSprite;
	int cursorIndex;// Slot that the cursor is hovering over
	int lastCursorIndex; // Last item the cursor was over. This is to reset effects if necessary.
	bool cursorMove = false; // Has the cursor just moved?
	// How many units should the menu box vertical size increase by
	public float verticalIncreaseUnits = 20f;

	//public string[] testOptions;

	string effectsString = "<w>"; // What effects will be added via rich text onto the currently highlighted option.

	public Color optionHighlightColor;

	// This function gets called from outside scripts and GameObjects to set up a menu.
	public void SetMenu(string[] optionArray, bool closeMenuOnSelect, OnReturnSelection newOnReturnSelection){
		_closeMenuOnSelect = closeMenuOnSelect;
		currentOnReturnSelection = newOnReturnSelection;

		// Reset values
		float verticalSizeUnits = 80f; // Start base size at 60f
		cursorIndex = 0;
		lastCursorIndex = -1; // Set this out of range so it doesn't affect the HighlightedTextEffects function yet.
		// Reset all Text fields.
		for (int i = 0; i < optionSlots.Length; i++) {
			optionSlots [i].gameObject.SetActive(false);
		}



		optionsCount = optionArray.Length;
		for(int i = 0; i < optionsCount; i++){ 
			verticalSizeUnits += verticalIncreaseUnits; // Increase size of the menu box depending on how many items are on the list
			optionSlots[i].gameObject.SetActive(true); // Activate Text fields for each option
			optionSlots[i].text = effectsString + optionArray[i]; // Put strings int he Text fields
			optionSlots[i].disableAnimatedText = true; // Initially disable all animated effects.
			optionSlots[i].color = Color.white; // Initially set all colors to default white.
		}

		HighlightedTextEffects (); // Set highlighted effects on the initial option.

		verticalSizeUnits = Mathf.Clamp(verticalSizeUnits, 100f, 480f); // Clamp to ensure that the text box doesn't get too big for the screen.
		menuBox.rectTransform.SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, verticalSizeUnits);

		// Turn on menu Game Object if it's not already active
		menuBox.gameObject.SetActive (true);
		inMenu = true;
	}

	public void CloseMenu(){
		menuBox.gameObject.SetActive (false);
		cursorSprite.gameObject.SetActive (false);
	}

	void Update (){

		if (inMenu) {
			UpdateCursorMoveIndex (); // Place the cursor.

			CheckCursorInput();

			CheckPlayerSelectionInput ();

		}
	}







	// Functions for controlling the menu when it has been opened

	void CheckCursorInput(){
		float inputThreshold = .2f; // Deadzone threshold for pushing the analog stick. Used in the next block.

		float inputY = Input.GetAxis("VerticalDigital");
		// Override with analog stick movement if the analog stick is pushed on the same frame.
		if(Mathf.Abs(Input.GetAxis("VerticalAnalog")) > inputThreshold){
			inputY = Input.GetAxis("VerticalAnalog");
		}

		// If you haven't just moved the cursor.
		if (!cursorMove) {
			// If input down
			if (inputY < -inputThreshold) {
				lastCursorIndex = cursorIndex;
				// If the next cursor slot is past the end of the array...
				if (cursorIndex + 1 == optionsCount) {
					//cursorIndex = 0;
				} else {
					cursorIndex++;
				}
				cursorMove = true;
				UpdateCursorMoveIndex ();
				//UpdateItemDescription();

			}
			// If input up
			if (inputY > inputThreshold) {
				lastCursorIndex = cursorIndex;
				// If the next cursor slot is below 0...
				if (cursorIndex - 1 < 0) {
					//cursorIndex = optionsCount - 1;
				} else {
					cursorIndex--;
				}
				cursorMove = true;
				UpdateCursorMoveIndex ();
				//UpdateItemDescription();

			}
		} else if (cursorMove && Mathf.Abs(inputY) < inputThreshold) {
			cursorMove = false;
		}
	}

	// Updates the current index of the cursor.
	void UpdateCursorMoveIndex(){
		// Sets the Y position of the cursor sprite equal to the y position of the current UI spot of the index.
		cursorSprite.transform.position = new Vector3(cursorSprite.transform.position.x, optionSlots [cursorIndex].transform.position.y, cursorSprite.transform.position.z);

		HighlightedTextEffects ();

		// Turn on cursor
		if (!cursorSprite.gameObject.activeInHierarchy) {
			cursorSprite.gameObject.SetActive (true);
		}
		// TODO: Update the item description box to reflect the newly highlighted item.

	}

	void HighlightedTextEffects(){

		// If there was a previously highlighted item, un-highlight.
		if (lastCursorIndex > -1) {
			optionSlots [lastCursorIndex].color = Color.white;
			optionSlots [lastCursorIndex].disableAnimatedText = true;
			optionSlots [lastCursorIndex].Rebuild ();
		}
		optionSlots [cursorIndex].color = optionHighlightColor;
		optionSlots [cursorIndex].disableAnimatedText = false;
		optionSlots [cursorIndex].Rebuild ();
	}

	void CheckPlayerSelectionInput(){
		if (Input.GetKeyDown(GAME_input_manager.Instance.selectionButton1) || Input.GetKeyDown(GAME_input_manager.Instance.selectionButton2)) {
			currentOnReturnSelection (cursorIndex); // Delegate function run from the class that originally opened the menu.
			if (_closeMenuOnSelect) { // Close and de-activate the menu if it's not needed.
				CloseMenu ();
				inMenu = false;
			}
		}
	}

	/*    Using a delegate function to call from outside this class instead. Originally was setting a flag and waiting others to check it.
	// Function for outside scripts to call when the player makes a selection. The outside script should know what to do with this slot ID.
	public int CheckPlayerSelectionIndex(){
		return cursorIndex;
	}*/
}