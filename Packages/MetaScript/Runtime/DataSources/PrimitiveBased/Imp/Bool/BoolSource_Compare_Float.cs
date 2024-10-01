using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class BoolSource_Compare_Float : BoolSource_Compare<float,IGettableSource<float>>
    {

        [SerializeReference]
        [TypeMenu("red")]
        private IGettableSource<float> _A;
        public override IGettableSource<float> A => _A;

        [SerializeReference]
        [TypeMenu("red")]
        private IGettableSource<float> _B;
        public override IGettableSource<float> B => _B;

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
