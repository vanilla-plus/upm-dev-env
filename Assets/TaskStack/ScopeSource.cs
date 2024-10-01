using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace MagicalProject
{
    public class ScopeSource : MonoBehaviour
    {
        [SerializeField]
        public string ScopeName = "Test";

        [SerializeField]
        public TaskSource target;
        
        [NonSerialized]
        public Scope scope;


        void OnValidate()
        {
            #if UNITY_EDITOR
            if (target == null) target = GetComponent<TaskSource>();
            #endif
        }
        
        [ContextMenu("Initiate")]
        public void Initiate()
        {
//            scope = new Scope(null,
//                              ScopeName);
//	        
//            scope.Add(target.task);
//            
//            scope.Run().Forget();

            HandleJump(null).Forget();
        }


        public async UniTask HandleJump(Scope scope)
        {
            this.scope = new Scope(scope,
                                   ScopeName);
	        
            this.scope.Add(target.task);

            await this.scope.Run();
        }


        [ContextMenu("Cancel")]
        public void Cancel() => scope?.Cancel();

    }
}
