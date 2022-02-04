using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Toggle_Behaviour_Enabled : TaskBase
	{

		public override string GetDescription() => $"Toggle [{DescribeComponent(target)}] enabled";

		[SerializeField]
		public Behaviour target; // Convert to Behaviour socket

		public override async UniTask Run() => target.enabled = !target.enabled;

	}

}