using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BookCurlPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuPage : MonoBehaviour
{
    public BookPro menuBook;
    public Image myTab;
    public Outline tabOutline;

    float tabStartYPos;

    float tabSelectOffset = 85f;

    float tabMoveDuration = .1f;

    private void Start()
    {
        if (myTab != null)
        {
            tabStartYPos = myTab.rectTransform.localPosition.y;
        }
        PageFlipCheck();

        menuBook.OnFlip.AddListener(PageFlipCheck);
    }

    /// <summary>
    /// Check to see if the current front page of the associated ProBook is this page.
    /// </summary>
    public void PageFlipCheck()
    {
        if (menuBook.papers[menuBook.CurrentPaper].Front == gameObject)
        {
            if (myTab != null)
            {
                //myTab.rectTransform.localPosition = new Vector3(myTab.rectTransform.localPosition.x, tabStartYPos - tabSelectOffset, myTab.rectTransform.localPosition.z);
                StartCoroutine(MoveTab(tabStartYPos - tabSelectOffset, tabMoveDuration, true));
                tabOutline.enabled = true;
            }

            OnPageOpen.Invoke();
        }
        else
        {
            if (myTab != null)
            {
                //myTab.rectTransform.localPosition = new Vector3(myTab.rectTransform.localPosition.x, tabStartYPos, myTab.rectTransform.localPosition.z);
                StartCoroutine(MoveTab(tabStartYPos, tabMoveDuration, false));
                tabOutline.enabled = false;
            }
        }
    }

    /// <summary>
    /// On Page Opened invocation list, called when this page is flipped to.
    /// </summary>
    public UnityEvent OnPageOpen;

    public IEnumerator MoveTab(float endPosY, float duration, bool overShoot)
    {
        float startPosition;
        if (overShoot)
        {
            startPosition = myTab.rectTransform.localPosition.y - (tabSelectOffset * 1.35f);
        }
        else
        {
            startPosition = myTab.rectTransform.localPosition.y;
        }

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newPosY = Mathf.Lerp(startPosition, endPosY, elapsedTime / duration);
            myTab.rectTransform.localPosition = new Vector3(myTab.rectTransform.localPosition.x, newPosY, myTab.rectTransform.localPosition.z);

            yield return null;
            //Debug.Log("End of while loop");
        }
        // If an overshoot happen, run the coroutine again to get to final target position.
        if (overShoot)
        {
            StartCoroutine(MoveTab(tabStartYPos, tabStartYPos - tabSelectOffset, false));
        }
        //Debug.Log("End of coroutine");

    }
}
