using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when it's affiliated Time of Day triggers it.
    /// </summary>

    [EventHandlerInfo("Vivarium",
                      "Time of Day (Bounded)",
                      "The block will execute between the bounds of two specified in-game times of day.")]
    [AddComponentMenu("")]
    public class TimeOfDayBounded : EventHandler
    {
        [Tooltip("Lower bounds of event in-game time.")]
        [SerializeField] protected GameTime eventStartTime;
        [Tooltip("Upper bounds of event in-game time.")]
        [SerializeField] protected GameTime eventEndTime;

        protected GAME_clock_manager globalTimeManager;

        protected Block myParentBlock;
        
        // If this block was already executed in the bounded times, don't execute it again.
        protected bool thisBlockExecuted;

        public virtual void Start()
        {
            globalTimeManager = WORLD_manager.Instance.timeManager;
            myParentBlock = ParentBlock;
        }

        // The code below ensures that time variables will be properly clamped when inputing them and setting up a schedule.
        [ExecuteInEditMode]
        protected virtual void OnValidate()
        {
            if (eventStartTime != null)
            {
                eventStartTime.OnValidate();
            }
            if (eventEndTime != null)
            {
                eventEndTime.OnValidate();
            }
            // TODO: Set up a function here that checks if eventStartTime and eventEndTime roll over the clock past midnight. For example an event that spans 22:00 - 01:00.
            // When that happens, set up a special case that checks for a new day beginning and then runs its minimum Time Met conditions.
           
        }

        protected virtual void OnEnable()
        {
            GAME_clock_manager.OnMinuteTick += CheckTimeOfDayExecution;
            // When enabling, reset the flag checking to see if the block has already been executed.
            thisBlockExecuted = false;
        }

        protected virtual void OnDisable()
        {
            GAME_clock_manager.OnMinuteTick -= CheckTimeOfDayExecution;
        }

        /// <summary>
        /// Gets the event end time. For use by the PlaceNPC function to sync end times of the Event Handler and the event NPC objects.
        /// </summary>
        /// <returns>The event end time.</returns>
        public virtual GameTime GetEventEndTime()
        {
            return eventEndTime;
        }

        protected virtual void CheckTimeOfDayExecution()
        {
            if (!myParentBlock.IsExecuting())
            {
                //if (eventStartTime.TimeMet(globalTimeManager.inGameTime) && !eventEndTime.TimeMet(globalTimeManager.inGameTime))
                if (globalTimeManager.inGameTime.TimeMet(eventStartTime) && !globalTimeManager.inGameTime.TimeMet(eventEndTime) && !thisBlockExecuted)
                {
                    ExecuteBlock();
                    thisBlockExecuted = true;
                    //Debug.Log("Begin bound time event.");
                }
            }
            else
            {
                //if (eventEndTime.TimeMet(globalTimeManager.inGameTime))
                if (globalTimeManager.inGameTime.TimeMet(eventEndTime)) 
                {
                    //Debug.Log("End bound time event.");
                    ParentBlock.Stop();
                }
            }
        }
    }
}