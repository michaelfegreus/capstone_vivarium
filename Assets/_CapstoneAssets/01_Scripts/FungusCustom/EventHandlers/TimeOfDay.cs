using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when it's affiliated Time of Day triggers it.
    /// </summary>

    [EventHandlerInfo("Vivarium",
                      "Time of Day (Instant)",
                      "The block will execute only at the specified in-game time of day.")]
    [AddComponentMenu("")]
    public class TimeOfDay : EventHandler
    {
        [Tooltip("In-game time to check for")]
        [SerializeField] protected GameTime eventTime;

        protected GAME_clock_manager globalTimeManager;

        public virtual void Start()
        {
            globalTimeManager = WORLD_manager.Instance.timeManager;
        }

        // The code below ensures that time variables will be properly clamped when inputing them and setting up a schedule.
        [ExecuteInEditMode]
        protected virtual void OnValidate()
        {
            if (eventTime != null)
            {
                eventTime.OnValidate();
            }
        }

        protected virtual void OnEnable()
        {
            GAME_clock_manager.OnMinuteTick += CheckTimeOfDayExecution;
        }

        protected virtual void OnDisable()
        {
            GAME_clock_manager.OnMinuteTick -= CheckTimeOfDayExecution;
        }

        protected virtual void CheckTimeOfDayExecution()
        {
            if (eventTime.TimeEquals(globalTimeManager.inGameTime)){
                ExecuteBlock();
            }
        }
    }
}