#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;
using System.Collections.Generic;
using System.Linq;

using Cysharp.Threading.Tasks;

using UnityEngine;

//using Vanilla.MetaScript.Debugger;
using Vanilla.TypeMenu;

//using Vanilla.DeltaValues;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class MetaTaskInstance : MonoBehaviour, IRunnable
	{


//		[SerializeField]
//		public DeltaBool Running = new DeltaBool("MetaTaskInstance Running",
//		                                         false);

//		[NonSerialized]
//		public bool Running = false;

		[SerializeField]
		public bool RunOnStart = false;

		[SerializeField]
		public KeyCode debugRunKey = KeyCode.None;
		
		[SerializeField]
		public KeyCode debugCancelKey = KeyCode.None;

//		[NonSerialized]
//		internal Tracer _tracer = null;

//		[SerializeField]
//		public HashSet<Tracer> tracers = new HashSet<Tracer>();

//		[SerializeField]
//		public MetaScriptDebugger debugger;

		[SerializeReference]
		[TypeMenu]
		[Only(typeof(MetaTask))]
		private MetaTask task;
		public MetaTask Task => task;

		void OnValidate()
		{
			#if UNITY_EDITOR
			task?.OnValidate();

//			if (string.IsNullOrEmpty(Running.Name)) Running.Name = $"[{Task.Name}] Running";
			#endif
		}


		void Start()
		{
			if (RunOnStart) StartChainOfExecution();
		}
//
//		[ContextMenu("Run")]
//		public void RunSelf()
//		{
//			Cancel();
//
////			_tracer       = new Tracer();
//
////			Running.Value = true;
//
//			_tracer = null;
//
////			if (_tracer != null)
////			{
//				// Dispose..? Recycle?
////			}
//
//			_tracer = new Tracer();
//
//			if (debugger) debugger.Connect(_tracer);
//
//			HandleJump(tracer: _tracer).ContinueWith(FinalizeTask);
//
////			task.Run(_tracer).Forget();
//		}


		[ContextMenu("StartChainOfExecution")]
		public Tracer StartChainOfExecution()
		{
//			Cancel();

//			_tracer       = new Tracer();

//			Running.Value = true;

//			_tracer = null;

//			if (_tracer != null)
//			{
			// Dispose..? Recycle?
//			}

//			Debug.LogWarning("StartChainOfExecution");

			var _tracer = new Tracer
			              {
				              Continue = true
			              };
			
//			tracers.Add(_tracer);

//			if (debugger) debugger.Connect(_tracer);

			Run(tracer: _tracer).ContinueWith(FinalizeChainOfExecution);

			return _tracer;
		}


		public async UniTask<Tracer> Run(Tracer tracer) => await task.Run(tracer);

		//
//		public async UniTask<Tracer> HandleJump(Tracer tracer)
//		{
////			Running       = true;
////			Running.Value = true;
//
////			Debug.Log($"Pre-HandleJump! Tracers ID is [{tracer.id}]");
//
//			var Tracer = await task.Run(tracer);
//			
////			Debug.Log($"Post-HandleJump! Tracers ID is [{tracer.id}]");
//
////			Running.Value = false;
////			Running = false;
//
//			return Tracer;
//		}

//
//		[ContextMenu("Cancel")]
//		public void Cancel()
//		{
////			Debug.Log("Cancel attempt!");
//
//			if (_tracer is
//			    {
//				    Continue: true
//			    })
//			{
////				Debug.Log("Cancel approved!");
//
//				_tracer.Continue = false;
//
////				Running.Value = false;
////				Running       = false;
//			}
//		}

//
//		[ContextMenu("Eliminate Tracer")]
//		public void EliminateTracer()
//		{
//			Debug.Log("Snerp");
//			
//			_tracer.Dispose();
//			
//			_tracer = null;
//		}


		private void FinalizeChainOfExecution(Tracer tracer)
		{
//			if (tracer.Continue)
//			{
//				Debug.LogWarning("FinalizeChainOfExecution - Completed successfully!");
//			}
//			else
//			{
//				Debug.LogWarning("FinalizeChainOfExecution - Cancelled!");
//			}
			
//			if (debugger) debugger.Disconnect();

//			tracers.Remove(tracer);
			
//			tracer.Dispose();
		}

		void Update()
		{
			if (Input.GetKeyDown(debugRunKey)) StartChainOfExecution();
//			if (Input.GetKeyDown(debugCancelKey)) CancelChainOfExecution();
		}

//
//		private void CancelChainOfExecution()
//		{
//			if (tracers.Count == 0) return;
//
//			// Find the first Tracer with Continue true and turn it off (thus allowing living tasks to naturally cease)
//			foreach (var t in tracers.Where(t => t.Continue)) t.Continue = false;
//		}

//
//		public void CancelAllTracers()
//		{
//			foreach (var t in tracers) t.Continue = false;
//		}

//		private void OnDestroy() => CancelAllTracers();

	}

}