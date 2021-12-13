using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerToolUse))]

public class PlayerManager : Singleton<PlayerManager> {
	
	private StateMachine playerStateMachine = new StateMachine();

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

	void Awake(){
		playerMovement = GetComponent<PlayerMovement> ();
		playerInteraction = GetComponent<PlayerInteraction> ();
		playerAnimation = GetComponent<PlayerAnimation> ();
        playerToolUse = GetComponent<PlayerToolUse>();

        collisionDelegateScript = playerMovement.playerMovementModule.GetComponent<PlayerCollisionModule>();

        playerInput = new PlayerInput();
        playerInput.Enable();

        EnterFreeState();
    }

    void Update(){
		this.playerStateMachine.ExecuteStateUpdate ();
	}

	public void EnterFreeState(){
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
}