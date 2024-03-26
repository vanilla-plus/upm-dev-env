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
		[TypeMenu("green")]
		public IScopeSource scopeSource;


		void OnValidate()
		{
			#if UNITY_EDITOR
			scopeSource ??= new Named_Scope_Source
			                {
				                name = "root"
			                };

			if (target == null) target = GetComponent<MetaTaskInstance>();
			#endif
		}
		
		void Start() => Fire();


		public void Fire()
		{
			if (target != null) target.Task?.Run(scopeSource.CreateScope(null));
		}

		void Update()
		{
			#if UNITY_EDITOR
			if (Input.GetKeyDown(debugRunKey)) Fire();
			#endif
		}
		
	}

}