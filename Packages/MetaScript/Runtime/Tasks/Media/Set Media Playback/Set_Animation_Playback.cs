using System;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	public class Set_Animation_Playback : Set_Media_Playback<UnityEngine.Animation>
	{

		protected override void Handle_Play() => targetComponent.Play();

		protected override void Handle_Pause() => targetComponent.Stop();

		protected override void Handle_Stop() => targetComponent.Stop();

	}
}