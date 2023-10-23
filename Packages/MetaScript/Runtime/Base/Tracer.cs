//#if UNITY_EDITOR || DEVELOPMENT_BUILD
//#define debug
//#endif
//
//using System;
//using System.Collections.Generic;
//using System.Threading;
//
//using UnityEngine;
//
//namespace Vanilla.MetaScript
//{
//
//	[Serializable]
//	public class Tracer
////	public struct Tracer
//	{
//
//		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
//		public static void StaticReset() => TracerCount = 0;
//
//		public static uint TracerCount = 0;
//
////		[NonSerialized]
////		private bool DebugMode = false;
//		
//		[SerializeField]
////		[NonSerialized]
//		public bool Continue = true;
////		public bool Continue;
//
//		[SerializeField]
////		[NonSerialized]
//		public uint id = 0;
////		public uint id;
//
//		[SerializeField]
////		[NonSerialized]
//		public uint Depth = 0;
////		public uint Depth;
//
//		[NonSerialized]
//		public Action OnCallStackChange;
//		
//		[NonSerialized]
//		public Stack<(uint, string)> CallStack;
//		
//		public void EnterMethod(string methodName)
//		{
//			Depth++;
//
//			#if debug
////			Debug.LogWarning($"Adding [{Depth},{methodName}] to CallStack");
////			#endif
//
//			
////			if (DebugMode)
////			{
//				CallStack.Push((Depth, methodName));
//
//				OnCallStackChange?.Invoke();
////			}
//			#endif
//		}
//
//
//		public void ExitMethod()
//		{
//			if (Depth > 0)
//			{
//				Depth--;
//
//				#if debug
//
////				if (DebugMode)
////				{
////					var outgoing = CallStack.Pop();
//
//				CallStack.Pop();
//
//				OnCallStackChange?.Invoke();
//
////					#if debug
////					Debug.LogWarning($"Removing [{outgoing.Item1},{outgoing.Item2}] from CallStack");
//				#endif
//
////				}
//			}
//			else { }
//
//		}
//
//
//		public Tracer()
//		{
//			id = TracerCount;
//
//			TracerCount++;
//
//			#if debug
//			CallStack = new Stack<(uint, string)>();
//
//			Debug.Log($"Tracer Created\n  • ID\t{id}\n  • Depth\t{Depth}\n  • TracerCount\t{TracerCount}");
//			#endif
//		}
//
//
//		~Tracer()
//		{
//			TracerCount--;
//
//			#if debug
//			CallStack.Clear();
//
//			CallStack = null;
//
//			Debug.Log($"Tracer Destroyed [{(Continue ? "Complete" : "Cancelled")}]\n  • ID\t{id}\n  • Depth\t{Depth}\n  • TracerCount\t{TracerCount}");
//			#endif
//		}
//
//
//		public bool HasBeenCancelled(MetaTask task)
//		{
//			if (Continue) return false;
//
//			task.LogRunCancelled(tracer: this);
//
//			return true;
//		}
//
////
////		public void Dispose()
////		{
////			Debug.LogWarning("Bin time lil buddy");
////
////			if (Depth > 0)
////			{
////				Debug.LogError("UNLESS??");
////				
////				return;
////			}
////			
////			TracerCount--;
////
////			#if debug
////			CallStack.Clear();
////
////			CallStack = null;
////
////			Debug.Log($"Tracer Destroyed\n  • ID\t{id}\n  • Depth\t{Depth}\n  • TracerCount\t{TracerCount}");
////			#endif
////		}
//
//	}
//
//}