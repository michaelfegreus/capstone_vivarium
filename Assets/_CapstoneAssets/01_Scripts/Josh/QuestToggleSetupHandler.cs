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

        // find and setup this toggle
        toggle.onValueChanged.AddListener(delegate { trackerCompanion.ToggleQuests(); });
    }


    void ToggleQuests()
    {

    }
}
