﻿using UnityEngine;

[RequireComponent(typeof(GameInputManager))]
[RequireComponent(typeof(ClockManager))]
/*[RequireComponent(typeof(GAME_menu_manager))]
[RequireComponent(typeof(GAME_event_manager))]
[RequireComponent(typeof(Yarn.Unity.DialogueRunner))] // Yarn component
[RequireComponent(typeof(ExampleVariableStorage))] // Yarn component
[RequireComponent(typeof(YARN_ui_manager))] // Yarn component*/

// TODO: FIX PERSISTANT SINGLETONS
public class GameManager : Singleton/*Persistant*/<GameManager> {

	// Instantiate personal state machine
	private StateMachine gameStateMachine = new StateMachine();

	// Use this "protected override void awake" to ensure that this and the SingletonPersistant interface both prevent destruction on load.
	// Without this, the engine might just run GAME_manager's Awake, and not the SingletonPersistant's awake that keeps it from being destroyed

	// Delete this function if you're not using the SingletonPersistant interface though.
	/*protected override*/ void Awake(){
		//base.Awake ();
        // Can add more awake stuff below.
        inputManager = GetComponent<GameInputManager>();
        cameraManager = GetComponent<CameraManager>();
        clockManager = GetComponent<ClockManager>();
        /*menuManager = GetComponent<GAME_menu_manager>();
        eventManager = GetComponent<GAME_event_manager>();
        dialogRunner = GetComponent<Yarn.Unity.DialogueRunner>();
        variableStorageManager = GetComponent<ExampleVariableStorage>();
        yarnUIManager = GetComponent<YARN_ui_manager>();*/
    }




    // Set up components
    [System.NonSerialized]
	public GameInputManager inputManager;
	[System.NonSerialized]
    public CameraManager cameraManager;
    [System.NonSerialized]
    public ClockManager clockManager;
    /*[System.NonSerialized]
    public GAME_menu_manager menuManager;
    [System.NonSerialized]
	public GAME_event_manager eventManager;
	[System.NonSerialized]
	public Yarn.Unity.DialogueRunner dialogRunner;
	[System.NonSerialized]
	public ExampleVariableStorage variableStorageManager;
	[System.NonSerialized]
	public YARN_ui_manager yarnUIManager;*/

    public int targetFrameRate;

    void Start(){

		EnterFreeState ();

		Application.targetFrameRate = targetFrameRate;
	}


	void Update(){
		this.gameStateMachine.ExecuteStateUpdate ();
	}
		
	public void EnterFreeState(){
		this.gameStateMachine.ChangeState(new GameStateFree(gameObject));
	}

	// Pausing functionality. Not sure if I'll use it in this way.
	/*
	public static bool gamePauseEnabled;
		
	public void PauseToggle(bool turnOn){
		if (turnOn) {
			this.gameStateMachine.ChangeState (new GAME_STATE_Paused (gameObject, EnterFreeState));
			gamePauseEnabled = true;
		} else {
			this.gameStateMachine.SwitchToPreviousState();
			gamePauseEnabled = false;
		}
	}*/
}