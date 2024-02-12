using System;

using UnityEngine.Playables;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Set_Playable_Playback : Set_Media_Playback<PlayableDirector>
    {

        protected override void Handle_Play() => targetComponent.Play();

        protected override void Handle_Pause() => targetComponent.Pause();

        protected override void Handle_Stop() => targetComponent.Stop();

    }
}
