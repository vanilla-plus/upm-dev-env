using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Playables;

namespace Vanilla.MetaScript.Media
{

	[Serializable]
	public class Stop_Playable : MetaTask
	{

		[SerializeField]
		private PlayableDirector playableDirector;
		
		protected override bool CanAutoName() => playableDirector != null;

		protected override string CreateAutoName() => $"Stop Playable on {playableDirector.gameObject.name}";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (playableDirector == null)
			{
				Debug.LogError("Animator or AnimationClip not set for PlayAnimationAndWait task.");

				return scope;
			}

			playableDirector.Stop();

			return scope;
		}

	}

}