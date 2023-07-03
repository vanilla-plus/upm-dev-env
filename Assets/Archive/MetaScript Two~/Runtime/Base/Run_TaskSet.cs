using System;
using System.Threading;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Run_TaskSet : TaskBase
	{

		public override string GetDescription() => $"Run [{DescribeComponent(item: taskSet)}]";

		[SerializeField]
		public TaskSet taskSet;

		public override async UniTask Run() => await taskSet.Run();

	}

}