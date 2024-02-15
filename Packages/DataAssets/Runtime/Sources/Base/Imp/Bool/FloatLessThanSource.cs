using System;

using UnityEngine;

using Vanilla.DataSources;
using Vanilla.TypeMenu;

namespace Vanilla.DataAssets
{
    
    [Serializable]
    public class FloatComparisonSource : BoolSource
    {

        public enum FloatComparisonType
        {
            LessThan,
            GreaterThan,
            Equals,
            LessThanOrEquals,
            GreaterThanOrEquals
        }
        
        [SerializeReference]
        [TypeMenu("red")]
        public FloatSource subject;
        
        [SerializeReference]
        [TypeMenu("red")]
        public FloatSource target;

        [SerializeField]
        public FloatComparisonType comparisonType = FloatComparisonType.Equals;
        
        public override bool Value
        {
            get
            {
                var s = subject.Value;
                var t = target.Value;

                return comparisonType switch
                       {
                           FloatComparisonType.LessThan            => s                < t,
                           FloatComparisonType.GreaterThan         => s                > t,
                           FloatComparisonType.Equals              => Mathf.Abs(s - t) < Mathf.Epsilon,
                           FloatComparisonType.LessThanOrEquals    => s < t || Mathf.Abs(s - t) < Mathf.Epsilon,
                           FloatComparisonType.GreaterThanOrEquals => s > t || Mathf.Abs(s - t) < Mathf.Epsilon,
                           _                                       => throw new ArgumentOutOfRangeException()
                       };
            }
            set { }
        }

        public override void OnBeforeSerialize() { }

        public override void OnAfterDeserialize() { }

    }
}
