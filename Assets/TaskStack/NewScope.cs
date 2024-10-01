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
//	public class NewScope : SomeTask
//	{
//
//		public string ScopeName = "Test";
//
////		[SerializeReference]
////		[TypeMenu("red")]
////		public SomeTask task;
//
//		protected override string AutoName => $"Begin a new scope called [{ScopeName}]";
//
//
//		public override async UniTask<Scope> Run(Scope scope)
//		{
//			var newScope = new Scope(scope,
//			                         Name);
//
////			newScope.Add(task);
//
//			await newScope.Run();
//
//			return scope;
//		}
//
//	}
//
//}