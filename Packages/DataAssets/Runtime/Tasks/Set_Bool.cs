using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript;

namespace Vanilla.DataAssets
{
	[Serializable]
	public class Set_Bool : MetaTask
	{
		public enum BoolAction
		{
			SetTrue,
			SetFalse,
			Toggle
		}

		[SerializeField]
		private BoolAsset targetBoolAsset;

		[SerializeField]
		private BoolAction action = BoolAction.SetTrue;

		protected override bool CanAutoName() => targetBoolAsset != null;

		protected override string CreateAutoName() => action switch
		                                              {
			                                              BoolAction.SetTrue  => $"Set [{targetBoolAsset.name}] to true",
			                                              BoolAction.SetFalse => $"Set [{targetBoolAsset.name}] to false",
			                                              BoolAction.Toggle   => $"Toggle [{targetBoolAsset.name}]",
			                                              _                   => DefaultAutoName
		                                              };


		protected override UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return UniTask.FromResult(scope);

			targetBoolAsset.DeltaValue.Value = action switch
			                                   {
				                                   BoolAction.SetTrue  => true,
				                                   BoolAction.SetFalse => false,
				                                   BoolAction.Toggle   => !targetBoolAsset.DeltaValue.Value,
				                                   _                   => targetBoolAsset.DeltaValue.Value
			                                   };
			
			return UniTask.FromResult(scope);
		}
	}
}