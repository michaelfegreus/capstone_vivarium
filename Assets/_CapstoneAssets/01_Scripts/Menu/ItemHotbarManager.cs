using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateInventorySystem.UI.Panels;
using Opsive.UltimateInventorySystem.UI.Item;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Copyright Smart Boy Trent© 2023 

// rights reserved by smart boy josh, circa 2024


public class ItemHotbarManager : MonoBehaviour
{

    [SerializeField] private DisplayPanel ItemHotbarPanel;
    [SerializeField] private Animator anim;

    public bool hotbarOpen;

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private RectTransform currRect;
    [SerializeField] private ItemViewSlot itemSlot;
    [SerializeField] private Vector2 vectorOffset;

    private GameObject currObject;
    private bool hotbarItemsSelected;

    public void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        Vector2 xVector = (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position) + vectorOffset;
        contentPanel.anchoredPosition = new Vector2(xVector.x, contentPanel.anchoredPosition.y);
                
    }

    // runs every frame
    private void Update()
    {
        if (hotbarItemsSelected && hotbarOpen)
        {
            currObject = EventSystem.current.currentSelectedGameObject;
            currObject.TryGetComponent<ItemViewSlot>(out itemSlot);
            if (itemSlot != null)
            {
                currRect = currObject.GetComponent<RectTransform>();
                SnapTo(currRect);
                ItemHotbarPanel.SetOpenSelectable(currRect.GetComponent<Selectable>());
            }
        }
    }

    // opens the hotbar panel
    public void OpenHotbarPanel()
    {
        // if the hotbar is already open, don't open it, trent.
        if (hotbarOpen) return;

        ItemHotbarPanel.gameObject.SetActive(true);
        ItemHotbarPanel.SmartOpen();
        EventSystem.current.SetSelectedGameObject(ItemHotbarPanel.GetOpenSelectable().gameObject);
        anim.SetTrigger("Appear");
        // set to open
        if (!opening)
            StartCoroutine(WaitToSet());
    }

    // closes the hotbar panel
    public void CloseHotbarPanel()
    {
        // if the hotbar is open, then close it, trent.
        if (hotbarOpen)

        anim.SetTrigger("Disappear");
        hotbarOpen = false;

        // now do the waiting
        if (!closing)
            StartCoroutine(WaitToDisable());
    }

    // wait to set the next frame
    bool opening;
    IEnumerator WaitToSet()
    {
        opening = true;
        yield return new WaitForFixedUpdate();
        hotbarOpen = true;
        opening = false;
    }

    // wiats to disable the hotbar
    bool closing;
    IEnumerator WaitToDisable()
    {
        closing = true;
        yield return new WaitForEndOfFrame();
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animationLength);
        ItemHotbarPanel.gameObject.SetActive(false);
        closing = false;
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
