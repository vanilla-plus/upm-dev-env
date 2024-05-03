using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Wait_Until_Bool_Equals : MetaTask
    {

        [TypeMenu("red")]
        [SerializeReference]
        public BoolSource A;
        
        [TypeMenu("red")]
        [SerializeReference]
        public BoolSource B;
        
        
        protected override bool Valid => A != null && B != null;


        protected override string CreateAutoName() => $"Wait until [{(A == null ? "null" : A)}] equals [{(B == null ? "null" : B)}]";


        protected override async UniTask<Scope> _Run(Scope scope)
        {
            if (A == null)
            {
                Debug.LogError("Wait_Until_Bool_Equals task has a null source [A]");
				
                return scope;
            }
            
            if (B == null)
            {
                Debug.LogError("Wait_Until_Bool_Equals task has a null source [B]");
				
                return scope;
            }

            while (A.Value != B.Value)
            {
                if (scope.Cancelled) return scope;

                await UniTask.Yield();
            }

            return scope;
        }

    }
}
