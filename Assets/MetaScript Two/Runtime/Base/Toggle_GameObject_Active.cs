using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Toggle_GameObject_Active : TaskBase
	{

		public override string GetDescription() => $"Toggle the active state of [{DescribeGameObject(target)}]";

		[SerializeField] //  Convert to GameObject socket
		public GameObject target;

		public override async UniTask Run() => target.SetActive(!target.activeSelf);

	}

}