using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public abstract class TaskSet : MonoBehaviour,
	                                IMetaScriptTask
	{

		private const string c_TaskSetParentName = "[ TaskSets ]";

		[SerializeField]
		protected string description;

		public string UpdateDescription() => description;

		public abstract string GetDescription();

		protected virtual void NameSelf() => gameObject.name = description;
		
		[SerializeField]
		public TaskRunner[] taskRunners = new TaskRunner[0];

		public virtual void OnValidate()
		{
			if (!string.Equals(a: transform.root.gameObject.name,
			                   b: c_TaskSetParentName))
			{
				var taskSetParent = gameObject.scene.GetRootGameObjects().FirstOrDefault(g => string.Equals(a: g.name,
				                                                                                            b: c_TaskSetParentName));

				if (taskSetParent == null)
				{
					taskSetParent = new GameObject(name: c_TaskSetParentName);
				}

				transform.SetParent(taskSetParent.transform);
			}

			NameSelf();
			
			foreach (var t in taskRunners)
			{
				t.OnValidate();
			}
		}
		
//		[SerializeField]
//		protected bool running = false;
		
//		public bool Running() => running;

//		public virtual async Task Run()
//		{
//			running = true;

//			cancelled = false;
//		}

		public abstract UniTask Run();

//		[SerializeField]
//		protected bool cancelled = false;
		
//		public virtual void Cancel() => cancelled = true;

		protected static string DescribeGameObject(GameObject item) => MetaScriptUtility.DescribeGameObject(item);

		protected static string DescribeComponent(Component item) => MetaScriptUtility.DescribeComponent(item);

	}

}