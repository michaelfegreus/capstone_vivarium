using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteBehaviors : MonoBehaviour
{

    private ParticleSystem particles;

    private void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }

    public void HidePlayerSprite()
    {
        PlayerManager.Instance.playerAnimation.EnableDisableCharacterSprite(false);
    }

    public void ShowPlayerSprite()
    {
        PlayerManager.Instance.playerAnimation.EnableDisableCharacterSprite(true);
    }

    public void MakeDiggingParticles()
    {
        particles.Play();
    }

}
