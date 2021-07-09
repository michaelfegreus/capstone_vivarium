using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;

public class LEVEL_prop_fade_trigger : MonoBehaviour
{
    [Tooltip("Setting this will cause the script to automatically add the children of this object to the fade array.")]
    public Transform fadeSpritesParent;

    [Tooltip("Setting this will cause the script to automatically add the children of this object to the enable/disable array.")]
    public Transform enableDisableSpriteParent;

    [Tooltip("Enable this to fade up and down sprites when the player enters this trigger volume. Otherwise, you can just call the public methods on this to fade up and down sprites.")]
    public bool affectOnPlayerTrigger = true;
    public float fadeDuration = .25f;
    [Tooltip("Fade up and down the alpha of sprites via coroutine.")]
    public SpriteRenderer[] spritesToFade;
    [Tooltip("Simply disable or enable the Sprite Rendered components instead of fading them via coroutine. The script will automatically take the items you put in this array out of the fader array above.")]
    public SpriteRenderer[] spritesToEnableDisable;

    bool firstFrame; //Used to check if it's the first frame, and if collider fucntions should immediately take effect.

    void Start()
    {
        // *** If you wanted to, you could streamline this by only adding children from the fade and disable enable parent objects, and not give an option to manually add stuff in the inspector.

        // Get sprites from the fade sprite parent "folder" and add them to the existing spritesToFade array
        if (fadeSpritesParent != null)
        {
            SpriteRenderer[] spriteParentArray = fadeSpritesParent.GetComponentsInChildren<SpriteRenderer>();
            spritesToFade = spritesToFade.Concat(spriteParentArray).ToArray();
        }

        // Get sprites from the enable/disable sprite parent "folder" and add them to the existing spritesToFade array
        if (enableDisableSpriteParent != null)
        {
            SpriteRenderer[] spriteParentArray = enableDisableSpriteParent.GetComponentsInChildren<SpriteRenderer>();
            spritesToEnableDisable = spritesToEnableDisable.Concat(spriteParentArray).ToArray();
        }

        // Also remove any disable/enable array from the fade array that might have got added. They will conflict otherwise.
        for (int i = 0; i < spritesToEnableDisable.Length; i++)
        {
            if (ArrayUtility.Contains(spritesToFade, spritesToEnableDisable[i]))
            {
                ArrayUtility.Remove(ref spritesToFade, spritesToEnableDisable[i]);
            }
        }

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

        
/*
        Ray2D ray = new Ray(PLAYER_manager.Instance.playerMovement.playerMovementModule.transform.position + new Vector3(0, 0, -1), Vector3.forward);

        RaycastHit2D hit;

        if (Physics2D.Raycast(ray, out hit)) ;

        if (rayHit.collider != null)
        {
            currSurf = rayHit.collider.gameObject.tag.Trim();
        }*/

    }

    IEnumerator EndOfFirstFrame()
    {
        yield return new WaitForEndOfFrame();
        firstFrame = false;
    }


    public void FadeUp()
    {
        if (gameObject.activeInHierarchy)
        {
            if (firstFrame)
            {
                // On first frame, immediately fade up all the sprites in associated with this script if the player is standing here.
                for (int i = 0; i < spritesToFade.Length; i++)
                {
                    spritesToFade[i].color = new Color(spritesToFade[i].color.r, spritesToFade[i].color.g, spritesToFade[i].color.b, 1f);
                }
                for (int i = 0; i < spritesToEnableDisable.Length; i++)
                {
                    spritesToEnableDisable[i].enabled = true;
                }
            }
            else
            {
                if (spritesToFade.Length != 0)
                {
                    StartCoroutine(SpriteFade(spritesToFade, 1f, fadeDuration));
                }
                // If there are only enable/disable, just enable them now. Otherwise if there are fade sprite, it'll do them at the end of that coroutine ^
                else
                {
                    for (int i = 0; i < spritesToEnableDisable.Length; i++)
                    {
                        spritesToEnableDisable[i].enabled = true;
                    }
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
            // Enable the Enable/Disable Sprites at the end.
            if(newAlpha == 1f)
            {
                for (int i = 0; i < spritesToEnableDisable.Length; i++)
                {
                    spritesToEnableDisable[i].enabled = true;
                    //Debug.Log("coroutine enable reached");
                }
            }
            yield return null;
        }
    }
}
