using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Media
{

	[Serializable]
	public class Stop_Animation : MetaTask
	{

		[SerializeField]
		private UnityEngine.Animation targetAnimation;

		protected override bool CanAutoName() => targetAnimation != null;

		protected override string CreateAutoName() => $"Stop current animation on {targetAnimation.gameObject.name}";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (targetAnimation == null)
			{
				Debug.LogError("Animator or AnimationClip not set for PlayAnimationAndWait task.");

				return scope;
			}

			targetAnimation.Stop();

			return scope;
		}

	}

}