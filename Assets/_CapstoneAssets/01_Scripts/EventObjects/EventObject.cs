using UnityEngine;

public class EventObject : MonoBehaviour
{
    [Header("Event Object Attributes")]
    [Tooltip("The Event Object type of this prefab. The object will reside in the corresponding object pool.")]
    public WORLD_objectpool_manager.EventObjectType eventObjectTag;
    [Tooltip("Prevents the Event Object from being disabled, regardless of its designated Event End Time.")]
    public bool ignoreEventEndTime;
    [Tooltip("The time when this object's event should end. Inhereting objects should designate how to use this variable.")]
    public GameTime eventEndTime;
    // Annoying, but this is necessary to not throw errors when quitting. See more in the OnDisable function code
    [HideInInspector] public bool quittingApplication;

    public virtual void OnEnable()
    {
        if (!ignoreEventEndTime)
        {
            // Begin listening for the minute tick to check if the Event End Time has come yet.
            GAME_clock_manager.OnMinuteTick += CheckEventEndTime;
        }
    }

    public virtual void OnDisable()
    {
        if (!ignoreEventEndTime)
        {
            GAME_clock_manager.OnMinuteTick -= CheckEventEndTime;
        }
        // This is annoying, but you have to check to see if the application isn't quitting first.
        // Quitting destroys the created object pool Game Objects, and then calls their OnDisable functions. When it tries to enqueue them it throws a null object reference error.
        if (!quittingApplication)
        {
            WORLD_manager.Instance.objectPoolManager.AddToQueue(gameObject, eventObjectTag);

            // This extra step has to be taken because an object can't be disabled and have its parent set on the same frame for whatever reason.
            Invoke("AttachToPoolParent", 0);
        }
    }

    private void AttachToPoolParent()
    {
        WORLD_manager.Instance.objectPoolManager.ReattachEventObject(gameObject);
    }

    private void OnApplicationQuit()
    {
        quittingApplication = true;
    }

    /// <summary>
    /// By default, this De-activates the game object. Override this when you want something else to happen on EventEndTime.
    /// </summary>
    public virtual void CheckEventEndTime()
    {
        if (WORLD_manager.Instance.timeManager.inGameTime.TimeMet(eventEndTime))
        {
            gameObject.SetActive(false);
        }
    }
}
