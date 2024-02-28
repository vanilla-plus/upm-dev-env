using System;

using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.StringFormatting;
using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Wait : TaskBase
	{

		public override string GetDescription() => $"Wait for [{milliseconds.GetImmediate().AsTimeFromMilliseconds()}]";

		[SerializeField] // Convert to int socket
		public ValueSocket<int, IntAsset> milliseconds = new ValueSocket<int, IntAsset>();

		public override async UniTask Run()
		{
			var w = await milliseconds.Get();

			await UniTask.Delay(millisecondsDelay: w);
		}

	}

}