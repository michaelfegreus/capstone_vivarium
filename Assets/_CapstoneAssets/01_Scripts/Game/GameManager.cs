﻿using UnityEngine;

[RequireComponent(typeof(GameInputManager))]
[RequireComponent(typeof(ClockManager))]

// TODO: FIX PERSISTANT SINGLETONS
public class GameManager : Singleton/*Persistant*/<GameManager> {

	// Instantiate personal state machine
	private StateMachine gameStateMachine = new StateMachine();

    [System.NonSerialized]
    public MenuInput menuInput;

    public NotebookMenuManager notebookMenuManager;

    // Use this "protected override void awake" to ensure that this and the SingletonPersistant interface both prevent destruction on load.
    // Without this, the engine might just run GAME_manager's Awake, and not the SingletonPersistant's awake that keeps it from being destroyed

    // Delete this function if you're not using the SingletonPersistant interface though.
    /*protected override*/
    void Awake(){
		//base.Awake ();
        // Can add more awake stuff below.
        inputManager = GetComponent<GameInputManager>();
        cameraManager = GetComponent<CameraManager>();
        clockManager = GetComponent<ClockManager>();

        menuInput = new MenuInput();
        menuInput.Enable();

        EnterFreeState();
    }

    // Set up components
    [System.NonSerialized]
	public GameInputManager inputManager;
	[System.NonSerialized]
    public CameraManager cameraManager;
    [System.NonSerialized]
    public ClockManager clockManager;

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