using System;
using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using UnityEngine;

namespace Vanilla.MetaScript
{
//
//    public interface IExecutionHandler
//    {
//
//        // Tokens will test their own .Continue as well as their sources
//        // while Sources only need to test their own .Continue
//        bool ShouldContinue();
//        
//        bool Continue
//        {
//            get;
//            set;
//        }
//
//        uint Depth
//        {
//            get;
//            set;
//        }
//
//        void Enter();
//
//        void Exit();
//
//    }
    
    [Serializable]
    public class ExecutionScope
    {

        public ExecutionTrace GetNewTrace() => new ExecutionTrace(this);
        
        [SerializeField]
        // ExecutionScope.Continue acts as the 'global' cancel for the execution chain
        private bool _Continue = true;
        public bool Continue => _Continue;

        [CanBeNull] public ExecutionScope parent = null;
        
        
        [SerializeField]
        private uint _Depth;
        public uint Depth
        {
            get => _Depth;
            set => _Depth = value;
        }

        [SerializeField]
        private int activeTasks = 0;
        public int ActiveTasks
        {
            get => activeTasks;
            set
            {
                activeTasks = value;
                
                Debug.Log($"[{Time.frameCount}] ActiveTasks is now [{activeTasks}]");

                if (activeTasks == 0)
                {
                    Debug.LogError($"[{Time.frameCount}] Execution is officially over with a status of [{(Continue ? "Completed" : "Cancelled")}]");
                }
            }
        }


        public ExecutionScope(ExecutionScope parent)
        {
            this.parent = parent;
            this.Depth  = parent != null ? parent.Depth + 1 : 0;
            
            Debug.Log($"Scope Created [{Depth}]");
        }


        ~ExecutionScope() => Debug.Log($"Scope Destroyed [{Depth}]");


        public void Cancel()
        {
            _Continue = false;
        }


        public void Cancel_With_Parent()
        {
            _Continue = false;
            
            parent?.Cancel();
        }
        
        public void Cancel_Recursive()
        {
            _Continue = false;

            parent?.Cancel_Recursive();
        }

        public bool Cancelled => !Continue || (parent?.Cancelled ?? false);

        public void Enter() => ActiveTasks++;

        public void Exit() => ActiveTasks--;

    }
}
