using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when a new day starts, as called by the WORLD_manager.
    /// </summary>

    [EventHandlerInfo("Vivarium",
                      "Day Start",
                      "The block will execute at the start of a new in-game day.")]
    [AddComponentMenu("")]
    public class DayStart : EventHandler
    {
        public void DayStartBlockExecute()
        {
            ExecuteBlock();
        }

        // Get notified from WORLD_manager when a day starts.

        protected virtual void OnEnable()
        {
            GAME_clock_manager.OnDayStart += DayStartBlockExecute;
        }

        protected virtual void OnDisable()
        {
            GAME_clock_manager.OnDayStart -= DayStartBlockExecute;
        }

    }
}