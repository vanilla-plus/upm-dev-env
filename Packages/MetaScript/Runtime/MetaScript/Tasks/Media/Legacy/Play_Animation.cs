using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.Media
{

	[Serializable]
	public class Play_Animation : MetaTask
	{

		[SerializeField]
		private Animation targetAnimation;

		protected override bool CanAutoName() => targetAnimation != null;

		protected override string CreateAutoName() => $"Play current animation on {targetAnimation.gameObject.name}";


		protected override UniTask<Scope> _Run(Scope scope)
		{
			if (targetAnimation == null)
			{
				Debug.LogError($"Animator reference is null for Play_Animation task [{AutoName}].");

				return UniTask.FromResult(scope);
			}

			targetAnimation.Play();

			return UniTask.FromResult(scope);
		}

	}

}