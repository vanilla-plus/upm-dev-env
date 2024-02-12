using System;

using UnityEngine;
using UnityEngine.Playables;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	public class Set_Custom_Media_Playback : Set_Media_Playback<PlayableDirector>
	{

		[SerializeField] public string PlayCallString = "Play";
		[SerializeField] public string PauseCallString = "Pause";
		[SerializeField] public string StopCallString = "Stop";

		protected override void Handle_Play() => targetComponent.SendMessage(PlayCallString);

		protected override void Handle_Pause() => targetComponent.SendMessage(PauseCallString);

		protected override void Handle_Stop() => targetComponent.SendMessage(StopCallString);

	}
}