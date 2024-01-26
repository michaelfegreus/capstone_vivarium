using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class QuestToggleSetupHandler : MonoBehaviour
{
    [SerializeField] Toggle toggle; // our toggle
    QuestTrackerCompanion trackerCompanion; // our quest tracker companion

    // Start is called before the first frame update
    void Start()
    {
        if (toggle == null) toggle = GetComponent<Toggle>();

        if (trackerCompanion == null) trackerCompanion = GameObject.FindObjectOfType<QuestTrackerCompanion>();

        // find and setup this toggle
        toggle.onValueChanged.AddListener(delegate { ToggleQuests(); });

        // then set our value on the tracker companion
        trackerCompanion.desireShowQuests = toggle.isOn;
    }


    void ToggleQuests()
    {
        // set our toggle
        trackerCompanion.desireShowQuests = toggle.isOn;
        
        // then change our value based on the current state of the toggle
        if (toggle.isOn)
            trackerCompanion.ShowQuests();
        else if (!toggle.isOn)
        {
            trackerCompanion.HideQuests();
        }
    }
}
