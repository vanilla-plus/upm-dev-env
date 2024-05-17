using System;

using UnityEngine;

using Cysharp.Threading.Tasks;

using Object = UnityEngine.Object;

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

//		[HideInInspector]
		[SerializeField]
		protected bool _isValid = false;
		public bool IsValid => _isValid;

		[SerializeField] public TaskOptions taskOptions = TaskOptions.Run | TaskOptions.Wait;
		
//		protected const string DefaultAutoName = "This task can't be auto-named yet.";
		protected const string InvalidAutoName = "Invalid Task";

		public virtual void OnValidate()
		{
			#if UNITY_EDITOR
			_isValid = Validate;

			if (_isValid)
			{
				AutoName = CreateAutoName();
			}
			else
			{
				AutoName = InvalidAutoName;
				
//				Debug.LogError($"Invalid MetaTask -> [origin:{this}] -> [{}]");
				
				return;
			}

			if (string.IsNullOrEmpty(Name) || string.Equals(a: Name, b: InvalidAutoName)) _Name = AutoName;
			#endif
		}
		
		

		protected abstract bool Validate
		{
			get;
		}

		protected abstract string CreateAutoName();

		public async UniTask<Scope> Run(Scope scope)
		{
			// You should just automatically check here instead of every single _Run...
			if (scope.Cancelled || !_isValid) return scope;

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
						_Run(s).Forget();

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