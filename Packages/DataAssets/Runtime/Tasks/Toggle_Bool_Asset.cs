using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;

namespace Vanilla.DataAssets.Tasks
{

	[Serializable]
	public class Toggle_Bool_Asset : MetaTask
	{

		[SerializeField]
		public BoolAsset asset;

		protected override bool CanAutoName() => asset != null;


		protected override string CreateAutoName() => $"Toggle [{asset}]";


		protected override UniTask<Scope> _Run(Scope scope)
		{
			if (asset == null)
			{
				Debug.LogError($"[{Name}] - null asset!");

				return UniTask.FromResult(scope);
			}

			asset.Source.Value = !asset.Source.Value;

			return UniTask.FromResult(scope);
		}

	}

}