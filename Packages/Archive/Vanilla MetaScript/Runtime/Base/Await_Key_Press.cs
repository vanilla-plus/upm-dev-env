using System;
using System.Threading;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Await_Key_Press : TaskBase
	{

//		[SerializeField] // Convert to... KeyCode socket..? Couldn't that just secretly be an int socket/override from int socket?
//		public KeyCode key = KeyCode.Space;

//		[SerializeReference]
		[SerializeField]
		public ValueSocket<KeyCode, KeyCodeAsset> keySocket = new ValueSocket<KeyCode, KeyCodeAsset>();
		
		public override string GetDescription() => $"Wait until [{keySocket.GetImmediate().ToString()}] is pressed";

		public override async UniTask Run()
		{
			var k = await keySocket.Get();
			
			while (!Input.GetKey(key: k)) await UniTask.Yield();
		}

	}

}