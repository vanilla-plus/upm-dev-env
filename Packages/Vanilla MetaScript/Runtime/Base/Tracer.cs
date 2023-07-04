using System;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Tracer
	{

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		public static void StaticReset()
		{
			TracerCount = 0;
		}

		public static int TracerCount = 0;
		
		[SerializeField]
		public bool Continue = true;

		[SerializeField]
		public int id = 0;

		[SerializeField]
		private int _depth = 0;
		public int Depth
		{
			get => _depth;
			set
			{
//				var old = _depth;
				
				_depth = value;
				
//				OnDepthChange?.Invoke();
				
//				Debug.LogWarning($"Tracer Depth [{old} => {_depth}]");
			}
		}

//		public Action OnDepthChange;

		public Action OnCallStackChange;
		
		public Stack<(int,string)> CallStack { get; private set; } = new();
		
		public void EnterMethod(string methodName)
		{
			Depth++;
			
			Debug.LogWarning($"Adding [{_depth},{methodName}] to CallStack");
			
			CallStack.Push((_depth, methodName));
			
			OnCallStackChange?.Invoke();
		}

		public void ExitMethod()
		{
			if (Depth > 0)
			{
				Depth--;
				
				var outgoing = CallStack.Pop();
				
				OnCallStackChange?.Invoke();

				Debug.LogWarning($"Removing [{outgoing.Item1},{outgoing.Item2}] from CallStack");
			}
		}


		public Tracer()
		{
			TracerCount++;

			id = TracerCount;
			
			Debug.LogError($"Tracer Created\t[{id}]");
		}


		~Tracer()
		{
			Debug.LogError($"Tracer Destroyed\t[{id}]");

			TracerCount--;
		}


		public bool Cancelled(MetaTask task)
		{
			if (Continue) return false;

			task.LogRunCancelled(this);

			return true;
		}
		
	}

}