using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

using Vanilla.SmartValues;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class MetaTaskInstance : MonoBehaviour
	{


		[SerializeField]
		public SmartBool Running = new SmartBool("MetaTaskInstance Running",
		                                         false);
		
		[SerializeField]
		public bool RunOnStart = false;

		[SerializeField]
		public KeyCode debugCancelKey = KeyCode.None;

		[SerializeField]
		public KeyCode debugRunKey = KeyCode.None;

//		[SerializeReference]
//		[TypeMenu]
//		[Only(typeof(MetaTaskSet))]
//		private MetaTaskSet taskSet;
//		public MetaTaskSet TaskSet => taskSet;

		[NonSerialized]
		internal Tracer _tracer = null;

		[SerializeReference]
		[TypeMenu]
		[Only(typeof(MetaTask))]
		private MetaTask task;
		public MetaTask Task => task;
		
		void OnValidate()
		{
			#if UNITY_EDITOR
			task?.OnValidate();
			#endif
		}


		void Start()
		{
			if (RunOnStart) RunSelf();
		}

		[ContextMenu("Run")]
		public void RunSelf()
		{
			Cancel();

//			_tracer       = new Tracer();

//			Running.Value = true;

			if (_tracer != null)
			{
				
			}

			Detour(_tracer = new Tracer()).ContinueWith(FinalizeTask);

//			task.Run(_tracer).Forget();
		}


		public async UniTask<Tracer> Detour(Tracer tracer)
		{
			Running.Value = true;

			var Tracer = await task.Run(tracer);

			Running.Value = false;

			return Tracer;
		}


		[ContextMenu("Cancel")]
		public void Cancel()
		{
			Debug.Log("Cancel attempt!");

			if (_tracer is
			    {
				    Continue: true
			    })
			{
				Debug.Log("Cancel approved!");

				_tracer.Continue = false;

				Running.Value = false;
			}
		}


		private void FinalizeTask(Tracer tracer)
		{
			if (tracer.Continue)
			{
				Debug.Log("Execution chain finalised - Completed successfully!");
			}
			else
			{
				Debug.LogWarning("Execution chain finalised - Cancelled!");
			}
		}

		void Update()
		{
			if (Input.GetKeyDown(debugCancelKey)) Cancel();
			if (Input.GetKeyDown(debugRunKey)) RunSelf();
		}

	}

}