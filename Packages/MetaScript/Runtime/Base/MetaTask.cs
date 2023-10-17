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

		[SerializeField]
		private TaskExecutionType _executionType = TaskExecutionType.RunAndWait;
		public TaskExecutionType ExecutionType => _executionType;

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


		public async UniTask<Tracer> Run(Tracer tracer)
		{
//			if (tracer == null) throw new Exception("Run call with a null tracer - how did that happen?");

			if (tracer.HasBeenCancelled(this)) return tracer;

			tracer.EnterMethod($"{Name} [{GetType().Name}]");

			try
			{
				switch (ExecutionType)
				{
					case TaskExecutionType.RunAndWait:

						LogRunBegin(tracer);

						await _Run(tracer);

						if (tracer.Continue) LogRunComplete(tracer);

						break;

					// ToDo - Parallel is bugged but I didn't document why! Woops...
					// ToDo - Figure out why support for .Parallel was dropped last round
					case TaskExecutionType.RunAndDontWait:
						LogRunBegin(tracer);
						_Run(tracer).ContinueWith(LogRunComplete).Forget();
						break;
					
					case TaskExecutionType.Cancel:
						tracer.Continue = false;
						break;

					case TaskExecutionType.Skip:
						LogRunSkipped(tracer);
						break;
				}
			}
			catch (Exception ex)
			{
				LogTaskError(tracer);

				Debug.LogException(ex);
			}
			finally
			{
//				if (ExecutionType == TaskExecutionType.Cancel) Debug.LogError("Yep!");
				
				tracer.ExitMethod();
			}
			
			// Does execution make it this far...?
			// Yes!

			return tracer;
		}


		protected const   int LongestExecutionType = -8;
		protected const int LongestTaskName      = -14;

		protected abstract UniTask<Tracer> _Run(Tracer tracer);

		#if debug
		public void LogRunBegin(Tracer tracer)     => Debug.Log($"{Time.frameCount:0000000}    {tracer.Depth}    {GetType().Name,LongestTaskName}    Begun        {ExecutionType,LongestExecutionType}    {Name}");
		public void LogRunSkipped(Tracer tracer)   => Debug.LogWarning($"{Time.frameCount:0000000}    {tracer.Depth}    {GetType().Name,LongestTaskName}    Skipped      {ExecutionType,LongestExecutionType}    {Name}");
		public void LogRunComplete(Tracer tracer)  => Debug.Log($"{Time.frameCount:0000000}    {tracer.Depth}    {GetType().Name,LongestTaskName}    Complete     {ExecutionType,LongestExecutionType}    {Name}");
//		public void LogRunComplete(Tracer tracer)  => Debug.Log($"{Time.frameCount:0000000}    {GetType().Name,LongestTaskName}    Complete     {ExecutionType,LongestExecutionType}    {Name}");
		public void LogRunCancelled(Tracer tracer) => Debug.LogWarning($"{Time.frameCount:0000000}    {tracer.Depth}    {GetType().Name,LongestTaskName}    Cancelled    {ExecutionType,LongestExecutionType}    {Name}");
		public void LogTaskError(Tracer tracer)    => Debug.LogError($"{Time.frameCount:0000000}    {tracer.Depth}    {GetType().Name,LongestTaskName}    Error        {ExecutionType,LongestExecutionType}    {Name}");
		public void LogTaskIdentity(Tracer tracer) => Debug.LogWarning($"{Time.frameCount:0000000}    {tracer.Depth}    {GetType().Name,LongestTaskName}    Identity     {ExecutionType,LongestExecutionType}    {Name}");
		#else
		public void LogRunBegin(Tracer tracer)     { }
		public void LogRunSkipped(Tracer tracer)   { }
		public void LogRunComplete(Tracer tracer)  { }
		public void LogRunCancelled(Tracer tracer) { }
		public void LogTaskError(Tracer tracer)    { }
		public void LogTaskIdentity(Tracer tracer) { }
		#endif

	}

}