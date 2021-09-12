using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]
[RequireComponent(typeof(PlayerAnimation))]
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

	void Awake(){
		EnterFreeState ();

		playerMovement = GetComponent<PlayerMovement> ();
		playerInteraction = GetComponent<PlayerInteraction> ();
		playerAnimation = GetComponent<PlayerAnimation> ();

        collisionDelegateScript = playerMovement.playerMovementModule.GetComponent<PlayerCollisionModule>();
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