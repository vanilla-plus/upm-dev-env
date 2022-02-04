using System;
using System.Threading;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Set_Behaviour_Enabled : TaskBase
	{

		public override string GetDescription() => $"Set [{DescribeComponent(target)}] [{(newEnabledState ? "Enabled" : "Disabled")}]";

		[SerializeField] // Convert to Behaviour socket
		public Behaviour target;

		[SerializeField] // Convert to bool socket
		public bool newEnabledState = true;

		public override async UniTask Run() => target.enabled = newEnabledState;

	}

}