using System;
using System.Threading;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DataAssets;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class Await_Key_Press : TaskBase
	{

//		[SerializeField] // Convert to... KeyCode socket..? Couldn't that just secretly be an int socket/override from int socket?
//		public KeyCode key = KeyCode.Space;

//		[SerializeReference]
		[SerializeField]
		public ValueSocket<KeyCode, KeyCodeSocket, KeyCodeAsset, KeyCodeAccessor> keySocket;
		
		public override string GetDescription() => $"Wait until [{keySocket.Get().ToString()}] is pressed";

		public override async UniTask Run()
		{
			var k = keySocket.Get();
			
			while (!Input.GetKey(key: k)) await UniTask.Yield();
		}

	}

}