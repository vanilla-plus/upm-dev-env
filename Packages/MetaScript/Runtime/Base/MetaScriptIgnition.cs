using System;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class MetaScriptIgnition : MonoBehaviour
	{

		[SerializeField]
		public KeyCode debugRunKey = KeyCode.Alpha1;
		
		[SerializeField]
		public MetaTaskInstance target;

		[SerializeReference]
		[TypeMenu]
		[Only(typeof(IScopeSource))]
		public IScopeSource scopeSource;


		void OnValidate()
		{
			#if UNITY_EDITOR
			scopeSource ??= new Named_Scope_Source
			                {
				                name = "root"
			                };
			#endif
		}
		
		void Start() => Fire();


		public void Fire()
		{
			if (target != null) target.StartTask(scopeSource.CreateScope(null));
		}

		void Update()
		{
			#if UNITY_EDITOR
			if (Input.GetKeyDown(debugRunKey)) Fire();
			#endif
		}
		
	}

}