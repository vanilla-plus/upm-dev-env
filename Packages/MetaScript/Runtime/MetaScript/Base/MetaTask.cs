using System;
using System.Threading.Tasks;

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

//		[SerializeReference]
//		[TypeMenu("green")]
////		[Only(typeof(IScopeSource))]
//		public IScopeSource scopeSource;
		
		public virtual void OnValidate()
		{
			#if UNITY_EDITOR
			AutoName = CanAutoName() ?
				              CreateAutoName() :
				              DefaultAutoName;

			if (string.IsNullOrEmpty(Name) || string.Equals(a: Name, b: DefaultAutoName)) _Name = AutoName;
			#endif
		}
		
		

		protected abstract bool CanAutoName();

		protected abstract string CreateAutoName();

		public async UniTask<Scope> Run(Scope scope)
		{
//			var s = scopeSource != null ?
//				        scopeSource.CreateScope(scope) :
//				        scope;
			
//			Debug.LogWarning(Name);
//			Debug.Log(scope.Name);

			var s = scope;

			try
			{
				if (taskOptions.HasFlag(TaskOptions.Run))
				{
//					scope.ActiveTasks++;

					if (taskOptions.HasFlag(TaskOptions.Wait))
					{
//						await _Run(scope).ContinueWith(FinalizeRun);

						s = await _Run(s);

//						s.ActiveTasks--;
					}
					else
					{
//						_Run(scope).ContinueWith(FinalizeRun).Forget();

						// Weird quirk time - we can't have our cake and eat it too when it comes to scopes.
						// At this stage, we're telling a task to run but not to wait for it.
						// Getting a returned scope from the task is only possible if we wait,
						// since we don't know what sort of task it is and it may be asynchronous.
	
						// So I think the best we can hope for is to pass the current scope in, wish it well and move on.
						// I can't wait for a designer to find this and trip over it
						// and for me to then struggle to remember and explain the issue.

						_Run(s);

						// This works as long as the task returns the scope with UniTask.FromResult(scope)
						// But if its async and simply returns scope...
						
//						s = _Run(scope).GetAwaiter().GetResult(); // Errors
//						s = _Run(scope).AsTask().Result; // Hard crash
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
			
//			Debug.Log(s.Name);

//
//			if (scopeSource != null)
//			{
//				s.Cancel();
//
//				s.Dispose();
//			}
			
			return s;
		}

		protected abstract UniTask<Scope> _Run(Scope scope);


//		private void FinalizeRun(Scope scope)
//		{
//			if (scope != null) scope.ActiveTasks--;
//		}

	}

}