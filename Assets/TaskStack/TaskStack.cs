//using System;
//using System.Collections;
//using System.Collections.Generic;
//
//using Cysharp.Threading.Tasks;
//
//using UnityEngine;
//
//using Vanilla.TypeMenu;
//
//namespace MagicalProject
//{
//    
//	[Serializable]
//	public class TaskStack : MonoBehaviour
//	{
//        
//		public Stack<SomeTask> stack = new Stack<SomeTask>();
//
//		[SerializeReference]
//		[TypeMenu("blue")]
//		public SomeTask someTask;
//
//		public UniTask CurrentTask;
//
//		public void AddSomeTask() => stack.Push(someTask);
//		
//		public async UniTask RunStack()
//		{
//			while (stack.TryPop(out var task))
//			{
//				CurrentTask = task.Run(); // This will run it straight away, right?
//				
//				await CurrentTask;
//			}
//		}
//        
//	}
//}