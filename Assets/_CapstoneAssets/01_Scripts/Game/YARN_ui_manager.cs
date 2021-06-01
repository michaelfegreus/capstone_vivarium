﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
//using TMPro;
using System.Collections.Generic;

// The following is an edited version a Yarn Spinner package example script, adapted for Vivarium's purposes.

/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

	/// Displays dialogue lines to the player, and sends
	/// user choices back to the dialogue system.

	/** Note that this is just one way of presenting the
     * dialogue to the user. The only hard requirement
     * is that you provide the RunLine, RunOptions, RunCommand
     * and DialogueComplete coroutines; what they do is up to you.
     */
public class YARN_ui_manager : Yarn.Unity.DialogueUIBehaviour
{

	/// The object that contains the dialogue and the options.
	/** This object will be enabled when conversation starts, and 
     * disabled when it ends.
     */

	public GameObject dialogueContainer;

	/// The UI element that displays lines
	//public Text lineText;				 // Unity Text
	//public TextMeshProUGUI lineText;   // Text Mesh Pro
	public SuperTextMesh lineText;		 // Super Text Mesh

	/// A UI element that appears after lines have finished appearing
	public GameObject continuePrompt;

	/// A delegate (ie a function-stored-in-a-variable) that
	/// we call to tell the dialogue system about what option
	/// the user selected
	private Yarn.OptionChooser SetSelectedOption;

	///    **** Removing buttons in favor of my analog/d-pad controller menus
	/// The buttons that let the user choose an option
	//public List<Button> optionButtons;

	///    **** Instead, run this via the PLAYER_manager state machine.
	///         --- However, this could be useful for hiding things such as other UI (i.e. on-screen clock or money)
	/// Make it possible to temporarily disable the controls when
	/// dialogue is active and to restore them when dialogue ends
	public RectTransform gameControlsContainer;

	bool runningDialogue;
	bool runningOptions;
	bool skipLine;

	string endLineString = "\n<a=right><b><readDelay=0><c=blink>...</c>"; // Use this as an alternative to the end line prompt sprite. Using rich text, this can be aligned, and take the form of animated sprite, text, or otherwise.

	/* Audio Source blip SFX
	 * // Text scroll sound.
	public AudioSource blipSound;
	bool alternateBlip;
	*/

	void Awake ()
	{
		// Start by hiding the container, line and option buttons
		if (dialogueContainer != null)
			dialogueContainer.SetActive(false);

		lineText.gameObject.SetActive (false);

		/*foreach (var button in optionButtons) {
			button.gameObject.SetActive (false);
		}*/

		// Hide the continue prompt if it exists
		if (continuePrompt != null)
			continuePrompt.SetActive (false);
	}

	void Update(){
		// For skipping to end of line:
		if (runningDialogue) {
			if (Input.GetKeyDown(GAME_input_manager.Instance.selectionButton1) || Input.GetKeyDown(GAME_input_manager.Instance.selectionButton2)) {
				skipLine = true;
				lineText.SkipToEnd();
			}
		}
	}

	/// Show a line of dialogue, gradually
	public override IEnumerator RunLine (Yarn.Line line)
	{
		// Show the text
		lineText.gameObject.SetActive (true);
		//textSpeed = storedTextSpeed;

		// Display the line
		lineText.text = line.text + endLineString;


		while (lineText.reading || skipLine) {
			skipLine = false;
			yield return null;
		}

		// Show the 'press any key' prompt when done, if we have one
		if (continuePrompt != null)
			continuePrompt.SetActive (true);
		
		// Wait for any user input
		while (!Input.GetKeyDown(GAME_input_manager.Instance.selectionButton1) && !Input.GetKeyDown(GAME_input_manager.Instance.selectionButton2)) {
			
			yield return null;
		}

		// Hide the text and prompt
		if(!runningOptions)
			lineText.gameObject.SetActive (false);

		if (continuePrompt != null)
			continuePrompt.SetActive (false);

	}

	/// Show a list of options, and wait for the player to make a selection.
	public override IEnumerator RunOptions (Yarn.Options optionsCollection, 
		Yarn.OptionChooser optionChooser)
	{
		// Do a little bit of safety checking
		/*if (optionsCollection.options.Count > optionButtons.Count) {
                Debug.LogWarning("There are more options to present than there are" +
                                 "buttons to present them in. This will cause problems.");
            }*/

		// Display each option in a button, and make it visible
		// *** Going to try to remove this, and replace it with my menu system.
		/*
			int i = 0;
            foreach (var optionString in optionsCollection.options) {
                optionButtons [i].gameObject.SetActive (true);
                optionButtons [i].GetComponentInChildren<Text> ().text = optionString;
                i++;
            }*/

		runningOptions = true;

		// Make an array out of the the options list strings.
		string[] optionStrings;
		optionStrings = new string[optionsCollection.options.Count];

		int i = 0;
		foreach (var optionString in optionsCollection.options) {
			optionStrings [i] = optionString;
			i++;
		}

		// My menu system:
		GAME_menu_manager.Instance.SetMenu (optionStrings, true, SetOption);

		// Record that we're using it
		SetSelectedOption = optionChooser;

		// Wait until the chooser has been used and then removed (see SetOption below)
		while (SetSelectedOption != null) {
			yield return null;
		}
		runningOptions = false;

		// Hide all the buttons
		/*foreach (var button in optionButtons) {
			button.gameObject.SetActive (false);
		}*/
	}

	/// Called by GAME_menu_manager singleton to make a selection.
	public void SetOption (int selectedOption)
	{

		// Call the delegate to tell the dialogue system that we've
		// selected an option.
		SetSelectedOption (selectedOption);

		// Now remove the delegate so that the loop in RunOptions will exit
		SetSelectedOption = null; 
	}

	/// Run an internal command.
	public override IEnumerator RunCommand (Yarn.Command command)
	{
		// "Perform" the command
		Debug.Log ("Command: " + command.text);

		yield break;
	}

	/// Called when the dialogue system has started running.
	public override IEnumerator DialogueStarted ()
	{
		//Debug.Log ("Dialogue starting!");
		runningDialogue = true;

		// Enable the dialogue controls.
		if (dialogueContainer != null)
			dialogueContainer.SetActive(true);


		// Set the player in the menu state
		PLAYER_manager.Instance.EnterMenuState();

		// Hide the game controls.
		if (gameControlsContainer != null) {
			gameControlsContainer.gameObject.SetActive(false);
		}

		yield break;
	}

	/// Called when the dialogue system has finished running.
	public override IEnumerator DialogueComplete ()
	{
		//Debug.Log ("Complete!");
		runningDialogue = false;

		// Hide the dialogue interface.
		if (dialogueContainer != null)
			dialogueContainer.SetActive(false);

		// Show the game controls.
		if (gameControlsContainer != null) {
			gameControlsContainer.gameObject.SetActive(true);
		}
			
		// Set the player in the free state
		PLAYER_manager.Instance.EnterFreeState();

		yield break;
	}

}