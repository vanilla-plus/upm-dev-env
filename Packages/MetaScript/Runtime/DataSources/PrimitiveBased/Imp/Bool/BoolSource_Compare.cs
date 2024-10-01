using System;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public abstract class BoolSource_Compare<T,S> : IGettableSource<bool>
        where T : struct, IComparable<T>, IEquatable<T>
        where S : IGettableSource<T>
    {

        public enum NumericalComparisonType
        {

            EqualTo,
            GreaterThan,
            LessThan,
            GreaterThanOrEqualTo,
            LessThanOrEqualTo

        }

        public NumericalComparisonType comparisonType = NumericalComparisonType.EqualTo;
        
        public abstract S A
        {
            get;
        }

        public abstract S B
        {
            get;
        }

        public override string ToString() => comparisonType switch
                                             {
                                                 NumericalComparisonType.EqualTo              => $"[{A} == {B}]",
                                                 NumericalComparisonType.GreaterThan          => $"[{A} > {B}]",
                                                 NumericalComparisonType.LessThan             => $"[{A} < {B}]",
                                                 NumericalComparisonType.GreaterThanOrEqualTo => $"[{A} >= {B}]",
                                                 NumericalComparisonType.LessThanOrEqualTo    => $"[{A} <= {B}]",
                                                 _                                            => $"[{A} ? {B}]"
                                             };

        public abstract bool Value
        {
            get;
        }

    }
}
