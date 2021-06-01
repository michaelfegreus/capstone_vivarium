using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// The block will execute when a Global Variable changes.
    /// This allows you to check to run Commands that might be affiliated with specific Global Variables.
    /// </summary>

    [EventHandlerInfo("Vivarium",
                      "Global Variable Change",
                      "The block will execute when a Fungus Global Variable changes.")]
    [AddComponentMenu("")]
    public class GlobalVariableChange : EventHandler
    {
        protected virtual void GlobalVariableChangeCheck()
        {
            ExecuteBlock();
        }

        protected virtual void OnEnable()
        {
            Flowchart.OnGlobalVariableChange += GlobalVariableChangeCheck;
        }

        protected virtual void OnDisable()
        {
            Flowchart.OnGlobalVariableChange -= GlobalVariableChangeCheck;
        }
    }
}