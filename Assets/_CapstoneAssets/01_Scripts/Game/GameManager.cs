using Opsive.UltimateInventorySystem.Core.InventoryCollections;
using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using Opsive.UltimateInventorySystem.Core;

[RequireComponent(typeof(GameInputManager))]
[RequireComponent(typeof(ClockManager))]

// TODO: FIX PERSISTENT SINGLETONS
public class GameManager : Singleton/*Persistent*/<GameManager> {

	// Instantiate personal state machine
	private StateMachine gameStateMachine = new StateMachine();

	[System.NonSerialized]
	public MenuInput menuInput;

	public bool startFromBeginning;

	public NotebookMenuManager notebookMenuManager;


	public ItemHotbarManager ItemHotbarManager;


	public Inventory playerInventory;
	public StandardUIQuestTracker questHUD;

	public Opsive.UltimateInventorySystem.UI.Panels.DisplayPanel cookingDisplayPanel;

	[SerializeField] private SeedPlotLogic currSeedPlot;

	[SerializeField] private ItemDefinition currItemInUse;

	// Use this "protected override void awake" to ensure that this and the SingletonPersistant interface both prevent destruction on load.
	// Without this, the engine might just run GAME_manager's Awake, and not the SingletonPersistent's awake that keeps it from being destroyed

	// Delete this function if you're not using the SingletonPersistent interface though.
	/*protected override*/
	void Awake() {
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

	void Start() {



		Application.targetFrameRate = targetFrameRate;

		if (startFromBeginning)
		{
			PlayerManager.Instance.EnterMenuState();
			DialogueManager.PlaySequence("Fade(stay,0.1,#000000)"); 
			StartCoroutine("StartDemo");
        }
        else
        {
			EnterFreeState();
		}

	}


	IEnumerator StartDemo()
    {
		yield return new WaitForSeconds(0.1f);
		cookingDisplayPanel.SmartClose();
        yield return new WaitForSeconds(2.9f);
		DialogueManager.instance.StartConversation("DemoStartDialogue");
	}

	void Update() {
		this.gameStateMachine.ExecuteStateUpdate();
        
	}

	public void EnterFreeState()
	{
		this.gameStateMachine.ChangeState(new GameStateFree(gameObject));
	}

	public void showQuestHUD()
	{
		questHUD.ShowTracker();
	}

	public void hideQuestHUD()
    {
		questHUD.HideTracker();
    }

	public void SetCurrentSeedPlot(SeedPlotLogic plot)
    {
		currSeedPlot = plot;
    }

	public void DestroyCurrentSeedPlot()
	{
		currSeedPlot.RemovePlant();
	}

	public void SetCurrentItem(ItemDefinition item)
    {
		currItemInUse = item;
    }

	public ItemDefinition GetCurrentItem()
    {
		return currItemInUse;
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