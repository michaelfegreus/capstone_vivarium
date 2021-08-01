﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RefocusEventSystemSelection : MonoBehaviour
{
    private EventSystem thisSystem;
    [SerializeField] private bool SetSelectionToNone = false;
    void Start()
    {
        if (this.GetComponent<EventSystem>() != null)
        {
            thisSystem = this.GetComponent<EventSystem>();
        }
    }

    void Update()
    {
        if (thisSystem != null && thisSystem.currentSelectedGameObject == null && (Input.GetAxis("Horizontal") > Mathf.Abs(0.3f) || Input.GetAxis("Vertical") != 0f || Input.GetButton("Submit")))
        {
            thisSystem.SetSelectedGameObject(thisSystem.firstSelectedGameObject);
        }
        if (SetSelectionToNone)
        {
            SetSelectionToNone = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }


}