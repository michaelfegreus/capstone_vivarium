using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class MarionneBehaviors : MonoBehaviour
{

    [SerializeField] private bool marionneDead;

    void Start()
    {
        marionneDead = true;
    }

    public void KillMarionne()
    {
        marionneDead = true;
        DialogueLua.SetVariable("NPC.MarionneDead", true);
    }

    public void ReviveMarionne()
    {
        marionneDead = false;
        DialogueLua.SetVariable("NPC.MarionneDead", false);
    }
}
