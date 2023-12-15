using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using BookCurlPro;

public class NotebookMenuManager : MonoBehaviour
{
    //[SerializeField] Canvas notebookCanvas;
    [SerializeField] RectTransform notebookParent;

    [SerializeField] BookPro menuBook;
    [SerializeField] AutoFlip autoFlip;
    [SerializeField] AutoFlipControls autoFlipControls;

    [SerializeField] float notebookCloseXPos;
    [SerializeField] float notebookOpenXPos;
    [SerializeField] float notebookMoveDuration = .5f;

    [SerializeField] bool turnPagesOnOpenClose; // Not sure if I want to use this feature so I set it up as an option for now.
    [SerializeField] float closeDelay; 

    [HideInInspector]
    public bool menuOpen;

    [SerializeField] int lastPage = 1; // The last page the player was on before closing the menu

    private void Start()
    {
        // Start book at closed
        menuOpen = false;
        notebookParent.localPosition = new Vector3(notebookCloseXPos, notebookParent.localPosition.y, notebookParent.localPosition.z);
        menuBook.currentPaper = 0;
    }

    public void OpenMenu()
    {
        if (turnPagesOnOpenClose)
        {
            if(lastPage >= 2) // Use this to get closer to the desired page so you don't wait too long flipping
            {
                menuBook.currentPaper = lastPage - 2;
               // menuBook.UpdateBook();
            }

            autoFlip.enabled = true;
            autoFlipControls.SetFlipTimeFast();
            autoFlip.StartFlipping(lastPage);
        }
        else
        {
            if (menuBook.currentPaper == 0)
            {
                autoFlip.enabled = true;
                autoFlipControls.SetFlipTimeFast();
                autoFlip.StartFlipping(1);
            }
        }

        StartCoroutine(MoveNotebook(notebookOpenXPos, notebookMoveDuration, true, true));
        OnMenuOpen.Invoke();
        StartCoroutine(WaitToSetOpen()); // This coroutine is used to prevent the player from accidentally opening and closing the menu on the same frame.
    }
    public void CloseMenu()
    {
        lastPage = menuBook.currentPaper;

        if (turnPagesOnOpenClose)
        {
            if (menuBook.currentPaper > 1) // This again is used to just speed up the opening and closing book flipping animations.
            {
                menuBook.currentPaper = 1;
            }
            autoFlip.enabled = true;
            autoFlipControls.SetFlipTimeFast();
            autoFlip.StartFlipping(0);
        }

        StartCoroutine(MoveNotebook(notebookCloseXPos, notebookMoveDuration, false, false));
        menuOpen = false;
    }


    /// <summary>
    /// TODO: This is extremely inefficent and stupid but I don't care, somebody else can fix it later
    /// -Love,
    ///          Trent <3
    /// </summary>
    /// <param name="parent"></param>
    public void DisableAllButtons(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.TryGetComponent(out Button button))
            {
                button.enabled = false;
            }
            DisableAllButtons(child);
        }
    }
    public void EnableAllButtons(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.TryGetComponent(out Button button))
            {
                button.enabled = true;
            }
            EnableAllButtons(child);
        }
    }



    /// <summary>
    /// On Menu Opened invocation list, called when the menu is opened.
    /// </summary>
    public UnityEvent OnMenuOpen;

    /// <summary>
    /// On Menu Closed invocation list, called when the menu is closed.
    /// </summary>
    public UnityEvent OnMenuClose;

    IEnumerator MoveNotebook(float endPosX, float duration, bool overShoot, bool opening)
    {
        // This so you can see the notebook closing a little before it goes off screen.
        if (!opening && turnPagesOnOpenClose)
        {
            yield return new WaitForSeconds(closeDelay);
        }

        float _endPosX = endPosX;
        if (overShoot)
        {
            _endPosX = endPosX * 1.35f;
        }

        float startPosition = notebookParent.localPosition.x;
        

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newPosX = Mathf.Lerp(startPosition, _endPosX, elapsedTime / duration);
            notebookParent.localPosition = new Vector3(newPosX, notebookParent.localPosition.y, notebookParent.localPosition.z);

            yield return null;
            //Debug.Log("End of while loop");
        }
        // If an overshoot happened, run the coroutine again to get to final target position.
        if (overShoot)
        {
            StartCoroutine(MoveNotebook(endPosX, duration, false, opening));
        }
        //Debug.Log("End of coroutine");
        if(!overShoot && !opening)
        {
            OnMenuClose.Invoke();
        }

    }
    /// <summary>
    /// Used to prevent the player from accidentally opening and closing the menu on the same frame.
    /// </summary>
    IEnumerator WaitToSetOpen()
    {
        yield return new WaitForEndOfFrame();

        menuOpen = true;
    }
}
