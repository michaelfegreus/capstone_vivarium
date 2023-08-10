﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnableRunEvent : MonoBehaviour
{
    public UnityEvent OnEnableEvent;
    public UnityEvent OnDisableEvent;

    private void OnEnable()
    {
        OnEnableEvent.Invoke();
    }

    private void OnDisable()
    {
        OnDisableEvent.Invoke();   
    }
}
