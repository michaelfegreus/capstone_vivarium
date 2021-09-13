using UnityEngine;

namespace PixelCrushers.DialogueSystem
{

    /// <summary>
    /// This class helps the Dialogue System's demo work with the New Input System. It
    /// registers the inputs defined in DemoInputControls for use with the Dialogue 
    /// System's Input Device Manager.
    /// </summary>
    public class DialogueSystemInputRegistration : MonoBehaviour
    {

#if USE_NEW_INPUT

        private DialogueSystemInput controls;

        // Track which instance of this script registered the inputs, to prevent
        // another instance from accidentally unregistering them.
        protected static bool isRegistered = false;
        private bool didIRegister = false;

        void Awake()
        {
            controls = new DialogueSystemInput();
        }

        void OnEnable()
        {
            if (!isRegistered)
            {
                isRegistered = true;
                didIRegister = true;
                controls.Enable();
                InputDeviceManager.RegisterInputAction("Horizontal", controls.ActionMap.Horizontal);
                InputDeviceManager.RegisterInputAction("Vertical", controls.ActionMap.Vertical);
                InputDeviceManager.RegisterInputAction("Fire1", controls.ActionMap.Fire1);
                InputDeviceManager.RegisterInputAction("Interact", controls.ActionMap.Fire1);
            }
        }

        void OnDisable()
        {
            if (didIRegister)
            {
                isRegistered = false;
                didIRegister = false;
                controls.Disable();
                InputDeviceManager.UnregisterInputAction("Horizontal");
                InputDeviceManager.UnregisterInputAction("Vertical");
                InputDeviceManager.UnregisterInputAction("Fire1");
                InputDeviceManager.RegisterInputAction("Interact", controls.ActionMap.Fire1);
            }
        }

#endif

    }
}
