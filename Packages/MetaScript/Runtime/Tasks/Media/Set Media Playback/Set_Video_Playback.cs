using System;

using UnityEngine.Video;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	public class Set_Unity_Video_Playback : Set_Media_Playback<VideoPlayer>
	{

		protected override void Handle_Play() => targetComponent.Play();

		protected override void Handle_Pause() => targetComponent.Pause();

		protected override void Handle_Stop() => targetComponent.Stop();

	}
}