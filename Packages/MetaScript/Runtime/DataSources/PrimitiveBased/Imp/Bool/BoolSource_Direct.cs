using System;

using UnityEngine;

namespace Vanilla.MetaScript.DataSources
{
    
    // If TypeMenu falls over because it can't swallow generic types like below,
    // try swapping these interfaces over to IGettableBool and ISettableBool instead
    [Serializable]
    public class BoolSource_Direct : IGettableSource<bool>,
                                     ISettableSource<bool>
    {

        [SerializeField]
        private bool _value = false;
        public bool Value
        {
            get => _value;
            set => _value = value;
        }

    }

}
