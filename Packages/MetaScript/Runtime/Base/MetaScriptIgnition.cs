using System;
using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class MetaScriptIgnition : MonoBehaviour
	{

		[SerializeField]
		public bool RunOnStart = false;

		[SerializeField]
		public bool CancelSupport = true;
		
		[SerializeField]
		public KeyCode debugRunKey = KeyCode.Alpha1;
		
		[SerializeField]
		public List<MetaTaskInstance> targets = new List<MetaTaskInstance>();

		void Start()
		{
			if (RunOnStart) StartChainOfExecution();
		}
		
		
		[ContextMenu("StartChainOfExecution")]
		public void StartChainOfExecution()
		{
			foreach (var t in targets)
			{
				if (CancelSupport)
				{
					t.StartTaskAndCacheScope();
				}
				else
				{
					t.StartTask();
				}
			}
		}

		void Update()
		{
			#if UNITY_EDITOR
			if (Input.GetKeyDown(debugRunKey)) StartChainOfExecution();
			#endif
		}
		
	}

}