using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Playables;

namespace Vanilla.MetaScript.Media
{

	
	[Serializable]
	public class Play_Timeline : MetaTask
	{
		[SerializeField]
		private PlayableDirector playableDirector;

		protected override bool CanAutoName() => playableDirector != null;

		protected override string CreateAutoName() => $"Play Timeline {playableDirector.name}";

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (playableDirector == null)
			{
				Debug.LogError("PlayableDirector not assigned in PlayTimeline task.");
				return scope;
			}

			playableDirector.Play();

			// Wait for the timeline to finish playing.
			while (playableDirector.state == PlayState.Playing)
			{
				if (scope.Cancelled) return scope;

				await UniTask.Yield(PlayerLoopTiming.Update);
			}

			return scope;
		}
	}
}