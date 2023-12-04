using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;

namespace Vanilla.DataAssets
{
	[Serializable]
	public class Set_Vec1 : MetaTask
	{
		public enum Vec1Action
		{
			Set,
			Add,
			Subtract,
			Multiply
		}

		[SerializeField]
		private Vec1Asset asset;
		
		[SerializeField]
		private Vec1Action action = Vec1Action.Set;

		[SerializeField]
		public float value = 1.0f;
		
		protected override bool CanAutoName() => asset != null;


		protected override string CreateAutoName() => action switch
		                                              {
			                                              Vec1Action.Set      => $"Set [{asset.name}] to [{value}]",
			                                              Vec1Action.Add      => $"Add [{value}] to [{asset.name}]",
			                                              Vec1Action.Subtract => $"Subtract [{value}] from [{asset.name}]",
			                                              Vec1Action.Multiply => $"Multiply [{asset.name}] by [{value}]",
			                                              _                    => DefaultAutoName
		                                              };


		protected override UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return UniTask.FromResult(scope);

			asset.DeltaValue.Value = action switch
			                         {
				                         Vec1Action.Set      => value,
				                         Vec1Action.Add      => asset.DeltaValue.Value + value,
				                         Vec1Action.Subtract => asset.DeltaValue.Value - value,
				                         Vec1Action.Multiply => asset.DeltaValue.Value * value,
				                         _                    => throw new ArgumentOutOfRangeException()
			                         };
			
			return UniTask.FromResult(scope);
		}
	}
}