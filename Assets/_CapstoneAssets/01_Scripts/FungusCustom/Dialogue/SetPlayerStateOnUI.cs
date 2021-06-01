using UnityEngine;

public class SetPlayerStateOnUI : MonoBehaviour
{

    /// <summary>
    /// Simple functionality that enables and disables player movement on enabling and disabling this script + affiliated object.
    /// I'm setting it up this way as a patch on a variant Prefab object from default Fungus.
    /// I didn't want to mess with the other scripts too much. If this ends up being too much of a stop-gap solution, feel free to remove this and try something else.
    /// </summary>

    public GameObject otherUI;

    private void OnEnable()
    {
        // While this UI GameObject is active, the Player should most definitely be in the Menu state.
        if (!(PLAYER_manager.Instance.GetPlayerState() is PLAYER_STATE_Menu))
        {
            PLAYER_manager.Instance.EnterMenuState();
        }
    }

    private void OnDisable()
    {
        if (otherUI == null)
        {
            PLAYER_manager.Instance.EnterFreeState();
        }
        else
        {
            // Wait a frame to make sure that another UI didn't just immediately open after this one closed.
            Invoke("CheckOtherUI", 0);
        }
    }

    private void CheckOtherUI()
    {
        if (!otherUI.activeInHierarchy)
        {
            PLAYER_manager.Instance.EnterFreeState();
        }
    }
}
