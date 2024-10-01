using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace MagicalProject
{
    
    [Serializable]
    public class TaskSource : MonoBehaviour
    {

//	    [SerializeField]
//	    public bool   NewScope = false;
//	    
//	    [SerializeField]
//        public string ScopeName     = "Test";
//
////        public Scope scope = new Scope(null, string.Empty);
//		[NonSerialized]
//	    public Scope scope;

        [SerializeReference]
        [TypeMenu("red")]
        public SomeTask task;
        
        void OnValidate()
        {
	        #if UNITY_EDITOR
	        task?.OnValidate();
	        #endif
        }
        
//        [ContextMenu("Initiate")]
//        public void Initiate()
//        {
//	        scope = new Scope(null,
//	                          ScopeName);
//	        
//	        scope.Add(task);
//            
//	        scope.Run().Forget();
//        }

//
//        public void JumpTo(Scope scope)
//        {
//	        
//        }
        
//        void OnEnable()
//        {
//	        scope.Name = ScopeName;
//	        
////            scope = new Scope(null,
////                              Name);
//            
//            scope.Add(task);
//            
////            scope.Stack.Push(task);
//
//            scope.Run().Forget();
//        }

    }
}
