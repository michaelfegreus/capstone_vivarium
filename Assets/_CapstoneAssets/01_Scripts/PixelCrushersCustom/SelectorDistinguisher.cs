using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine.Events;
using Opsive.UltimateInventorySystem.Core.DataStructures;
using UnityEngine;

public class SelectorDistinguisher : MonoBehaviour
{

    public bool itemOnlyUsable = false;
    public Usable currUsable;

    public UnityEvent OnSelectUsable;
    public UnityEvent OnDeselectUsable;
    public UnityEvent OnUseUsable;

    public void OnSelect()
    {
        if (!itemOnlyUsable)
        {
            OnSelectUsable.Invoke();
        }
    }

    public void OnDeselect()
    {
        if (!itemOnlyUsable)
        {
            OnDeselectUsable.Invoke();
        }
    }

    public void OnUse()
    {
        if (!itemOnlyUsable)
        {
            OnUseUsable.Invoke();
        }
    }

    public void SetUsable()
    {
        currUsable = GetComponent<CustomProximitySelector>().CurrentUsable;
        if (currUsable.CompareTag("ItemOnlyUsable"))
        {
            itemOnlyUsable = true;
        }
        else
        {
            itemOnlyUsable = false;
        }
    }
}
