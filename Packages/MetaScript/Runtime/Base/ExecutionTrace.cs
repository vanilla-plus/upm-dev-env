using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
//    public struct ExecutionToken : IExecutionHandler
    public struct ExecutionTrace
    {

        public ExecutionScope scope;
//
//        [SerializeField]
//        private bool _Continue; // ExecutionToken.Continue acts as the 'local' cancel for the execution chain
//        public bool Continue
//        {
//            get => _Continue;
//            set => _Continue = value;
//        }
        
//        [SerializeField]
//        private uint _Depth;
//        public uint Depth
//        {
//            get => _Depth;
//            set => _Depth = value;
//        }
        
        public ExecutionTrace(ExecutionScope executionScope)
        {
            scope = executionScope;

//            _Continue = scope.Continue;
//            _Depth    = 0; // Hope this doesn't accidentally keep resetting the depth every time the token is passed by value...
        }

//        public bool Cancelled => !Continue || scope.Cancelled;
        public bool Cancelled => scope.Cancelled;

        public void Enter()
        {
//            Depth++;

            scope.ActiveTasks++;
        }


        public void Exit()
        {
//            Depth--;
            
            scope.ActiveTasks--;
        }
        
    }

}
