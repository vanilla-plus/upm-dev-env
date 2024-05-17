using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
	[Serializable]
	public class BoolSource_Compare_Int : BoolSource_Compare<int,IntSource>
	{

		[SerializeReference]
		[TypeMenu("red")]
		private IntSource _A;
		public override IntSource A => _A;

		[SerializeReference]
		[TypeMenu("red")]
		private IntSource _B;
		public override IntSource B => _B;

		public override bool Value
		{
			get => comparisonType switch
			       {
				       NumericalComparisonType.EqualTo              => Math.Abs(A.Value - B.Value) < Mathf.Epsilon,
				       NumericalComparisonType.GreaterThan          => A.Value                     > B.Value,
				       NumericalComparisonType.LessThan             => A.Value                     < B.Value,
				       NumericalComparisonType.GreaterThanOrEqualTo => A.Value                     >= B.Value,
				       NumericalComparisonType.LessThanOrEqualTo    => A.Value                     <= B.Value,
				       _                                            => throw new ArgumentOutOfRangeException()
			       };
			set { }
		}

		public override void OnBeforeSerialize() { }

		public override void OnAfterDeserialize() { }



	}
}