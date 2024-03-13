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
                    Debug.Log("Currently Selected: " + currentlySelected);
                    previouslySelected = currentlySelected;
                }

            }
        }
       

    }

    //This shit is brain-damaged but i don't care :) 
    //love,
    //trent

    public void SetSelectedObjectOnDelay(GameObject itemToSelect)
    {
        StartCoroutine(WaitALittleBit(itemToSelect));
    }

    IEnumerator WaitALittleBit(GameObject go)
    {
        yield return new WaitForSeconds(0.5f);
        system.SetSelectedGameObject(go);
    }


    public void SetSelectedObjectOnDelayShorter(GameObject itemToSelect)
    {
        StartCoroutine(WaitALittleBitShorter(itemToSelect));
    }

    IEnumerator WaitALittleBitShorter(GameObject go)
    {
        yield return new WaitForSeconds(0.1f);
        system.SetSelectedGameObject(go);
    }
}
