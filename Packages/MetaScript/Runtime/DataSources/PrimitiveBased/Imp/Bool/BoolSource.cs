using System;

namespace Vanilla.MetaScript.DataSources
{
    
    [Serializable]
    public abstract class BoolSource : IGettableSource<bool>,
                                       ISettableSource<bool>
    {

        public abstract bool Value
        {
            get;
            set;
        }

        // This assumes that the source is settable :\
//        public void Flip() => Value = !Value;

        public override string ToString() => Value.ToString();

        public static implicit operator bool(BoolSource input) => input is
                                                                  {
                                                                      Value: true
                                                                  };

    }
}
