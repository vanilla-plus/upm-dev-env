using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.Collection
{

	[Serializable]
	public class CollectionItem
	{

		public string Name;


		internal async UniTask Removed() => Debug.Log($"[{Name}] My time has come...");

		internal async UniTask Added() => Debug.Log($"[{Name}] The future is now, old man!");

	}
	

}