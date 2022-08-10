using UnityEngine;
using PixelCrushers.DialogueSystem;

public class RoomEvent : MonoBehaviour
{
    [Tooltip("Use this flag to disable children of this object as a way to 'reset' the event.")]
    [SerializeField] protected bool deactivateChildrenOnDisable = true;
    protected ClockManager gameClockManager;

    private void OnEnable()
    {
        if (gameClockManager == null)
        {
            gameClockManager = GameManager.Instance.clockManager;
        }
        ClockManager.OnMinuteTick += CheckEventTime;
    }

    private void OnDisable()
    {
        ClockManager.OnMinuteTick -= CheckEventTime;
        if (deactivateChildrenOnDisable)
        {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);
        }
    }

    // Cache a reference to the GameManager's clock

    private void Start()
    {

    }

    /// <summary>
    /// Override this method with inhereted member's specific event time check functionality.
    /// </summary>
    public virtual void CheckEventTime()
    {

    }

    protected void EventDebugLog()
    {
        Debug.Log("Ran Dialogue System Trigger of a Room Event on " + gameObject.name + " at " + gameClockManager.inGameTime.hour + " : " + gameClockManager.inGameTime.minute);
    }
}