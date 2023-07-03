using UnityEngine;

namespace Vanilla.MetaScript
{

	public static class MetaScriptUtility
	{

		public static string DescribeGameObject(GameObject item) =>
			item != null ?
				item.name :
				MetaScript.Unknown;


		public static string DescribeComponent(Component item) =>
			item != null ?
				$"{item.gameObject.name}.{item.GetType().Name}" :
				MetaScript.Unknown;


		public static string DescribeTaskRunner(TaskRunner item) =>
			item != null ?
				item.task.GetDescription() :
				MetaScript.Unknown;


		public static string DescribeTask(IMetaScriptTask item) =>
			item != null ?
				item.GetDescription() :
				MetaScript.Unknown;

	}

}