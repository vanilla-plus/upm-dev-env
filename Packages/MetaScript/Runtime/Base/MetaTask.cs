using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public abstract class MetaTask
	{

		[SerializeField]
		private string _Name;
		public string Name => _Name;
		
		[SerializeField]
		public string AutoName;

		[SerializeField]
		public TaskOptions taskOptions = TaskOptions.Run | TaskOptions.Wait;

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

		public async UniTask<Scope> Run(Scope scope)
		{
			if (scope.Cancelled)
			{
				Debug.LogWarning($"[{Name}] - I'm not doing anything because my Scope is cancelled");
				
				return scope;
			}
			
			try
			{
				if (taskOptions.HasFlag(TaskOptions.Run))
				{
					scope.ActiveTasks++;

					if (taskOptions.HasFlag(TaskOptions.Wait))
					{
						await _Run(scope).ContinueWith(FinalizeRun);
					}
					else
					{
						_Run(scope).ContinueWith(FinalizeRun).Forget();
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

			return scope;
		}

		protected abstract UniTask<Scope> _Run(Scope scope);
		
		private void FinalizeRun(Scope scope) => scope.ActiveTasks--;

		
//		protected const   int LongestExecutionType = -8;
//		protected const int LongestTaskName      = -14;

		//
//		#if debug
//		public void LogRunBegin(Scope scope)    => Debug.Log($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Begun        {executionFlags}    {Name}");
//		public void LogRunSkipped(Scope scope)  => Debug.LogWarning($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Skipped      {executionFlags.ToString()}    {Name}");
//		public void LogRunComplete(Scope scope) => Debug.Log($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Complete     {executionFlags.ToString()}    {Name}");
////		public void LogRunComplete(trace trace)  => Debug.Log($"{Time.frameCount:0000000}    {GetType().Name,LongestTaskName}    Complete     {ExecutionType,LongestExecutionType}    {Name}");
//		public void LogRunCancelled(Scope scope) => Debug.LogWarning($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Cancelled    {executionFlags.ToString()}    {Name}");
//		public void LogTaskError(Scope scope)    => Debug.LogError($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Error        {executionFlags.ToString()}    {Name}");
//		public void LogTaskIdentity(Scope scope) => Debug.LogWarning($"{Time.frameCount:0000000}    {trace.source.Activity}    {GetType().Name,LongestTaskName}    Identity     {executionFlags.ToString()}    {Name}");
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