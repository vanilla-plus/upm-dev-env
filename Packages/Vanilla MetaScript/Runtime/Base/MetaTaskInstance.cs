using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

using Vanilla.MetaScript.TaskSets;
using Vanilla.SmartValues;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class MetaTaskInstance : MonoBehaviour
	{

		[NonSerialized] private CancellationTokenSource _cancellationTokenSource;

		[SerializeField]
		public bool RunOnStart = true;

		[SerializeField]
		public KeyCode debugKey = KeyCode.Space;

		[SerializeReference]
		[TypeMenu]
		[Only(typeof(MetaTaskSet))]
		private MetaTaskSet taskSet;
		public MetaTaskSet TaskSet => taskSet;

		[SerializeField]
		public SmartBool Running = new SmartBool("MetaTaskInstance Running", false);
		
		void OnValidate()
		{
			#if UNITY_EDITOR
			taskSet?.OnValidate();
			#endif
		}


		void Start()
		{
			if (RunOnStart) Run();
		}


		[ContextMenu("Run")]
		public void Run()
		{
			Cancel();

			_cancellationTokenSource = new CancellationTokenSource();

//			taskSet.Run(_cancellationTokenSource).ContinueWith(DisposeCancellationTokenSource);
//			taskSet.Run(_cancellationTokenSource).Forget();

//			taskSet.Run(_cancellationTokenSource).ContinueWith(FinalizeTask).Forget();

			Running.Value = true;

			taskSet.Run(_cancellationTokenSource).ContinueWith(FinalizeTask);
		}
		
//		private void FinalizeTask()
//		{
//			if (_cancellationTokenSource.IsCancellationRequested)
//			{
////				taskSet.LogRunCancelled();
////				Debug.LogWarning("Task was cancelled!");
//			}
//			
//			DisposeCancellationTokenSource();
//		}


//		[ContextMenu("Cancel")]
//		public void Cancel()
//		{
//			Debug.Log("Cancel!");
//			
//			_cancellationTokenSource?.Cancel();
//
//			DisposeCancellationTokenSource();
//		}

		[ContextMenu("Cancel")]
		public void Cancel()
		{
			Debug.Log("Cancel attempt!");

//			_cancellationTokenSource?.Cancel();

			if (_cancellationTokenSource != null)
			{
				Debug.Log("Cancel approved!");

				if (!_cancellationTokenSource.IsCancellationRequested)
				{
					_cancellationTokenSource.Cancel();
				}

				_cancellationTokenSource.Dispose();
				_cancellationTokenSource = null;

				Running.Value = false;
			}
		}

		private void FinalizeTask()
		{
//			Debug.Log("Finalize!");

//			if (_cancellationTokenSource == null) return; // Check if it's already disposed.

			Running.Value = false;

			if (_cancellationTokenSource.IsCancellationRequested)
			{
				Debug.LogWarning("Task was cancelled!");
			}
			else
			{
				Debug.Log("Task execution chain completed successfully!");
			}
			
//			Debug.LogWarning("Dispose..?");

//			DisposeCancellationTokenSource();
		}

		private void DisposeCancellationTokenSource()
		{
			if (_cancellationTokenSource == null) return; // Check if it's already disposed.

			Debug.LogWarning("Dispose..?");
    
//			_cancellationTokenSource.Dispose();
//			_cancellationTokenSource = null;
		}
		
//		private void DisposeCancellationTokenSource()
//		{
//			Debug.LogWarning("Disposing!");
//			
//			_cancellationTokenSource?.Dispose();
//
//			_cancellationTokenSource = null;
//		}

		void Update()
		{
			if (!Input.GetKeyDown(debugKey)) return;

			if (_cancellationTokenSource == null)
			{
				Run();
			}
			else
			{
				Cancel();
			}
//
//		Cancel();
//
//		Run();

		}

	}

}