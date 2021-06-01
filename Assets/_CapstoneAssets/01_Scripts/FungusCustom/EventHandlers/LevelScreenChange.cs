using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when the player changes level screens and enters a new backdrop.
    /// This allows you to run things when the player doesn't see the changes taking place.
    /// </summary>

    [EventHandlerInfo("Vivarium",
                      "Level Screen Change",
                      "The block will execute when the player changes level screens and enters a new backdrop.")]
    [AddComponentMenu("")]
    public class LevelScreenChange : EventHandler
    {
        protected virtual void LevelScreenChangeCheck()
        {
            ExecuteBlock();
        }

        protected virtual void OnEnable()
        {
            GAME_camera_manager.OnLevelScreenChange += LevelScreenChangeCheck;
        }

        protected virtual void OnDisable()
        {
            GAME_camera_manager.OnLevelScreenChange -= LevelScreenChangeCheck;
        }
    }
}