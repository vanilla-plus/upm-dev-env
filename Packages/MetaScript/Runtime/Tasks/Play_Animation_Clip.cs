using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Play_Animation_Clip : MetaTask
	{

		[SerializeField]
		private Animation targetAnimation;

		[SerializeField]
		private AnimationClip animationClip;

		protected override bool CanAutoName() => targetAnimation != null && animationClip != null;

		protected override string CreateAutoName() => $"Play animation {animationClip.name} on {targetAnimation.gameObject.name}";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (targetAnimation == null ||
			    animationClip  == null)
			{
				Debug.LogError("Animator or AnimationClip not set for PlayAnimationAndWait task.");

				return scope;
			}

			// Start playing the animation
			targetAnimation.Play(animationClip.name);
			
			var animationDuration = animationClip.length;

			// Check for the duration of the animation or if the scope gets cancelled
			for (float timer = 0;
			     timer < animationDuration;
			     timer += Time.deltaTime)
			{
				if (scope.Cancelled)
				{
					targetAnimation.Stop(animationClip.name);
					
					return scope;
				}

				await UniTask.Yield(PlayerLoopTiming.Update);
			}

			return scope;
		}

	}

}