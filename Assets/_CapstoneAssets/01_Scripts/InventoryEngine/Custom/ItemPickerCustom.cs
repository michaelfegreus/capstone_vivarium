using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InventoryEngine;
using UnityEngine.Events;

public class ItemPickerCustom : ItemPicker
{

    public UnityEvent onSuccessfulPick;
    public UnityEvent onUnsuccessfulPick;

    [SerializeField] float delayToDisableObject;

    /// <summary>
    /// Use this to make the item disappear after the animation of the grab happens.
    /// </summary>
    IEnumerator DelayDisable()
    {
        yield return new WaitForSeconds(delayToDisableObject);

        gameObject.SetActive(false);
    }    

    protected override void PickSuccess()
    {
        base.PickSuccess();
        onSuccessfulPick.Invoke();

    }
    protected override void PickFail()
    {
        base.PickFail();
        Debug.Log("Failed to pick up item.");
        onUnsuccessfulPick.Invoke();
    }

    protected override void DisableObjectIfNeeded()
    {
        StartCoroutine(DelayDisable());
    }
}