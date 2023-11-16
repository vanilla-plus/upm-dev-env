using System;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class MetaScriptIgnition : MonoBehaviour
	{

		[SerializeField]
		public KeyCode debugRunKey = KeyCode.Alpha1;
		
		[SerializeField]
		public MetaTaskInstance target;

		void Start() => Fire();


		public void Fire()
		{
			if (target != null) target.StartTask();
		}

		void Update()
		{
			#if UNITY_EDITOR
			if (Input.GetKeyDown(debugRunKey)) Fire();
			#endif
		}
		
	}

}