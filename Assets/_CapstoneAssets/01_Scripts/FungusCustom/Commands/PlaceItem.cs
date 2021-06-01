using UnityEngine;
using Fungus;

[CommandInfo("Vivarium Event Object",
             "Place Item",
             "Place an Item from the object pool and set it up with its position, sprite, and Item-type. This is just a shell that will invoke a method from WORLD_objectpool_manager.")]

public class PlaceItem : Command
{
    [Header("Event Progression")]

    [Tooltip("Check this to prevent auto-continue to the next command unless the Event Item has been picked up by the Player. This allows for the event to continue on to do something else on collecting the item, such as setting a global variable, or spawning another item.")]
    [SerializeField] protected bool pickupItemToContinue;

    protected GameObject spawnedEventItem;

    [Header("Event Item Attributes")]

    [Tooltip("Set a transform reference in the scene as the spawn point for the Item. This will override the Spawn Position Coordinates variable if set.")]
    [SerializeField] protected Transform spawnPositionTransform;
    [Tooltip("Directly set the coordinates to spawn the Item at within the scene. This will be overridden by the Spawn Position Transform variable if that is set.")]
    [SerializeField] protected Vector3 spawnPositionCoordinates;

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

    [Tooltip("The Item-type associated with this event object.")]
    [SerializeField] protected Item item;

    [Tooltip("Set a time for this Item to despawn and play its exit animation.")]
    [SerializeField] protected GameTime eventEndTime;

    [Tooltip("Check this variable to prevent this Item from despawning, regardless of Event End Time.")]
    [SerializeField] protected bool preventDespawn = true;



    public override void OnEnter()
    {
        if (!pickupItemToContinue)
        {
            WORLD_manager.Instance.objectPoolManager.SpawnEventItem(transform, spawnPositionCoordinates, item, eventEndTime, preventDespawn);
            Continue();
        }
        else
        {
            WORLD_manager.Instance.objectPoolManager.SpawnEventItem(transform, spawnPositionCoordinates, item, eventEndTime, preventDespawn, Continue);
        }
    }
}