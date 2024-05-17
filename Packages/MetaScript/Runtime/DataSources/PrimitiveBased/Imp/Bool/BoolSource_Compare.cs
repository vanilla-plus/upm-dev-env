using System;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public abstract class BoolSource_Compare<T,S> : BoolSource
        where T : struct, IComparable<T>, IEquatable<T>
        where S : IDataSource<T>
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

    }
}
