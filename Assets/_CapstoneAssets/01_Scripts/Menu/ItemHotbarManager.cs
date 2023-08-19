using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateInventorySystem.UI.Panels;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Copyright Smart Boy Trent© 2023 


public class ItemHotbarManager : MonoBehaviour
{

    [SerializeField] private DisplayPanel ItemHotbarPanel;
    [SerializeField] private Animator anim;

    public bool hotbarOpen;

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private RectTransform currRect;
    [SerializeField] private Vector2 vectorOffset;

    private GameObject currObject;
    private bool hotbarItemsSelected;

    public void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        Vector2 xVector = (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position) + vectorOffset;
        contentPanel.anchoredPosition = new Vector2(xVector.x, contentPanel.anchoredPosition.y);
                
    }

    private void Update()
    {
        if (hotbarItemsSelected)
        {
            currObject = EventSystem.current.currentSelectedGameObject;
            currObject.TryGetComponent<RectTransform>(out currRect);
            if (currRect != null)
            {
                SnapTo(currRect);
            }
        }


    }

    public void OpenHotbarPanel()
    {
        ItemHotbarPanel.gameObject.SetActive(true);
        ItemHotbarPanel.SmartOpen();
        anim.SetTrigger("Appear");
        StartCoroutine(WaitToSetOpen());
    }


    public void CloseHotbarPanel()
    {
        anim.SetTrigger("Disappear");
        hotbarOpen = false;
    }

    IEnumerator WaitToSetOpen()
    {
        yield return new WaitForEndOfFrame();

        hotbarOpen = true;
    }


    public void HotBarItemSelected()
    {
        hotbarItemsSelected = true;
    }

    public void HotBarItemDeselected()
    {
        hotbarItemsSelected = false;
    }

}
