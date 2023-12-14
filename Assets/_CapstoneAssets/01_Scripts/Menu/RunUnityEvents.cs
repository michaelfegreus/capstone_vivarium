using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunUnityEvents : MonoBehaviour
{
    public UnityEvent Event1;
    public UnityEvent Event2;

    private void RunEvent1()
    {
        Event1.Invoke();
    }

    private void RunEvent2()
    {
        Event2.Invoke();   
    }
}
