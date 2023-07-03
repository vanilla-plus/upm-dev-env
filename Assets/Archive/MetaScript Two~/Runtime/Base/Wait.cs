using System;

using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.StringFormatting;
using Vanilla.DataAssets;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Wait : TaskBase
	{

		public override string GetDescription() => $"Wait for [{milliseconds.Get().AsTimeFromMilliseconds()}]";

		[SerializeField] // Convert to int socket
		public ValueSocket<int, IntSocket, IntAsset, IntAccessor> milliseconds;

		public override async UniTask Run()
		{
			var w = milliseconds.Get();

			await UniTask.Delay(millisecondsDelay: w);
		}

	}

}