using UnityEngine;
using System.Collections;

public class LEVEL_prop_fade_trigger : MonoBehaviour
{
    [Tooltip("Enable this to fade up and down sprites when the player enters this trigger volume. Otherwise, you can just call the public methods on this to fade up and down sprites.")]
    public bool affectOnPlayerTrigger = true;
    public float fadeDuration = .5f;
    [Tooltip("Fade up and down the alpha of sprites via coroutine.")]
    public SpriteRenderer[] spritesToFade;
    [Tooltip("Simply disable or enable the Sprite Rendered components instead of fading them via coroutine.")]
    public SpriteRenderer[] spritesToEnableDisable;

    bool firstFrame; //Used to check if it's the first frame, and if collider fucntions should immediately take effect.

    void Start()
    {
        firstFrame = true;

        // Start with setting off all disable / fading sprites
        // Disabling
        for (int i = 0; i < spritesToEnableDisable.Length; i++)
        {
             spritesToEnableDisable[i].enabled = false;
        }
        // Fading
        for (int i = 0; i < spritesToFade.Length; i++)
        {
            spritesToFade[i].color = new Color(spritesToFade[i].color.r, spritesToFade[i].color.g, spritesToFade[i].color.b, 0f);
        }
        // Then, wait for the first frame of collision checks to enable and fade up what should be on screen.

        StartCoroutine(EndOfFirstFrame());
       
    }

    IEnumerator EndOfFirstFrame()
    {
        yield return new WaitForEndOfFrame();
        //Debug.Log("First frame ended.");
        firstFrame = false;
    }


    public void FadeUp()
    {
        if (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < spritesToEnableDisable.Length; i++)
            {
                spritesToEnableDisable[i].enabled = true;
            }

            if (firstFrame)
            {
                // On first frame, immediately fade up all the sprites in associated with this script if the player is standing here.
                for (int i = 0; i < spritesToFade.Length; i++)
                {
                    spritesToFade[i].color = new Color(spritesToFade[i].color.r, spritesToFade[i].color.g, spritesToFade[i].color.b, 1f);
                }
            }
            else
            {
                if (spritesToFade.Length != 0)
                {
                    StartCoroutine(SpriteFade(spritesToFade, 1f, fadeDuration));
                }
            }
        }
    }
    public void FadeDown()
    {
        if (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < spritesToEnableDisable.Length; i++)
            {
                spritesToEnableDisable[i].enabled = false;
            }



            if (spritesToFade.Length != 0)
            {
                StartCoroutine(SpriteFade(spritesToFade, 0f, fadeDuration));
            }
        } 
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (affectOnPlayerTrigger)
        {
            if (col.tag.Trim().Equals("Player".Trim()))
            {
                FadeUp();
                Debug.Log("Trigger fade up on " + this.gameObject.name);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (affectOnPlayerTrigger) {
            if (col.tag.Trim().Equals("Player".Trim()))
            {
                FadeDown();
                //Debug.Log("Trigger fade down on " + this.gameObject.name);
            }
        }
    }

    public IEnumerator SpriteFade(SpriteRenderer[] sprites, float endValue, float duration)
    {
        float startValue = sprites[0].color.a;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, newAlpha);
            }
            yield return null;
        }
    }
}
