using System;
using System.Threading;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Set_GameObject_Active : TaskBase
	{

		public override string GetDescription() => $"Set [{DescribeGameObject(target)}] to [{(newActiveState ? "Active" : "Inactive")}]";

		[SerializeField] // Convert to GameObject socket
		public GameObject target;

		[SerializeField]
		public bool newActiveState = true; // Convert to bool socket

		public override async UniTask Run() => target.SetActive(newActiveState);

	}

}