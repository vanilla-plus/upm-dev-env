using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class TaskSet_Sequence : TaskSet
	{

		public override    string GetDescription()   => "Run each of these tasks in order";

		[ContextMenu(itemName: "Run")]
		public override async UniTask Run()
		{
			foreach (var t in taskRunners)
			{
				await t.Run();
			}
		}

	}

}