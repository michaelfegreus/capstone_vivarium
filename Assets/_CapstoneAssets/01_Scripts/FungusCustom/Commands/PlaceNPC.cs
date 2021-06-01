using UnityEngine;
using Fungus;

[CommandInfo("Vivarium Event Object",
             "Place NPC",
             "Place an NPC from the object pool and set it up with its position, animation, and dialogue. This is just a shell that will invoke a method from WORLD_objectpool_manager.")]

public class PlaceNPC : Command
{

    // This ensures that the spawn coordinates are overidden by the spawn point values.
    [ExecuteInEditMode]
    protected virtual void OnValidate()
    {
        if (!Application.isPlaying)
        {
            if (spawnPositionTransform != null)
            {
                spawnPositionCoordinates = spawnPositionTransform.position;
            }
            if (eventEndTime != null)
            {
                eventEndTime.OnValidate();
            }
            if (ParentBlock != null)
            {
                if (ParentBlock._EventHandler is TimeOfDayBounded)
                {
                    TimeOfDayBounded eventHandler = (TimeOfDayBounded)ParentBlock._EventHandler;
                    eventEndTime = eventHandler.GetEventEndTime();
                }
            }
        }
    }

    [Header("Event NPC Attributes")]

    [Tooltip("Set a transform reference in the scene as the spawn point for the NPC. This will override the Spawn Position Coordinates variable if set.")]
    [SerializeField] protected Transform spawnPositionTransform;
    [Tooltip("Directly set the coordinates to spawn the NPC at within the scene. This will be overridden by the Spawn Position Transform variable if that is set.")]
    [SerializeField] protected Vector3 spawnPositionCoordinates;

    [Tooltip("Set the name for the spawned NPC's game object. (Defaults to the Parent Block's name.)")]
    [SerializeField] protected string NPCName;

    /*[Tooltip("Set a Yarn node to direct the dialogue to during this event.")]
    [SerializeField] protected string yarnNode;*/

    [Tooltip("Set the animation override controller to play this NPC's animation set. Note that leaving a field blank on the Override Controller will result in playing a blank animation.")]
    [SerializeField] protected AnimatorOverrideController animationOverrideController;
   


    [Tooltip("Check this variable to prevent this NPC from despawning, regardless of Event End Time.")]
    [SerializeField] protected bool preventDespawn;

    [Tooltip("Set a time for this NPC to despawn and play its exit animation.")]
    [SerializeField] protected GameTime eventEndTime;

    [Header("Interaction Options")]

    [Tooltip("Check this variable to prevent the player from interacting with the NPC during this event.")]
    [SerializeField] protected bool nonInteractableToggle;

    [Tooltip("Block to Execute when interacting with this NPC. Generally, this will be used to run dialogue.")]
    [SerializeField] protected BlockReference interactionBlock;

   /*[Tooltip("Check this variable to cause the NPC to play its exit animation on interaction. (The NPC will wait for dialogue to finish first.")]
    [SerializeField] protected bool exitOnInteraction;

    [Header("Event Progression")]

    [Tooltip("Check this to prevent auto-continue to the next command unless the NPC has exited. You could use this in conjunction with exitOnInteract. This allows for the event to continue on to do something else on interacting with the NPC, such as setting a global variable, or spawning another NPC.")]
    [SerializeField] protected bool continueOnExit;*/

    public override void OnEnter()
    {
        // Override spawn coordinates with spawnPositionTransform, if there is a transform to use.
        if (spawnPositionTransform != null)
        {
            spawnPositionCoordinates = spawnPositionTransform.position;
        }

        if (NPCName == "")
        {
            NPCName = ParentBlock.BlockName;
        }

        WORLD_manager.Instance.objectPoolManager.SpawnEventNPC(transform, spawnPositionCoordinates, nonInteractableToggle, animationOverrideController, eventEndTime, NPCName, interactionBlock, preventDespawn);
        Continue();

        /*if (!continueOnExit)
        {
            WORLD_manager.Instance.objectPoolManager.SpawnEventNPC(transform, spawnPositionCoordinates, yarnNode, nonInteractableToggle, animationOverrideController, eventEndTime, NPCName, preventDespawn, exitOnInteraction);
            Continue();
        }
        else
        {
            WORLD_manager.Instance.objectPoolManager.SpawnEventNPC(transform, spawnPositionCoordinates, yarnNode, nonInteractableToggle, animationOverrideController, eventEndTime, NPCName, preventDespawn, exitOnInteraction, Continue);
        }*/
    }

}