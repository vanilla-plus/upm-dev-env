using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace MagicalProject
{

	[Serializable]
	public class Scope
	{

		[NonSerialized]
		public string Name;

		[NonSerialized]
		public List<Scope> children = new List<Scope>();
		
//		[NonSerialized]
//		public Scope parent;

		[NonSerialized]
		public sbyte Depth = 0;

		[NonSerialized]
		public  Stack<SomeTask>         Stack                   = new Stack<SomeTask>();

		[SerializeField]
		public bool DebugMode = true;

		[SerializeField]
		public string CurrentTask;
		
		[SerializeField]
		public List<string> DebugTasks = new List<string>();
		
		[NonSerialized]
		private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();


		public Scope(Scope parent, string name)
		{
			Debug.LogWarning($"Scope Created! [{name}]");
			
			if (parent != null)
			{
				parent.children.Add(this);
				
				Depth = (sbyte) (parent.Depth + 1);
			}
			else
			{
				Depth = 0;
			}

//			this.parent = parent;
            
			Name = name;

//			Depth = (sbyte) (parent != null ?
//				                parent.Depth + 1 :
//				                0);
//
//			#if debug
//			var output = $"+ {Name}";
//            
//			for (var i = 0;
//			     i < Depth;
//			     i++)
//			{
//				output = "    " + output;
//			}
//            
//			Debug.Log(output);
//			#endif
		}


		public void Add(SomeTask task)
		{
			Stack.Push(task);

			RecountDebugTasks();
		}


		public void RecountDebugTasks()
		{
			if (DebugMode)
			{
				CurrentTask = string.Empty;
				DebugTasks.Clear();
				
				foreach (var t in Stack)
				{
					DebugTasks.Add(t.Name);
				}
			}
		}
		
		public async UniTask Run()
		{
			// Would this work..?

			while (Stack.TryPop(out var task))
			{
				RecountDebugTasks();

				if (DebugMode) CurrentTask = task.Name;
				
				await task.Run(this).AttachExternalCancellation(cancellationTokenSource.Token);

//				RecountDebugTasks();
			}
			
			RecountDebugTasks();


			// Or do you need to track for scope changes? I can't imagine that far ahead...

//			var currentScope = this;
//			
//			while (Stack.TryPop(out var task))
//			{
//				currentScope = await task.Run(currentScope).AttachExternalCancellation(cancellationTokenSource.Token);
//			}
		}

		public void Cancel()
		{
			cancellationTokenSource.Cancel();
			
			Stack.Clear();

			foreach (var child in children)
			{
				child.Cancel();
			}
		}
	}

}