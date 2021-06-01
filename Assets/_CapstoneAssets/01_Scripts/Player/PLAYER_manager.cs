using UnityEngine;

[RequireComponent(typeof(PLAYER_movement_directional_2d))]
[RequireComponent(typeof(PLAYER_interaction))]
[RequireComponent(typeof(PLAYER_animation))]
public class PLAYER_manager : Singleton<PLAYER_manager> {
	
	private StateMachine playerStateMachine = new StateMachine();

	[System.NonSerialized]
	public PLAYER_movement_directional_2d playerMovement;
	[System.NonSerialized]
	public PLAYER_interaction playerInteraction;
	[System.NonSerialized]
	public PLAYER_animation playerAnimation;
    [System.NonSerialized]
    public PLAYER_collision_delegate collisionDelegateScript;

	void Awake(){
		EnterFreeState ();

		playerMovement = GetComponent<PLAYER_movement_directional_2d> ();
		playerInteraction = GetComponent<PLAYER_interaction> ();
		playerAnimation = GetComponent<PLAYER_animation> ();

        collisionDelegateScript = playerMovement.playerMovementModule.GetComponent<PLAYER_collision_delegate>();
	}
    
	void Update(){
		this.playerStateMachine.ExecuteStateUpdate ();
	}

	public void EnterFreeState(){
		this.playerStateMachine.ChangeState(new PLAYER_STATE_FreeControl(gameObject));
	}
	public void EnterMenuState(){
		this.playerStateMachine.ChangeState(new PLAYER_STATE_Menu(gameObject));
	}
    public void EnterBespokeActionState()
    {
        this.playerStateMachine.ChangeState(new PLAYER_STATE_BespokeAction(gameObject));
    }
    public void EnterPreviousState(){
		this.playerStateMachine.SwitchToPreviousState ();
	}

    public IState GetPlayerState()
    {
        return playerStateMachine.GetCurrentState();
    }
}