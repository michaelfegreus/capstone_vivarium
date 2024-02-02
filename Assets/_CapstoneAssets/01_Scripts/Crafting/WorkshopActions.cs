using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopActions : MonoBehaviour
{
    private Animator anim;
    
    [SerializeField] private GameObject UIScanvas;
    private CanvasGroup canvasGroup;
    private ParticleSystem particles;

    void Start()
    {
        anim = GetComponent<Animator>();
        canvasGroup = UIScanvas.GetComponent<CanvasGroup>();
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();
    }

    public void CraftSuccessfully()
    {
        anim.Play("repairingtable_jenny_crafting");
        canvasGroup.alpha = 0f;
        StartCoroutine("ParticlesAfterDelay");
    }

    public void ShowCraftingMenu()
    {
        canvasGroup.alpha = 1f;
        particles.Stop();
    }

    IEnumerator ParticlesAfterDelay()
    {
        yield return new WaitForSeconds(0.35f);
        particles.Play();
    }
}
