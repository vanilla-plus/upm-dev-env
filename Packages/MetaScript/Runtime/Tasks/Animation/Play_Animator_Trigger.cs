using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Animation
{

	[Serializable]
	public class Play_Animator_Trigger : MetaTask
	{

		[SerializeField]
		private Animator targetAnimator;

		[SerializeField]
		private string triggerName;

		[SerializeField]
		private int layer = 0; // Animator layer


		// This task can auto-name itself if the Animator and triggerName are set.
		protected override bool CanAutoName() => targetAnimator != null && !string.IsNullOrEmpty(triggerName);


		// Create the AutoName here.
		protected override string CreateAutoName() => $"Trigger {triggerName} on {targetAnimator.gameObject.name} and wait";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (targetAnimator == null ||
			    string.IsNullOrEmpty(triggerName))
			{
				Debug.LogError("Animator or trigger name not set for TriggerAnimationAndWait task.");

				return scope;
			}

			targetAnimator.SetTrigger(triggerName);

			// Wait for the animation to start and finish
			await WaitForAnimation(scope);

			return scope;
		}


		private async UniTask WaitForAnimation(Scope scope)
		{
			var isAnimationPlaying = true;

			while (isAnimationPlaying)
			{
				if (scope.Cancelled) return;

				var stateInfo = targetAnimator.GetCurrentAnimatorStateInfo(layer);
				
				if (stateInfo.IsName(triggerName) &&
				    stateInfo.normalizedTime >= 1.0f)
				{
					isAnimationPlaying = false;
				}

				await UniTask.Yield(PlayerLoopTiming.Update);
			}
		}

	}

}