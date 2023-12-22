using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalFunctions : MonoBehaviour
{
    // Global functions to call regarding the player. This is good for when you want to set up an Event system interact with the player from the Inspector.
    // It'll make sense to keep adding to this over the course of development.

    /// <summary>
    /// Sets the player's state to menu instantly
    /// </summary>
    public void SetPlayerMenuState()
    {        
        // slowly start the menu state so that it doesn't happen at the same time as the free state
        StartCoroutine(SlowMenuState());
    }

    /// <summary>
    /// Sets the player's state to menu quickly if true, slow if false
    /// </summary>
    public void SetPlayerMenuState(bool fast)
    {
        if (fast)
            PlayerManager.Instance.EnterMenuState();
        else
            SetPlayerMenuState();
    }

    /// <summary>
    /// local coroutine for setting the player menu state on the next frame
    /// </summary>
    /// <returns></returns>
    IEnumerator SlowMenuState()
    {
        Debug.Log("Waiting for fixed update...");
        yield return new WaitForFixedUpdate();

        Debug.Log("Setting player menu state");
        PlayerManager.Instance.EnterMenuState();
    }

    /// <summary>
    /// Sets the player to the free state instantly
    /// </summary>
    public void SetPlayerFreeState()
    {
        Debug.Log("Setting player free state");
        PlayerManager.Instance.EnterFreeState();
    }

    public void SetPlayerAnimationState(string stateName)
    {
        PlayerManager.Instance.playerAnimation.PlayAnimationState(stateName);
    }
    public void SetPlayerPosition(Transform destinationPosition)
    {
        PlayerManager.Instance.playerMovement.playerMovementModule.transform.position = destinationPosition.transform.position;
    }
}
