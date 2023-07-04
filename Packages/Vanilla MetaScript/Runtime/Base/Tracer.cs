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
				
				OnDepthChange?.Invoke();
				
//				Debug.LogWarning($"Tracer Depth [{old} => {_depth}]");
			}
		}

		public Action OnDepthChange;
		
		public Stack<string> CallStack { get; private set; } = new Stack<string>();
		
		public void EnterMethod(string methodName)
		{
			Depth++;
			
			CallStack.Push(methodName);
		}

		public void ExitMethod()
		{
			if (Depth > 0)
			{
				Depth--;
				
				CallStack.Pop();
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