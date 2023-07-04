#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

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

//		[NonSerialized]
//		private bool DebugMode = false;
		
		[SerializeField]
		public bool Continue = true;

		[SerializeField]
		public int id = 0;

		[NonSerialized]
		public int Depth = 0;
		
		[NonSerialized]
		public Action OnCallStackChange;

		public Stack<(int, string)> CallStack;
		
		public void EnterMethod(string methodName)
		{
			Depth++;
			
			#if debug
			Debug.LogWarning($"Adding [{Depth},{methodName}] to CallStack");
//			#endif

			
//			if (DebugMode)
//			{
				CallStack.Push((Depth, methodName));

				OnCallStackChange?.Invoke();
//			}
			#endif
		}


		public void ExitMethod()
		{
			if (Depth > 0)
			{
				Depth--;

					#if debug
//				if (DebugMode)
//				{
					var outgoing = CallStack.Pop();

					OnCallStackChange?.Invoke();

//					#if debug
					Debug.LogWarning($"Removing [{outgoing.Item1},{outgoing.Item2}] from CallStack");
					#endif
//				}
			}
		}


		public Tracer(bool debugMode = false)
		{
//			DebugMode = debugMode;
			
			TracerCount++;

			id = TracerCount;
			
			#if debug
			CallStack = new Stack<(int, string)>();
			
			Debug.Log($"Tracer Created\t[{id}]");
			#endif
		}


		~Tracer()
		{
			#if debug
			CallStack.Clear();

			Debug.Log($"Tracer Destroyed\t[{id}]");
			#endif

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