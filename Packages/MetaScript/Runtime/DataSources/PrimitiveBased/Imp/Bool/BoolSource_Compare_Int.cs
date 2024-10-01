using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class BoolSource_Compare_Int : BoolSource_Compare<int, IGettableSource<int>>
	{

		[SerializeReference]
		[TypeMenu("red")]
		private IGettableSource<int> _A;
		public override IGettableSource<int> A => _A;

		[SerializeReference]
		[TypeMenu("red")]
		private IGettableSource<int> _B;
		public override IGettableSource<int> B => _B;

		public override bool Value => comparisonType switch
		                              {
			                              NumericalComparisonType.EqualTo              => Math.Abs(A.Value - B.Value) < Mathf.Epsilon,
			                              NumericalComparisonType.GreaterThan          => A.Value                     > B.Value,
			                              NumericalComparisonType.LessThan             => A.Value                     < B.Value,
			                              NumericalComparisonType.GreaterThanOrEqualTo => A.Value                     >= B.Value,
			                              NumericalComparisonType.LessThanOrEqualTo    => A.Value                     <= B.Value,
			                              _                                            => throw new ArgumentOutOfRangeException()
		                              };

	}

}