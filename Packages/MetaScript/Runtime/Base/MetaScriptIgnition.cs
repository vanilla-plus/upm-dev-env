using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class MetaScriptIgnition : MonoBehaviour
	{

		[SerializeField]
		public bool RunOnStart = false;

		[SerializeField]
		public KeyCode debugRunKey = KeyCode.Alpha1;

		[SerializeField]
		public KeyCode debugCancelByIndex = KeyCode.Alpha2;
		
		[SerializeField]
		public KeyCode debugCancelAllKey = KeyCode.Alpha3;

		public int debugCancelIndex = 0;
		
		[SerializeField]
		public List<MetaTaskInstance> targets = new List<MetaTaskInstance>();
		
		[SerializeReference]
		public List<ExecutionScope> scopes = new List<ExecutionScope>();

		void Start()
		{
			if (RunOnStart) StartChainOfExecution();
		}
		
		
		[ContextMenu("StartChainOfExecution")]
		public void StartChainOfExecution()
		{
			foreach (var t in targets)
			{
				var source = new ExecutionScope(null);

				var trace = source.GetNewTrace();
				
				scopes.Add(source);

//				if (debugger) debugger.Connect(_trace);

				MonitorExecution(source);

//				t.Run(trace: token).ContinueWith(FinalizeChainOfExecution); 
				t.Run(trace: trace);

				// This might be a bit wonky still.
				// It works okay in testing so far - but making sure FinalizeChainOfExecution is only called when
				// ...any chains of execution have ended? All of them?
				// It's tough because we need to hold a reference to ExecutionSource (in case we want to cancel of our own accord)
				// But that also means knowing when to intelligently release the reference to ExecutionSource so that it can be
				// properly cleaned up or disposed of from memory.
				// The Tokens should be fine since they're struct/values - they'll still expire when the function call they're in does.
				// But we could end up in a weird situation if something is accidentally holding onto an ExecutionSource reference...
				// Like... this class!
				// Tl;dr - make sure FinalizeChainOfExecution only happens... when...?
			}
		}


		private async UniTask MonitorExecution(ExecutionScope scope)
		{
//			await UniTask.NextFrame();

			Debug.LogWarning($"[{Time.frameCount}] Beginning execution");

//			byte framesSpentWithNoActivity = 0;
//
//			do
//			{
//				if (source.GlobalDepth == 0)
//				{
//					framesSpentWithGlobalDepth0++;
//				}
//				else
//				{
//					framesSpentWithGlobalDepth0 = 0;
//				}
//
//				await UniTask.Yield();
//			}
//			while (framesSpentWithGlobalDepth0 < 2);
//
//			while (framesSpentWithNoActivity < 2)
//			{
//				if (source.Activity == 0)
//				{
//					framesSpentWithNoActivity++;
//				}
//				else
//				{
//					framesSpentWithNoActivity = 0;
//				}
//
//				await UniTask.Yield();
//			}

			await UniTask.WaitUntil(() => scope.ActiveTasks == 0);
//
//			do
//			{
//				await UniTask.Yield();
//			}
//			while (source.Activity != 0);

//			while (source.Activity != 0) await UniTask.Yield();

//			await UniTask.WaitUntil(() => source.GlobalDepth == 0);

//			Debug.LogWarning($"[{Time.frameCount}] Execution complete");
			Debug.LogWarning($"[{Time.frameCount}] Execution {(scope.Continue ? "completed successfully" : "cancelled")}");

			FinalizeChainOfExecution(scope);
		}
		
		private void FinalizeChainOfExecution(ExecutionScope scope)
		{
//			Debug.LogError("All done here..!? <- no ignore this bad lucas or even better comment it out");

			// Even if we know that when ExecutionSource.GlobalDepth reaches 0 again, it means execution is over...
			// how is the ExecutionSource going to let this Ignition script instance know to release it's reference of it?
			
//			if (debugger) debugger.Disconnect();

			scopes.Remove(scope);
			
//			trace.Dispose();
		}
		
		void Update()
		{
			if (Input.GetKeyDown(debugRunKey)) StartChainOfExecution();
			if (Input.GetKeyDown(debugCancelByIndex)) CancelTargetChainOfExecution(debugCancelIndex);
			if (Input.GetKeyDown(debugCancelAllKey)) CancelAllChainsOfExecution();
		}


		public void CancelTargetChainOfExecution(int index)
		{
			var scope = scopes[index];
			
			scopes.RemoveAt(index);
			
//			scope.Continue = false;
			
//			scope.Cancel();
			scope.Cancel();
		}


		public void CancelAllChainsOfExecution()
		{
			foreach (var scope in scopes) scope.Cancel();
			
			scopes.Clear();
		}
		
	}

}