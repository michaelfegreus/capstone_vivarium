using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using BookCurlPro;


public class AutoFlipControls : MonoBehaviour
{

    public MenuInput menuInput;
    
    AutoFlip autoFlipScript;
    BookPro myBook;

    // For clicking on a tab.
    [SerializeField] float flipTimeFast = .1f;
    // For regular paging through with keys / shoulder buttons.
    [SerializeField] float flipTimeNormal = .28f;

    // Start is called before the first frame update
    void Awake()
    {
        menuInput = new MenuInput();
        menuInput.Enable();

        autoFlipScript = GetComponent<AutoFlip>();
        myBook = GetComponent<BookPro>();

        SetFlipTimeNormal();
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
        SetFlipTimeNormal();
        // Don't bother trying to flip if it's at the end.
        if (myBook.CurrentPaper > 0)
        {
            autoFlipScript.FlipLeftPage();
        }
    }

    void flipRight(InputAction.CallbackContext obj)
    {
        SetFlipTimeNormal();
        // Don't bother trying to flip if it's at the end.
        if (myBook.CurrentPaper < myBook.papers.Length - 1)
        {
            autoFlipScript.FlipRightPage();
        }
    }

    public void SetFlipTimeNormal()
    {
        autoFlipScript.PageFlipTime = flipTimeNormal;
    }

    /// <summary>
    /// Use this to jump to a tab quickly when the player clicks on one rather than paging through.
    /// </summary>
    public void SetFlipTimeFast()
    {
        autoFlipScript.PageFlipTime = flipTimeFast;
    }
}
