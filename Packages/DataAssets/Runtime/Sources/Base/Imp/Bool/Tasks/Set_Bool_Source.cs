using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.DataSources;
using Vanilla.MetaScript;
using Vanilla.TypeMenu;

namespace Vanilla.DataAssets
{
    
	[Serializable]
	public class Set_Bool_Source : MetaTask
	{

		[SerializeReference]
		[TypeMenu("red")]
		public BoolSource source = new AssetBoolSource();

		[SerializeReference]
		[TypeMenu("red")]
		public BoolSource target = new AssetBoolSource();
		
		protected override bool CanAutoName() => source != null && target != null;


		protected override string CreateAutoName() => $"Set [{(source is AssetBoolSource boolSource && boolSource.Asset != null ? boolSource.Asset.name : source.GetType().Name)}] to [{(target is AssetBoolSource targetSource && targetSource.Asset != null ? targetSource.Asset.name : target.GetType().Name)}]";


		protected override UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return UniTask.FromResult(scope);

			if (source != null && target != null)
			{
				source.Value = target.Value;
			}

			return UniTask.FromResult(scope);
		}

	}
}