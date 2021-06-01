using UnityEngine;
using Fungus;

[CommandInfo("Vivarium Conditionals",
             "Continue on Player Trigger Collision",
             "Only continue this if the Player is colliding with the specified target Trigger Collider2D.")]

public class ContinueOnCollider : Command
{
    [SerializeField] protected Collider2D targetCollider;
    [Tooltip("Check this to Continue from this Command on the Player exiting the target collider, rather than entering.")]
    [SerializeField] protected bool continueOnExit;

    protected virtual void OnEnable()
    {
        //PLAYER_manager.Instance.collisionDelegateScript.OnTriggerEnterEvent += OnPlayerEnterTrigger;
        if (!continueOnExit)
        {
            PLAYER_collision_delegate.OnTriggerEnterEvent += OnPlayerTriggerEvent;
        }
        else
        {
            PLAYER_collision_delegate.OnTriggerExitEvent += OnPlayerTriggerEvent;
        }
    }

    protected virtual void OnDisable()
    {
        // PLAYER_manager.Instance.collisionDelegateScript.OnTriggerEnterEvent -= OnPlayerEnterTrigger;
        if (!continueOnExit)
        {
            PLAYER_collision_delegate.OnTriggerEnterEvent -= OnPlayerTriggerEvent;
        }
        else
        {
            PLAYER_collision_delegate.OnTriggerExitEvent -= OnPlayerTriggerEvent;
        }
    }

    private void OnPlayerTriggerEvent(Collider2D otherCollider)
    {
        if (otherCollider == targetCollider)
        {
            Continue();
        }
    }

}