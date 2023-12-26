using Opsive.UltimateInventorySystem.UI.CompoundElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EscapeHelper : MonoBehaviour
{
    public InputActionAsset actions;
    Button ourButton; // our button

    void Awake()
    {
        actions["UI/Escape"].performed += CloseWindow;
    }

    void Start()
    {
        actions.Enable();

        ourButton = GetComponent<Button>();
    }

    // callback the close window
    void CloseWindow(InputAction.CallbackContext obj)
    {
        // invoke our button
        ourButton.onClick.Invoke();
    }
}
