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
		public KeyCode debugRunKey = KeyCode.Alpha1;
//
//		[SerializeField]
//		public KeyCode debugCancelByIndex = KeyCode.Alpha2;
//		
//		[SerializeField]
//		public KeyCode debugCancelAllKey = KeyCode.Alpha3;
//
//		public int debugCancelIndex = 0;
//		
		[SerializeField]
		public List<MetaTaskInstance> targets = new List<MetaTaskInstance>();
		
//		[SerializeReference]
//		public List<Scope> scopes = new List<Scope>();

		void Start()
		{
			if (RunOnStart) StartChainOfExecution();
		}
		
		
		[ContextMenu("StartChainOfExecution")]
		public void StartChainOfExecution()
		{
			foreach (var t in targets)
			{
				var scope = new Scope(null, "I G N I T I O N", "MetaScriptIgnition");

//				scopes.Add(scope);

//				MonitorExecution(scope);

				t.Run(scope).ContinueWith(s => s.Dispose());
			}
		}

//
//		private async UniTask MonitorExecution(Scope scope)
//		{
//			await UniTask.WaitUntil(() => scope.ActiveTasks == 0);
//
//			scopes.Remove(scope);
//		}

		void Update()
		{
			if (Input.GetKeyDown(debugRunKey)) StartChainOfExecution();
//			if (Input.GetKeyDown(debugCancelByIndex)) CancelTargetChainOfExecution(debugCancelIndex);
//			if (Input.GetKeyDown(debugCancelAllKey)) CancelAllChainsOfExecution();
		}

//
//		public void CancelTargetChainOfExecution(int index)
//		{
//			if (index < 0 ||
//			    index > scopes.Count - 1)
//			{
//				Debug.LogError($"Scope array index out-of-range [{index}]");
//				return;
//			}
//			
//			var scope = scopes[index];
//			
//			scopes.RemoveAt(index);
//			
//			scope.Cancel();
//		}
//
//
//		public void CancelAllChainsOfExecution()
//		{
//			foreach (var scope in scopes) scope.Cancel();
//			
//			scopes.Clear();
//		}
//		
	}

}