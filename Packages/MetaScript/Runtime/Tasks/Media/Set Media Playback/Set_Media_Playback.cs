using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public abstract class Set_Media_Playback<T> : MetaTask
    {

        protected override bool CanAutoName() => targetComponent != null;

        protected override string CreateAutoName() => $"Set playback on [{targetComponent.GetType().Name}] to [{action.ToString()}]";

        public enum PlayableAction
        {

            Play,
            Pause,
            Stop,
//            Previous,
//            Next,
//            SeekForward,
//            SeekBack

        }

        [SerializeReference]
        protected T targetComponent;
        
        [SerializeField]
        protected PlayableAction action = PlayableAction.Play;


        protected override UniTask<Scope> _Run(Scope scope)
        {
            if (scope.Cancelled) return UniTask.FromResult(scope);

            HandleSet();
            
            return UniTask.FromResult(scope);
        }


        public void HandleSet()
        {
            Action methodToCall = action switch
                                  {
                                      PlayableAction.Play        => Handle_Play,
                                      PlayableAction.Pause       => Handle_Pause,
                                      PlayableAction.Stop        => Handle_Stop,
//                                      PlayableAction.Previous    => Handle_Previous,
//                                      PlayableAction.Next        => Handle_Next,
//                                      PlayableAction.SeekBack    => Handle_SeekBack,
//                                      PlayableAction.SeekForward => Handle_SeekForward,
                                      _                          => throw new ArgumentOutOfRangeException()
                                  };

            methodToCall();
        }


        protected abstract void Handle_Play();
        protected abstract void Handle_Pause();
        protected abstract void Handle_Stop();
//        protected abstract void Handle_Previous();
//        protected abstract void Handle_Next();
//        protected abstract void Handle_SeekBack();
//        protected abstract void Handle_SeekForward();

    }
}
