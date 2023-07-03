using System;
using System.Linq;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Object = UnityEngine.Object;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class DestroyGameObjects : MetaTask
	{

		public GameObject[] targets = Array.Empty<GameObject>();


		protected override bool CanDescribe() => targets is
		                                         {
			                                         Length: > 0
		                                         };


		protected override string DescribeTask() => targets.Aggregate("Destroy",
		                                                              (s,
		                                                               target) => s + $" {target.name}");


		protected override UniTask _Run(CancellationTokenSource cancellationTokenSource)
		{
			for (var i = targets.Length - 1;
			     i >= 0;
			     i--)
			{
				Object.Destroy(targets[i]);
			}

			return default;
		}

	}

}