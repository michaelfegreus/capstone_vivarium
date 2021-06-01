using UnityEngine;
using Fungus;

public class EVENT_npc : EventObject, IInteractable
{
    // Delegate to run function on flag goal met.
    /*public delegate void ExitDelegate();
    [HideInInspector] public ExitDelegate exitDelegate;*/

    [HideInInspector] public bool exitOnInteraction;

    [Header("NPC Attributes")]
    // Optional name to set on the NPC object.
    public string NPCName;
    // Override with a specific yarn node.
    //public string yarnNode;
    // Block to Execute when interacting with this NPC. Generally, this will be used to run dialogue.
    public BlockReference interactBlock;
    // Set whether the NPC can be interacted with or not.
    public bool nonInteractable;
    // Prevents the NPC from being disabled, regardless of its designated Event End Time.
   // public bool ignoreEventEndTime;
    public AnimatorOverrideController animatorOverride;

    [Header("Components")]
    public Animator anim;
    public INTERACTABLE_npc_dialogue dialogueScript;

    public override void OnEnable()
    {
        base.OnEnable();
        
        if (nonInteractable)
        {
            tag = "NonInteractable";
        }
        else
        {
            tag = "Interactable";
        }

        gameObject.name = NPCName;

        //dialogueScript.SetTalkToNode(yarnNode);

        if (anim != null)
        {
            anim.runtimeAnimatorController = animatorOverride;
        }

    }

    public override void OnDisable()
    {
        base.OnDisable();

        /*if (exitDelegate != null)
        {
            exitDelegate();
        }*/
    }



    public override void CheckEventEndTime()
    {
        if (WORLD_manager.Instance.timeManager.inGameTime.TimeMet(eventEndTime))
        {
            // Run end of event functions here
            // TODO: Make it so Yarn, or other factors, cause the NPC to exit too.
            PlayExitAnimation();
            //anim.Play("Exit"); // <-- The NPC Animator is set up to disable after playing the "Exit" animation
        }
    }

    private void PlayExitAnimation()
    {
        anim.Play("Exit"); // <-- The NPC Animator is set up to disable after playing the "Exit" animation
    }

    public void OnInteract()
    {
        if (interactBlock.Equals(null))
        {
            Debug.Log("No Interact Block applied to Event NPC " + gameObject.name);
        }
        else
        {
            interactBlock.Execute();
        }

        // Run this Coroutine to wait until dialogue is finished, if the NPC is supposed to exit on interaction.
        /*if (exitOnInteraction)
        {
            // Do this to wait a frame before starting the coroutine, to make sure the dialogue starts running.
            Invoke("StartDialogueFinishCheck", .1f);
        }*/
    }

    // Have to take this extra step to use an invocation to wait a frame before starting the coroutine.
    /*private void StartDialogueFinishCheck()
    {
        StartCoroutine(DialogueFinishCheck());
    }

    IEnumerator DialogueFinishCheck()
    {
        while (GAME_manager.Instance.dialogRunner.isDialogueRunning)
        {
            yield return new WaitForSeconds(.1f);
        }
        PlayExitAnimation();
    }*/

        // Backup: 
        /*
         * using UnityEngine;
        //using System.Collections;

        public class EVENT_npc : MonoBehaviour
        {
            Animator anim;
            INTERACTABLE_npc_dialogue dialogueScript;
            //public Collider2D myCollider;

            //int animIterator = 0;

            [Header("Event Attributes")]
            // Optional name to set on the NPC object.
            public string NPCName;
            // Override with a specific yarn node.
            public string yarnNode;
            // Set whether the NPC can be interacted with or not.
            public bool nonInteractable;
            // Prevents the NPC from being disabled, regardless of its designated Event End Time.
            public bool disableDespawn;
            public AnimatorOverrideController animatorOverride;
            // The in-game time when this NPC should play its exit animation, then de-activate.
            public GameTime eventEndTime;
            // Annoying, but this is necessary to not throw errors when quitting. See more in the OnDisable function code
            bool quittingApplication;

            // Has this been set by WORLD_objectpool_manager?
           //[HideInInspector] public bool takeFromQueue;

            private void OnEnable()
            {
                // Begin listening for the minute tick to check if the Event End Time has come yet.
                WORLD_time_manager.OnMinuteTick += CheckEventEndTime;

                if (anim == null)
                {
                    anim = GetComponent<Animator>();
                }
                if (dialogueScript == null)
                {
                    dialogueScript = GetComponent<INTERACTABLE_npc_dialogue>();
                }

                if (nonInteractable)
                {
                    tag = "NonInteractable";
                }
                else
                {
                    tag = "Interactable";
                }

                gameObject.name = NPCName;

                dialogueScript.SetTalkToNode(yarnNode);

                if (anim != null)
                {
                    anim.runtimeAnimatorController = animatorOverride;
                }

            }

            private void OnDisable()
            {
                WORLD_time_manager.OnMinuteTick -= CheckEventEndTime;

                // This is annoying, but you have to check to see if the application isn't quitting first.
                // Quitting destroys the created object pool Game Objects, and then calls their OnDisable functions. When it tries to enqueue them it throws a null object reference error.
                if (!quittingApplication)
                {
                    WORLD_manager.Instance.objectPoolManager.AddToQueue(gameObject, WORLD_objectpool_manager.EventObjectType.NPC);
                }

            }

            private void OnApplicationQuit()
            {
                quittingApplication = true;
            }

            private void CheckEventEndTime() {
                if (WORLD_manager.Instance.timeManager.inGameTime.TimeMet(eventEndTime))
                {
                    // Run end of event functions here
                    anim.Play("Exit");
                }
            }
        }
        */
    }