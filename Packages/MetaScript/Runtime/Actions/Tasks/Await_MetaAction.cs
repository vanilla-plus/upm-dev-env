using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

    [Serializable]
    public class Await_MetaAction : MetaTask
    {

        [SerializeField] public MetaAction metaAction;

        protected override bool Validate => metaAction != null;

        protected override string CreateAutoName() => $"Await {metaAction.name} invocation";


        protected override async UniTask<Scope> _Run(Scope scope)
        {
            if (metaAction == null)
            {
                Debug.LogError($"[{Time.frameCount}] Await_MetaAction has a null slot.");

                return scope;
            }

            #if debug
            Debug.Log($"Beginning await for the MetaAction [{metaAction.name}]");
            #endif

            var proceed = false;

            void HandleInvoke()
            {
                #if debug
                Debug.Log($"MetaAction [{metaAction.name}] invocation handled!");
                #endif

                proceed = true;
            }

            metaAction.OnInvoke += HandleInvoke;

            while (!proceed)
            {
                if (scope.Cancelled) return scope;

                await UniTask.Yield();
            }

            metaAction.OnInvoke -= HandleInvoke;

            #if debug
            Debug.Log($"Ending await for the MetaAction [{metaAction.name}]");
            #endif

            return scope;
        }

    }

}
