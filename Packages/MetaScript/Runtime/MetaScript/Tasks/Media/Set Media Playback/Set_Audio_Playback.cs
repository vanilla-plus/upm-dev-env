using System;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	public class Set_Audio_Playback : Set_Media_Playback<AudioSource>
	{

		protected override void Handle_Play() => targetComponent.Play();

		protected override void Handle_Pause() => targetComponent.Pause();

		protected override void Handle_Stop() => targetComponent.Stop();

	}
}