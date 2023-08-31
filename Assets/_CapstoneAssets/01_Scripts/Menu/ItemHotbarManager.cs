using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateInventorySystem.UI.Panels;
using Opsive.UltimateInventorySystem.UI.Item;
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

    private void Update()
    {
        if (hotbarItemsSelected)
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

    public void OpenHotbarPanel()
    {
        ItemHotbarPanel.gameObject.SetActive(true);
        ItemHotbarPanel.SmartOpen();
        EventSystem.current.SetSelectedGameObject(ItemHotbarPanel.GetOpenSelectable().gameObject);
        anim.SetTrigger("Appear");
        StartCoroutine(WaitToSetOpen());
    }


    public void CloseHotbarPanel()
    {
        anim.SetTrigger("Disappear");
        hotbarOpen = false;
        StartCoroutine(WaitToDisable());

    }

    IEnumerator WaitToSetOpen()
    {
        yield return new WaitForEndOfFrame();

        hotbarOpen = true;
    }

    IEnumerator WaitToDisable()
    {
        yield return new WaitForEndOfFrame();
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animationLength);
        ItemHotbarPanel.gameObject.SetActive(false);
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
