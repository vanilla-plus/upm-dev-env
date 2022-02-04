using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public abstract class TaskBase : IMetaScriptTask
	{

		public abstract string GetDescription();

		public virtual void OnValidate() { }

//		[SerializeField]
//		internal bool running;
		
//		public bool Running() => running;

		public abstract UniTask Run();

//		[SerializeField]
//		internal bool cancelled;
		
//		public void Cancel() => cancelled = true;

		protected static string DescribeGameObject(GameObject item) => MetaScriptUtility.DescribeGameObject(item);

		protected static string DescribeComponent(Component item) => MetaScriptUtility.DescribeComponent(item);

	}

}