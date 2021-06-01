using UnityEngine;
using Fungus;

/// <summary>
/// Finds a object in the scene by name to be active / inactive.
/// </summary>
[CommandInfo("Vivarium Event Object",
             "Set Child Active State",
             "Finds a child object by name to be active / inactive. This is useful to use in conjunction with spawned Event Objects.")]

public class SetChildActive : Command
{
    protected GameObject parentObject;

    [Tooltip("The name of the object to search for. (Defaults to the Parent Block's name.)")]
    [SerializeField] protected string targetGameObjectName;

    [Tooltip("The active state to set the child object. Set true to set the object active, set false to set the object inactive.")]
    [SerializeField] protected bool activeState;

    [Header("Optional")]
    [Tooltip("The parent game. (You might use this to set another Flowchart's spawned child objects inactive.) Otherwise, defaults to this Flowchart as parent if there is nothing input.")]
    [SerializeField] protected GameObject alternateParentObject;

    public override void OnEnter()
    {
        if (targetGameObjectName == "")
        {
            targetGameObjectName = ParentBlock.BlockName;
        }
        // Defaults to the Flowchart object if nothing is set in alternativeParentObject.
        if (alternateParentObject != null)
        {
            parentObject = alternateParentObject;
        }
        else
        {
            parentObject = GetFlowchart().gameObject;
        }

        Transform childObject;

        // Find and set active state of child
        childObject = parentObject.transform.Find(targetGameObjectName);

        if (childObject == null)
        {
            Debug.Log("Fungus Set Child Active command did not find a child object with the given name.");
        }
        else
        {
            childObject.gameObject.SetActive(activeState);
        }

        Continue();
    }
}

