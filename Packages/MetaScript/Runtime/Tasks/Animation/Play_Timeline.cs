using System;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Playables;

namespace Vanilla.MetaScript.Animation
{
	[Serializable]
	public class Play_Timeline : MetaTask
	{
		[SerializeField]
		private PlayableDirector timelineDirector;

		protected override bool CanAutoName() => timelineDirector != null;

		protected override string CreateAutoName() => $"Play Timeline {timelineDirector.name}";

		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (timelineDirector == null)
			{
				Debug.LogError("PlayableDirector not assigned in PlayTimeline task.");
				return scope;
			}

			timelineDirector.Play();

			// Wait for the timeline to finish playing.
			while (timelineDirector.state == PlayState.Playing)
			{
				if (scope.Cancelled) return scope;

				await UniTask.Yield(PlayerLoopTiming.Update);
			}

			return scope;
		}
	}
}