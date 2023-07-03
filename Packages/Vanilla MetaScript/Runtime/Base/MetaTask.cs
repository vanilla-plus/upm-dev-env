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
	public abstract class MetaTask
	{

		[SerializeField]
		[HideInInspector]
		private string _Name;
		public string Name => _Name;

//		[TextArea(1,4)]
//		[SerializeField]
//		private string Description;

		[SerializeField]
		private TaskExecutionType _executionType = TaskExecutionType.Await;
		public TaskExecutionType ExecutionType => _executionType;

		private const string c_TaskDefiesDescription = "[Task can't be described]";

		public virtual void OnValidate()
		{
			#if UNITY_EDITOR
			_Name = CanDescribe() ?
				        DescribeTask() :
				        c_TaskDefiesDescription;
			#endif
		}
		
		

		protected abstract bool CanDescribe();

		protected abstract string DescribeTask();


		public async UniTask Run(CancellationTokenSource cancellationTokenSource)
		{
			if (cancellationTokenSource == null) throw new Exception("Run call with a null cancellationTokenSource - How did that happen?");

//			cancellationTokenSource.Token.ThrowIfCancellationRequested();
			
			try
			{
				switch (ExecutionType)
				{
					case TaskExecutionType.Await:
						LogRunBegin();
						
						await _Run(cancellationTokenSource).AttachExternalCancellation(cancellationTokenSource.Token);
						
						if (!cancellationTokenSource.IsCancellationRequested) LogRunComplete();
						break;
					
					case TaskExecutionType.FireAndForget:
						LogRunBegin();

//						_Run(cancellationTokenSource)
//							.ContinueWith(() =>
//							              {
//								              if (!cancellationTokenSource.IsCancellationRequested) LogRunComplete();
//							              });
						
						_Run(cancellationTokenSource).ContinueWith(LogRunComplete).Forget();
						
						break;
					
					case TaskExecutionType.Skip:
						LogRunSkipped();
						return;
				}
			}
			catch (OperationCanceledException)
			{
				LogRunCancelled();

				return; // Stops execution of remaining tasks
			}
			catch (Exception ex)
			{
				LogTaskError();
//				Debug.LogError("Task failed with exception:");

				Debug.LogException(ex);

				return;
			}
		}


		private const int LongestExecutionType = -13;
		private const int LongestTaskName = -36;

		protected abstract UniTask _Run(CancellationTokenSource cancellationTokenSource);

		#if debug
		public void LogRunBegin()     => Debug.Log($"[{Time.frameCount:0000000}] Task Begun     [{ExecutionType,LongestExecutionType}] [{GetType(),LongestTaskName}] [{Name}]");
		public void LogRunSkipped()   => Debug.LogWarning($"[{Time.frameCount:0000000}] Task Skipped   [{ExecutionType,LongestExecutionType}] [{GetType(),LongestTaskName}] [{Name}]");
		public void LogRunComplete()  => Debug.Log($"[{Time.frameCount:0000000}] Task Complete  [{ExecutionType,LongestExecutionType}] [{GetType(),LongestTaskName}] [{Name}]");
		public void LogRunCancelled() => Debug.LogWarning($"[{Time.frameCount:0000000}] Task Cancelled [{ExecutionType,LongestExecutionType}] [{GetType(),LongestTaskName}] [{Name}]");
		public void LogTaskError()    => Debug.LogError($"[{Time.frameCount:0000000}] Task Error     [{ExecutionType,LongestExecutionType}] [{GetType(),LongestTaskName}] [{Name}]");
		public void LogTaskIdentity() => Debug.LogWarning($"[{Time.frameCount:0000000}] Task Identity  [{ExecutionType,LongestExecutionType}] [{GetType(),LongestTaskName}] [{Name}]");
		#else
		public void LogRunBegin()     { }
		public void LogRunSkipped()   { }
		public void LogRunComplete()  { }
		public void LogRunCancelled() { }
		public void LogTaskError()    { }
		public void LogTaskIdentity() { }
		#endif

	}

}