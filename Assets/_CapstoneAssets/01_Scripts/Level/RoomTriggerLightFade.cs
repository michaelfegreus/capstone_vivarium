using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;

public class RoomTriggerLightFade : MonoBehaviour, IRoomTrigger
{
    //[Tooltip("Setting this will cause the script to automatically add the children of this object to the fade array.")]
    // Temporarily edited out, since I'll try using only disable/enable for simplicity
    //public Transform fadeLightsParent;

    [Tooltip("Setting this will cause the script to automatically add the children of this object to the enable/disable array.")]
    public Transform enableDisableLightsParent;

    [Tooltip("Enable this to fade up and down lights when the player enters this trigger volume. Otherwise, you can just call the public methods on this to fade up and down lights.")]
    public bool affectOnPlayerTrigger = true;
    float fadeDuration = .25f;

    Light2D[] fadeLights;
    Light2D[] enableDisableLights;

    bool firstFrame; //Used to check if it's the first frame, and if collider fucntions should immediately take effect.

    [SerializeField] bool alwaysFadedUp;

    void Start()
    {
        // Initialize arrays to avoid errors. Remove this if you end up putting these arrays as editable in the inspector for any reason.
        fadeLights = new Light2D[0];
        enableDisableLights = new Light2D[0];

        //fadeLights = fadeLightsParent.GetComponentsInChildren<Light2D>();
        enableDisableLights = enableDisableLightsParent.GetComponentsInChildren<Light2D>();

        firstFrame = true;

        // Start with setting off all disable / fading sprites

        // Disabling
        if (enableDisableLights.Length > 0)
        {
            for (int i = 0; i < enableDisableLights.Length; i++)
            {
                enableDisableLights[i].enabled = false;
            }
        }
        // Fading
        if (fadeLights.Length > 0)
        {
            for (int i = 0; i < fadeLights.Length; i++)
            {
                fadeLights[i].intensity = 0f;
            }
        }

        // Then, wait for the first frame of collision checks to enable and fade up what should be on screen.

        StartCoroutine(EndOfFirstFrame());

        if (alwaysFadedUp)
        {
            for (int i = 0; i < enableDisableLights.Length; i++)
            {
                enableDisableLights[i].enabled = true;
            }
            // On first frame, immediately fade up all the sprites in associated with this script if the player is standing here.
            if (fadeLights.Length > 0)
            {
                for (int i = 0; i < fadeLights.Length; i++)
                {
                    fadeLights[i].intensity = 1f;
                }
            }
        }
    }

    IEnumerator EndOfFirstFrame()
    {
        yield return new WaitForEndOfFrame();
        firstFrame = false;
    }

    public void FadeUp()
    {
        if (!alwaysFadedUp)
        {
            if (gameObject.activeInHierarchy)
            {
                for (int i = 0; i < enableDisableLights.Length; i++)
                {
                    enableDisableLights[i].enabled = true;
                }

                if (firstFrame)
                {
                    // On first frame, immediately fade up all the sprites in associated with this script if the player is standing here.
                    if (fadeLights.Length > 0)
                    {
                        for (int i = 0; i < fadeLights.Length; i++)
                        {
                            fadeLights[i].intensity = 1f;
                        }
                    }
                }
                else
                {
                    if (fadeLights.Length != 0)
                    {
                        StartCoroutine(SpriteFade(fadeLights, 1f, fadeDuration));
                    }
                }
            }
        }
    }
    public void FadeDown()
    {
        if (!alwaysFadedUp)
        {
            if (gameObject.activeInHierarchy)
            {
                if (enableDisableLights.Length > 0)
                {
                    for (int i = 0; i < enableDisableLights.Length; i++)
                    {
                        enableDisableLights[i].enabled = false;
                    }
                }



                if (fadeLights.Length != 0)
                {
                    StartCoroutine(SpriteFade(fadeLights, 0f, fadeDuration));
                }
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
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (affectOnPlayerTrigger)
        {
            if (col.tag.Trim().Equals("Player".Trim()))
            {
                FadeDown();
            }
        }
    }
    
    public IEnumerator SpriteFade(Light2D[] lights, float endValue, float duration)
    {
        float startValue = lights[0].intensity;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].intensity = newValue;
            }
            yield return null;
        }
    }

    public void OnEnterRoom()
    {
        FadeUp();
    }

    public void OnExitRoom()
    {
        FadeDown();
    }
}