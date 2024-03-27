using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Genius God of a Man Trent ©

public class MenuInputExitLogic : MonoBehaviour
{
    MenuInput exitInput;
    [SerializeField] private Button[] exitButtons;

    void Start()
    {
        exitInput = new MenuInput();
        enabled = false;
    }

    private void OnEnable()
    {
        if (exitInput != null)
        {
            exitInput.Enable();
            exitInput.InventoryEngine.Cancel.performed += ExitMenu;
        }
    }

    private void OnDisable()
    {
        exitInput.Disable();
        exitInput.InventoryEngine.Cancel.performed -= ExitMenu;
    }

    void ExitMenu(InputAction.CallbackContext obj)
    {
        GameObject currentButton;
        currentButton = EventSystem.current.currentSelectedGameObject;

        if (currentButton.name.Equals("Exit Button"))
        {
            currentButton.GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            foreach (Button b in exitButtons)
            {
                if (b.isActiveAndEnabled)
                {
                    EventSystem.current.SetSelectedGameObject(b.gameObject);
                }
            }
        }

    }
}
