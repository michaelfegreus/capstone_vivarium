using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteBehaviors : MonoBehaviour
{

    public void HidePlayerSprite()
    {
        PlayerManager.Instance.playerAnimation.EnableDisableCharacterSprite(false);
    }

    public void ShowPlayerSprite()
    {
        PlayerManager.Instance.playerAnimation.EnableDisableCharacterSprite(true);
    }

}
