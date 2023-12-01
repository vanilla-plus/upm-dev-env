using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{

	[Serializable]
	public abstract class MetaTask
	{

		[SerializeField]
		private string _Name;
		public string Name => _Name;
		
		[HideInInspector]
		[SerializeField]
		public string AutoName;

		[SerializeField]
		public TaskOptions taskOptions = TaskOptions.Run | TaskOptions.Wait;
		
		protected const string DefaultAutoName = "This task can't be auto-named yet.";

		[SerializeReference]
		[TypeMenu]
		[Only(typeof(IScopeSource))]
		public IScopeSource scopeSource;
		
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
			var s = scopeSource != null ?
				        scopeSource.CreateScope(scope) :
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

			if (scopeSource != null)
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