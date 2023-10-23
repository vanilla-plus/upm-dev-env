#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public abstract class MetaTask : IRunnable
	{

		[SerializeField]
		private string _Name;
		public string Name => _Name;
		
		[SerializeField]
		public string AutoName;

//		[SerializeField]
//		private TaskExecutionType _executionType = TaskExecutionType.RunAndWait;
//		public TaskExecutionType ExecutionType => _executionType;

		[SerializeField]
//		public ExecutionOptions executionOptions = ExecutionOptions.Run | ExecutionOptions.Wait | ExecutionOptions.CancelLocal & ExecutionOptions.CancelGlobal;
		public ExecutionOptions executionOptions = ExecutionOptions.Run | ExecutionOptions.Wait;

		private const string DefaultAutoName = "This task can't be auto-named yet.";

		public virtual void OnValidate()
		{
			#if UNITY_EDITOR
			AutoName = CanAutoName() ?
				              CreateAutoName() :
				              DefaultAutoName;

			if (string.IsNullOrEmpty(Name) || string.Equals(Name, DefaultAutoName)) _Name = AutoName;
			#endif
		}
		
		

		protected abstract bool CanAutoName();

		protected abstract string CreateAutoName();

//
//		public async UniTask<trace> Run(trace trace)
//		{
////			if (trace == null) throw new Exception("Run call with a null trace - how did that happen?");
//
//			if (trace.HasBeenCancelled(this)) return trace;
//
//			trace.EnterMethod($"{Name} [{GetType().Name}]");
//
//			try
//			{
//				switch (ExecutionType)
//				{
//					case TaskExecutionType.RunAndWait:
//
//						LogRunBegin(trace);
//
//						await _Run(trace);
//
//						if (trace.Continue) LogRunComplete(trace);
//
//						break;
//
//					// ToDo - Parallel is bugged but I didn't document why! Woops...
//					// ToDo - Figure out why support for .Parallel was dropped last round
//					case TaskExecutionType.RunAndDontWait:
//						LogRunBegin(trace);
//						_Run(trace).ContinueWith(LogRunComplete).Forget();
//						break;
//					
//					case TaskExecutionType.Cancel:
//						trace.Continue = false;
//						break;
//
//					case TaskExecutionType.Skip:
//						LogRunSkipped(trace);
//						break;
//				}
//			}
//			catch (Exception ex)
//			{
//				LogTaskError(trace);
//
//				Debug.LogException(ex);
//			}
//			finally
//			{
////				if (ExecutionType == TaskExecutionType.Cancel) Debug.LogError("Yep!");
//				
//				trace.ExitMethod();
//			}
//			
//			// Does execution make it this far...?
//			// Yes!
//
//			return trace;
//		}

//
//		public async UniTask<ExecutionTrace> Run(ExecutionTrace trace)
//		{
//			if (trace.Cancelled) return trace;
//			
////			trace.EnterMethod($"{Name} [{GetType().Name}]");
//
//			trace.Enter();
//
//			try
//			{
//				if (executionFlags.HasFlag(TaskExecutionFlags.Run))
//				{
//					LogRunBegin(trace);
//
//					if (executionFlags.HasFlag(TaskExecutionFlags.Wait))
//					{
//						await _Run(trace);
//
////						if (trace.HasBeenCancelled(this)) return trace;
//						if (trace.Cancelled) return trace;
//
//						LogRunComplete(trace);
//					}
//					else
//					{
//						_Run(trace).ContinueWith(LogRunComplete).Forget();
////						_Run(trace).ContinueWith(DelayedCompletion).Forget();
//					}
//				}
//				else
//				{
//					LogRunSkipped(trace);
//				}
//
//				if (executionFlags.HasFlag(TaskExecutionFlags.Continue))
//				{
//					return trace;
//				}
//				else
//				{
//					trace.Continue = false; // Turn off .Continue I guess
//
//					return trace;
//				}
//			}
//			catch (Exception ex)
//			{
//				LogTaskError(trace);
//
//				Debug.LogException(ex);
//			}
//			finally
//			{
//				trace.Exit();
//			}
//
//			return trace;
//		}


		public async UniTask<ExecutionTrace> Run(ExecutionTrace trace)
		{
//			if (executionFlags.HasFlag(TaskExecutionFlags.CancelGlobal))
//			{
//				trace.Continue        = false;
//				trace.source.Continue = false;
//			}
//			else if (executionFlags.HasFlag(TaskExecutionFlags.CancelLocal))
//			{
//				trace.Continue = false;
//			}
			
			if (trace.Cancelled)
			{
				Debug.LogWarning($"[{Name}] - I'm not doing anything because my ExecutionTrace is cancelled");
				
				return trace;
			}
			
			try
			{
				if (executionOptions.HasFlag(ExecutionOptions.Run))
				{
					trace.scope.ActiveTasks++;

					if (executionOptions.HasFlag(ExecutionOptions.Wait))
					{
						await _Run(trace).ContinueWith(FinalizeRun);
					}
					else
					{
						_Run(trace).ContinueWith(FinalizeRun).Forget();
					}
				}
				else
				{
					// "Skipped"
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}

			return trace;
		}

		protected const   int LongestExecutionType = -8;
		protected const int LongestTaskName      = -14;

		protected abstract UniTask<ExecutionTrace> _Run(ExecutionTrace trace);


		private void FinalizeRun(ExecutionTrace trace)
		{
			Debug.LogWarning($"{Name} finalizing");
			
			trace.scope.ActiveTasks--;
//			
//			if (executionOptions.HasFlag(ExecutionOptions.CancelGlobal))
//			{
//				trace.Continue        = false;
//				trace.source.Continue = false;
//			}
//			else if (executionOptions.HasFlag(ExecutionOptions.CancelLocal))
//			{
//				trace.Continue = false;
//			}
		}


//
//		#if debug
//		public void LogRunBegin(ExecutionTrace trace)    => Debug.Log($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Begun        {executionFlags}    {Name}");
//		public void LogRunSkipped(ExecutionTrace trace)  => Debug.LogWarning($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Skipped      {executionFlags.ToString()}    {Name}");
//		public void LogRunComplete(ExecutionTrace trace) => Debug.Log($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Complete     {executionFlags.ToString()}    {Name}");
////		public void LogRunComplete(trace trace)  => Debug.Log($"{Time.frameCount:0000000}    {GetType().Name,LongestTaskName}    Complete     {ExecutionType,LongestExecutionType}    {Name}");
//		public void LogRunCancelled(ExecutionTrace trace) => Debug.LogWarning($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Cancelled    {executionFlags.ToString()}    {Name}");
//		public void LogTaskError(ExecutionTrace trace)    => Debug.LogError($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Error        {executionFlags.ToString()}    {Name}");
//		public void LogTaskIdentity(ExecutionTrace trace) => Debug.LogWarning($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Identity     {executionFlags.ToString()}    {Name}");
//		#else
//		public void LogRunBegin(trace trace)     { }
//		public void LogRunSkipped(trace trace)   { }
//		public void LogRunComplete(trace trace)  { }
//		public void LogRunCancelled(trace trace) { }
//		public void LogTaskError(trace trace)    { }
//		public void LogTaskIdentity(trace trace) { }
//		#endif

	}

}