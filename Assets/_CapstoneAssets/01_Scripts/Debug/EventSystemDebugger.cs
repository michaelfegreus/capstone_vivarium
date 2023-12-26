using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EventSystemDebugger : MonoBehaviour
{

    public EventSystem system;

    public string previouslySelected = "";
    public string currentlySelected = "";

    public bool debug = false;
    // Start is called before the first frame update
    void Start()
    {
        system = GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (debug)
        {
            if (system.currentSelectedGameObject != null)
            {
                currentlySelected = system.currentSelectedGameObject.name;
                if (!string.Equals(previouslySelected, currentlySelected))
                {
                    // Debug.Log("Currently Selected: " + currentlySelected);
                    previouslySelected = currentlySelected;
                }

            }
        }
       

    }
}
