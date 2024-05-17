using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class BoolSource_Compare_Float : BoolSource_Compare<float,FloatSource>
    {

        [SerializeReference]
        [TypeMenu("red")]
        private FloatSource _A;
        public override FloatSource A => _A;

        [SerializeReference]
        [TypeMenu("red")]
        private FloatSource _B;
        public override FloatSource B => _B;

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
