using System;

using UnityEngine;

using Cysharp.Threading.Tasks;

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

		[SerializeField] public TaskOptions taskOptions = TaskOptions.Run | TaskOptions.Wait;
		
		protected const string DefaultAutoName = "This task can't be auto-named yet.";

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
			var s = scope;

			try
			{
				if ((taskOptions & TaskOptions.Run) != 0)
				{
					if ((taskOptions & TaskOptions.Wait) != 0)
					{
						s = await _Run(s);
					}
					else
					{
						_Run(s);

						// Just a heads up - it isn't possible to return a scope from unawaited tasks.
						// Makes sense - the return type is the UniTask<Scope>, not the scope payload.
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(exception: ex);
			}

//			if (s.Cancelled)
//			{
//				s = s.parent;
//			}
			
			return s;
//			return s.GetLastActiveScope();
		}

		protected abstract UniTask<Scope> _Run(Scope scope);

	}

}