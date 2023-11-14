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
		
//		[SerializeField]
//		public virtual TaskOptions taskOptions { get; } = TaskOptions.Run | TaskOptions.Wait;

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

			var s = taskOptions.HasFlag(flag: TaskOptions.NewScope) ?
				        new Scope(parent: scope,
				                  taskName: Name,
				                  taskType: GetType().Name) :
				        scope;

			try
			{
				if (taskOptions.HasFlag(TaskOptions.Run))
				{
					s.ActiveTasks++;

					if (taskOptions.HasFlag(TaskOptions.Wait))
					{
						await _Run(s).ContinueWith(FinalizeRun);
					}
					else
					{
						_Run(s).ContinueWith(FinalizeRun).Forget();
					}
				}
				else
				{
					// "Skipped"
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(exception: ex);
			}

			if (taskOptions.HasFlag(flag: TaskOptions.NewScope))
			{
				s.Cancel();

				s.Dispose();
			}

			return scope;
		}

		protected abstract UniTask<Scope> _Run(Scope scope);


		private void FinalizeRun(Scope scope)
		{
			if (scope != null) scope.ActiveTasks--;
		}

	}

}