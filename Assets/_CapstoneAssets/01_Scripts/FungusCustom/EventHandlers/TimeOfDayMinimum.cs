using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when it's affiliated Time of Day triggers it.
    /// </summary>

    [EventHandlerInfo("Vivarium",
                      "Time of Day (Minimum)",
                      "The block will execute if the clock has reached or passed the specified in-game time of day.")]
    [AddComponentMenu("")]
    public class TimeOfDayMinimum : EventHandler
    {
        [Tooltip("In-game time to check for")]
        [SerializeField] protected GameTime eventTime;

        protected GAME_clock_manager globalTimeManager;

        protected Block myParentBlock;

        // If this has been executed today, then flag that, so it doesn't happen continually.
        protected bool executedToday;

        public virtual void Start()
        {
            globalTimeManager = WORLD_manager.Instance.timeManager;
            myParentBlock = ParentBlock;
        }

        // The code below ensures that time variables will be properly clamped when inputing them and setting up a schedule.
        [ExecuteInEditMode]
        protected virtual void OnValidate()
        {
            if (eventTime != null)
            {
                eventTime.OnValidate();
            }
            // TODO: Set up a function here that checks if eventStartTime and eventEndTime roll over the clock past midnight. For example an event that spans 22:00 - 01:00.
            // When that happens, set up a special case that checks for a new day beginning and then runs its minimum Time Met conditions.
        }

        protected virtual void OnEnable()
        {
            GAME_clock_manager.OnMinuteTick += CheckTimeOfDayExecution;
            GAME_clock_manager.OnDayStart += ResetExecutedToday;
        }

        protected virtual void OnDisable()
        {
            GAME_clock_manager.OnMinuteTick -= CheckTimeOfDayExecution;
            GAME_clock_manager.OnDayStart -= ResetExecutedToday;
        }

        /// <summary>
        /// Resets the daily execution cap for the Block.
        /// </summary>
        protected virtual void ResetExecutedToday()
        {
            executedToday = false;
        }

        protected virtual void CheckTimeOfDayExecution()
        {
            if (!executedToday)
            {
                if (!myParentBlock.IsExecuting())
                {
                    if (globalTimeManager.inGameTime.TimeMet(eventTime))
                    {
                        ExecuteBlock();
                        executedToday = true;
                    }
                }
                else if (myParentBlock.IsExecuting())
                {
                    if (globalTimeManager.inGameTime.TimeMet(eventTime) == false)
                    {
                        // This might only be useful for debug and prototyping since I doubt the player will be going back in time in a build.
                        myParentBlock.Stop();
                    }
                }
            }
        }
    }
}