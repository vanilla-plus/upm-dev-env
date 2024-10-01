using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
	
	[Serializable]
	public class Wait_Until_Float_Equals : MetaTask
	{

		[TypeMenu("red")]
		[SerializeReference]
		public FloatSource A = new FloatSource_Asset
		                       {
			                       _asset = null
		                       };

		[TypeMenu("red")]
		[SerializeReference]
		public FloatSource B = new FloatSource_Direct
		                       {
			                       _value = 1.0f
		                       };

		[TypeMenu("red")]
		[SerializeReference]
		public FloatSource Epsilon = new FloatSource_Direct
		                             {
			                             _value = float.Epsilon
		                             };
        
		protected override bool Validate => A != null && B != null && Epsilon != null;


		protected override string CreateAutoName() => $"Wait until [{(A == null ? "null" : A)}] equals [{(B == null ? "null" : B)}]";


		protected override async UniTask<Scope> _Run(Scope scope)
		{
			if (A == null)
			{
				Debug.LogError("Wait_Until_Float_Equals task has a null source [A]");
				
				return scope;
			}
            
			if (B == null)
			{
				Debug.LogError("Wait_Until_Float_Equals task has a null source [B]");
				
				return scope;
			}
			
			if (Epsilon == null)
			{
				Debug.LogError("Wait_Until_Float_Equals task has a null source [Epsilon]");
				
				return scope;
			}

			while (Mathf.Abs(A.Value - B.Value) > Epsilon.Value)
			{
				if (scope.Cancelled) return scope;

				await UniTask.Yield();
			}

			return scope;
		}

	}
}