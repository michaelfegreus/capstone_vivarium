using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalFunctions : MonoBehaviour
{
    // Global functions to call regarding the player. This is good for when you want to set up an Event system interact with the player from the Inspector.
    // It'll make sense to keep adding to this over the course of development.

    public void SetPlayerMenuState()
    {
        PlayerManager.Instance.EnterMenuState();
    }
    public void SetPlayerFreeState()
    {
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
