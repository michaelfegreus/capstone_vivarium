using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

// https://www.pixelcrushers.com/phpbb/viewtopic.php?f=3&t=3147
public class STMFastForward : PixelCrushers.DialogueSystem.StandardUIContinueButtonFastForward
{
	public SuperTextMesh targetTextMesh;

	private bool noMoreClick;

	private UnityEngine.UI.Button contButton;

	public override void Awake()
	{
		contButton = GetComponent<UnityEngine.UI.Button>();
		base.Awake();
	}

	public override void OnFastForward()
	{
		var state = DialogueManager.currentConversationState;

		if (!string.IsNullOrWhiteSpace(targetTextMesh.leftoverText) && !targetTextMesh.reading)
		{
			targetTextMesh.Continue();
		}
		else if (targetTextMesh != null && targetTextMesh.reading)
		{
			targetTextMesh.SpeedRead();
		}
		else
		{
			if (state != null)
			{
				if (state.hasPCResponses && !state.hasForceAutoResponse)
				{
					runtimeDialogueUI.OnContinueConversation();
				}
				else if (!state.hasPCResponses)
				{
					if (!noMoreClick)
					{
						noMoreClick = true;
						StartCoroutine(UnReadThenFastForward());
					}
				}
			}
		}

	}

	IEnumerator UnReadThenFastForward()
	{
		targetTextMesh.UnRead();
		yield return new WaitForSeconds(targetTextMesh.totalUnreadTime);
		base.OnFastForward();
		noMoreClick = false;
		contButton.interactable = true;
	}
}