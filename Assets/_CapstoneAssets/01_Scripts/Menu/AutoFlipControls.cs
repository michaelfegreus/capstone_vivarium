using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BookCurlPro;


public class AutoFlipControls : MonoBehaviour
{

    public MenuInput menuInput;

    public AutoFlip autoFlipScript;

    // Start is called before the first frame update
    void Awake()
    {
        menuInput = new MenuInput();
        menuInput.Enable();

    }

    private void OnEnable()
    {
        menuInput.Enable();

        menuInput.NotebookNavigation.FlipLeft.performed += flipLeft;
        menuInput.NotebookNavigation.FlipRight.performed += flipRight;

    }
    private void OnDisable()
    {
        menuInput.Disable();

        menuInput.NotebookNavigation.FlipLeft.performed -= flipLeft;
        menuInput.NotebookNavigation.FlipRight.performed -= flipRight;
    }

    void flipLeft(InputAction.CallbackContext obj)
    {
        autoFlipScript.FlipLeftPage();
    }

    void flipRight(InputAction.CallbackContext obj)
    {
        autoFlipScript.FlipRightPage();
    }

}
