using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerToolUse))]

public class PlayerManager : Singleton<PlayerManager> 
{
	
	private StateMachine playerStateMachine = new StateMachine();

    // is the player in dialogue?
    public bool inDialogue = false;

    // our notebook manager
    NotebookMenuManager notebookManager;

	[System.NonSerialized]
	public PlayerMovement playerMovement;
	[System.NonSerialized]
	public PlayerInteraction playerInteraction;
	[System.NonSerialized]
	public PlayerAnimation playerAnimation;
    [System.NonSerialized]
    public PlayerCollisionModule collisionDelegateScript;
    [System.NonSerialized]
    public PlayerInput playerInput;
    [System.NonSerialized]
    public PlayerToolUse playerToolUse;

    void Start()
    {
        notebookManager = FindObjectOfType<NotebookMenuManager>();
    }

	void Awake(){
		playerMovement = GetComponent<PlayerMovement> ();
		playerInteraction = GetComponent<PlayerInteraction> ();
		playerAnimation = GetComponent<PlayerAnimation> ();
        playerToolUse = GetComponent<PlayerToolUse>();

        collisionDelegateScript = playerMovement.playerMovementModule.GetComponent<PlayerCollisionModule>();

        playerInput = new PlayerInput();
        playerInput.Enable();
        if (!GameManager.Instance.startFromBeginning)
        {
            EnterFreeState();
        }
        else
        {
            EnterMenuState();
        }
    }

    void Update(){
		this.playerStateMachine.ExecuteStateUpdate ();

        // are we in a dialogue state?
        if (inDialogue && notebookManager.menuOpen)
        {
            notebookManager.CloseMenu();
        }
	}

	public void EnterFreeState()
    {
		this.playerStateMachine.ChangeState(new PlayerStateFreeControl(gameObject));
	}
	public void EnterMenuState(){
		this.playerStateMachine.ChangeState(new PlayerStateMenu(gameObject));
	}
    public void EnterBespokeActionState()
    {
        this.playerStateMachine.ChangeState(new PlayerStateBespokeAction(gameObject));
    }
    public void EnterPreviousState(){
		this.playerStateMachine.SwitchToPreviousState ();
	}

    public IState GetPlayerState()
    {
        return playerStateMachine.GetCurrentState();
    }

    public void DialogueState(bool state)
    {
        inDialogue = state;
    }
}