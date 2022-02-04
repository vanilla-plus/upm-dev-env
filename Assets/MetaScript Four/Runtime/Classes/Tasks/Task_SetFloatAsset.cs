using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DataAssets.Three;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Four
{
	[Serializable]
	public struct Task_SetFloatAsset : ITask
	{

		public void Validate() => target?.Validate();

		[SerializeField]
		private bool _async;
		public bool async
		{
			get => _async;
			set => _async = value;
		}

		[SerializeField]
		public FloatAsset asset;
		
		[SerializeReference]
		[TypeMenu]
		public FloatSource target;
        
		public async UniTask Run() => asset.source.Set(newValue: target.Get());

	}
}